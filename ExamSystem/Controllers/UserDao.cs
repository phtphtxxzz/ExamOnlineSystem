using Microsoft.AspNetCore.Mvc;
using WinForm_ADO;
using System.Data.SqlClient;
using System.Data;

namespace ExamSystem.Controllers
{
    public class UserDao : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        DataProvider d = new DataProvider();

        //public User getUserById(int id)
        //{
            
        //    string strSearch = "select * from USER where USER.USER_ID = @id";
        //    List<SqlParameter> param = new List<SqlParameter>
        //        {
        //            new SqlParameter("@id",id),
        //        };
        //    IDataReader dr = d.executeQuery2(strSearch, param.ToArray());
        //    while (dr.Read())
        //    {
        //        User user = new User();
        //        if (!string.IsNullOrEmpty(dr.GetString(2)))
        //            user.userName = dr.GetString(2);
        //        else user.userName = "";
        //        if (!string.IsNullOrEmpty(dr.GetString(3)))
        //            user.image = dr.GetString(2);
        //        else user.image = "";
        //        if (!string.IsNullOrEmpty(dr.GetString(4)))
        //            user.phone = dr.GetString(4);
        //        else user.phone = "";
        //        if (!string.IsNullOrEmpty(dr.GetString(5)))
        //            user.address = dr.GetString(5);
        //        else user.address = "";
        //        if (!string.IsNullOrEmpty(dr.GetString(6)))
        //            user.gender = dr.GetString(6);
        //        else user.gender = "";
        //        if (!string.IsNullOrEmpty(dr.GetString(7)))
        //            user.email = dr.GetString(7);
        //        else user.email = "";
        //        user.userID = id;
        //        dr.Close();
        //        return user;

        //    }
        //    dr.Close();
        //    return null;
        //}

    }
}
