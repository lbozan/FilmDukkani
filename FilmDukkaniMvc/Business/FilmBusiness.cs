using FilmDukkaniMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDukkaniMvc.Business
{
    public class FilmBusiness : IDisposable
    {
        private FilmDukkaniDBEntities _db;

        public FilmBusiness()
        {
            _db = new FilmDukkaniDBEntities();
        }

        internal bool IlEkle(Iller model)
        {
            try
            {
                model.IlAdi = model.IlAdi.Trim();
                var il = _db.Iller.SingleOrDefault(x => x.IlAdi == model.IlAdi);
                if (il != null)
                    return true;
                _db.Iller.Add(new Iller() { IlAdi = model.IlAdi, Aktifmi = true });
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal List<Iller> IlListesi()
        {
            return _db.Iller.ToList();
        }
        internal bool IlSil(Iller model)
        {
            try
            {
                var il = _db.Iller.First(x => x.ID == model.ID);
                _db.Iller.Remove(il);
                var ilce = _db.Ilceler.Where(x => x.IlID == model.ID).ToList();
                _db.Ilceler.RemoveRange(ilce);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal Iller IlDetails(int Id)
        {
            return _db.Iller.First(x => x.ID == Id);
        }
        internal bool IlDuzenle(Iller model)
        {
            try
            {
                var duzenle = _db.Iller.First(x => x.ID == model.ID);
                duzenle.IlAdi = model.IlAdi.Trim();
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal List<Ilceler> IlceListesi(int Id)
        {
            return _db.Ilceler.Where(x => x.IlID == Id).ToList();
        }
        internal bool IlceEkle(Ilceler model)
        {
            try
            {
                model.IlceAdi = model.IlceAdi.Trim();
                var ilce = _db.Ilceler.SingleOrDefault(x => x.IlceAdi == model.IlceAdi);
                if (ilce != null)
                    return true;
                _db.Ilceler.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool IlceSil(Ilceler model)
        {
            try
            {
                var sil = _db.Ilceler.First(x => x.ID == model.ID);
                _db.Ilceler.Remove(sil);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal Ilceler IlceDetails(int Id)
        {
            return _db.Ilceler.SingleOrDefault(x => x.ID == Id);
        }
        internal bool IlceDuzenle(Ilceler model)
        {
            try
            {
                var duzen = _db.Ilceler.First(x => x.ID == model.ID);
                duzen.IlceAdi = model.IlceAdi.Trim();
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal List<Rafs> RafListesi()
        {
            return _db.Rafs.ToList();
        }
        internal bool RafEkle(Rafs model)
        {
            try
            {
                model.RafAdi = model.RafAdi.Trim();
                model.KayitTarihi = DateTime.Today;
                var raf = _db.Rafs.SingleOrDefault(x => x.RafAdi == model.RafAdi);
                if (raf != null)
                    return true;
                _db.Rafs.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal Rafs RafDetails(int Id)
        {
            return _db.Rafs.SingleOrDefault(x => x.ID == Id);
        }
        internal bool RafDuzenle(Rafs model)
        {
            try
            {
                var duzen = _db.Rafs.First(x => x.ID == model.ID);
                duzen.RafAdi = model.RafAdi.Trim();
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool RafSil(Rafs model)
        {
            try
            {
                var sil = _db.Rafs.First(x => x.ID == model.ID);
                _db.Rafs.Remove(sil);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal List<Oduls> OdulListesi()
        {
            return _db.Oduls.ToList();
        }
        internal bool OdulEkle(Oduls model)
        {
            try
            {
                model.OdulAdi = model.OdulAdi.Trim();

                var o = _db.Oduls.SingleOrDefault(x => x.OdulAdi == model.OdulAdi);
                if (o != null)
                    return true;

                _db.Oduls.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal Oduls OdulDetails(int Id)
        {
            return _db.Oduls.Single(x => x.ID == Id);
        }
        internal bool OdulDuzenle(Oduls model)
        {
            try
            {
                var odul = _db.Oduls.First(x => x.ID == model.ID);
                odul.OdulAdi = model.OdulAdi.Trim();
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool OdulSil(Oduls model)
        {
            try
            {
                var odul = _db.Oduls.Single(x => x.ID == model.ID);
                _db.Oduls.Remove(odul);
                var relation = _db.FilmOdulRelation.Where(x => x.OdulID == model.ID).ToList();
                _db.FilmOdulRelation.RemoveRange(relation);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal List<AltYazilar> AltyaziListesi()
        {
            return _db.AltYazilar.ToList();
        }
        internal bool AltyaziEkle(AltYazilar model)
        {
            try
            {
                model.DilAdi = model.DilAdi.Trim();
                var a = _db.AltYazilar.SingleOrDefault(x => x.DilAdi == model.DilAdi);
                if (a != null)
                    return true;

                _db.AltYazilar.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal AltYazilar AltyaziDetails(int Id)
        {
            return _db.AltYazilar.FirstOrDefault(x => x.ID == Id);
        }
        internal bool AltyaziDuzenle(AltYazilar model)
        {
            try
            {
                var duzen = _db.AltYazilar.First(x => x.ID == model.ID);
                duzen.DilAdi = model.DilAdi.Trim();
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool AltyaziSil(AltYazilar model)
        {
            try
            {
                var sil = _db.AltYazilar.Include("Films").FirstOrDefault(x => x.ID == model.ID);
                if (sil != null)
                    return true;
                _db.AltYazilar.Remove(sil);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal List<Paketler> PaketListesi()
        {
            return _db.Paketler.ToList();
        }
        internal bool PaketEkle(Paketler model)
        {
            try
            {
                model.PaketAdi = model.PaketAdi.Trim();
                model.PaketBilgi = model.PaketBilgi.Trim();
                model.Tarih = DateTime.Today;
                _db.Paketler.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal Paketler PaketDetails(int Id)
        {
            return _db.Paketler.FirstOrDefault(x => x.ID == Id);
        }
        internal bool PaketSil(Paketler model)
        {
            try
            {
                var sil = _db.Paketler.First(x => x.ID == model.ID);

                _db.Paketler.Remove(sil);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool PaketDuzenle(Paketler model)
        {
            try
            {
                var duzen = _db.Paketler.Single(x => x.ID == model.ID);
                duzen.MaxFilmAdet = model.MaxFilmAdet;
                duzen.PaketAdi = model.PaketAdi.Trim();
                duzen.PaketBilgi = model.PaketBilgi.Trim();
                duzen.PaketFiyat = model.PaketFiyat;
                duzen.DegisimAdet = model.DegisimAdet;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal List<Oyuncular> OyuncuListesi()
        {
            return _db.Oyuncular.ToList();
        }
        internal bool OyuncuEkle(Oyuncular model)
        {
            try
            {
                model.Adi = model.Adi.Trim();
                model.Soyadi = model.Soyadi.Trim();

                var o = _db.Oyuncular.SingleOrDefault(x => x.Adi == model.Adi);
                if (o != null)
                    return true;
                _db.Oyuncular.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal Oyuncular OyuncuDetails(int Id)
        {
            return _db.Oyuncular.SingleOrDefault(x => x.ID == Id);
        }
        internal bool OyuncuDuzenle(Oyuncular model)
        {
            try
            {
                var duzen = _db.Oyuncular.First(x => x.ID == model.ID);
                duzen.Adi = model.Adi.Trim();
                duzen.Soyadi = model.Soyadi.Trim();
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool OyuncuSil(Oyuncular model)
        {
            try
            {
                var sil = _db.Oyuncular.Single(x => x.ID == model.ID);
                _db.Oyuncular.Remove(sil);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal List<Yonetmens> YonetmenListesi()
        {
            return _db.Yonetmens.ToList();
        }
        internal bool YonetmenEkle(Yonetmens model)
        {
            try
            {
                model.Adi = model.Adi.Trim();
                model.Soyadi = model.Soyadi.Trim();
                model.KayitTarihi = DateTime.Today.ToString();
                var y = _db.Yonetmens.SingleOrDefault(x => x.Adi == model.Adi);
                if (y != null)
                    return true;
                _db.Yonetmens.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal Yonetmens YonetmenDetails(int Id)
        {
            return _db.Yonetmens.SingleOrDefault(x => x.ID == Id);
        }
        internal bool YonetmenDuzenle(Yonetmens model)
        {
            try
            {
                var duz = _db.Yonetmens.Single(x => x.ID == model.ID);
                duz.Adi = model.Adi.Trim();
                duz.Soyadi = model.Soyadi.Trim();
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool YonetmenSil(Yonetmens model)
        {
            try
            {
                var sil = _db.Yonetmens.Include("Films").SingleOrDefault(x => x.ID == model.ID);
                _db.Yonetmens.Remove(sil);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal List<Kategories> KategoriListesi(int? Id = null)
        {
            return _db.Kategories.ToList();
        }
        internal bool KategoriEkle(Kategories model)
        {
            try
            {
                model.KategoriAdi = model.KategoriAdi.Trim();
                model.Tarih = DateTime.Today.ToString();
                var k = _db.Kategories.SingleOrDefault(x => x.KategoriAdi == model.KategoriAdi);
                if (k != null)
                    return true;
                _db.Kategories.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal Kategories KategoriDetails(int Id)
        {
            return _db.Kategories.SingleOrDefault(x => x.ID == Id);
        }
        internal bool KategoriDuzenle(Kategories model)
        {
            try
            {
                var k = _db.Kategories.SingleOrDefault(x => x.ID == model.ID);
                k.KategoriAdi = model.KategoriAdi.Trim();
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool KategoriSil(Kategories model)
        {
            try
            {
                var k = _db.Kategories.Include("Films").SingleOrDefault(x => x.ID == model.ID);
                _db.Kategories.Remove(k);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        internal List<Films> FilmListesi()
        {
            return _db.Films.Where(x => x.Aktifmi == true).ToList();
        }
        internal int FilmEkle(FilmModel model)
        {
            try
            {
                Films film = new Films()
                {
                    RafID = model.RafId,
                    Aciklama = model.Aciklama.Trim(),
                    Aktifmi = true,
                    BarkodNo = model.BarkodNo.Trim(),
                    FilmAdi = model.FilmAdi.Trim(),
                    EditorSectiMi = model.EditorSectiMi,
                    Fiyat = model.Fiyat,
                    ImdbPuan = model.ImdbPuan,
                    YeniFilmMi = model.YeniFilmMi,
                    YapimTarihi = model.YapimTarihi,
                    FragmanUrl = model.FragmanUrl.Trim(),
                    UcBoyutlumu = model.UcBoyutlumu,
                    Stok = model.Stok,
                    Sure = model.Sure,
                    AfisPath = "default.png".Trim(),
                    YabanciAdi = model.YabanciAdi.Trim(),
                    KayitTarihi = DateTime.Today

                };
                _db.Films.Add(film);
                _db.SaveChanges();
                return film.ID;
            }
            catch
            {
                return 0;
            }
        }
        internal Films FilmDetails(int Id)
        {
            return _db.Films.SingleOrDefault(x => x.ID == Id);
        }
        internal bool filmSil(Films model)
        {
            try
            {
                var film = _db.Films.Include("Kategories").Include("AltYazilar").Include("Yonetmens").FirstOrDefault(x => x.ID == model.ID);
                _db.Films.Remove(film);


                var begen = _db.MusteriFilmBegens.Where(x => x.FilmID == model.ID).ToList();
                _db.MusteriFilmBegens.RemoveRange(begen);


                var yorum = _db.MusteriFilmYorums.Where(x => x.FilmID == model.ID).ToList();
                _db.MusteriFilmYorums.RemoveRange(yorum);


                var oyunci = _db.FilmOyuncuRelation.Where(x => x.FilmID == model.ID).ToList();
                _db.FilmOyuncuRelation.RemoveRange(oyunci);


                var yonetmen = _db.MusteriFilmYorums.Where(x => x.FilmID == model.ID).ToList();
                _db.MusteriFilmYorums.RemoveRange(yonetmen);


                var mfilmList = _db.MusteriFilmListesi.Where(x => x.FilmlerID == model.ID).ToList();
                _db.MusteriFilmListesi.RemoveRange(mfilmList);

                var odul = _db.FilmOdulRelation.Where(x => x.FilmID == model.ID).ToArray();
                _db.FilmOdulRelation.RemoveRange(odul);

                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool FilmDuzenle(FilmModel model)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == model.ID);
                f.RafID = model.RafId;
                f.Aciklama = model.Aciklama.Trim();
                f.Aktifmi = true;
                f.BarkodNo = model.BarkodNo.Trim();
                f.FilmAdi = model.FilmAdi.Trim();
                f.EditorSectiMi = model.EditorSectiMi;
                f.Fiyat = model.Fiyat;
                f.ImdbPuan = model.ImdbPuan;
                f.YeniFilmMi = model.YeniFilmMi;
                f.YapimTarihi = model.YapimTarihi;
                f.FragmanUrl = model.FragmanUrl.Trim();
                f.UcBoyutlumu = model.UcBoyutlumu;
                f.Stok = model.Stok;
                f.Sure = model.Sure;
                f.YabanciAdi = model.YabanciAdi.Trim();

                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        internal bool FilmAfisEkle(string fileName, int filId)
        {
            try
            {
                var film = _db.Films.SingleOrDefault(x => x.ID == filId);
                film.AfisPath = fileName.Trim();

                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool filmKategoriEkle(int filmId, int kId)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == filmId);
                var k = _db.Kategories.SingleOrDefault(x => x.ID == kId);
                f.Kategories.Add(k);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
        internal bool filmYonetmenEkle(int fId, int yId)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == fId);
                var y = _db.Yonetmens.SingleOrDefault(x => x.ID == yId);
                f.Yonetmens.Add(y);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool filmOyuncuEkle(int fId, int oId, bool rolu)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == fId);
                var o = _db.Oyuncular.SingleOrDefault(x => x.ID == oId);
                var r = _db.FilmOyuncuRelation.SingleOrDefault(x => x.FilmID == f.ID && x.OyuncuID == o.ID);
                if (r != null)
                    return true;

                _db.FilmOyuncuRelation.Add(new FilmOyuncuRelation() { FilmID = f.ID, OyuncuID = o.ID, BasrolMi = rolu });
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool filmOdulEkle(int fId, int oId, bool aldi)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == fId);
                var o = _db.Oduls.SingleOrDefault(x => x.ID == oId);

                var r = _db.FilmOdulRelation.SingleOrDefault(x => x.FilmID == f.ID && x.OdulID == o.ID);
                if (r != null)
                    return true;

                _db.FilmOdulRelation.Add(new FilmOdulRelation { FilmID = f.ID, OdulID = o.ID, AldiMi = aldi });
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool FilmAltYaziEkle(int fId, int aId)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == fId);
                var a = _db.AltYazilar.SingleOrDefault(x => x.ID == aId);
                f.AltYazilar.Add(a);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool filmKategoriSil(int Id, int kId)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == Id);
                var k = _db.Kategories.SingleOrDefault(x => x.ID == Id);
                f.Kategories.Remove(k);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool filmYonetmensil(int Id, int yId)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == Id);
                var y = _db.Yonetmens.SingleOrDefault(x => x.ID == yId);
                f.Yonetmens.Remove(y);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool filmOyuncuSil(int Id, int oId)
        {
            try
            {
                var f = _db.FilmOyuncuRelation.Single(x => x.FilmID == Id && x.OyuncuID == oId);
                _db.FilmOyuncuRelation.Remove(f);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool filmOdulSil(int Id, int oId)
        {
            try
            {
                var f = _db.FilmOdulRelation.SingleOrDefault(x => x.FilmID == Id && x.OdulID == oId);
                _db.FilmOdulRelation.Remove(f);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool filmAltYaziSil(int Id, int aId)
        {
            try
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == Id);
                var a = _db.AltYazilar.SingleOrDefault(x => x.ID == aId);
                f.AltYazilar.Remove(a);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        internal List<MusteriFilmYorums> FilmYorumlari(int Id)
        {
            return _db.MusteriFilmYorums.Where(x => x.FilmID == Id).ToList();
        }
        internal bool FilmYorumlariSil(int yId)
        {
            try
            {
                var y = _db.MusteriFilmYorums.SingleOrDefault(x => x.ID == yId);
                _db.MusteriFilmYorums.Remove(y);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool FilmYorumlariAktifYap(int yId)
        {
            try
            {
                var y = _db.MusteriFilmYorums.SingleOrDefault(x => x.ID == yId);
                y.Aktifmi = true;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        internal List<BozukFilms> BozukFilmListesi()
        {
            return _db.BozukFilms.ToList();
        }
        internal bool BozukFilmSil(int Id)
        {
            try
            {
                var bf = _db.BozukFilms.SingleOrDefault(x => x.ID == Id);
                _db.BozukFilms.Remove(bf);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool BozukFilmEkle(BozukFilms model)
        {
            try
            {
                model.Aciklama = model.Aciklama.Trim();
                model.Aktifmi = true;
                var f = _db.Films.SingleOrDefault(x => x.ID == model.FilmID);

                if (f.Stok >= 0)
                {
                    f.Stok = (f.Stok - 1);
                    model.Tarih = DateTime.Now;
                    _db.BozukFilms.Add(model);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        internal List<Musteriler> MusteriListesi()
        {
            return _db.Musteriler.ToList();
        }
        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                GC.SuppressFinalize(this);
            }
        }
        internal List<AylikParaKesimRelation> FaturaKesimleri()
        {
            List<AylikParaKesimRelation> liste = new List<AylikParaKesimRelation>();

            _db.Musteriler.Where(x => x.Users.Aktifmi == true && x.Tarih == DateTime.Today).ToList().ForEach(x =>
            {
                liste.Add(new AylikParaKesimRelation() { KesilenPara = x.Paketler.PaketFiyat, Musteriler = x, PaketID = x.PaketID });
            });

            return liste;
        }
        internal void FaturaKesimleriYap()
        {

            _db.Musteriler.Where(x => x.Users.Aktifmi == true && x.Tarih == DateTime.Today).ToList().ForEach(x =>
            {
                //Todo: kredi kart işlemleri (ücreti kesilemiyenlerin user'ını aktifliğini =false olarak değiştir. // ve yeni AylıkParaKesim'e kayıt ekle

            });
            //Todo: Kesimde Hata Oluşursa Admin ve Musteriye Eposta Gönderilmesi Lazım.
        }


        List<MusteriFilmListeleModel> _mfilmModel = new List<MusteriFilmListeleModel>();
        List<MusteriFilmListesi> mfilmListesi = new List<MusteriFilmListesi>();
        internal List<MusteriFilmSiparisListesi> FilmsSiparisleriHazırla()
        {
            foreach (SiparisTarihleri x in _db.SiparisTarihleri.Where(x => x.SiparisTarih == DateTime.Today).ToList())
            {
                if (x.Musteriler.MusteriFilmListesi.Where(s => s.GonderildiMi == false && s.Aktifmi == true).Count() > 1) //Todo >10'dan DÜşük Sitesi Olanlar olacak
                {
                    x.Musteriler.MusteriFilmListesi.Where(s => s.GonderildiMi == false && s.Aktifmi == true).ToList().ForEach(s =>
                    {
                        mfilmListesi.Add(new MusteriFilmListesi()
                        {
                            FilmlerID = s.FilmlerID,
                            Films = s.Films,
                            OncelikSirasi = s.OncelikSirasi,
                            MusteriID = s.MusteriID,
                            Musteriler = s.Musteriler,
                            ID = s.ID,
                            Tarih = s.Tarih
                        });
                    });
                }
                else
                {
                    //Todo: Listesi 10'dan düşük Olanlara E-posta GÖndercek.
                }
            }
            if (mfilmListesi.Count > 0)
            {
                int ilk = 0;//Todo: Değiştirilebiir (Pakete göre değilde Sİparis Listesinde OncelikSirasi En yüksekinkini ALıp gönsün 2. plan)
                for (int i = 0; i < _db.Paketler.OrderByDescending(x => x.DegisimAdet).First().DegisimAdet; i++)
                {
                    Paketle(ilk);
                    ilk = mfilmListesi.Where(x => x.OncelikSirasi > ilk).OrderBy(s => s.OncelikSirasi).First().OncelikSirasi;
                }
                bool kontrol = SurecBaslat();
            }

            return _db.MusteriFilmSiparisListesi.OrderBy(x => x.Ilce).ToList();

        }

        private void Paketle(int say)
        {
            var ilk = mfilmListesi.Where(x => x.OncelikSirasi > say).OrderBy(s => s.OncelikSirasi).First().OncelikSirasi;
            DateTime? eskiTarih = Convert.ToDateTime("2000-04-08 00:00:00.000");
            foreach (List<MusteriFilmListesi> oncelikListesi in mfilmListesi.Where(x => x.OncelikSirasi == ilk).GroupBy(x => x.FilmlerID).Select(x => x.ToList()))
            {
                foreach (MusteriFilmListesi oncelik in oncelikListesi)
                {
                    int? stok = _db.Films.SingleOrDefault(x => x.ID == oncelik.FilmlerID).Stok;
                    if (stok >= oncelikListesi.Count)
                    {
                        var kontrol = _mfilmModel.SingleOrDefault(x => x.Adi == oncelik.Musteriler.Adi && x.Soyadi == oncelik.Musteriler.Soyadi);
                        if (kontrol == null)
                        {
                            _mfilmModel.Add(new MusteriFilmListeleModel()
                            {
                                MusteriID = oncelik.MusteriID,
                                Adi = oncelik.Musteriler.Adi,
                                Soyadi = oncelik.Musteriler.Soyadi,
                                Adres = oncelik.Musteriler.DigerBilgiler.Adres,
                                Ilce = oncelik.Musteriler.DigerBilgiler.Ilceler.IlceAdi,
                                PaketAdet = oncelik.Musteriler.Paketler.DegisimAdet,
                                ID = oncelik.Musteriler.ID,
                                FilmListem = new List<FilmListesi>() { new Models.FilmListesi() { 
                                    FilmId = oncelik.FilmlerID, 
                                    MusteriId = oncelik.MusteriID,
                                    OncelikSirasi = 1, 
                                    ID = oncelik.ID } }
                            });
                        }
                        else
                        {
                            if (kontrol.FilmListem.Count != oncelik.Musteriler.Paketler.DegisimAdet)
                            {
                                kontrol.FilmListem.Add(new FilmListesi()
                                {
                                    FilmId = oncelik.FilmlerID,
                                    MusteriId = oncelik.MusteriID,
                                    OncelikSirasi = oncelik.OncelikSirasi,
                                    ID = oncelik.ID
                                });
                            }
                        }
                    }
                    else
                    {
                        if (_mfilmModel.Count < stok)
                        {
                            var oncelikT = oncelikListesi.Where(z => z.Tarih > eskiTarih).OrderBy(x => x.Tarih).FirstOrDefault();
                            eskiTarih = oncelikListesi.Where(z => z.Tarih > eskiTarih).OrderBy(x => x.Tarih).FirstOrDefault().Tarih;
                            var kontrol = _mfilmModel.SingleOrDefault(x => x.Adi == oncelikT.Musteriler.Adi && x.Soyadi == oncelikT.Musteriler.Soyadi);
                            if (kontrol == null)
                            {
                                _mfilmModel.Add(new MusteriFilmListeleModel()
                                {
                                    MusteriID = oncelik.MusteriID,
                                    Adi = oncelikT.Musteriler.Adi,
                                    Soyadi = oncelikT.Musteriler.Soyadi,
                                    Adres = oncelikT.Musteriler.DigerBilgiler.Adres,
                                    Ilce = oncelikT.Musteriler.DigerBilgiler.Ilceler.IlceAdi,
                                    PaketAdet = oncelikT.Musteriler.Paketler.DegisimAdet,
                                    ID = oncelikT.Musteriler.ID,
                                    FilmListem = new List<FilmListesi>() { new Models.FilmListesi() { 
                                        FilmId = oncelikT.FilmlerID,
                                        MusteriId = oncelikT.MusteriID, 
                                        OncelikSirasi = 1, 
                                        ID = oncelikT.ID } }
                                });
                            }
                            else
                            {
                                if (kontrol.FilmListem.Count != oncelikT.Musteriler.Paketler.DegisimAdet)
                                {
                                    kontrol.FilmListem.Add(new FilmListesi()
                                    {
                                        FilmId = oncelikT.FilmlerID,
                                        MusteriId = oncelikT.MusteriID,
                                        OncelikSirasi = oncelikT.OncelikSirasi,
                                        ID = oncelikT.ID
                                    });
                                }
                            }
                        }
                    }
                }
            }

        }
        private bool SurecBaslat()
        {
            try
            {
                _mfilmModel.ToList().ForEach(x =>
                {
                    MusteriFilmSiparisListesi model = new MusteriFilmSiparisListesi() { GondermeDurumu = true, Adi = x.Adi, Adres = x.Adres, Soyadi = x.Soyadi, Ilce = x.Ilce, Tarih = DateTime.Today, PaketAdet = x.PaketAdet, MusteriID = x.MusteriID };
                    _db.MusteriFilmSiparisListesi.Add(model);
                    _db.SaveChanges();

                    int Id = model.ID;
                    x.FilmListem.ToList().ForEach(z =>
                    {
                        _db.FilmSiparisListesi.Add(new FilmSiparisListesi() { FilmID = z.FilmId, MusteriFilmSiparisID = Id, OncelikSirasi = z.OncelikSirasi });
                        _db.SaveChanges();
                        var filmStokDus = _db.Films.SingleOrDefault(s => s.ID == z.FilmId);
                        filmStokDus.Stok = (filmStokDus.Stok - 1);
                        var filmGonder = _db.MusteriFilmListesi.SingleOrDefault(s => s.ID == z.ID);
                        filmGonder.GonderildiMi = true;

                        _db.SaveChanges();
                    });
                });
                var tarihSil = _db.SiparisTarihleri.Where(z => z.SiparisTarih == DateTime.Today).ToList();
                _db.SiparisTarihleri.RemoveRange(tarihSil);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal bool FilmSiparisCikar(int Id)
        {
            try
            {
                var model = _db.MusteriFilmSiparisListesi.SingleOrDefault(x => x.ID == Id);
                _db.MusteriFilmSiparisListesi.Remove(model);
                _db.FilmSiparisListesi.RemoveRange(model.FilmSiparisListesi.ToList());
                _db.SaveChanges();
                //Todo:Musteriye Sms ve Eposta Gönderilecek yer
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
