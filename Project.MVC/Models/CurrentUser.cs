using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Core.Entities;

namespace Project.MVC.Models
{
    public class CurrentUser
    {
        public static BlogUser User
        {
            get {
                return Get<BlogUser>("login");
            }
        }
        public static void Set<T>(string key,T t)
        {
            HttpContext.Current.Session[key] = t;
        }
        public static T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key]!=null)
            {
                return (T)HttpContext.Current.Session[key];
            }
            return default(T);
        }
        public static void Remove(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }
        public static void Clear(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Clear();
            }
        }

    }
}