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
    [Authorize]
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        public ActionResult Index()
        {
            if (User.IsInRole("Personel"))
                return RedirectToAction("Index", "Film");

            return RedirectToAction("Index", "Home");
        }
        public ActionResult HataBildirimi(string Bilgi)
        {

            ViewBag.mesaj = Bilgi + " Users Kontroller";
            return View();
        }


        private UsersBusiness _users = new UsersBusiness();
        private FilmBusiness _film = new FilmBusiness();


        #region Personel İşlemleri

        [Authorize(Roles = "Admin")]
        public ActionResult PersonelListesi()
        {
            return View(_users.PersonelListesi());
        }
     

        [Authorize(Roles = "Admin")]
        public ActionResult PersonelEkle()
        {
            ViewBag.sehir = _film.IlListesi();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult PersonelEkle(PersonelModel model, FormCollection data)
        {
            if (ModelState.IsValid)
            {
                string ilce = data["Ilce"].ToString();
                if (!string.IsNullOrWhiteSpace(ilce))
                {
                    string cinsiyet = data["cinsiyet"].ToString();
                    int kontrol = _users.PersonelEkle(model, ilce, cinsiyet);
                    if (kontrol == 1)
                    {
                        return RedirectToAction("PersonelListesi");
                    }
                    else if (kontrol == -1)
                    {
                        ModelState.AddModelError("Hata", "Aynı Kullanıcı Adında var zaten.");
                    }
                    else
                    {
                        return RedirectToAction("HataBildirimi", new { Bilgi = "Personel Eklenirken hata oluştu." });
                    }
                }
                else
                {
                    ModelState.AddModelError("Ilce", "Lütfen İlce Secin");
                }

            }
            ModelState.AddModelError("Hata", "Boş Bırakmayın");
            ViewBag.sehir = _film.IlListesi();
            return View(model);
        }

        public ActionResult PersonelDetails(int Id, string username = "")
        {
            if (username != null)
                Id = _users.PersonelGetUsername(username);

            PersonelModel model = _users.PersonelDetails(Id);
            ViewBag.ilce = _film.IlceDetails(model.Ilce).IlceAdi;
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PersonelSil(int Id)
        {
            return View(_users.PersonelDetails(Id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("PersonelSil")]
        public ActionResult PersonelSil(PersonelModel model)
        {
            if (model != null)
            {
                bool kontrol = _users.PersonelSil(model);
                if (kontrol)
                {
                    return RedirectToAction("PersonelListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Silme İşleminde Hata Oldu" });
                }
            }
            return RedirectToAction("HataBildirimi", new { Bilgi = "Bir Sorun Oluştu." });
        }

        public ActionResult PersonelDuzenle(int Id)
        {
            PersonelModel model = _users.PersonelDetails(Id);
            ViewBag.sehir = _film.IlListesi();
            ViewBag.sehirId = _film.IlceDetails(model.Ilce).IlID;
            ViewBag.ilceId = _film.IlceDetails(model.Ilce).ID;
            return View(model);
        }
        [HttpPost]
        public ActionResult PersonelDuzenle(PersonelModel model, FormCollection data)
        {
            if (ModelState.IsValid)
            {
                string ilce = data["Ilce"].ToString();
                string cinsiyet = data["cinsiyet"].ToString();
                model.Ilce = Convert.ToInt32(ilce);
                model.Cinsiyet = cinsiyet;
                int kontrol = _users.PersonelDuzenle(model);
                if (kontrol == 1)
                {
                    return RedirectToAction("PersonelDetails", new { Id = model.ID });
                }
                else if (kontrol == -1)
                {
                    ModelState.AddModelError("Hata", "Belirlediğiniz Kullanıcı Adı Başkasına Ait.");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Duzenlemede hata oluştu" });
                }
            }
            ModelState.AddModelError("Hata", "Boş Bırakmayın");
            ViewBag.sehir = _film.IlListesi();
            ViewBag.sehirId = _film.IlceDetails(model.Ilce).IlID;
            ViewBag.ilceId = _film.IlceDetails(model.Ilce).ID;
            return View(model);
        }
        #endregion

        #region Müşteri İşlemleri
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult MusteriListesi()
        {
            return View(_users.MusteriListesi());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult MusteriAktif(int Id, bool durum)
        {
            bool kontrol = _users.MusteriAktif(Id, durum);
            if (kontrol)
            {
                return RedirectToAction("MusteriDetails", new { Id = Id });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Müşteri Aktif veya DeAktif yapılırken hata oluştu." });
            }
        }

        [Authorize]
        public ActionResult MusteriFilmListesi(int Id, bool durum = true)
        {
            // durum true=>Gönderilmiş / false=>Listede
            ViewBag.Baslik = durum;
            ViewBag.Id = Id;
            return View(_users.MusteriFilmListesi(Id, durum));
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult MusteriFilmDurum(int Id, int fId, bool durum)
        {
            // durum true=>Alındı / false=>Gönderildi
            _users.MusteriFilmDurum(fId, durum);
            return RedirectToAction("MusteriDetails", new { Id = Id });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult MusteriSil(int Id)
        {
            return View(_users.MusteriDetails(Id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("MusteriSil")]
        public ActionResult MusteriSil(Musteriler model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _users.MusteriSil(model);
                if (kontrol)
                {
                    return RedirectToAction("MusteriListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Müşteri Silinirken Hata Oluştu." });
                }
            }
            return View();
        }

        public ActionResult MusteriDuzenle(int Id)
        {
            Musteriler model = _users.MusteriDetails(Id);
            ViewBag.sehir = _film.IlListesi();
            ViewBag.sehirId = _film.IlceDetails(model.DigerBilgiler.IlceID).IlID;
            ViewBag.ilceId = _film.IlceDetails(model.DigerBilgiler.IlceID).ID;
            return View(model);
        }
        [HttpPost]
        public ActionResult MusteriDuzenle(Musteriler model, FormCollection data)
        {
            if (ModelState.IsValid)
            {
                string ilce = data["Ilce"].ToString();

                string cinsiyet = data["cinsiyet"].ToString();
                model.DigerBilgiler.Cinsiyet = cinsiyet;
                model.DigerBilgiler.IlceID = Convert.ToInt32(ilce);
                bool kontrol = _users.MusteriDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("MusteriDetails", new { Id = model.ID });
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Düzenlemede Hata Oluştu" });
                }
            }
            ViewBag.sehir = _film.IlListesi();
            ViewBag.sehirId = _film.IlceDetails(model.DigerBilgiler.IlceID).IlID;
            ViewBag.ilceId = _film.IlceDetails(model.DigerBilgiler.IlceID).ID;
            return View(model);
        }
        public ActionResult MusteriDetails(int Id, string username = null)
        {
            if (username != null)
                Id = _users.MusteriGetUsername(username);

            return View(_users.MusteriDetails(Id));
        }


        #endregion

        [Authorize]
        [ValidateInput(false)]
        public ActionResult FilmYorumEkle(FormCollection data)
        {


            //Todo : View Kısmında Yorum Yapanın Id'sini belirlemen lazım.Login İşlemi yapıldıktan sonra yorum yapıla bileçek
            // Gelen Yorumun Güvenlikten Geçir.
            int musteriId = (int)Membership.GetUser(HttpContext.User.Identity.Name).ProviderUserKey;
            MusteriFilmYorums m = new MusteriFilmYorums();
            m.MusteriID = musteriId;
            m.FilmID = Convert.ToInt32(data["filmId"].ToString());
            m.Yorum = data["yorum"].ToString();
            if (m.Yorum != null)
            {
                _users.MusteriFilmYorumEkle(m);
            }
            return RedirectToAction("FilmDetails", "Home", new { Id = m.FilmID });
        }
        [Authorize]
        public ActionResult FilmBegen(int Id, int fId, bool durum)
        {
            _users.MusteriFilmBegen(Id, fId, durum);
            return RedirectToAction("FilmDetails", "Home", new { Id = fId });
        }
        [Authorize]
        public ActionResult MusteriFilmEkle(string user, int fId)
        {
            int userId = _users.MusteriGetUsername(user);

            if (!_users.MusteriFilmKontrol(userId, fId))
                return RedirectToAction("HataBildirimi", new { Bilgi = "Zaten Listenizde Bulunuyor." });

            MusteriFilmListesi film = new MusteriFilmListesi();
            film.Aktifmi = true;
            film.Alindimi = false;
            film.FilmlerID = fId;
            film.GonderildiMi = false;
            film.MusteriID = userId;
            film.Tarih = DateTime.Today;
            bool kontrol = _users.MusteriFilmEkle(film);
            if (kontrol)
            {
                return RedirectToAction("FilmListem", "Users", new { user = User.Identity.Name });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Listene Film Eklerken Hata Oluştu." });
            }
        }

        [Authorize]
        public ActionResult FilmListem(string user)
        {
            bool durum = false;
            int Id = _users.MusteriGetUsername(user);

            return View(_users.MusteriFilmListesi(Id, durum));

        }
        [Authorize]
        public ActionResult FilmListemSil(int Id)
        {
            bool kontrol = _users.MusteriFilmListeSil(Id);
            if (!kontrol)
                return RedirectToAction("HataBildirimi", new { Bilgi = "Listeden Film Silmede Hata Oluştu" });
            return RedirectToAction("FilmListem", new { user = User.Identity.Name });
        }
        [Authorize]
        public ActionResult FilmListemSira(int Id, int fId, bool durum)
        {
            //Todo: yeniden sıralama işlemine - FilmListem actionname bak
            _users.MusteriFilmListeSira(Id, fId, durum);
            return RedirectToAction("FilmListem", new { user = User.Identity.Name });
        }
        [Authorize]
        public ActionResult FilmSiparisGunleri()
        {
            int Id = _users.MusteriGetUsername(User.Identity.Name);
            return View(_users.MusteriSiparisGunleri(Id));
        }
        [Authorize]
        public ActionResult FilmSiparisGunEkle()
        {
            int Id = _users.MusteriGetUsername(User.Identity.Name);
            if (!_users.MusteriSiparisGunleriSinir(Id))
                return RedirectToAction("HataBildirimi", new { Bilgi = "Bu Ayki Sipariş İstekleriniz Paketinizi Aşamaz. " });

            return View(new SiparisTarihleri() { MusteriID = Id });
        }

        [Authorize]
        [HttpPost]
        public ActionResult FilmSiparisGunEkle(SiparisTarihleri model)
        {
            if (ModelState.IsValid)
            {
                DateTime data = DateTime.Today.AddDays(1);
                if (model.SiparisTarih > data)
                {
                    string pazar = _users.GunAdi(model.SiparisTarih.DayOfWeek);
                    if (!pazar.Equals("Pazar"))
                    {
                        if (_users.MusteriSiparisGunleriKontrol(model.SiparisTarih))
                        {
                            _users.MusteriSiparisGunleriEkle(model);
                        }
                        else
                        {
                            ModelState.AddModelError("SiparisTarih", "Zaten Belirlediğiniz Tarihe Ait Gun var. !");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("SiparisTarih", "Seçtiğiniz Gün Pazar Gününe Denk Geliyor. !");
                    }
                }
                else
                {
                    ModelState.AddModelError("SiparisTarih", "En Az 2 Gün Sonrası İçin Siparişinizi Belirleyin.");
                }
            }
            return View(model);
        }
        [Authorize]
        public ActionResult FilmSiparisGunSil(int Id)
        {
            _users.MusteriSiparisGunSil(Id);
            return RedirectToAction("FilmSiparisGunleri");
        }

        // Fatura Kes (Yani Bugün Kimin Üçret Kesimi gelmişse kesilecek )
        [Authorize(Roles = "Admin")]
        public ActionResult FaturalariKes()
        {
            return View(_film.FaturaKesimleri());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult FaturaUcretleriAl()
        {

            _film.FaturaKesimleriYap();
            return RedirectToAction("FaturalariKes");
        }
    }
}
