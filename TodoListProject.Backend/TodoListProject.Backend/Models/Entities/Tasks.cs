using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListProject.Backend.Models.Entities
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        public int TaskId {get; set; }
        public string TaskTitle { get; set; }
        public string Explanation { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool Priority { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
        public Users Users { get; set; }

  
    }
}
