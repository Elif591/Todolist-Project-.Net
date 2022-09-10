using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoListProject.Backend.Models.DataContext;
using TodoListProject.Backend.Models.Entities;

namespace TodoListProject.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        DataContext db = new DataContext();

        [HttpPost("newtask")]
        public bool createTask(Tasks tasks)
        {
            if (tasks != null)
            {
                tasks.StartDate = DateTime.Now;


                db.Add(tasks);
                db.SaveChanges();
                return true;
            }
            return false;
        }



        [HttpPost("alltask")]
        public List<Tasks> AllTask(Tasks task)
        {

            List<Tasks> tasks = new List<Tasks>();
            if (task != null)
            {
                tasks = db.Tasks.Where(x => x.UserId == task.UserId).ToList();

            }
            return tasks;
        }

        [HttpGet("searchtask")]

        public List<Tasks> SearchTasks(int userId, string searchTerm)
        {
            List<Tasks> tasks = new List<Tasks>();
            if (userId != 0)
            {
                tasks = db.Tasks.Where(x => x.UserId == userId).ToList();


            }
            List<Tasks> searchTasks = new List<Tasks>();
            searchTasks = tasks.Where(x => x.TaskTitle.Substring(0, 2).ToLower() == searchTerm.Substring(0, 2).ToLower()).ToList();

            return searchTasks;
        }

        [HttpPost("deletetask")]
        public bool DeleteTask(Tasks task)
        {
            var deleteTask = db.Tasks.Where(x => x.TaskId == task.TaskId);
            if (deleteTask != null)
            {
                db.RemoveRange(deleteTask);
                db.SaveChanges();
                return true;

            }
            return false;

        }

        [HttpPost("complatedtask")]
        public bool ComplatedTask(Tasks task)
        {
            List<Tasks> complatedTask = new List<Tasks>();
            complatedTask = db.Tasks.Where(x => x.TaskId == task.TaskId).ToList();
            if (complatedTask != null)
            {
                complatedTask.Find(x => x.Completed = true);
                var finishdate = complatedTask.Find(x => x.TaskId == task.TaskId);
                finishdate.FinishDate = DateTime.Now;
                db.SaveChanges();
                return true;

            }
            return false;

        }


        [HttpPost("complatedchangetask")]
        public bool ComplatedChangeTask(Tasks task)
        {
            List<Tasks> complatedTask = new List<Tasks>();
            complatedTask = db.Tasks.Where(x => x.TaskId == task.TaskId).ToList();


            if (complatedTask != null)
            {
                complatedTask.Find(x => x.Completed = false);

                db.SaveChanges();
                return true;

            }
            return false;

        }


        [HttpPost("prioritytask")]
        public bool PriorityTask(Tasks task)
        {
            List<Tasks> priorityTask = new List<Tasks>();
            priorityTask = db.Tasks.Where(x => x.TaskId == task.TaskId).ToList();
            if (priorityTask != null)
            {
                priorityTask.Find(x => x.Priority = false);
                db.SaveChanges();
                return true;

            }
            return false;

        }


        [HttpPost("prioritychangetask")]
        public bool PriorityChangeTask(Tasks task)
        {
            List<Tasks> priorityTask = new List<Tasks>();
            priorityTask = db.Tasks.Where(x => x.TaskId == task.TaskId).ToList();
            if (priorityTask != null)
            {
                priorityTask.Find(x => x.Priority = true);
                db.SaveChanges();
                return true;

            }
            return false;

        }
    }
}
