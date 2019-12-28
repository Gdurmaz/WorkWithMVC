using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVC.Models
{
    public class SuccessViewModel:NotifyPageModel<string>
    {
        public SuccessViewModel()
        {
            Title = "İşlem Başarılı";
        }
    }
}