using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TelefonRehberi.Models.EntityFramework;

namespace TelefonRehberi.Models.Class
{
    public class DepartmanModel
    {
        public int dprid { get; set; }
        [Required(ErrorMessage ="Departman Adı zorunludur.")]
        public string dprAd { get; set; }

        public virtual ICollection<Personel> Personel { get; set; }
    }
}