using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmDukkaniMvc.Models
{
    public class PersonelModel
    {
        public int ID { get; set; }
        [Required]
        public string Adi { get; set; }
        [Required]
        public string Soyadi { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Şifre")]
        [Required]
        public string Password { get; set; }
        public int Ilce { get; set; }
        public string Adres { get; set; }
        [Display(Name = "Posta Kodu")]
        public int? PostaKod { get; set; }
        [Required]
        [StringLength(11), Display(Name = "Telefon No")]
        public string TelefonNo { get; set; }
        [Range(0, 99), Required, Display(Name = "Yaş")]
        public int Yasi { get; set; }
        public string Cinsiyet { get; set; }
        [Required, Display(Name = "E-Posta"), EmailAddress(ErrorMessage = "Doğru Eposta girin")]
        public string Eposta { get; set; }
    }
}
