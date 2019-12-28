using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Core.Entities
{
    [Table("tblBegenmeler")]
    public class Like
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Note Note { get; set; }
        public virtual BlogUser BlogUser { get; set; }
    }
}
