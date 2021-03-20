using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBugTracker.Data.FileManager;
using TaskBugTracker.Data.Repository;
using TaskBugTracker.Models;
using TaskBugTracker.ViewModels;

namespace TaskBugTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public PanelController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "Status_desc" : "Status";

            var projects = from s in _repo.GetAllProjects()
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Title_desc":
                    projects = projects.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    projects = projects.OrderByDescending(s => s.LastModified);
                    break;
                case "Date_desc":
                    projects = projects.OrderBy(s => s.LastModified);
                    break;
                case "Status":
                    projects = projects.OrderBy(s => s.Status);
                    break;
                case "Status_desc":
                    projects = projects.OrderByDescending(s => s.Status);
                    break;
                default:
                    projects = projects.OrderBy(s => s.Title);
                    break;
            }
            return View(projects.ToList());
        }

        [HttpGet]
        public IActionResult EditProject(int? id)
        {
            if (id == null)
            {
                return View(new ProjectViewModel());
            }
            else
            {
                var project = _repo.GetProject((int)id);
                return View(new ProjectViewModel
                    {
                        ProjectId = project.ProjectId,
                        Title = project.Title,
                        Description = project.Description,
                        CurrentImage = project.Image,
                        Status = project.Status,
                        Tasks = project.Tasks,
                        Bugs = project.Bugs

                    }
                );
            }

        }

        [HttpGet]
        public IActionResult EditTask(int projectId, int? id)
        {
            if (id == null)
            {
                return View(new TaskBugViewModel 
                {
                    ProjectId = projectId
                });
            }
            else
            {
                var task = _repo.GetTask((int)id);
                return View(new TaskBugViewModel
                {
                    Id = task.TaskId,
                    ProjectId = task.ProjectId,
                    Title = task.Title,
                    Description = task.Description,
                    CurrentImage = task.Image,
                    Status = task.Status,
                    Priority = task.Priority,
                });
            }

        }

        [HttpGet]
        public IActionResult EditBug(int projectId, int? id)
        {
            if (id == null)
            {
                return View(new TaskBugViewModel
                {
                    ProjectId = projectId
                });
            }
            else
            {
                var bug = _repo.GetBug((int)id);
                return View(new TaskBugViewModel
                {
                    Id = bug.BugId,
                    ProjectId = bug.ProjectId,
                    Title = bug.Title,
                    Description = bug.Description,
                    CurrentImage = bug.Image,
                    Status = bug.Status,
                    Priority = bug.Priority

                }
                );
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditProject(ProjectViewModel vm)
        {
            var project = new Project
            {
                ProjectId = vm.ProjectId,
                Title = vm.Title,
                Description = vm.Description,
                Image = await _fileManager.SaveImage(vm.Image),
                Status = vm.Status,
                Tasks = vm.Tasks,
                Bugs = vm.Bugs
            };

            if (vm.Image == null)
                project.Image = vm.CurrentImage;
            else
                project.Image = await _fileManager.SaveImage(vm.Image);


            if (project.ProjectId > 0)
                _repo.UpdateProject(project);
            else
                _repo.AddProject(project);


            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(project);

        }

        [HttpPost]
        public async Task<IActionResult> EditTask(TaskBugViewModel vm)
        {
            var task = new Models.Task
            {
                TaskId = vm.Id,
                ProjectId = vm.ProjectId,
                Title = vm.Title,
                Description = vm.Description,
                Image = await _fileManager.SaveImage(vm.Image),
                Status = vm.Status,
                Priority = vm.Priority
            };

            if (vm.Image == null)
                task.Image = vm.CurrentImage;
            else
                task.Image = await _fileManager.SaveImage(vm.Image);


            if (task.TaskId > 0)
                _repo.UpdateTask(task);
            else
                _repo.AddTask(task);


            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index", "Home");
            else
                return View(task);

        }

        [HttpPost]
        public async Task<IActionResult> EditBug(TaskBugViewModel vm)
        {
            var bug = new Bug
            {
                BugId = vm.Id,
                ProjectId = vm.ProjectId,
                Title = vm.Title,
                Description = vm.Description,
                Image = await _fileManager.SaveImage(vm.Image),
                Status = vm.Status,
                Priority = vm.Priority
            };

            if (vm.Image == null)
                bug.Image = vm.CurrentImage;
            else
                bug.Image = await _fileManager.SaveImage(vm.Image);


            if (bug.BugId > 0)
                _repo.UpdateBug(bug);
            else
                _repo.AddBug(bug);


            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index", "Home");
            else
                return View(bug);

        }

        [HttpGet]
        public async Task<IActionResult> RemoveProject(int id)
        {
            _repo.RemoveProject(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> RemoveTask(int id)
        {
            _repo.RemoveTask(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> RemoveBug(int id)
        {
            _repo.RemoveBug(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
