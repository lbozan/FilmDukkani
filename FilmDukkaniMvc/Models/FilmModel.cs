using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmDukkaniMvc.Models
{
    public class FilmModel
    {
        [Required(ErrorMessage = "Raf Secin")]
        public int RafId { get; set; }
        [Key]
        public int ID { get; set; }
        [Display(Name = "Barkod No")]
        public string BarkodNo { get; set; }
        [Display(Name = "Film Adı"), Required]
        public string FilmAdi { get; set; }
        [Display(Name = "(Yabancı)Film Adı"), Required]
        public string YabanciAdi { get; set; }
        [StringLength(250)]
        public string Aciklama { get; set; }
        [Display(Name = "Film Süresi"), DataType(DataType.Time)]
        public System.TimeSpan Sure { get; set; }
        [Display(Name = "Film Tarihi"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime YapimTarihi { get; set; }
        [DataType(DataType.Currency)]
        public decimal Fiyat { get; set; }
        [Required]
        public Nullable<int> Stok { get; set; }

        [DataType(DataType.Url)]
        public string FragmanUrl { get; set; }
        public Nullable<bool> UcBoyutlumu { get; set; }
        [Range(0, 10)]
        [Display(Name = "Imdb Puan")]
        public Nullable<double> ImdbPuan { get; set; }
        [Display(Name = "Yeni Film")]
        public Nullable<bool> YeniFilmMi { get; set; }
        [Display(Name = "Editörün Seçimi")]
        public Nullable<bool> EditorSectiMi { get; set; }

    }
}
