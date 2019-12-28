using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Core.Entities
{
    [Table("tblNotlar")]
    public class Note:CommonProp
    {
        public Note()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
        [Column("Baslik"),DisplayName("Not Başlığı"), Required, StringLength(60)]
        public string Title { get; set; }

        [Column("NotMetni"), DisplayName("Not Metni"), Required, StringLength(2000)]
        public string Text { get; set; }

        [Column("TaslakMi"), DisplayName("Taslak")]
        public bool IsDraft { get; set; }

        [Column("Begenilme"), DisplayName("Beğenilme")]
        public int LikeCount { get; set; }
        [Column("KategoriID"),DisplayName("Kategori"),]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual BlogUser BlogUser { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}
