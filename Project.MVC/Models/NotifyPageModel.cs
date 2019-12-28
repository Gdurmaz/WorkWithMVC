using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVC.Models
{
    public class NotifyPageModel<T>
    {
        public List<T> Items { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public bool IsRedirectTo { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeOut { get; set; }
        public NotifyPageModel()
        {
            //Base Page 
            this.Header = "Yönlendiriliyorsunuz";
            this.Title = "Geçersiz İşlem";
            this.IsRedirectTo = true;
            this.RedirectingUrl = "/Home/Index";
            this.RedirectingTimeOut = 8000;
            Items = new List<T>();
        }

    }
}