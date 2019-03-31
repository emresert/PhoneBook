using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi.Models.Class;
using TelefonRehberi.Models.EntityFramework;

namespace TelefonRehberi.Controllers
{
    public class PublicUIController : Controller
    {
        RehberDBEntities db = new RehberDBEntities();

        public ActionResult Index()
        {
            Session["Admin"] = null;
            var model = db.Personel.ToList();
            return View(model);
        }

        public ActionResult Detail(int id)
        {

            var model = db.Personel.FirstOrDefault(x => x.pid == id);
            if (model.dprFk != null)
            {
                var dpr = db.Departman.Single(d => d.dprid == model.dprFk);
                ViewBag.Departman = dpr.dprAd;
            }
            else
            {
                ViewBag.Departman = "*Bu personelin herhangi bir departman kaydı bulunmuyor.";
            }
            if (model.yonetici == null)
            {
                ViewBag.Yonetici = " Herhangi bir yönetici atanmamış ";
            }
            var person = new PersonelModel();
            person.pid = model.pid;
            person.pAd = model.pAd;
            person.pSoyad = model.pSoyad;
            person.pTelNo = model.pTelNo;
            person.yonetici = model.yonetici;
            
            return View(person);

        }       
    }
}