using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THA.MODEL;
using THA.DAO;

namespace THA.WEB
{
    public class AccountController : Controller
    {
        // GET: Account    
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListUser()
        {            
            return View();
        }
        public ActionResult List()
        {
            List<UserViewModel> liUser = GetUsers();
            return View(liUser);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(UserViewModel model)
        {
            return View(model);
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
            
            return View();
        }

        private List<UserViewModel> GetUsers()
        {
            var userDao = new UserDAO();
            return userDao.GetUsers();
        }
    }
}