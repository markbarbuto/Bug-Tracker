using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBugTracker.ViewModels
{
    public class ProjectViewModel : AssignmentViewModel
    {
        public int ProjectId { get; set; }
        public IEnumerable<Models.Task> Tasks { get; set; }
        public IEnumerable<Models.Bug> Bugs { get; set; }
    }
}
