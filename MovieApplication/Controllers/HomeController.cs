using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieApplication.Models;
using PagedList;
namespace MovieApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            DbmovieEntities db = new DbmovieEntities();
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Movies1.ToList();
            IPagedList<Movies> stu = list.ToPagedList(pageindex, pagesize);


            return View(stu);

            return View();
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
    }
}