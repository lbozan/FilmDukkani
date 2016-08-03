using FilmDukkaniMvc.Business;
using FilmDukkaniMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FilmDukkaniMvc.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private FilmBusiness _film = new FilmBusiness();
        private UsersBusiness _users = new UsersBusiness();
        public ActionResult Index(int? Id, bool? yeni)
        {
            if (HttpContext.User.IsInRole("Personel"))
                return RedirectToAction("Index", "Film");
            //Id demek KategoriId demektir.
            ViewBag.kategoriListesi = _film.KategoriListesi();
            if (Id != null)
            {
                List<Films> filmListesi = new List<Films>();
                _film.FilmListesi().ToList().ForEach(x =>
                {
                    x.Kategories.Where(s => s.ID == Id).ToList().ForEach(s =>
                    {
                        filmListesi.Add(new Films() { ID = x.ID, FilmAdi = x.FilmAdi, ImdbPuan = x.ImdbPuan, Aciklama = x.Aciklama, AfisPath = x.AfisPath });
                    });
                });
                ViewBag.filmListesi = filmListesi.Where(z => yeni == null || z.YeniFilmMi == true).ToList();

            }
            else
            {
                ViewBag.filmListesi = _film.FilmListesi().Where(z => yeni == null || z.YeniFilmMi == true).ToList(); ;
            }


            return View();
        }
        public ActionResult Giris()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();


        }
        [HttpPost]
        public ActionResult Giris(FormCollection form, string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            string[] temizle = new string[] { " ", "'", "-", "=", ",", "." };
            string username = form["username"];
            string password = form["password"];

            foreach (string item in temizle)
            {
                username = username.Replace(item, "");
                password = password.Replace(item, "");
            }

            if (username.Trim() != string.Empty && password.Trim() != string.Empty)
            {
                // password = Crypto.SHA256(password);
                if (Membership.ValidateUser(username, password))
                {

                    FormsAuthentication.SetAuthCookie(username, true, returnUrl);
                    var a = HttpContext.User.Identity.Name;
                    //_users.SongirisTarihi((int)Membership.GetUser(username).ProviderUserKey);
                 

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    var a = HttpContext.User.Identity.Name;
                    FormsAuthentication.SignOut();
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public JsonResult IlceListesi(int Id)
        {
            //json için bulunmaktadır.
            var ilce = _film.IlceListesi(Id).Select(x => new { ID = x.ID, IlceAdi = x.IlceAdi });
            return Json(new SelectList(ilce.ToArray(), "ID", "IlceAdi"), JsonRequestBehavior.AllowGet);
        }
        public ActionResult MusteriEkle()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            ViewBag.paketler = _film.PaketListesi();
            ViewBag.sehir = _film.IlListesi();
            ViewBag.sozlesme = "Bu Sözleşmeyi Kabul Ediyorum";
            return View();
        }
        public ActionResult SifreHatirla()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreHatirla(FormCollection data)
        {
            //Todo: Eposta yeni şifre gönderme.
            ModelState.AddModelError("eposta", "Posta Gönderme Action'ı Aktif değil.");
            return View();
        }
        [HttpPost]
        public ActionResult MusteriEkle(MusteriModel model, FormCollection data)
        {
            if (data["sozlesme"] == "evet")
            {
                if (ModelState.IsValid)
                {
                    string ilce = data["ilceId"].ToString();
                    string paket = data["paketId"].ToString();
                    if (paket != null && paket != "")
                    {
                        if (!string.IsNullOrWhiteSpace(ilce))
                        {
                            model.IlceID = Convert.ToInt32(ilce);
                            model.PaketID = Convert.ToInt32(paket);
                            int userKontrol = _users.MusteriEkle(model);
                            if (userKontrol == 1)
                            {
                                // Kredi Kart Kontrolü Yapılması Lazım.

                                return RedirectToAction("Giris");
                                // Login olmalı

                            }
                            else if (userKontrol == -2)
                            {
                                ModelState.AddModelError("UserName", model.UserName + "Kullanıcı Zaten Var");
                            }
                            else if (userKontrol == -3)
                            {
                                ModelState.AddModelError("Eposta", model.UserName + model.Eposta + "E-posta Zaten Var");
                            }
                            else if (userKontrol == -4)
                            {
                                ModelState.AddModelError("KrediKartNo", model.KrediKartNo + "Kredi Kart Var");
                            }
                            else if (userKontrol == -5)
                            {
                                ModelState.AddModelError("TCNo", model.TCNo + "Tc No Var");
                            }
                            else
                            {
                                ModelState.AddModelError("Hata", "Kayıt Oluşturulamadı");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("paket", "Bir Paket Seçin.");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("sozlesme", "Sözleşmeyi Kabul Edin.");
            }

            ModelState.AddModelError("Ilce", "İlçe Seçin");
            ViewBag.sehir = _film.IlListesi();
            return View(model);
        }

        public ActionResult FilmDetails(int Id)
        {
            ViewBag.userId = 0;
            if (User.Identity.Name != "")
                ViewBag.userId = _users.MusteriGetUsername(User.Identity.Name);
            ViewBag.kategoriListesi = _film.KategoriListesi();
            return View(_film.FilmDetails(Id));
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            ViewBag.kategoriListesi = _film.KategoriListesi();
            return View();
        }

    }
}
