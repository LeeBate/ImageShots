﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Images.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            return View(db.Pictures.ToList());
        }


        [HttpPost]
        public ViewResult search(string searchString)
        {
            /*   ViewBag.BrandSortParm = String.IsNullOrEmpty(sortOrder) ? "Brand_desc" : "";/*/

               var pic = from p in db.Pictures
                           select p;

               if (!String.IsNullOrEmpty(searchString))
               {

                   pic = pic.Where(p =>  p.Name.ToUpper().Contains(searchString.ToUpper()));
               }

             /*  switch (sortOrder)
               {
                   case "Brand_desc":
                       phone = phone.OrderByDescending(p => p.brand);
                       break;
                   default:
                       phone = phone.OrderBy(p => p.product_id);
                       break;
               }*/
            return View(pic);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }


        public ActionResult AddOrder(int? id, float? total)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }

            Entities1 db1 = new Entities1();
            //var order = db1.OrderPics;

            

            
            OrderPic order = new OrderPic();
            order.User_email = User.Identity.Name;
            order.Pic_ID = id;
            order.total = total;
            db1.OrderPics.Add(order);
            db1.SaveChanges();

            
            return RedirectToAction("Index");
            //return View(picture);
        }
    }
}