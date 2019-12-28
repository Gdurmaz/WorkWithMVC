using Project.Core.Entities;
using Project.Core.Repository;
using Project.Core.Helper.MessageMethod;
using Project.Core.ViewModel;
using Project.Core.Helper.EMailMethod;
using System;

namespace Project.Core.BusinessService
{
    public class UserService:RepositoryBase<BlogUser>
    {
        private ResultMethod<BlogUser> res_user = new ResultMethod<BlogUser>();
        private int result;
        public ResultMethod<BlogUser> ControlAndInsert(RegisterModel model)
        {
            var user =Find(I => I.Username == model.Username || I.Email.Equals(model.Email));
            if (user!=null)
            {
                if (user.Username==model.Username)
                {
                    res_user.AddErrorCode(ErrorCode.UsernameAlreadyExists,$"{model.Username} Sisteme kayıtlıdır");
                }
                if (user.Email==model.Email)
                {
                    res_user.AddErrorCode(ErrorCode.UserCouldNotInserted,$"{model.Email} Sisteme kayıtlıdır");
                }
            }
            else
            {
                result = base.Insert(new BlogUser() {
                    Username=model.Username,
                    Password=model.Password,
                    Email=model.Email,
                    IsAdmin=false,
                    IsActive=false,
                    ActivateGuid=Guid.NewGuid(),
                    ProfileImageFilename = "user-icon.png"
                });
                if (result>0)
                {
                    res_user.Result = Find(I => I.Username == model.Username && I.Email.Equals(model.Email));
                    string siteurl = ConfigHelper.Get<string>("SiteUrl");
                    string activateurl = $"{siteurl}/Home/UserActiveCode/{res_user.Result.ActivateGuid}";
                    string body = $"Merhaba {res_user.Result.Username} ;<br><br> hesabınızı aktif etmek için <a href='activateurl' targer='_blank'> tıklayınız </a>";
                    //will not work because it is not a valid email.
                    //EmailHelper.SendMail(body, res_user.Result.Email, "Kullanıcı Aktif etme");
                }
            }
            return res_user;
        }
        public ResultMethod<BlogUser> LoginUser(LoginModel model)
        {
            res_user.Result = Find(I => I.Username == model.Username && I.Password == model.Password);
            if (res_user.Result != null)
            {
                if (!res_user.Result.IsActive)
                {
                    res_user.AddErrorCode(ErrorCode.UserIsNotActive,"kullanici aktif edilmemiştir.");
                    res_user.AddErrorCode(ErrorCode.CheckYourEmail, "Lütfen e-mailinizi kontrol ediniz.");

                }
            }
            else
            {
                res_user.AddErrorCode(ErrorCode.UsernameOrPasswordWrong,"Hatalı kullanıcı adı veya şifre girişi yaptınız");
            }
            return res_user;
        }
        public ResultMethod<BlogUser> ActivateUser(Guid id)
        {
            res_user.Result =Find(I => I.ActivateGuid==id);
            if (res_user.Result!=null)
            {
                if (res_user.Result.IsActive==true)
                {
                    res_user.AddErrorCode(ErrorCode.UserAllreadyActive, "Kullanıcı daha önceden aktif edilmiştir");
                    return res_user;
                }
                else
                {
                    res_user.Result.IsActive = true;
                    Update(res_user.Result);
                }
            }
            else
            {
                res_user.AddErrorCode(ErrorCode.ActiveIDDoesNotExists, "Kullanıcı daha önceden aktif edilmiştir");

            }
            return res_user;
        }
        public ResultMethod<BlogUser> GetUserProfile(int id)
        {
            res_user.Result = Find(I => I.Id == id);
            if (res_user.Result==null)
            {
                res_user.AddErrorCode(ErrorCode.UserNotFound, "Kullanıcı Bulunamadı");
            }
            return res_user;
        }
        public ResultMethod<BlogUser> UpdateProfile(BlogUser model)
        {
            var user = Find(I=> I.Username==model.Username && I.Email == model.Email);
            if (user!=null && user.Id != model.Id)
            {
                if (user.Username == model.Username)
                {
                    res_user.AddErrorCode(ErrorCode.UsernameAlreadyExists, $"{model.Username} Sisteme kayıtlıdır");
                }
                if (user.Email == model.Email)
                {
                    res_user.AddErrorCode(ErrorCode.UserCouldNotInserted, $"{model.Email} Sisteme kayıtlıdır");
                }
                return res_user;
            }
            else
            {
                if (string.IsNullOrEmpty(model.ProfileImageFilename) == false)
                {
                    user.ProfileImageFilename = model.ProfileImageFilename;
                }
                res_user.Result = Find(I => I.Id == model.Id);
                var update = base.Update(new BlogUser() {
                    Name = res_user.Result.Name,
                    Surname = res_user.Result.Surname,
                    Username = res_user.Result.Username ,
                    Password = res_user.Result.Password ,
                    Email = res_user.Result.Email,
                    ProfileImageFilename = res_user.Result.ProfileImageFilename
                });
                if (update<1)
                {
                    res_user.AddErrorCode(ErrorCode.ProfileCouldNotUpdate, "Profil güncellenemedi");
                }
                return res_user;
            }
        }
        public ResultMethod<BlogUser> RemoveProfile(int id)
        {
            res_user.Result = Find(I => I.Id == id);
            if (res_user.Result!=null)
            {
                if (Delete(res_user.Result)==0)
                {
                    res_user.AddErrorCode(ErrorCode.UserCouldNotRemove, "Kullanıcı Silinemedi");
                }
                return res_user;
            }
            else
            {
                res_user.AddErrorCode(ErrorCode.UserNotFound, "Kullanıcı Bulunamadı");
                return res_user;
            }
        }
        //Method Hiding...
        public new ResultMethod<BlogUser> Insert(BlogUser model)
        {
            res_user.Result = model;
            var user = Find(I => I.Username == model.Username || I.Email.Equals(model.Email));
            if (user != null)
            {
                if (user.Username == model.Username)
                {
                    res_user.AddErrorCode(ErrorCode.UsernameAlreadyExists, $"{model.Username} Sisteme kayıtlıdır");
                }
                if (user.Email == model.Email)
                {
                    res_user.AddErrorCode(ErrorCode.UserCouldNotInserted, $"{model.Email} Sisteme kayıtlıdır");
                }
            }
            else
            {
                res_user.Result.ProfileImageFilename = "user-icon.png";
                res_user.Result.ActivateGuid = Guid.NewGuid();
                result = base.Insert(res_user.Result);
                if (result == 0)
                {
                    res_user.AddErrorCode(ErrorCode.UserCouldNotInserted, $"Kayıt işlemi başarısızdır");
                }
            }
            return res_user;
        }
        public new ResultMethod<BlogUser> Update(BlogUser model)
        {
            res_user.Result = Find(I => I.Username == model.Username && I.Email == model.Email);
            if (res_user.Result != null && res_user.Result.Id != model.Id)
            {
                if (res_user.Result.Username == model.Username)
                {
                    res_user.AddErrorCode(ErrorCode.UsernameAlreadyExists, $"{model.Username} Sisteme kayıtlıdır");
                }
                if (res_user.Result.Email == model.Email)
                {
                    res_user.AddErrorCode(ErrorCode.UserCouldNotInserted, $"{model.Email} Sisteme kayıtlıdır");
                }
                return res_user;
            }
            else
            {
                if (string.IsNullOrEmpty(model.ProfileImageFilename) == false)
                {
                    res_user.Result.ProfileImageFilename = model.ProfileImageFilename;
                }
                res_user.Result = Find(I => I.Id == model.Id);
                var _update = base.Update(new BlogUser()
                {
                    Name = res_user.Result.Name,
                    Surname = res_user.Result.Surname,
                    Username = res_user.Result.Username,
                    Password = res_user.Result.Password,
                    Email = res_user.Result.Email,
                    ProfileImageFilename = res_user.Result.ProfileImageFilename,
                    IsActive=res_user.Result.IsActive,
                    IsAdmin = res_user.Result.IsAdmin
                });
                if (_update == 0)
                {
                    res_user.AddErrorCode(ErrorCode.ProfileCouldNotUpdate, "Profil güncellenemedi");
                }
                return res_user;
            }
        }
    }
}
