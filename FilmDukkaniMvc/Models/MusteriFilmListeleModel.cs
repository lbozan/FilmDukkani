using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDukkaniMvc.Models
{
    public class MusteriFilmListeleModel
    {
        public int ID { get; set; }
        public int MusteriID { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Ilce { get; set; }
        public string Adres { get; set; }
        public int PaketAdet { get; set; }

        public virtual ICollection<FilmListesi> FilmListem { get; set; }

    }

    public class FilmListesi
    {
        public int ID { get; set; }
        public int MusteriId { get; set; }
        public int FilmId { get; set; }
        public int OncelikSirasi { get; set; }
    }
}
