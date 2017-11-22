using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using changelog.Models;
using changelog.dal;
 
namespace changelog.Controllers
{
    public class ChangeLogController : Controller
    {
        //
        // GET: /ChangeLog/

        public ActionResult index()
        {
            ChangeLogModel model = new ChangeLogModel { 
                _db_author = new db_ChangeLog().getProjectsAuthorView()
            };
            return View(model);
        }

        public ActionResult project(int id)
        {
            string sTitleProject = "";
            switch (id)
            {
                case 1: sTitleProject = "Журнал проекта AVIAV3"; break;
                case 2: sTitleProject = "Журнал проекта #2"; break;
                case 3: sTitleProject = "Журнал проекта #3"; break;
                default: sTitleProject = "Проект не известен"; break;
            }
            ViewBag.TitleProject = sTitleProject;

            //ChangeLogModel model = new ChangeLogModel
            //{
            //    _db_projects = new db_ChangeLog().getProjectsView(id)
            //};

            return View();
        }

        public ActionResult GetData(int pageIndex, int pageSize)
        {  
            return Json(new db_ChangeLog().getProjectsView(Convert.ToInt32(Request.QueryString["id"]), pageIndex, pageSize), JsonRequestBehavior.AllowGet);
        }
    }
}
