using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Core.BusinessService;
using Project.Core.Entities;
using Project.Core.Helper.MessageMethod;
using Project.Core.ViewModel;
using Project.MVC.Filters;
using Project.MVC.Models;

namespace Project.MVC.Controllers
{
    [Mistake]
    public class HomeController : Controller
    {
        private NoteService noteService = new NoteService();
        private CategoryServices categoryServices = new CategoryServices();
        private UserService UserService = new UserService();
        private ErrorViewModel errorViewModel = new ErrorViewModel();
        private SuccessViewModel successViewModel = new SuccessViewModel();
        private InfoViewModel infoViewModel = new InfoViewModel();
        private WarningViewModel warningViewModel = new WarningViewModel();
        // GET: Home
        public ActionResult Index()
        {
            int a = 0, b = 1, c = a / b;

            return View(noteService.Select(I => I.IsDraft == false).OrderByDescending(I => I.ModifiedOn).ToList());
        }
        [HttpGet]
        public ActionResult GetCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoryid = categoryServices.Find(I => I.Id == id.Value);
            if (categoryid == null)
            {
                return HttpNotFound();
            }
            return View("Index", categoryid.Notes.OrderByDescending(I => I.ModifiedOn).ToList());
        }
        public ActionResult MostLike()
        {
            noteService = new NoteService();
            return View("Index", noteService.Select().OrderByDescending(I => I.LikeCount).ToList());
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult AccessBlocking()
        {
            warningViewModel.Title = "Yetkisiz İşlem";
            warningViewModel.RedirectingUrl = "/Home/Index";
            warningViewModel.RedirectingTimeOut = 3000;
            warningViewModel.IsRedirectTo = true;
            warningViewModel.Items.Add("Erişim yetkiniz yeterli değildir");
            return View("Warning", warningViewModel);
        }
        public ActionResult HasError()
        {
            if (TempData["Mistake"] != null)
            {
                var ex = TempData["mistake"] as Exception;
                errorViewModel.Title = "Yetkisiz İşlem";
                errorViewModel.RedirectingUrl = "/Home/Index";
                errorViewModel.RedirectingTimeOut = 6000;
                errorViewModel.IsRedirectTo = true;
                errorViewModel.Items.Add(new ErrorMessage()
                {
                    Message = ex.ToString()
                });
            }
            return View("Error", errorViewModel);
        }
        #region login Settings
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserService.LoginUser(model);
                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(I => ModelState.AddModelError("", I.Message));
                    return View(model);
                }
                else
                {
                    Session["login"] = user.Result;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        #endregion
        #region Register Settings
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            UserService = new UserService();
            if (ModelState.IsValid)
            {
                var register = UserService.ControlAndInsert(model);
                if (register.Errors.Count > 0)
                {
                    register.Errors.ForEach(I => ModelState.AddModelError("", I.Message));
                    return View(model);
                }
                successViewModel.Title = "Başarılıdır";
                successViewModel.Header = "Kayıt oldunuz";
                successViewModel.RedirectingTimeOut = 4000;
                successViewModel.RedirectingUrl = "/Home/Login";
                successViewModel.Items.Add("Kaydınız oluşmuştur.Lütfen e-mail hesabınıza gönderilen aktivasyon mailini tıklayınız." +
                    "Atkif olmayan hesap not yazamaz ve not beğenemez");
                return View("Success", successViewModel);
            }
            return View(model);
        }
        #endregion
        #region Active_Code
        public ActionResult UserActiveCode(Guid id)
        {
            UserService = new UserService();
            var user = UserService.ActivateUser(id);
            if (user.Errors.Count > 0)
            {
                TempData["errors"] = user.Errors;
                ErrorViewModel errorViewModel = new ErrorViewModel()
                {
                    Title = "Hesap Aktifleşmedi",
                    Items = user.Errors,
                };
                return RedirectToAction("Error", errorViewModel);
            }
            else
            {
                successViewModel.Header = "Yönlendiriliyorsunuz";
                successViewModel.Title = "Hesap Aktifleştirildi";
                successViewModel.RedirectingTimeOut = 5000;
                successViewModel.RedirectingUrl = "/Home/Login";
                successViewModel.Items.Add("Hesabınız aktifleştirilmiştir.Not beğenebilir veya not yazabilirsiniz");
                RedirectToAction("Success");
            }
            return View();
        }
        #endregion
        #region Profile
        [Auth]
        public ActionResult ShowProfile()
        {
            var user = Session["login"] as BlogUser;
            var res_user = UserService.GetUserProfile(user.Id);
            if (res_user.Errors.Count > 0)
            {
                errorViewModel.Title = "Profil gösterilemedi";
                errorViewModel.Items = res_user.Errors;
                return View("Error", errorViewModel);
            }
            return View(res_user.Result);
        }
        [Auth]
        public ActionResult EditProfile()
        {
            var user = Session["login"] as BlogUser;
            UserService = new UserService();
            if (user != null)
            {
                var res_user = UserService.GetUserProfile(user.Id);
                if (res_user.Errors.Count > 0)
                {
                    errorViewModel.Title = "Profil Düzenlenemedi";
                    errorViewModel.Items = res_user.Errors;
                    return View("Error", errorViewModel);
                }
                return View(res_user.Result);
            }
            else
            {
                infoViewModel.Header = "Yönlendiriliyorsunuz";
                infoViewModel.Title = "Oturum Doldu";
                infoViewModel.RedirectingTimeOut = 2000;
                infoViewModel.RedirectingUrl = "/Home/Login";
                infoViewModel.Items.Add("Oturum sırasında bir işlem yapmadığınız için yönlendiriliyorsunuz");
                return View("Info", infoViewModel);
            }
        }
        [Auth]
        [HttpPost]
        public ActionResult EditProfile(BlogUser model, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Content/Image/{filename}"));
                    model.ProfileImageFilename = filename;
                }
                var res_user = UserService.UpdateProfile(model);
                if (res_user.Errors.Count > 0)
                {
                    errorViewModel.Header = "Geçersiz İşlem";
                    errorViewModel.Title = "Profil Güncellenemedi";
                    errorViewModel.RedirectingUrl = "/Home/EditProfile";
                    errorViewModel.Items = res_user.Errors;
                    return View("Error", errorViewModel);
                }
                else
                {
                    Session["login"] = res_user.Result as BlogUser;
                    return RedirectToAction("ShowProfile");
                }
            }
            return View(model);
        }
        [Auth]
        public ActionResult DeleteProfile()
        {
            BlogUser current_user = Session["login"] as BlogUser;
            var delete = UserService.RemoveProfile(current_user.Id);
            if (delete.Errors.Count > 0)
            {
                errorViewModel.Header = "Yönlendiriliyorsunuz";
                errorViewModel.Title = "Geçersiz İşlem";
                errorViewModel.Items = delete.Errors;
                errorViewModel.RedirectingUrl = "/Home/ShowProfile";
                return View("Error", errorViewModel);
            }
            Session.Clear();
            return RedirectToAction("Index");
        }
        #endregion
    }
}