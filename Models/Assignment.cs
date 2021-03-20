using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBugTracker.Models
{
    public class Assignment
    {        
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; } = "";
        
        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; } = "";
        
        public string Image { get; set; } = "";

        [Required]
        public DateTime LastModified { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(20)]
        [Column(TypeName="varchar(20)")]
        public string Status { get; set; } = "Unclaimed";
    }
}
