using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.ViewModel
{
    public class RegisterModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} değer boş geçilemez"),
            StringLength(25, ErrorMessage = "{0} değer maksimun 25 karakter alabilir")]
        public string Username { get; set; }
        [DisplayName("E-Mail"), Required(ErrorMessage = "{0} değer boş geçilemez"),
            StringLength(25, ErrorMessage = "{0} değer maksimun 50 karakter alabilir"), DataType(DataType.EmailAddress),
            EmailAddress(ErrorMessage = " {0} için geçerli bir e-mail giriniz")]
        public string Email { get; set; }
        [DisplayName("Sifre"), Required(ErrorMessage = "{0} değer boş geçilemez"), DataType(DataType.Password),
            StringLength(40, ErrorMessage = "{0} değer maksimun 25 karakter alabilir")]
        public string Password { get; set; }
        [DisplayName("Tekrar Sifre"), Required(ErrorMessage = "{0} değer boş geçilemez"), DataType(DataType.Password),
            Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor"), StringLength(25, ErrorMessage = "{0} değer maksimun 25 karakter alabilir")]
        public string RePassword { get; set; }
    }
}