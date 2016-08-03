using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmDukkaniMvc.Models
{
    public class MusteriModel
    {
        public int ID { get; set; }
        public int PaketID { get; set; }
        public int BilgiID { get; set; }
        public int IlceID { get; set; }

        [Display(Name = "Kullanıcı Adı"), Required]
        public string UserName { get; set; }

        [Display(Name = "Şifre"), Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "(Tekrar) Şifre"), Required, Compare("Password", ErrorMessage = "Aynı Şifreyi Girin"), DataType(DataType.Password)]
        public string TPassword { get; set; }

        [Required, DataType(DataType.EmailAddress, ErrorMessage = "E-posta Doğru girin.")]
        public string Eposta { get; set; }

        //--------------------

        [Display(Name = "Adı"), Required]
        public string Adi { get; set; }

        [Display(Name = "Soyadı"), Required]
        public string Soyadi { get; set; }

        [Display(Name = "Telefon No"), MaxLength(15, ErrorMessage = "Uzun Girdiniz.")]
        public string TelefonNo { get; set; }

        [Display(Name = "T.C No"), Required, Range(1, 99999999999)]
        public int TCNo { get; set; }

        public string Cinsiyet { get; set; }

        [Display(Name = "Yaş")]
        public int Yasi { get; set; }


        [Display(Name = "Kredi Kart No"), Required]
        public int KrediKartNo { get; set; }

        [Display(Name = "Kredi Kart SKT"), Required, DataType(DataType.Date)]
        public System.DateTime KrediKartSKT { get; set; }

        [Display(Name = "Kredi Kart CNR No")]
        public int KrediKartCNRNo { get; set; }

        public string Adres { get; set; }

        [Display(Name = "Posta Kodu")]
        public Nullable<int> PostaKod { get; set; }

 
    }
}
