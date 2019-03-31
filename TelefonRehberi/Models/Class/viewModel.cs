using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelefonRehberi.Models.EntityFramework;

namespace TelefonRehberi.Models.Class
{
    public class viewModel
    {
        public IEnumerable<Departman>DepartmanInfo { get; set; }
        public IEnumerable<Personel> PersonelInfo { get; set; }

        public Personel Personel { get; set; }
    }
}