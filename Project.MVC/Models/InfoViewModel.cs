using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVC.Models
{
    public class InfoViewModel:NotifyPageModel<string>
    {
        public InfoViewModel()
        {
            Title = "Bilgilendirme";
        }
    }
}