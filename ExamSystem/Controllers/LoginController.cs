using ExamSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using WinForm_ADO;

namespace ExamSystem.Controllers
{
    public class LoginController : Controller
    {
        DataProvider d = new DataProvider();
        public IActionResult Index(string userName, string pass)
        {
            List<Category> categories = context.Categories.ToList();
            ViewBag.categories = categories;

            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).ToList();
            ViewBag.listExam = listExam;
            string userID = HttpContext.Session.GetString("userID");
            if (!string.IsNullOrEmpty(userID))
            {
                List<User> list = context.Users.ToList();
                ViewBag.User = context.Users.Find(Int32.Parse(userID));
                ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
                ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            }
            else if(!string.IsNullOrEmpty(userName))
            {


                List<Account> list = context.Accounts.ToList();
                List<User> users = context.Users.ToList();
                string strSearch = "select * from ACCOUNT where ACCOUNT.USERNAME = @name and ACCOUNT.PASSWORD= @pass";
                List<SqlParameter> param = new List<SqlParameter>
                {
                    new SqlParameter("@name",userName),
                    new SqlParameter("@pass",pass),
                };

                IDataReader dr = d.executeQuery2(strSearch, param.ToArray());

                userID = "";
                foreach (Account item in list)
                {
                    if (item.Username.Equals(userName))
                    {
                        userID = item.AccountId.ToString();
                        ViewBag.Role = item.Role;
                    }

                }

                HttpContext.Session.SetString("userID", userID);
                ViewBag.User = context.Users.Find(Int32.Parse(userID));
                ViewBag.Message = "Hello " + users.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            }
            
            ViewBag.Page = 1;
            return PartialView("/Views/Home/Index.cshtml");

                       
        }
        UserDao userDao = new UserDao();
        OnlineExamSystemContext context = new OnlineExamSystemContext();
        [HttpPost]
        public IActionResult SignUp(string signUpName, string signUpPass, string signUpMail,string name,string gender)
        {
            
            try
            {
                if(context.Accounts.FirstOrDefault(i=>i.Username.Equals(signUpName))!=null)
                    return PartialView("/Views/Login/Index.cshtml");
                context.Accounts.Add(new Account
                {
                    Username = signUpName,
                    Password = signUpPass,
                    Email = signUpMail,
                    Role = "Student",
                });
               
                context.SaveChanges();
                string userID = "";
                List<Account> list = context.Accounts.ToList();
                foreach (Account item in list)
                {
                    if (item.Username.Equals(signUpName))
                        userID = item.AccountId.ToString();
                    
                }
                context.Users.Add(new User
                {
                    UserId = Int32.Parse(userID),
                    Role = "Student",
                    Email = signUpMail,
                    Name = name,
                    Gender = gender
                });
                context.SaveChanges();
                HttpContext.Session.SetString("userID", userID);
                ViewBag.User = context.Users.Find(Int32.Parse(userID));
                ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
                ViewBag.Message = "Hello " + context.Users.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
                return PartialView("/Views/Login/Index.cshtml");
                
            }
            catch (Exception ex)
            {
                return PartialView("/Views/Home/Index.cshtml");

            }
            ViewBag.Page = 1;
            return PartialView("/Views/Home/Index.cshtml");
        }


        public IActionResult Profile()
        {
            List<User> list = context.Users.ToList();
            
                string userID = HttpContext.Session.GetString("userID");

                ViewBag.User = context.Users.Find(Int32.Parse(userID));
            ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
            ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            return PartialView("/Views/Login/Index.cshtml");
               
        }

        public IActionResult AddCourse(string numOfQuestion)
        {
            List<User> list = context.Users.ToList();

            string userID = HttpContext.Session.GetString("userID");
            if (!string.IsNullOrEmpty(numOfQuestion))
            {
                ViewBag.NumOfQuestion = Int32.Parse(numOfQuestion);
                
            }
            else
            ViewBag.NumOfQuestion = 10;


            ViewBag.Categories = context.Categories.ToList();
            ViewBag.User = context.Users.Find(Int32.Parse(userID));
            ViewBag.Role = context.Accounts.Find(Int32.Parse(userID)).Role;
            ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            ViewBag.Page = 1;
            return PartialView("/Views/Course/AddCourse.cshtml");

        }
        [HttpPost]
        public JsonResult CheckExist(string username, string pass)
        {
            Account a = context.Accounts.FirstOrDefault(i => i.Username == username && i.Password == pass);
            
            
            if(a != null)
            return Json(1);
            else return Json(0);
        }

        [HttpPost]
        public JsonResult CheckUserNameExist(string username)
        {
            Account a = context.Accounts.FirstOrDefault(i => i.Username == username);


            if (a == null)
                return Json(1);
            else return Json(0);
        }

        public IActionResult LogOut()
        {
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).ToList();
            List<Category> categories = context.Categories.ToList();
            ViewBag.categories = categories;

            ViewBag.listExam = listExam;
            ViewBag.Page = 1;
            HttpContext.Session.Remove("userID");           
            return PartialView("/Views/Home/Index.cshtml");


        }


    }
}
