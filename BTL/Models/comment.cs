namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("comment")]
    public partial class comment
    {
        public long id { get; set; }

        public long articleId { get; set; }

        public long infoId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string description { get; set; }

        public DateTime createAt { get; set; }

        public DateTime? updateAt { get; set; }

        public virtual article article { get; set; }

        public virtual info info { get; set; }
    }
}
