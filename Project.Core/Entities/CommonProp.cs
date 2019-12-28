using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Core.Entities
{
    public class CommonProp
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("OlusturulmaTarihi"),DisplayName("Oluşturma Tarihi"), ScaffoldColumn(false), Required]
        public DateTime CreatedOn { get; set; }

        [Column("GuncellemeTarihi"), DisplayName("Güncelleme Tarihi"), ScaffoldColumn(false), Required]
        public DateTime ModifiedOn { get; set; }

        [Column("Guncelleyen"), DisplayName("Güncelleyen"), ScaffoldColumn(false), Required, StringLength(30)]
        public string ModifiedUsername { get; set; }
    }
}
