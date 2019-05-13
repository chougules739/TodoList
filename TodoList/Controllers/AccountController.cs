using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class AccountController : Controller
    {
        List<User> _users = new List<User>()
        {
            new User
            {
                Id = Guid.NewGuid(),
                EmailId = "chougules739@gmail.com",
                Name = "Santosh",
                Password = "Test@123",
                UserName = "chougules739@gmail.com",
                ContactNo = "899404999",
                RoleId = 2
            },
            new User
            {
                Id = Guid.NewGuid(),
                EmailId = "priyasantosh115@gmail.com",
                Name = "Priyanka",
                Password = "Test@123",
                UserName = "priyasantosh115@gmail.com",
                ContactNo = "891404990",
                RoleId = 1
            },
            new User
            {
                Id = Guid.NewGuid(),
                EmailId = "x17170737@gmail.com",
                Name = "Sumit",
                Password = "Test@123",
                UserName = "x17170737@gmail.com",
                ContactNo = "892404927",
                RoleId = 3
            }
        };

        //Get request to login
        [HttpGet]
        //[RequireHttps]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[RequireHttps]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User userModel)
        {
            User _user = _users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).SingleOrDefault();

            if (_user != null)
            {
                FormsAuthentication.SetAuthCookie(userModel.UserName, false);

                var _authTicket = new FormsAuthenticationTicket(1, _user.UserName, DateTime.Now,
                    DateTime.Now.AddMinutes(20), false, GetRoleNameById(_user.RoleId));
                string _encryptedTicket = FormsAuthentication.Encrypt(_authTicket);
                var _authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, _encryptedTicket);
                HttpContext.Response.Cookies.Add(_authCookie);

                Session["User"] = _user;
                
                return RedirectToAction("AllTasks", "Task");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(userModel);
            }
        }

        private string GetRoleNameById(int roleId)
        {
            return roleId == (int)UserType.Manager ? "Manager" : roleId == (int)UserType.Developer ? "Developer" :
                roleId == (int)UserType.Tester ? "Tester" : "";
        }

        public enum UserType
        {
            Manager = 1,
            Developer = 2,
            Tester = 3
        }
    }
}
