using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TelefonRehberi.Models.EntityFramework;

namespace TelefonRehberi.Models.Class
{
    public class PersonelModel
    {

        public int pid { get; set; }
        [Required(ErrorMessage ="Ad alanı zorunludur.")]
        public string pAd { get; set; }
        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string pSoyad { get; set; }
        [Required(ErrorMessage = "Lütfen gerçek bir telefon numarası giriniz.")]
        public string pTelNo { get; set; }
        //[Required(ErrorMessage = "Lütfen yönetici seçiniz")]
        public string yonetici { get; set; }
       // [Required(ErrorMessage = "Lütfen departman seçiniz")]
        public Nullable<int> dprFk { get; set; }
        public virtual Departman Departman { get; set; }
        public string Description
        {
            get
            {
                return pAd + " " + pSoyad;
            }
        }
    }
}