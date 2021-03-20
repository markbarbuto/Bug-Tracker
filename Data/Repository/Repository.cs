using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBugTracker.Models;

namespace TaskBugTracker.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void AddProject(Project project)
        {
            _ctx.Projects.Add(project);
        }
        public void AddTask(Models.Task task)
        {
            _ctx.Tasks.Add(task);
        }

        public void AddBug(Bug bug)
        {
            _ctx.Bugs.Add(bug);
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _ctx.Projects.ToList();
        }

        public IEnumerable<Models.Task> GetAllTasks(int projectId)
        {
            //throw new NotImplementedException();
            return _ctx.Tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        public IEnumerable<Bug> GetAllBugs(int projectId)
        {
            //throw new NotImplementedException();
            return _ctx.Bugs.Where(b => b.ProjectId == projectId).ToList();
        }

        public Project GetProject(int id)
        {
            var project = _ctx.Projects.FirstOrDefault(p => p.ProjectId == id);
            return project;

        }
        public Models.Task GetTask(int id)
        {
            return _ctx.Tasks.FirstOrDefault(t => t.TaskId == id);
        }

        public Bug GetBug(int id)
        {
            return _ctx.Bugs.FirstOrDefault(b => b.BugId == id);
        }

        public void RemoveProject(int id)
        {
            _ctx.Projects.Remove(GetProject(id));
        }
        public void RemoveTask(int id)
        {
            _ctx.Tasks.Remove(GetTask(id));
        }

        public void RemoveBug(int id)
        {
            _ctx.Bugs.Remove(GetBug(id));
        }

        public void UpdateProject(Project project)
        {
            _ctx.Projects.Update(project);
        }
        public void UpdateTask(Models.Task task)
        {
            _ctx.Tasks.Update(task);
        }

        public void UpdateBug(Bug bug)
        {
            _ctx.Bugs.Update(bug);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true; 
            }
            return false;
        }

    }
}
