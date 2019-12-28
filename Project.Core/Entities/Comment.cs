using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project.Core.Entities
{
    [Table("tblYorumlar")]
    public class Comment : CommonProp
    {
        [Column("Metin"),DisplayName("Metin"),Required, StringLength(250)]
        public string Text { get; set; }
        public virtual Note Note { get; set; }
        public virtual BlogUser BlogUser { get; set; }
    }
}
