using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Core.Entities
{
    [Table("tblKullanıcılar")]
    public class BlogUser:CommonProp
    {
        public BlogUser()
        {
            Notes = new List<Note>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }

        [Column("Isim"),DisplayName("İsim"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [Column("Soyad"), DisplayName("Soyad"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [Column("KullaniciAdi"), DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [Column("EMail"), DisplayName("E-Posta"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(40, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column("Sifre"),DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),DataType(DataType.Password)]
        public string Password { get; set; }

        [Column("DogumTarihi"), DisplayName("Doğum Tarihi"), DataType(DataType.Date)]
        public DateTime? BirthOfDay { get; set; }

        [Column("ProfiFoto"),StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }

        [Column("AktifMi"), DisplayName("Aktif")]
        public bool IsActive { get; set; }

        [Column("AdminMi"), DisplayName("Yönetici")]
        public bool IsAdmin { get; set; }

        [Column("AktiviteKod"),Required, ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}
