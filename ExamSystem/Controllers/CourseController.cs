using Microsoft.AspNetCore.Mvc;
using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Security.Cryptography;

namespace ExamSystem.Controllers
{
    public class CourseController : Controller
    {
        OnlineExamSystemContext context = new OnlineExamSystemContext();
        public IActionResult Index(int cate, string searchText)
        {
            List<Category> categories = context.Categories.ToList();
            ViewBag.categories = categories;

            List<Exam>listExam = context.Exams.Include(i=>i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i=>i.Category).ToList();
            if(cate != 0)
                listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).Where(i => i.CategoryId == cate).ToList();
            ViewBag.CategoryID = cate;

            if (!string.IsNullOrEmpty(searchText))
            {
                listExam = listExam.Where(i => i.ExamName.Contains(searchText)).ToList();
                ViewBag.searchText = searchText;
            }
            ViewBag.listExam = listExam;

            List<User> list = context.Users.ToList();

            string userID = HttpContext.Session.GetString("userID");
            if(userID != null)
            {
                ViewBag.User = context.Users.Find(Int32.Parse(userID));
                ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
                ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            }
            ViewBag.Page = 2;
            return PartialView("/Views/Course/Index.cshtml");
        }

        public IActionResult CourseDetail(int eID)
        {
            List<User> list = context.Users.ToList();

            string userID = HttpContext.Session.GetString("userID");

            ViewBag.User = context.Users.Find(Int32.Parse(userID));
            ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
            ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).ToList();
            ViewBag.listExam = listExam;
            Exam exam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).FirstOrDefault(i => i.ExamId == eID);
           
            ViewBag.exam = exam;
            ViewBag.Page = 2;
            return PartialView("/Views/Course/CourseDetail.cshtml");
        }
        [HttpPost]
        public JsonResult SubmitCourse(string AnswerList, int eID)
        {
            CompletedExam completedExam = context.CompletedExams.OrderBy(i => i.CompletedExamId).Last();

            List<User> list = context.Users.ToList();

            string userID = HttpContext.Session.GetString("userID");
            string[] answerList = AnswerList.Split(" ");
            
            ViewBag.User = context.Users.Find(Int32.Parse(userID));
            ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
            ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Questions).Include(i => i.Category).ToList();
            ViewBag.listExam = listExam;
            Exam exam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).Include(i=>i.Questions).FirstOrDefault(i => i.ExamId == eID);
            List<ExamAnswer> examAnswers = context.ExamAnswers.Where(i => i.CompletedExamId == completedExam.CompletedExamId).ToList();
            for (int i = 1; i < answerList.Length; i++)
            {
                examAnswers[i-1].Answer = Int32.Parse(answerList[i]);
            }
            ViewBag.exam = exam;
            ViewBag.size = exam.Questions.LongCount();
            context.SaveChanges();
            return Json(1);
        }

       

        public IActionResult StartCourse(int eID, int numofQ)
        {
            List<User> list = context.Users.ToList();

            string userID = HttpContext.Session.GetString("userID");

            ViewBag.User = context.Users.Find(Int32.Parse(userID));
            ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
            ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Questions).Include(i => i.Category).ToList();
            ViewBag.listExam = listExam;
            Exam exam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).Include(i => i.Questions).FirstOrDefault(i => i.ExamId == eID);
            Random random = new Random();
            int k = random.Next(0, 4);
            List<Question> listQ = exam.Questions.ToList();

            int n = listQ.Count - k;
            Question question = listQ[k];
            listQ[k] = listQ[9-k];
            listQ[9-k] = question;
            question = listQ[0];
            listQ[0] = listQ[8-k];
            listQ[8 - k] = question;
            List<Question> listQ2 = new List<Question>();
            for (int i = 0; i < numofQ; i++)
            {
                listQ2.Add(listQ[i]);
            }

            exam.Questions = listQ2;

            ViewBag.exam = exam;
            ViewBag.size = exam.Questions.LongCount();
            CompletedExam completedExam = new CompletedExam();
            completedExam.ExamId = eID;
            completedExam.UserId = Int32.Parse(userID);
            context.CompletedExams.Add(completedExam);
            context.SaveChanges();
            List<Question> questions = context.Questions.Where(i=>i.ExamId == eID).ToList();
            CompletedExam newCompletedExam = context.CompletedExams.OrderBy(i => i.CompletedExamId).Last();
            foreach (Question item in questions)
            {
                ExamAnswer examAnswer = new ExamAnswer();
                examAnswer.CompletedExamId = newCompletedExam.CompletedExamId;
                examAnswer.QuestionId = item.QuestionId;
                examAnswer.CorrectAnswer = item.CorrectAnswer;
                examAnswer.Answer = -1;
                context.ExamAnswers.Add(examAnswer);
            }
            context.SaveChanges();
            ViewBag.Num = 1;
            ViewBag.Page = 2;
            return PartialView("/Views/Course/StartCourse.cshtml");
        }
        [HttpPost]
        public JsonResult AddCourse(string Question, string AnswerA, string AnswerB, string AnswerC, string AnswerD, int numOfQuestion, string CorrectAnswer, string examTitle, int categoryId,int timeLimit)
        {
            string userID = HttpContext.Session.GetString("userID");
            string[] listQuestion = Question.Split("||");
            string[] listAnswerA = AnswerA.Split("||");
            string[] listAnswerB = AnswerB.Split("||");
            string[] listAnswerC = AnswerC.Split("||");
            string[] listAnswerD = AnswerD.Split("||");
            string[] listCorrectAnswer = CorrectAnswer.Split("||");
            Exam exam = new Exam()
            {
                ExamName = examTitle,
                CategoryId = categoryId,
                Owner = Int32.Parse(userID),
                Time = timeLimit,
                Pictures = "img/courses-2.jpg",
                Numofquestion = numOfQuestion

            };
            context.Exams.Add(exam);
            context.SaveChanges();
            Exam newExam = context.Exams.OrderBy(i => i.ExamId).Last();

            for (int i = 1; i <= numOfQuestion; i++)
            {
                Question q = new Question
                {
                    Question1 = listQuestion[i],
                    Answer1 = listAnswerA[i],
                    Answer2 = listAnswerB[i],
                    Answer3 = listAnswerC[i],
                    Answer4 = listAnswerD[i],
                    CorrectAnswer = Int32.Parse(listCorrectAnswer[i]),
                    ExamId = newExam.ExamId,
                };
                context.Questions.Add(q);
            }
            ViewBag.Page = 2;
            context.SaveChanges();
            return Json(1);
        }

        public IActionResult CourseResult()
        {
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).ToList();
            ViewBag.listExam = listExam;

            List<User> list = context.Users.ToList();

            string userID = HttpContext.Session.GetString("userID");
            if (userID != null)
            {
                ViewBag.User = context.Users.Find(Int32.Parse(userID));
                ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
                ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            }

            CompletedExam completedExam = context.CompletedExams.Include(i=>i.ExamAnswers).Include(i => i.Exam).OrderBy(i => i.CompletedExamId).Last();
            float count = 0;
            
            foreach (ExamAnswer item in completedExam.ExamAnswers)
            {
                Question question = context.Questions.FirstOrDefault(i=>i.QuestionId ==item.QuestionId);
                item.Question = question;
                if (item.Question.CorrectAnswer == item.Answer)
                    count++;

            }
            completedExam.Mark = (count / completedExam.Exam.Numofquestion)*10;
            context.SaveChanges();
            ViewBag.count = count;
            ViewBag.completedExam = completedExam;
            ViewBag.Questions = context.Questions.ToList();
            ViewBag.Page = 2;
            return View();
        }

        [HttpPost]

        public IActionResult CourseResult(int completedExamId)
        {
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).ToList();
            ViewBag.listExam = listExam;

            List<User> list = context.Users.ToList();

            string userID = HttpContext.Session.GetString("userID");
            if (userID != null)
            {
                ViewBag.User = context.Users.Find(Int32.Parse(userID));
                ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
                ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            }

            CompletedExam completedExam = context.CompletedExams.Include(i => i.ExamAnswers).Include(i => i.Exam).FirstOrDefault(i=>i.CompletedExamId == completedExamId);
            float count = 0;

            foreach (ExamAnswer item in completedExam.ExamAnswers)
            {
                Question question = context.Questions.FirstOrDefault(i => i.QuestionId == item.QuestionId);
                item.Question = question;
                if(item.Answer == 1)
                {
                    item.AnswerContent = item.Question.Answer1;
                }
                if (item.Answer == 2)
                {
                    item.AnswerContent = item.Question.Answer2;
                }
                if (item.Answer == 3)
                {
                    item.AnswerContent = item.Question.Answer3;
                }
                if (item.Answer == 4)
                {
                    item.AnswerContent = item.Question.Answer4;
                }
                if (item.Answer == -1)
                {
                    item.AnswerContent = "You didn't answer";
                }
                if (item.Question.CorrectAnswer == item.Answer)
                    count++;

            }
            completedExam.Mark = (count / completedExam.Exam.Numofquestion)*10;
            context.SaveChanges();
            ViewBag.count = count;
            ViewBag.completedExam = completedExam;
            ViewBag.Questions = context.Questions.ToList();
            ViewBag.Page = 2;
            return View();
        }

        public IActionResult MyResult()
        {
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).ToList();
            ViewBag.listExam = listExam;

            List<User> list = context.Users.ToList();

            string userID = HttpContext.Session.GetString("userID");
            if (userID != null)
            {
                ViewBag.User = context.Users.Find(Int32.Parse(userID));
                ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
                ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            }
            List<CompletedExam> completedExams = context.CompletedExams.Include(i=>i.User).Include(i => i.Exam).Include(i => i.Exam.OwnerNavigation).Include(i => i.Exam.OwnerNavigation.User).Where(i => i.UserId==Int32.Parse(userID)).ToList();
            foreach (CompletedExam item in completedExams)
            {
                if (item.Mark == null)
                    item.Mark = 0;
            }
            ViewBag.completedExams = completedExams;
            return View();
        }

        public IActionResult ScoreStatics(int categoryID, string choosenExam,string byMark)
        {
            
            List<User> list = context.Users.ToList();
            
            string userID = HttpContext.Session.GetString("userID");
            if (userID != null)
            {
                ViewBag.User = context.Users.Find(Int32.Parse(userID));
                ViewBag.Role = context.Users.Find(Int32.Parse(userID)).Role;
                ViewBag.Message = "Hello " + list.FirstOrDefault(i => i.UserId == Int32.Parse(userID)).Name;
            }
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).Where(i => i.Owner == Int32.Parse(userID)).ToList();
            List<Exam> listChoosenExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).Where(i => i.Owner == Int32.Parse(userID)).ToList();

            if (categoryID != null)
            {
                if (categoryID != 0)
                {
                    listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).Where(i => i.Owner == Int32.Parse(userID) && i.CategoryId == categoryID).ToList();
                    listChoosenExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).Where(i => i.Owner == Int32.Parse(userID) && i.CategoryId == categoryID).ToList();
                    ViewBag.CategoryID = categoryID;
                }
            }
            if (!string.IsNullOrEmpty(choosenExam))
            {
                string[] choosen = choosenExam.Split(" ");
                if (!choosen.Contains("-1"))
                {
                    listChoosenExam = new List<Exam>();
                    foreach (Exam item in listExam)
                    {
                        if(choosen.Contains(item.ExamId+""))
                            listChoosenExam.Add(item);
                    }
                    
                }
            }

            ViewBag.listExam = listExam;
            List<Category> categories= context.Categories.ToList();
            ViewBag.categories = categories;
            List<CompletedExam> completedExams = context.CompletedExams.Include(i => i.User).Include(i => i.Exam).Include(i => i.Exam.OwnerNavigation).Include(i => i.Exam.OwnerNavigation.User).ToList();
            List<CompletedExam> completedExamsList = new List<CompletedExam>();
            
            foreach (CompletedExam item in completedExams)
            {
                if (item.Mark == null)
                    item.Mark = 0;
                foreach (Exam exam in listChoosenExam)
                {
                    if (item.ExamId == exam.ExamId)
                        completedExamsList.Add(item);
                }
                
            }
            
            if (!string.IsNullOrEmpty(byMark))
            {
                if (byMark.EndsWith("Low"))
                    completedExamsList.Sort((a, b) => (int)(a.Mark - b.Mark));
                if (byMark.EndsWith("High"))
                    completedExamsList.Sort((a, b) => (int)(b.Mark - a.Mark));

                ViewBag.byMark = byMark;
            }
            
                string examIdList = "";
            string choosenExamIdList = "";
            foreach (Exam item in listExam)
            {
                examIdList = examIdList + " " + item.ExamId;
            }
            foreach (Exam item in listChoosenExam)
            {
                choosenExamIdList = choosenExamIdList + " " + item.ExamId;
            }

            ViewBag.examIdList = examIdList;
            ViewBag.choosenExamIdList = choosenExamIdList;
            ViewBag.size = listExam.Count();
            ViewBag.completedExamsList = completedExamsList;
            return View();
        }
        public IActionResult DoCourse()
        {
            List<User> list = context.Users.ToList();

            
            List<Exam> listExam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Questions).Include(i => i.Category).ToList();
            ViewBag.listExam = listExam;
            Exam exam = context.Exams.Include(i => i.OwnerNavigation.User).Include(i => i.OwnerNavigation).Include(i => i.Category).Include(i => i.Questions).FirstOrDefault(i => i.ExamId == 1);
            ViewBag.Index = 1;
            ViewBag.exam = exam;
            ViewBag.size = exam.Questions.LongCount();
            ViewBag.Num = 1;
            ViewBag.Page = 2;


            return View();
        }
        }
}
