using FilmDukkaniMvc.Business;
using FilmDukkaniMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmDukkaniMvc.Controllers
{
    [Authorize]
    public class FilmController : Controller
    {
        //
        // GET: /Film/
        // Film Listesi
        private FilmBusiness _film = new FilmBusiness();
        private UsersBusiness _users = new UsersBusiness();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult HataBildirimi(string Bilgi)
        {
            ViewBag.mesaj = Bilgi;
            return View();
        }

        #region il - ilce İşlemleri


        [Authorize]
        public ActionResult IlListesi()
        {
            return View(_film.IlListesi());
        }
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult IlEkle()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult IlEkle(Iller model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.IlEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("IlListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "İl Ekleme İşleminde Bir Hata Oluştur" });
                }

            }
            return View(model);
        }
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult IlSil(int Id, string Name)
        {

            @ViewBag.ilAdi = Name;
            return View(new Iller() { ID = Id });
        }
        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IlSil(Iller model)
        {
            if (model != null)
            {
                bool kontrol = _film.IlSil(model);
                if (kontrol)
                {
                    return RedirectToAction("IlListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "İl Silinmek İstenirken Hata Oluştu" });
                }
            }
            return RedirectToAction("IlListesi");
        }
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult IlDuzenle(int Id)
        {
            return View(_film.IlDetails(Id));
        }
        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult IlDuzenle(Iller model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.IlDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("IlListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "İl Duzenlenirken Bir Hata Oluştu." });
                }
            }
            return View(model);
        }
        [Authorize]
        public ActionResult IlceListesi(int IlId)
        {

            ViewBag.ilAdi = _film.IlDetails(IlId).IlAdi;
            ViewBag.IlId = IlId;
            return View(_film.IlceListesi(IlId));
        }
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult IlceEkle(int IlId)
        {
            return View(new Ilceler() { IlID = IlId });
        }
        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult IlceEkle(Ilceler model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.IlceEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("IlceListesi", new { IlId = model.IlID });
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "İlce Eklenirken Hata Oluştu" });
                }
            }
            return View(model);
        }
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult IlceSil(int Id, string Name, int IlId)
        {
            ViewBag.ilceAdi = Name;
            return View(new Ilceler() { ID = Id, IlID = IlId });
        }
        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IlceSil(Ilceler model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.IlceSil(model);
                if (kontrol)
                {
                    return RedirectToAction("IlceListesi", new { IlId = model.IlID });
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "İlce Silinirken Bir Hata Oluştu." });
                }
            }
            return View(model);
        }
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult IlceDuzenle(int Id)
        {
            return View(_film.IlceDetails(Id));
        }
        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult IlceDuzenle(Ilceler model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.IlceDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("IlceListesi", new { IlId = model.IlID });
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "İlce Düzenlenirken Bir Hata Oluştur." });
                }
            }
            return View(model);
        }
        #endregion

        #region Raf İşlemleri

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult RafListesi()
        {
            return View(_film.RafListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult RafEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult RafEkle(Rafs model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.RafEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("RafListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Raf Eklem İşleminde Hata Oluştu." });
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult RafDuzenle(int Id)
        {
            return View(_film.RafDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult RafDuzenle(Rafs model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.RafDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("RafListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Raf Düzenleme İşleminde Hata Oluştu" });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult RafSil(int Id)
        {
            return View(_film.RafDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost, ActionName("RafSil")]
        public ActionResult RafSil(Rafs model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.RafSil(model);
                if (kontrol)
                {
                    return RedirectToAction("RafListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Raf Silinirken Bir Hata Oluştu" });
                }
            }
            return View(model);
        }

        #endregion

        #region Ödül İşlemleri
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OdulListesi()
        {
            return View(_film.OdulListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OdulEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult OdulEkle(Oduls model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.OdulEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("OdulListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Odul Ekleme İşleminde Bir Hata Oluştu" });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OdulDuzenle(int Id)
        {
            return View(_film.OdulDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult OdulDuzenle(Oduls model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.OdulDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("OdulListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Ödül Düzenlenirken Hata Oluştu." });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OdulSil(int Id)
        {
            return View(_film.OdulDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost, ActionName("OdulSil")]
        public ActionResult OdulSil(Oduls model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.OdulSil(model);
                if (kontrol)
                {
                    return RedirectToAction("OdulListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Ödül silinirken Bir Hata Oluştu" });
                }
            }
            return View(model);
        }
        #endregion

        #region Alt Yazı İşlemleri

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult AltYaziListesi()
        {
            return View(_film.AltyaziListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult AltYaziEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult AltYaziEkle(AltYazilar model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.AltyaziEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("AltYaziListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Alt Yazı Ekleme İşleminde Hata Oluştu" });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult AltYaziDuzenle(int Id)
        {
            return View(_film.AltyaziDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult AltYaziDuzenle(AltYazilar model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.AltyaziDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("AltYaziListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Alt Yazı Düzenleme İşleminde Hata Oluştu" });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult AltYaziSil(int Id)
        {
            return View(_film.AltyaziDetails(Id));
        }
        [HttpPost, ActionName("AltYaziSil")]

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult AltYaziSil(AltYazilar model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.AltyaziSil(model);
                if (kontrol)
                {
                    return RedirectToAction("AltYaziListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Alt Yazı Sil İşleminde Hata Oluştu" });
                }
            }
            return View(model);
        }
        #endregion

        #region Paket İşlemleri
        // Paket işlemleri.
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult PaketListesi()
        {
            return View(_film.PaketListesi());
        }
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult PaketDetails(int Id)
        {
            return View(_film.PaketDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult PaketEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult PaketEkle(Paketler model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.PaketEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("PaketListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Paket Oluşturulurken Hata Oluştu." });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult PaketDuzenle(int Id)
        {
            return View(_film.PaketDetails(Id));
        }
        [HttpPost]

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult PaketDuzenle(Paketler model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.PaketDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("PaketListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Paket Düzenlerken Hata Oluştur" });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult PaketSil(int Id)
        {
            return View(_film.PaketDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost, ActionName("PaketSil")]
        public ActionResult PaketSil(Paketler model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.PaketSil(model);
                if (kontrol)
                {
                    return RedirectToAction("PaketListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Paket Silinirken Hata Oluştu" });
                }
            }
            return View(model);
        }
        #endregion

        #region Oyuncu İşlemleri

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OyuncuListesi()
        {
            return View(_film.OyuncuListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OyuncuEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult OyuncuEkle(Oyuncular model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.OyuncuEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("OyuncuListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Oyuncu Eklerken Hata oluştu." });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OyuncuDuzenle(int Id)
        {
            return View(_film.OyuncuDetails(Id));
        }
        [HttpPost]

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OyuncuDuzenle(Oyuncular model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.OyuncuDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("OyuncuListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Oyuncu Düzenlenirken Hata Oluştu" });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult OyuncuSil(int Id)
        {
            return View(_film.OyuncuDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost, ActionName("OyuncuSil")]
        public ActionResult OyuncuSil(Oyuncular model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.OyuncuSil(model);
                if (kontrol)
                {
                    return RedirectToAction("OyuncuListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Oyuncu Silinirken Hata Oluştu" });
                }
            }
            return View(model);
        }
        #endregion

        #region Yönetmen İşlemleri

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult YonetmenListesi()
        {
            return View(_film.YonetmenListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult YonetmenEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult YonetmenEkle(Yonetmens model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.YonetmenEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("YonetmenListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Yönetmen Ekleme İşleminde Hata Oluştur." });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult YonetmenDuzenle(int Id)
        {
            return View(_film.YonetmenDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult YonetmenDuzenle(Yonetmens model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.YonetmenDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("YonetmenListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Yönetmen Düzenlenirken Hata Oluştur." });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult YonetmenSil(int Id)
        {
            return View(_film.YonetmenDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost, ActionName("YonetmenSil")]
        public ActionResult YonetmenSil(Yonetmens model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.YonetmenSil(model);
                if (kontrol)
                {
                    return RedirectToAction("YonetmenListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Yonetmek Silinirken Hata Oluştu" });
                }
            }
            return View(model);
        }

        #endregion

        #region Kategori İşlemleri
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult KategoriListesi()
        {
            return View(_film.KategoriListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult KategoriEkle(Kategories model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.KategoriEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("KategoriListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Kategori EKleme İşleminde Hata Oluştu." });
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult KategoriDuzenle(int Id)
        {
            return View(_film.KategoriDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult KategoriDuzenle(Kategories model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.KategoriDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("KategoriListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Kategori Düzenlemesinde Hata Oluştu" });
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult KategoriSil(int Id)
        {
            return View(_film.KategoriDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost, ActionName("KategoriSil")]
        public ActionResult KategoriSil(Kategories model)
        {

            bool kontrol = _film.KategoriSil(model);
            if (kontrol)
            {
                return RedirectToAction("KategoriListesi");
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Kategori Silme İşleminde Hata Oluştu" });
            }
        }

        #endregion

        #region Film İşlemleri
        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmListesi()
        {
            return View(_film.FilmListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmEkle()
        {
            ViewBag.rafList = _film.RafListesi();
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult FilmEkle(FilmModel model)
        {
            if (ModelState.IsValid)
            {
                int filmID = _film.FilmEkle(model);
                if (filmID > 0)
                {
                    return RedirectToAction("FilmDetails", new { Id = filmID });
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Film Eklenirken Bİr Hata oluştur." });
                }
            }
            ViewBag.rafList = _film.RafListesi();
            ModelState.AddModelError("Hata", "Boş Yer Bırakmayın");
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmDetails(int Id)
        {
            return View(_film.FilmDetails(Id));
            //http://localhost:26932/Film/FilmDetails/3
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmSil(int Id)
        {
            return View(_film.FilmDetails(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult FilmSil(Films model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.filmSil(model);
                if (kontrol)
                {
                    return RedirectToAction("FilmListesi");
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Film Silme İşleminde Hata Oluştu" });
                }
            }
            return RedirectToAction("HataBildirimi", new { Bilgi = "Film Silme İşleminde Hata Oluştu" });
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmDuzenle(int Id)
        {
            ViewBag.rafList = _film.RafListesi();

            var model = _film.FilmDetails(Id);
            FilmModel film = new FilmModel()
            {
                RafId = model.RafID,
                Aciklama = model.Aciklama,
                BarkodNo = model.BarkodNo,
                FilmAdi = model.FilmAdi,
                EditorSectiMi = model.EditorSectiMi,
                Fiyat = model.Fiyat,
                ImdbPuan = model.ImdbPuan,
                YeniFilmMi = model.YeniFilmMi,
                YapimTarihi = model.YapimTarihi,
                FragmanUrl = model.FragmanUrl,
                UcBoyutlumu = model.UcBoyutlumu,
                Stok = model.Stok,
                Sure = model.Sure,
                YabanciAdi = model.YabanciAdi
            };
            return View(film);
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult FilmDuzenle(FilmModel model)
        {
            if (ModelState.IsValid)
            {
                bool kontrol = _film.FilmDuzenle(model);
                if (kontrol)
                {
                    return RedirectToAction("FilmDetails", new { Id = model.ID });
                }
                else
                {
                    return RedirectToAction("HataBildirimi", new { Bilgi = "Film Düzenlenirken Hata oluştur." });
                }
            }
            ViewBag.rafList = _film.RafListesi();
            ModelState.AddModelError("Hata", "Boş Yer Bırakmayın");
            return View(model);
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult AfisEkle(int Id)
        {
            ViewBag.filmId = Id;
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult AfisEkle(HttpPostedFileBase file, string filmId)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType == "image/jpeg")
                {
                    string newName = Guid.NewGuid().ToString();
                    var fileName = Path.GetFileName(newName + file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Afis"), fileName);
                    int ID = Convert.ToInt32(filmId);
                    bool kontrol = _film.FilmAfisEkle(fileName, ID);
                    if (kontrol)
                    {
                        file.SaveAs(path);
                        return RedirectToAction("FilmDetails", new { Id = filmId });
                    }
                    else
                    {
                        ModelState.AddModelError("Hata", "Bir Hata Oluştu");
                    }
                }
                ModelState.AddModelError("Hata", "Lütfen Resim Seçin");
            }
            ModelState.AddModelError("Hata", "Bir Hata Oluştu");
            ViewBag.filmId = filmId;
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmKategoriEkle(int Id)
        {
            ViewBag.filmId = Id;
            return View(_film.KategoriListesi(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult FilmKategoriEkle(string filmId, string kategoriId)
        {
            if (kategoriId != null && kategoriId != "" && filmId != null)
            {
                int kSecim = Convert.ToInt32(kategoriId);
                int fId = Convert.ToInt32(filmId);
                bool kontrol = _film.filmKategoriEkle(fId, kSecim);
                if (kontrol)
                {
                    ModelState.AddModelError("Onay", "Ekleme İşlemi Başarı Geçti.");
                    ModelState.AddModelError("Hata", "Yenisini Eklemek İçin Tekrar Seçin");
                }
                else
                {
                    ModelState.AddModelError("Hata", "Ekleme İşleminde Hata oldu Tekrar deneyin");
                }
            }
            else
            {
                ModelState.AddModelError("Hata", "Secim Yapın");
            }

            ViewBag.filmId = filmId;
            return View(_film.KategoriListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmYonetmenEkle(int Id)
        {
            ViewBag.filmId = Id;
            return View(_film.YonetmenListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult FilmYonetmenEkle(string filmId, string yonetmenId)
        {
            if (yonetmenId != null && yonetmenId != "" && filmId != null)
            {
                int fId = Convert.ToInt32(filmId);
                int yId = Convert.ToInt32(yonetmenId);
                bool kontrol = _film.filmYonetmenEkle(fId, yId);
                if (kontrol)
                {
                    ModelState.AddModelError("Onay", "Ekleme İşlemi Başarı Geçti.");
                    ModelState.AddModelError("Hata", "Yenisini Eklemek İçin Tekrar Seçin");
                }
                else
                {
                    ModelState.AddModelError("Hata", "Ekleme İşleminde Hata oldu Tekrar deneyin");
                }
            }
            else
            {
                ModelState.AddModelError("Hata", "Secim Yapın");
            }

            ViewBag.filmId = filmId;
            return View(_film.YonetmenListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmOyuncuEkle(int Id)
        {
            ViewBag.filmId = Id;
            return View(_film.OyuncuListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult FilmOyuncuEkle(string filmId, string oyuncuId, string brol)
        {
            if (oyuncuId != null && oyuncuId != "" && filmId != null)
            {
                int fId = Convert.ToInt32(filmId);
                int yId = Convert.ToInt32(oyuncuId);
                bool rolu = brol == "Basrol" ? true : false;
                bool kontrol = _film.filmOyuncuEkle(fId, yId, rolu);
                if (kontrol)
                {
                    ModelState.AddModelError("Onay", "Ekleme İşlemi Başarı Geçti.");
                    ModelState.AddModelError("Hata", "Yenisini Eklemek İçin Tekrar Seçin");
                }
                else
                {
                    ModelState.AddModelError("Hata", "Ekleme İşleminde Hata oldu Tekrar deneyin");
                }
            }
            else
            {
                ModelState.AddModelError("Hata", "Secim Yapın");
            }
            ViewBag.filmId = filmId;
            return View(_film.OyuncuListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmOdulEkle(int Id)
        {
            ViewBag.filmId = Id;
            return View(_film.OdulListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult FilmOdulEkle(string filmId, string odulId, string odul)
        {
            if (odulId != null && odulId != "" && filmId != null)
            {
                int fId = Convert.ToInt32(filmId);
                int oId = Convert.ToInt32(odulId);
                bool aldi = odul == "Aldi" ? true : false;
                bool kontrol = _film.filmOdulEkle(fId, oId, aldi);
                if (kontrol)
                {
                    ModelState.AddModelError("Onay", "Ekleme İşlemi Başarı Geçti.");
                    ModelState.AddModelError("Hata", "Yenisini Eklemek İçin Tekrar Seçin");
                }
                else
                {
                    ModelState.AddModelError("Hata", "Ekleme İşleminde Hata oldu Tekrar deneyin");
                }
            }
            else
            {
                ModelState.AddModelError("Hata", "Secim Yapın");
            }
            ViewBag.filmId = filmId;
            return View(_film.OdulListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmAltYaziEkle(int Id)
        {
            ViewBag.filmId = Id;
            return View(_film.AltyaziListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult FilmAltYaziEkle(string filmId, string altyaziId)
        {
            if (altyaziId != null && altyaziId != "" && filmId != null)
            {
                int fId = Convert.ToInt32(filmId);
                int aId = Convert.ToInt32(altyaziId);
                bool kontrol = _film.FilmAltYaziEkle(fId, aId);
                if (kontrol)
                {
                    ModelState.AddModelError("Onay", "Ekleme İşlemi Başarı Geçti.");
                    ModelState.AddModelError("Hata", "Yenisini Eklemek İçin Tekrar Seçin");
                }
                else
                {
                    ModelState.AddModelError("Hata", "Ekleme İşleminde Hata oldu Tekrar deneyin");
                }
            }
            else
            {
                ModelState.AddModelError("Hata", "Secim Yapın");
            }
            ViewBag.filmId = filmId;
            return View(_film.AltyaziListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmKategoriSil(int Id, int kId)
        {
            bool kontrol = _film.filmKategoriSil(Id, kId);
            if (kontrol)
            {
                return RedirectToAction("FilmDetails", new { Id = Id });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Filme Bağlı Kategori Silinirken Hata Oluştur" });
            }
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmYonetmenSil(int Id, int yId)
        {
            bool kontrol = _film.filmYonetmensil(Id, yId);
            if (kontrol)
            {
                return RedirectToAction("FilmDetails", new { Id = Id });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Filme Bağlı Yönetmen Silinirken Hata Oluştur" });
            }
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmOyuncuSil(int Id, int oId)
        {
            bool kontrol = _film.filmOyuncuSil(Id, oId);
            if (kontrol)
            {
                return RedirectToAction("FilmDetails", new { Id = Id });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Filme Bağlı Oyuncu Silinirken Hata Oluştur" });
            }
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmOdulSil(int Id, int oId)
        {
            bool kontrol = _film.filmOdulSil(Id, oId);
            if (kontrol)
            {
                return RedirectToAction("FilmDetails", new { Id = Id });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Filme Bağlı Odul Silinirken Hata Oluştur" });
            }
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmAltYaziSil(int Id, int aId)
        {
            bool kontrol = _film.filmAltYaziSil(Id, aId);
            if (kontrol)
            {
                return RedirectToAction("FilmDetails", new { Id = Id });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Filme Bağlı Alt Yazı Silinirken Hata Oluştur" });
            }
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmYorumlari(int Id)
        {
            ViewBag.filmId = Id;
            return View(_film.FilmYorumlari(Id));
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmYorumlariSil(int Id, int yId)
        {

            bool kontrol = _film.FilmYorumlariSil(yId);
            if (kontrol)
            {
                return RedirectToAction("FilmYorumlari", new { Id = Id });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Silme İşleminde Bir Hata Oluştu" });
            }
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult FilmYorumlariAktifYap(int Id, int yId)
        {
            bool kontrol = _film.FilmYorumlariAktifYap(yId);
            if (kontrol)
            {
                return RedirectToAction("FilmYorumlari", new { Id = Id });
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Silme İşleminde Bir Hata Oluştu" });
            }
        }
        #endregion

        #region Bozuk Filmler

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult BozukFilmListesi()
        {
            return View(_film.BozukFilmListesi());
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult BozukFilmSil(int Id)
        {
            bool kontrol = _film.BozukFilmSil(Id);
            if (kontrol)
            {
                return RedirectToAction("BozukFilmListesi");
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Bozuk Film Silinirken Hata Oluştu" });
            }
        }

        [Authorize(Roles = "Admin,Personel")]
        public ActionResult BozukFilmEkle()
        {
            ViewBag.filmList = _film.FilmListesi();
            ViewBag.musteriList = _film.MusteriListesi();
            return View();
        }

        [Authorize(Roles = "Admin,Personel")]
        [HttpPost]
        public ActionResult BozukFilmEkle(BozukFilms model)
        {
            if (ModelState.IsValid)
            {
                model.Tarih = DateTime.Now;
                bool kontrol = _film.BozukFilmEkle(model);
                if (kontrol)
                {
                    return RedirectToAction("BozukFilmListesi");
                }
                else
                {
                    ModelState.AddModelError("Hata", "Hata Oluştu Boş Yer Bırakmayın");
                }
            }
            ModelState.AddModelError("Hata", "Hata Oluştu Boş Yer Bırakmayın");
            ViewBag.filmList = _film.FilmListesi();
            ViewBag.musteriList = _film.MusteriListesi();
            return View(model);
        }
        #endregion


        [Authorize(Roles = "Personel,Admin")]
        public ActionResult SiparisleriHazirla()
        {
            List<MusteriFilmSiparisListesi> siparisListesi = _film.FilmsSiparisleriHazırla();

            siparisListesi.ToList().ForEach(x =>
            {
                if (_users.MusteriFilmListesi(x.MusteriID, true).Count >= x.PaketAdet)
                {
                    x.GondermeDurumu = false;
                }
                else
                {
                    x.GondermeDurumu = true;
                }
            });

            return View(siparisListesi);
        }
        [Authorize(Roles = "Personel,Admin")]
        public ActionResult SiparisCikar(int Id)
        {
            bool kontrol = _film.FilmSiparisCikar(Id);
            if (kontrol)
            {
                return RedirectToAction("SiparisleriHazirla", "Film");
            }
            else
            {
                return RedirectToAction("HataBildirimi", new { Bilgi = "Siparis Çıkarılamadı. " });
            }


        }
    }
}
