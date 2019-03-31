using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TelefonRehberi.Models.Class;
using TelefonRehberi.Models.EntityFramework;

namespace TelefonRehberi.Controllers
{
    public class AdminUIController : Controller
    {
        RehberDBEntities db = new RehberDBEntities();
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "AdminUI");
            }
            viewModel vm = new viewModel();
            vm.DepartmanInfo = db.Departman.ToList();
            vm.PersonelInfo = db.Personel.ToList();

            return View(vm);
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanıcı kullanici)
        {
            var admin = db.Kullanıcı.FirstOrDefault(k => k.kAdi == kullanici.kAdi && k.kSifre == kullanici.kSifre);
            if (admin !=null)
            {
                FormsAuthentication.SetAuthCookie(admin.kAdi, false);
                Session["Admin"] = admin;

                return RedirectToAction("Index", "AdminUI");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya parola.";
                ViewBag.MesajCss = "alert alert-danger";

                return View();
            }
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Admin"] = null;

            return RedirectToAction("Login","AdminUI");
        }
        public ActionResult PasswordEdit()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "AdminUI");
            }

            var session = Session["Admin"] as Kullanıcı;
            var admin = db.Kullanıcı.FirstOrDefault(a => a.kId == session.kId);
            var change = new Kullanıcı();
            change.kId = admin.kId;
            change.kAdi = admin.kAdi;
            change.kSifre = admin.kSifre;

            return View(change);
        }
        [HttpPost]
        public ActionResult PasswordEdit(Kullanıcı kullanici)
        {
            var session = Session["Admin"] as Kullanıcı;
            var admin = db.Kullanıcı.FirstOrDefault(a => a.kId == session.kId);
            admin.kSifre = kullanici.kSifre;
            db.SaveChanges();

            return RedirectToAction("Index","AdminUI");
        }
        public ActionResult PersonelAdd()
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "AdminUI");
            }

            ViewBag.yonetici = new SelectList(db.Personel, "pAd", "Description");
            ViewBag.dprFk = new SelectList(db.Departman, "dprid", "dprAd");

            return View("PersonelAdd", new PersonelModel());
        }

        [HttpPost]
        public ActionResult PersonelAdd(PersonelModel pModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.yonetici = new SelectList(db.Personel, "pAd", "Description");
                ViewBag.dprFk = new SelectList(db.Departman, "dprid", "dprAd");
                return View("PersonelAdd");
            }

            var personel = new Personel();
                personel.pAd = pModel.pAd;
                personel.pSoyad = pModel.pSoyad;
                personel.pTelNo = pModel.pTelNo;
                personel.yonetici = pModel.yonetici;
                personel.dprFk = pModel.dprFk;
                ViewBag.yonetici = pModel.Description;
                ViewBag.dprFk = new SelectList(db.Departman, "dprid", "dprAd", pModel.dprFk);
                db.Personel.Add(personel);
                db.SaveChanges();
                return RedirectToAction("Index", "AdminUI");
        }
        public ActionResult PersonelEdit(int id)
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "AdminUI");
            }

            var personel = db.Personel.FirstOrDefault(p=>p.pid == id);
            var change = new PersonelModel();
            change.pAd = personel.pAd;
            change.pSoyad = personel.pSoyad;
            change.pTelNo = personel.pTelNo;
            ViewBag.yonetici = new SelectList(db.Personel, "pAd", "Description");
            ViewBag.dprFk = new SelectList(db.Departman, "dprid", "dprAd",personel.dprFk);

            return View("PersonelEdit",change);
        }
        [HttpPost]
        public ActionResult PersonelEdit(int id,PersonelModel pModel)
        {

            var personel = db.Personel.FirstOrDefault(p => p.pid == id);
            if (!ModelState.IsValid)
            {
                ViewBag.yonetici = new SelectList(db.Personel, "pAd", "Description");
                ViewBag.dprFk = new SelectList(db.Departman, "dprid", "dprAd");
                return View("PersonelEdit");
            }

            personel.pAd = pModel.pAd;
            personel.pSoyad = pModel.pSoyad;
            personel.pTelNo = pModel.pTelNo;
            personel.yonetici = pModel.yonetici;
            personel.dprFk = pModel.dprFk;
            db.SaveChanges();

            return RedirectToAction("Index","AdminUI");
        }

        public ActionResult PersonelDelete(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "AdminUI");
            }

            var personel = db.Personel.FirstOrDefault(p => p.pid == id);
               
                var personelTest = db.Personel.FirstOrDefault(x=>x.yonetici==personel.pAd);

                if (personelTest == null)
                {
              
                db.Personel.Remove(personel);
                db.SaveChanges();
            }
               else
                 {
                //  Test for wrong data
                return Json(new { error = "test", data = "test", time = "test"});
                
                  }

                return RedirectToAction("Index", "AdminUI");
               
           
            
          
           
        }
        public ActionResult DepartmanAdd()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "AdminUI");
            }

            return View("DepartmanAdd", new DepartmanModel());
        }
        [HttpPost]
        public ActionResult DepartmanAdd(DepartmanModel dModel)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanAdd");
            }
            var dpr = new Departman();
            dpr.dprid = dModel.dprid;
            dpr.dprAd = dModel.dprAd;
            db.Departman.Add(dpr);
            db.SaveChanges();

            return RedirectToAction("Index", "AdminUI");
        }

        public ActionResult DepartmanEdit(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "AdminUI");
            }

            var dpr = db.Departman.FirstOrDefault(d => d.dprid == id);
            var change = new DepartmanModel();  
            change.dprAd = dpr.dprAd;

            return View("DepartmanEdit", change);
        }
        [HttpPost]
        public ActionResult DepartmanEdit(int id,DepartmanModel dModel)
        {
            var dpr = db.Departman.FirstOrDefault(d => d.dprid == id);

            if (!ModelState.IsValid)
            {
                return View("DepartmanEdit");
            }

            dpr.dprAd = dModel.dprAd;
            db.SaveChanges();

            return RedirectToAction("Index","AdminUI");
        }
        
        public  ActionResult DepartmanDelete(int id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "AdminUI");
            }

          
            var dpr = db.Departman.FirstOrDefault(d => d.dprid == id);
            var control = db.Personel.FirstOrDefault(c=>c.dprFk==id);
            
            if (control!=null)
            {
                /*Test Process for wrong Data */
                return Json(new { error = "test", data = "test" });
            }
            else
            {
                db.Departman.Remove(dpr);
                db.SaveChanges();
            }
                
            return RedirectToAction("Index","AdminUI");
        }
            
        

    }
}