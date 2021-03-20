using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBugTracker.ViewModels
{
    public class TaskBugViewModel : AssignmentViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Priority { get; set; } = "Low";
    }
}
