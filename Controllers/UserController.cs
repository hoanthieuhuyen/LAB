using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
    public class UserController : BaseController
    {
        // GET: User
        // chu thich
        UserDAO userDao = new UserDAO();
        public ActionResult Index()
        {
            DataTable products = GetAllToTable();
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(H_USERS model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userDao.AddUser(1, model.USERID, model.USERNAME, model.PASSWORD, model.FULLNAME, model.MA_DV, model.NGAY_TAO.ToString(), model.STATUS);
                    SetAlert("Thêm mới thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                }
            }
            return View("Create");
        }
   
        public ActionResult User_ReadList([DataSourceRequest]DataSourceRequest request)
        {
            var gridData = GetAllToList();
            var result = new DataSourceResult()
            {
                Data = gridData,
                Total = gridData.Count
            };

            return Json(result);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName("DanhSachNguoiDung")]
        public JsonResult User_ReadList()
        {
            var gridData = GetAllToList();       

            return Json(new { data=gridData,totalCount=gridData.Count() },JsonRequestBehavior.AllowGet);
        }
        private List<H_USERS> GetAllToList()
        {
            return userDao.GetAllToList();
        }

        public ActionResult GetDonVi()
        {
            try
            {
                var donviDao = new DonViDAO();
                List<DONVI> lDonVi = donviDao.GetDonVi();
                return Json(lDonVi, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
            }
            return View("Create");
        }
        public ActionResult User_ReadTable([DataSourceRequest]DataSourceRequest request)
        {
            DataTable products = GetAllToTable();
            return Json(products.ToDataSourceResult(request));
        }
        private DataTable GetAllToTable()
        {
            return userDao.GetAllToTable();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            H_USERS userOject = new H_USERS();
            userOject = userDao.GetUserByUserId(long.Parse(id));
            return View(userOject);
        }
        [HttpPost]
        public ActionResult Edit(H_USERS model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userDao.AddUser(2, model.USERID, model.USERNAME, model.PASSWORD, model.FULLNAME, model.MA_DV, model.NGAY_TAO.ToString(), model.STATUS);
                    SetAlert("Sửa thông tin thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                }
            }
            return View("Edit");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, H_USERS model)
        {
            try
            {
                userDao.AddUser(3, model.USERID, model.USERNAME, model.PASSWORD, model.FULLNAME, model.MA_DV, model.NGAY_TAO.ToString(), model.STATUS);
                SetAlert("Xóa thông tin thành công", "success");
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View("Index");
            }           
        }
    }
}