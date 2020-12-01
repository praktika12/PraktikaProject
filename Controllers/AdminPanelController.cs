using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraktikaProject.Models;
namespace PraktikaProject.Controllers
{
    public class AdminPanelController : Controller
    {
        WorkTimeDBEntities WTDBE = new WorkTimeDBEntities();
        TasksDBEntities TDBE = new TasksDBEntities();
        public ActionResult Index()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            List<User> usersList = WTDBE.Users.ToList();
            ViewData["usersDataVD"] = usersList;
            List<Task> tasksList = TDBE.Tasks.ToList();
            ViewData["tasksList"] = tasksList;
            return View();
        }
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var findRequest = WTDBE.Users.Find(id);
            return View(findRequest);
        }

        [HttpPost, ActionName("DeleteUser")]
        public ActionResult DeleteRequestConfirmed(int id)
        {
            var findRequest = WTDBE.Users.Find(id);
            WTDBE.Users.Remove(findRequest);
            WTDBE.SaveChanges();
            return RedirectToAction("Index", "AdminPanel");
        }
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            List<User> usersList = WTDBE.Users.ToList();
            ViewData["usersDataEdit"] = usersList;
            var getUser = WTDBE.Users.Where(x => x.userID == id).FirstOrDefault();
            return View(getUser);
        }

        [HttpPost]
        public ActionResult EditUser(int id, User user)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            WTDBE.Entry(user).State = EntityState.Modified;
            WTDBE.SaveChanges();
            return RedirectToAction("Index", "AdminPanel");
        }
        [HttpGet]
        public ActionResult addUser()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult addUser(User currentRequestToAdd)
        {
            WTDBE.Users.Add(currentRequestToAdd);
            if (currentRequestToAdd.username != null && currentRequestToAdd.password != null)
            {
                WTDBE.SaveChanges();
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index", "AdminPanel");
        }

        [HttpGet]
        public ActionResult DeleteTask(int id)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var findRequest = TDBE.Tasks.Find(id);
            return View(findRequest);
        }

        [HttpPost, ActionName("DeleteTask")]
        public ActionResult DeleteTaskRequestConfirmed(int id)
        {
            var findRequest = TDBE.Tasks.Find(id);
            TDBE.Tasks.Remove(findRequest);
            TDBE.SaveChanges();
            return RedirectToAction("Index", "AdminPanel");
        }
        [HttpGet]
        public ActionResult EditTask(int id)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Task> tasksList = TDBE.Tasks.ToList();
            ViewData["usersDataEdit"] = tasksList;
            var getTask = TDBE.Tasks.Where(x => x.TaskId == id).FirstOrDefault();
            return View(getTask);
        }

        [HttpPost]
        public ActionResult EditTask(int id, Task task)
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            TDBE.Entry(task).State = EntityState.Modified;
            TDBE.SaveChanges();
            return RedirectToAction("Index", "AdminPanel");
        }

        [HttpGet]
        public ActionResult addTask()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            bool checkAdmin = (bool)Session["isCurrentUserAdmin"];
            if (checkAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult addTask(Task currentRequestToAdd)
        {
            TDBE.Tasks.Add(currentRequestToAdd);
            TDBE.SaveChanges();
            return RedirectToAction("Index", "AdminPanel");
        }

    }
}