using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Kendo.Mvc.UI;

namespace THA.WEB
{
    public class ApiHomeController : ApiController
    {
        /// <summary>
        /// Read event of Kendo Grid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public DataSourceResult ReadOrders([ModelBinder(typeof(WebApiDataSourceRequestModelBinder))] DataSourceRequest request)
        {
            var gridData = GetOrders();
            var result = new DataSourceResult()
            {
                Data = gridData,
                Total = gridData.Count
            };

            return result;
        }

        /// <summary>
        /// Delete event of Kendo UI Grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteOrders([ModelBinder(typeof(WebApiDataSourceRequestModelBinder))] DataSourceRequest request, OrderViewModel order)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            if (order != null)
            {
                var northwind = new NorthwindEntities();
                List<Order_Detail> orderDetailsDelete = northwind.Order_Details.Where(m => m.OrderID == order.OrderID).ToList();
                Order orderDelete = northwind.Orders.First(m => m.OrderID == order.OrderID);
                foreach (var item in orderDetailsDelete)
                {
                    northwind.Order_Details.Remove(item);
                }

                northwind.SaveChanges();

                northwind.Orders.Remove(orderDelete);
                northwind.SaveChanges();
            }

            return response;
        }

        /// <summary>
        /// Edit event of Kendo UI Grid
        /// </summary>
        /// <param name="request"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage EditOrders([ModelBinder(typeof(WebApiDataSourceRequestModelBinder))] DataSourceRequest request, OrderViewModel order)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            if (order != null && ModelState.IsValid)
            {
                var northwind = new NorthwindEntities();
                Order orderUpdate = northwind.Orders.First(m => m.OrderID == order.OrderID);
                orderUpdate.OrderDate = order.OrderDate;
                orderUpdate.ShipName = order.ShipName;
                orderUpdate.ShipAddress = order.ShipAddress;
                orderUpdate.ShipCity = order.ShipCity;
                northwind.SaveChanges();
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Validation Failed");
            }

            return response;
        }

        /// <summary>
        /// Get data from Northwind Database
        /// </summary>
        /// <returns></returns>
        private List<OrderViewModel> GetOrders()
        {
            var northwind = new NorthwindEntities();
            return northwind.Orders.Select(order => new OrderViewModel
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                OrderDate = order.OrderDate,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity
            }).ToList();
        }
    }
}