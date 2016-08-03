using FilmDukkaniMvc.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FilmDukkaniMvc.Business
{
    public class UsersBusiness : IDisposable
    {
        private FilmDukkaniDBEntities _db;
        public UsersBusiness()
        {
            _db = new FilmDukkaniDBEntities();
        }




        internal List<Personel> PersonelListesi()
        {
            return _db.Personel.ToList();
        }
        private Users PersonelKontrol(Users model)
        {
            try
            {
                var kontrol = _db.Users.SingleOrDefault(x => x.UserName == model.UserName);
                return kontrol;
            }
            catch
            {
                return null;
            }
        }
        internal int PersonelEkle(PersonelModel model, string ilce, string cinsiyet)
        {
            try
            {
                Users users = new Users() { UserName = model.UserName.Trim(), Password = model.Password.Trim(), Aktifmi = true };
                DigerBilgiler bilgi = new DigerBilgiler() { Adres = model.Adres.Trim(), Cinsiyet = cinsiyet, Eposta = model.Eposta.Trim(), IlceID = Convert.ToInt32(ilce), PostaKod = model.PostaKod, TelefonNo = model.TelefonNo.Trim(), Yasi = model.Yasi };

                var kontrol = PersonelKontrol(users);
                if (kontrol == null)
                {
                    _db.Users.Add(users);
                    _db.SaveChanges();
                    _db.DigerBilgiler.Add(bilgi);
                    _db.SaveChanges();
                    int userId = users.ID;
                    int bilgiId = bilgi.ID;

                    Personel personel = new Personel() { UserID = userId, BilgiID = bilgiId, Adi = model.Adi.Trim(), Soyadi = model.Soyadi.Trim(), Tarih = DateTime.Today };
                    _db.Personel.Add(personel);
                    _db.SaveChanges();
                    UsersInRole rol = new UsersInRole() { RoleID = 8, UserID = userId };
                    _db.UsersInRole.Add(rol);
                    _db.SaveChanges();
                    return 1; //Herşey tamam
                }
                else
                {
                    return -1; // Users Var
                }

            }
            catch
            {
                return -2; //Kayıt hatası
            }
        }
        internal PersonelModel PersonelDetails(int Id)
        {
            PersonelModel _model = new PersonelModel();

            var x = _db.Personel.SingleOrDefault(s => s.ID == Id);

            _model.ID = x.ID;
            _model.Adi = x.Adi;
            _model.Soyadi = x.Soyadi;
            _model.UserName = x.Users.UserName;
            _model.Password = x.Users.Password;
            _model.Ilce = x.DigerBilgiler.IlceID;
            _model.Adres = x.DigerBilgiler.Adres;
            _model.PostaKod = x.DigerBilgiler.PostaKod;
            _model.TelefonNo = x.DigerBilgiler.TelefonNo;
            _model.Yasi = x.DigerBilgiler.Yasi;
            _model.Cinsiyet = x.DigerBilgiler.Cinsiyet;
            _model.Eposta = x.DigerBilgiler.Eposta;

            return _model;
        }
        internal bool PersonelSil(PersonelModel model)
        {
            try
            {
                Personel personel = _db.Personel.SingleOrDefault(x => x.ID == model.ID);
                var userId = personel.Users;
                var bilgiId = personel.DigerBilgiler;


                var userrol = _db.UsersInRole.Where(x => x.UserID == personel.UserID).ToList();

                _db.Personel.Remove(personel);
                _db.UsersInRole.RemoveRange(userrol);
                _db.Users.Remove(userId);
                _db.DigerBilgiler.Remove(bilgiId);

                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal int PersonelDuzenle(PersonelModel model)
        {
            try
            {

                Personel personel = _db.Personel.SingleOrDefault(x => x.ID == model.ID);
                personel.Soyadi = model.Soyadi.Trim();
                personel.Adi = model.Adi.Trim();

                var u = _db.Users.Single(x => x.UserName == model.UserName && x.ID != personel.UserID);
                if (u != null)
                    return -1;


                Users users = _db.Users.SingleOrDefault(x => x.ID == personel.UserID);

                users.Password = model.Password.Trim();
                users.UserName = model.UserName.Trim();

                DigerBilgiler bilgi = _db.DigerBilgiler.SingleOrDefault(x => x.ID == personel.BilgiID);
                bilgi.Adres = model.Adres.Trim();
                bilgi.Cinsiyet = model.Cinsiyet;
                bilgi.Eposta = model.Eposta.Trim();
                bilgi.IlceID = model.Ilce;
                bilgi.PostaKod = model.PostaKod;
                bilgi.TelefonNo = model.TelefonNo.Trim();
                bilgi.Yasi = model.Yasi;

                _db.SaveChanges();
                return 1;

            }
            catch
            {
                return 0;
            }
        }

        internal List<Musteriler> MusteriListesi()
        {
            return _db.Musteriler.ToList();
        }
        internal int MusteriEkle(MusteriModel model)
        {
            int uId = 0, bId = 0, krediId = 0;
            try
            {
                var u = _db.Users.SingleOrDefault(x => x.UserName == model.UserName);
                if (u != null)
                    return -2; // username ve passs var ise    
                var eposta = _db.DigerBilgiler.SingleOrDefault(x => x.Eposta == model.Eposta);
                if (eposta != null)
                    return -3; // eposta var ise
                var kredi = _db.KrediKartBilgileri.SingleOrDefault(x => x.KrediKartNo == model.KrediKartNo);
                if (kredi != null)
                    return -4; //kredi kart no var ise
                var tc = _db.Musteriler.SingleOrDefault(x => x.TCNo == model.TCNo);
                if (tc != null)
                    return -5; // tc var ise
                //Todo : Kredi Kart Kontrol - çalışırsa user'da aktif edilir çalışmaz ise hata verir.

                Users user = new Users() { UserName = model.UserName.Trim(), Password = model.Password.Trim(), Aktifmi = true };
                _db.Users.Add(user);
                _db.SaveChanges();
                uId = user.ID;

                DigerBilgiler bilgi = new DigerBilgiler() { Adres = model.Adres.Trim(), Cinsiyet = model.Cinsiyet, Eposta = model.Eposta.Trim(), IlceID = model.IlceID, PostaKod = model.PostaKod, TelefonNo = model.TelefonNo.Trim(), Yasi = model.Yasi };
                _db.DigerBilgiler.Add(bilgi);
                _db.SaveChanges();
                bId = bilgi.ID;

                KrediKartBilgileri krediKart = new KrediKartBilgileri() { KrediKartCNRNo = model.KrediKartCNRNo, KrediKartNo = model.KrediKartNo, KrediKartSKT = model.KrediKartSKT, Aktifmi = true };
                _db.KrediKartBilgileri.Add(krediKart);
                _db.SaveChanges();
                krediId = krediKart.ID;

                Musteriler musteri = new Musteriler() { KrediKartID = krediId, Adi = model.Adi.Trim(), Soyadi = model.Soyadi.Trim(), TCNo = model.TCNo, BilgiID = bId, UserID = uId, Tarih = DateTime.Today, PaketID = model.PaketID };
                _db.Musteriler.Add(musteri);
                _db.SaveChanges();
                //Todo: RoleId Musteri seçilmiştir eğer Musteri Id'si değişirse Burdakide Değişmeli.
                UsersInRole ur = new UsersInRole();
                ur.RoleID = 7;
                ur.UserID = user.ID;
                _db.UsersInRole.Add(ur);
                _db.SaveChanges();



                return 1; // tmm ise
            }
            catch
            {
                if (uId > 0)
                {
                    var userdel = _db.Users.SingleOrDefault(x => x.ID == uId);
                    _db.Users.Remove(userdel);
                }
                if (bId > 0)
                {

                    var bilgidel = _db.DigerBilgiler.SingleOrDefault(x => x.ID == bId);
                    _db.DigerBilgiler.Remove(bilgidel);
                }
                if (krediId > 0)
                {


                    var kredidel = _db.KrediKartBilgileri.SingleOrDefault(x => x.ID == krediId);
                    _db.KrediKartBilgileri.Remove(kredidel);
                }
                _db.SaveChanges();
                return -1; //büyük Hatalarda 
            }
        }
        internal Musteriler MusteriDetails(int Id)
        {
            return _db.Musteriler.SingleOrDefault(x => x.ID == Id);
        }
        internal bool MusteriAktif(int Id, bool durum)
        {
            try
            {
                var m = _db.Users.SingleOrDefault(x => x.ID == Id);
                m.Aktifmi = durum;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal List<MusteriFilmListesi> MusteriFilmListesi(int? Id, bool durum)
        {
            if (Id != null)
                return _db.MusteriFilmListesi.Where(x => x.MusteriID == Id && x.GonderildiMi == durum).OrderBy(x => x.OncelikSirasi).ToList();

            return _db.MusteriFilmListesi.Where(x => x.GonderildiMi == durum).ToList();
        }
        internal void MusteriFilmDurum(int fId, bool durum)
        {
            var mf = _db.MusteriFilmListesi.SingleOrDefault(x => x.ID == fId);
            if (durum)
            {
                mf.Alindimi = true;
                var f = _db.Films.SingleOrDefault(x => x.ID == mf.FilmlerID);
                f.Stok += 1;
            }
            else
            {
                var f = _db.Films.SingleOrDefault(x => x.ID == mf.FilmlerID);
                f.Stok -= 1;
                mf.GonderildiMi = true;
            }
            _db.SaveChanges();
        }
        internal bool MusteriSil(Musteriler model)
        {
            try
            {
                //Todo : Bağlantılarıda Sil
                var kredi = _db.Musteriler.SingleOrDefault(x => x.ID == model.ID);
                var filmlist = _db.MusteriFilmListesi.Where(x => x.MusteriID == model.ID).ToList();
                var yorum = _db.MusteriFilmYorums.Where(x => x.MusteriID == model.ID).ToList();

                var user = _db.Users.Single(x => x.ID == kredi.UserID);
                _db.Users.Remove(user);
                var userrol = _db.UsersInRole.Where(x => x.UserID == user.ID);
                _db.UsersInRole.RemoveRange(userrol);

                _db.MusteriFilmListesi.RemoveRange(filmlist);
                _db.MusteriFilmYorums.RemoveRange(yorum);
                _db.Musteriler.Remove(kredi);
                _db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool MusteriDuzenle(Musteriler model)
        {
            try
            {
                //Todo :Kontrolleri Yükselt örneğin:Düzenleden Başkasının KrediKart'ını giremesin.
                Musteriler m = _db.Musteriler.SingleOrDefault(x => x.ID == model.ID);
                m.Users.Password = model.Users.Password.Trim();
                m.DigerBilgiler.Eposta = model.DigerBilgiler.Eposta.Trim();
                m.Adi = model.Adi.Trim();
                m.Soyadi = model.Soyadi.Trim();
                m.DigerBilgiler.TelefonNo = model.DigerBilgiler.TelefonNo.Trim();
                m.TCNo = model.TCNo;
                m.DigerBilgiler.Cinsiyet = model.DigerBilgiler.Cinsiyet;
                m.DigerBilgiler.Yasi = model.DigerBilgiler.Yasi;
                m.KrediKartBilgileri.KrediKartCNRNo = model.KrediKartBilgileri.KrediKartCNRNo;
                m.KrediKartBilgileri.KrediKartNo = model.KrediKartBilgileri.KrediKartNo;
                m.KrediKartBilgileri.KrediKartSKT = model.KrediKartBilgileri.KrediKartSKT;
                m.DigerBilgiler.Adres = model.DigerBilgiler.Adres.Trim();
                m.DigerBilgiler.PostaKod = model.DigerBilgiler.PostaKod;
                m.DigerBilgiler.IlceID = model.DigerBilgiler.IlceID;

                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        internal void MusteriFilmYorumEkle(MusteriFilmYorums m)
        {
            try
            {
                m.KayitTarihi = DateTime.Now;
                m.Aktifmi = true;
                m.Yorum = m.Yorum.Trim();
                _db.MusteriFilmYorums.Add(m);
                _db.SaveChanges();
            }
            catch
            {
            }
        }
        internal int MusteriGetUsername(string username)
        {
            return _db.Musteriler.SingleOrDefault(x => x.Users.UserName == username).ID;
        }
        internal int PersonelGetUsername(string username)
        {
            return _db.Personel.SingleOrDefault(x => x.Users.UserName == username).ID;
        }
        internal void MusteriFilmBegen(int Id, int fId, bool durum)
        {

            var filmBegen = _db.MusteriFilmBegens.SingleOrDefault(x => x.MusteriID == Id && x.FilmID == fId);
            if (filmBegen != null)
                return;

            _db.MusteriFilmBegens.Add(new MusteriFilmBegens() { Begenilen = durum, MusteriID = Id, FilmID = fId, KayitTarihi = DateTime.Now });
            _db.SaveChanges();
        }
        internal bool MusteriFilmKontrol(int userId, int fId)
        {
            try
            {
                //var paketId = _db.Musteriler.SingleOrDefault(x => x.ID == userId).PaketID;
                //if (paketId == 0)
                //    return false;
                //var paketAdet = _db.Paketler.SingleOrDefault(x => x.ID == paketId).MaxFilmAdet;
                //if (paketAdet == null)
                //    return false;

                var filmListesi = _db.MusteriFilmListesi.SingleOrDefault(x => x.MusteriID == userId && x.Aktifmi == true && x.FilmlerID == fId);
                if (filmListesi != null)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool MusteriFilmEkle(MusteriFilmListesi model)
        {
            try
            {
                var sira = 0;
                if (_db.MusteriFilmListesi.Any(x => x.MusteriID == model.MusteriID))
                    sira = _db.MusteriFilmListesi.Where(x => x.MusteriID == model.MusteriID).Max(x => x.OncelikSirasi);

                model.OncelikSirasi = sira + 1;
                _db.MusteriFilmListesi.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal bool MusteriFilmListeSil(int Id)
        {
            try
            {
                var f = _db.MusteriFilmListesi.SingleOrDefault(x => x.ID == Id);
                _db.MusteriFilmListesi.Remove(f);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        internal void MusteriFilmListeSira(int Id, int fId, bool durum)
        {
            // True : Yukarı | False :Aşağı

            var sira = _db.MusteriFilmListesi.SingleOrDefault(x => x.Aktifmi == true && x.MusteriID == Id && x.ID == fId);
            int eski = sira.OncelikSirasi;

            if (durum == true)
            {
                var degisim = _db.MusteriFilmListesi
                                 .Where(x => x.OncelikSirasi < eski && x.Aktifmi == true && x.MusteriID == Id)
                                 .OrderByDescending(x => x.ID)
                                 .Take(1)
                                 .FirstOrDefault();

                sira.OncelikSirasi = degisim.OncelikSirasi;
                degisim.OncelikSirasi = eski;

            }
            else
            {
                var degisim = _db.MusteriFilmListesi
                                 .Where(x => x.OncelikSirasi > eski && x.Aktifmi == true && x.MusteriID == Id)
                                 .Take(1).FirstOrDefault();
                sira.OncelikSirasi = degisim.OncelikSirasi;
                degisim.OncelikSirasi = eski;

            }
            _db.SaveChanges();
        }

        internal List<SiparisTarihleri> MusteriSiparisGunleri(int Id)
        {

            return _db.SiparisTarihleri.Where(x => x.MusteriID == Id && x.SiparisTarih >= DateTime.Today).OrderBy(x => x.SiparisTarih).ToList();
        }

        internal void MusteriSiparisGunSil(int Id)
        {
            var tarih = _db.SiparisTarihleri.SingleOrDefault(x => x.ID == Id);
            _db.SiparisTarihleri.Remove(tarih);
            _db.SaveChanges();
        }

        internal bool MusteriSiparisGunleriKontrol(DateTime dateTime)
        {
            var tarih = _db.SiparisTarihleri.SingleOrDefault(x => x.SiparisTarih == dateTime);
            if (tarih != null)
                return false;
            return true;
        }

        internal void MusteriSiparisGunleriEkle(SiparisTarihleri model)
        {
            model.Tarih = DateTime.Now;

            _db.SiparisTarihleri.Add(model);
            _db.SaveChanges();
        }

        internal bool MusteriSiparisGunleriSinir(int Id)
        {
            int musteriPaket = _db.Musteriler.SingleOrDefault(x => x.ID == Id).PaketID;
            var paketAdet = _db.Paketler.SingleOrDefault(x => x.ID == musteriPaket);
            int adet = paketAdet.DegisimAdet;
            int max = paketAdet.MaxFilmAdet;
            var siparisTarihList = _db.SiparisTarihleri.Where(x => x.MusteriID == Id && x.SiparisTarih.Month == DateTime.Today.Month).ToList().Count;

            if (siparisTarihList >= (max / adet))
                return false;

            return true;
        }


        internal string GunAdi(DayOfWeek dayOfWeek)
        {
            return CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.DayNames[(int)dayOfWeek];
        }
    }
}
