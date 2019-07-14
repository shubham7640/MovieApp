using MovieApplication.Models;
using MovieApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Net;

namespace MovieApplication.Controllers
{
    public class AdminController : Controller
    {
        DbmovieEntities db = new DbmovieEntities();
        // GET: Admin

        [HttpGet]
        public ActionResult login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult login(Admin avm)
        {
            Admin ad = db.Admins.Where(x => x.Username == avm.Username && x.Password == avm.Password).SingleOrDefault();
            if (ad != null)
            {

                Session["AdminId"] = ad.AdminId.ToString();
                return RedirectToAction("ListMovie");

            }
            else
            {
                ViewBag.error = "Invalid username or password";

            }

            return View();
        }

        
        public ActionResult Create()
        {
            ViewBag.movies = db.Actors.ToList();
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Movies md ,HttpPostedFileBase imgfile,int [] listActors)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                
                Movies cat = new Movies();
                cat.MovName = md.MovName;
                cat.Realease = md.Realease;
                cat.Plot = md.Plot;
                cat.Poster = path;
                db.Movies1.Add(cat);
               

                foreach (var act in listActors)
                {
                    MoviesActor mv = new MoviesActor();
                    mv.MovieId = md.MoviesId;
                    mv.CastId = act;
                    db.MoviesActors.Add(mv);
                    
                }
               
                db.SaveChanges();
                return RedirectToAction("ListMovie");
            }
            
            return View();
        }
        public ActionResult ListMovie(int? page)
        {
            //int pagesize = 9, pageindex = 1;
            //pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            ////  var list = db.Movies1.Join(db.Actors,a=>a.MoviesId=) .OrderByDescending(x=>x.MoviesId).Select(x=>new { });
            //ShowData sd = new ShowData();
            var list = (from mv in db.Movies1
                       join ac in db.MoviesActors
                       on
                       mv.MoviesId equals ac.MovieId
                       join
                       act in db.Actors
                       on
                       ac.CastId equals act.Id
                       select new ShowData
                       {
                           MovName=mv.MovName,Plot=mv.Plot,Poster=mv.Poster,Realease=mv.Realease,ActName=act.ActName
                       }
                       ).ToList();
            //ViewBag.list = list;
                  
           // IPagedList<Movies> stu = list.ToPagedList(pageindex, pagesize);
           //foreach(var p in list)
            
            return View(list);

            //return View();
        }

        public ActionResult AddActor()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddActor(Actor md)
        {
            Actor cat = new Actor();
            cat.ActName = md.ActName;
            cat.ActSex = md.ActSex;
            cat.ActDOB = md.ActDOB;
            cat.ActBio = md.ActBio;
            db.Actors.Add(cat);
            db.SaveChanges();
            return RedirectToAction("ListMovie");
        }

        public ActionResult AddButtonActor()
        {
            ViewBag.movies = db.Actors.ToList();
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddButtonActor(Actor md)
        {
            ViewBag.movies = db.Actors.ToList();

            Actor cat = new Actor();
            cat.ActName = md.ActName;
            cat.ActSex = md.ActSex;
            cat.ActDOB = md.ActDOB;
            cat.ActBio = md.ActBio;
            db.Actors.Add(cat);
            db.SaveChanges();
            return View("Create");
            //RedirectToAction("Create");
        }
        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "/Content/upload/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }

            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }



            return path;
        }
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("login");
            }
            var mov = db.Movies1.Find(id);

            return View(mov);
        }
        public ActionResult EditActor(int? id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("login");
            }
            var mov = db.Movies1.Find(id);
            return View(mov);
        }
        public ActionResult Delete()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }
    }
}