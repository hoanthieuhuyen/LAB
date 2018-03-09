using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using Oracle.DataAccess.Client;
using System.Data;
using THA.MODEL;
using System;
using THA.DAO;

namespace THA.WEB
{
    public class ApiUserController : ApiController
    {
        /// <summary>
        /// Read event of Kendo Grid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public DataSourceResult ReadUsers([ModelBinder(typeof(WebApiDataSourceRequestModelBinder))] DataSourceRequest request)
        {
            var gridData = GetUsers();
            var result = new DataSourceResult()
            {
                Data = gridData,
                Total = gridData.Count
            };

            return result;
        }

        public List<UserViewModel> GetUsers()
        {
            var userDao = new UserDAO();
            return userDao.GetUsers();
        }
    }
}
