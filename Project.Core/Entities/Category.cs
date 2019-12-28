using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Core.Entities
{
    [Table("tblKategori")]
    public class Category : CommonProp
    {
        public Category()
        {
            Notes = new List<Note>();
        }
        [Column("Kategori"), DisplayName("Kategori"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter içermeli.")]
        public string Title { get; set; }

        [Column("Aciklama"), DisplayName("Açıklama"),
            StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter içermeli.")]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; }


    }
}
