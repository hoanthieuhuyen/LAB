using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THA.DAO;
using THA.MODEL;

namespace THA.WEB
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDAO();
                bool bCheckLogin = userDao.CheckLogin(model.USERNAME, model.PASSWORD);
                if (bCheckLogin)
                {
                    var mUserView = new UserViewModel();
                    mUserView = userDao.GetUserByUserName(model.USERNAME);
                    var mUserLogin = new UserLogin();
                    mUserLogin.UserName = mUserView.USERNAME;
                    mUserLogin.UserID = mUserView.USERID;

                    Session.Add(Constants.USER_SESSION, mUserLogin);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không đúng");
                }
            }

            return View("Index");
        }

        private List<UserViewModel> GetUsers()
        {
            var userDao = new UserDAO();
            return userDao.GetUsers();
        }
    }
}