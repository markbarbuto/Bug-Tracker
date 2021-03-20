using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBugTracker.Models
{
    public class Project : Assignment
    {
        //public Project()
        //{
        //    Tasks = new List<Models.Task>();
        //    Bugs = new List<Bug>();
        //}
        public int ProjectId { get; set; }

        public IEnumerable<Models.Task> Tasks { get; set; }

        public IEnumerable<Models.Bug> Bugs { get; set; }

    }
}
