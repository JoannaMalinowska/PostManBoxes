using PostManBoxes.Data;
using PostManBoxes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PostManBoxes.Controllers
{
    public class HomeController : Controller
    {

        private postman_pack_dbEntities dbContext = new postman_pack_dbEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult GetBoxesList()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            PropertyInfo propertyInfo = typeof(Boxes).GetProperty(sortColumn);
            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            // Getting all Customer data    
            List<Boxes> boxesData = dbContext.boxes.Select(x => new Boxes()
            {
                Id = x.id,
                CustomerKey = x.customer_key,
                PackNumber = x.pack_number,
                Dimensions = x.dimensions,
                Weight = x.weight,
                ToPay = x.to_pay,
                DestinationId = x.destination_id,
                CurrentCheckPoint = x.current_check_point,
                DeliveryStatus = x.delivery_status
            }).ToList();

            

            //Sorting    
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                if (sortColumnDir == "asc")
                {
                    boxesData = boxesData.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                }
                else
                {
                    boxesData = boxesData.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                }
            }
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                try
                {
                    boxesData = boxesData.Where(m => m.PackNumber.Contains(searchValue)).ToList();
                }
                catch { }
            }

            //total number of rows count     
            recordsTotal = boxesData.Count();
            //Paging     
            var data = boxesData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

        }
    }
}
