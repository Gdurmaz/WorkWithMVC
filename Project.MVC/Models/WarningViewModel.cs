using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVC.Models
{
    public class WarningViewModel:NotifyPageModel<string>
    {
        public WarningViewModel()
        {
            Title = "Uyarı";
        }
    }
}