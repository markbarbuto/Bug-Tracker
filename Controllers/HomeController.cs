using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBugTracker.Data;
using TaskBugTracker.Data.FileManager;
using TaskBugTracker.Data.Repository;
using TaskBugTracker.Models;

namespace TaskBugTracker.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(IRepository repo, IFileManager fileManager)
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

        public IActionResult Project(int id, string taskSortOrder, string bugSortOrder)
        {
            var project = _repo.GetProject(id);

            ViewBag.TitleSortParm = String.IsNullOrEmpty(taskSortOrder) ? "Title_desc" : "";
            ViewBag.DateSortParm = taskSortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.PrioritySortParm = taskSortOrder == "Priority" ? "Priority_desc" : "Priority";
            ViewBag.StatusSortParm = taskSortOrder == "Status" ? "Status_desc" : "Status";

            ViewBag.TitleSortParm = String.IsNullOrEmpty(bugSortOrder) ? "Title_desc" : "";
            ViewBag.DateSortParm = bugSortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.PrioritySortParm = bugSortOrder == "Priority" ? "Priority_desc" : "Priority";
            ViewBag.StatusSortParm = bugSortOrder == "Status" ? "Status_desc" : "Status";

            var tasks = from t in _repo.GetAllTasks(id)
                        select t;

            switch (taskSortOrder)
            {
                case "Title_desc":
                    tasks = tasks.OrderByDescending(t => t.Title);
                    break;
                case "Date":
                    tasks = tasks.OrderByDescending(t => t.LastModified);
                    break;
                case "Date_desc":
                    tasks = tasks.OrderBy(t => t.LastModified);
                    break;
                case "Priority":
                    tasks = tasks.OrderBy(t => t.Priority);
                    break;
                case "Priority_desc":
                    tasks = tasks.OrderByDescending(t => t.Priority);
                    break;
                case "Status":
                    tasks = tasks.OrderBy(t => t.Status);
                    break;
                case "Status_desc":
                    tasks = tasks.OrderByDescending(t => t.Status);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Title);
                    break;
            }

            project.Tasks = tasks;

            var bugs = from b in _repo.GetAllBugs(id)
                        select b;

            switch (taskSortOrder)
            {
                case "Title_desc":
                    bugs = bugs.OrderByDescending(b => b.Title);
                    break;
                case "Date":
                    bugs = bugs.OrderByDescending(b => b.LastModified);
                    break;
                case "Date_desc":
                    bugs = bugs.OrderBy(b => b.LastModified);
                    break;
                case "Priority":
                    bugs = bugs.OrderBy(b => b.Priority);
                    break;
                case "Priority_desc":
                    bugs = bugs.OrderByDescending(b => b.Priority);
                    break;
                case "Status":
                    bugs = bugs.OrderBy(b => b.Status);
                    break;
                case "Status_desc":
                    bugs = bugs.OrderByDescending(b => b.Status);
                    break;
                default:
                    bugs= bugs.OrderBy(b => b.Title);
                    break;
            }

            project.Bugs = bugs;
            return View(project);
        }

        public IActionResult Task(int id)
        {
            var task = _repo.GetTask(id);
            return View(task);
        }

        public IActionResult Bug(int id)
        {
            var bug = _repo.GetBug(id);
            return View(bug);
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.')+1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }
    }
}
