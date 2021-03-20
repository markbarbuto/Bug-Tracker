using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBugTracker.Models;

namespace TaskBugTracker.Data.Repository
{
    public interface IRepository
    {
        Project GetProject(int id);
        Models.Task GetTask(int id);
        Bug GetBug(int id);

        IEnumerable<Project> GetAllProjects();
        IEnumerable<Models.Task> GetAllTasks(int projectId);
        IEnumerable<Bug> GetAllBugs(int projectId);

        void AddProject(Project project);
        void AddTask(Models.Task task);
        void AddBug(Bug bug);

        void UpdateProject(Project project);
        void UpdateTask(Models.Task task);
        void UpdateBug(Bug bug);

        void RemoveProject(int id);
        void RemoveTask(int id);
        void RemoveBug(int id);

        Task<bool> SaveChangesAsync();
    }
}
