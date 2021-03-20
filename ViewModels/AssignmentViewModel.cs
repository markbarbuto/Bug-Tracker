using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBugTracker.ViewModels
{
    public class AssignmentViewModel
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public IFormFile Image { get; set; } = null;
        public string CurrentImage { get; set; } = $"../content/static/default-img.jpg";
        public DateTime LastModified { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Unclaimed";
    }
}
