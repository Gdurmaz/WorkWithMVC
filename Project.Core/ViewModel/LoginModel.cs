using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.ViewModel
{
    public class LoginModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "Bu {0} değer boş geçilemez"),
            StringLength(25, ErrorMessage = "Bu {0} değer maksimun 25 karakter alabilir")]
        public string Username { get; set; }
        [DisplayName("Sifre"), Required(ErrorMessage = "Bu {0} değer boş geçilemez"), DataType(DataType.Password),
            StringLength(25, ErrorMessage = "Bu {0} değer maksimun 25 karakter alabilir")]
        public string Password { get; set; }
    }
}