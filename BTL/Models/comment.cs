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

        [Required]
        [StringLength(50)]
        public string userId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string description { get; set; }

        public DateTime createAt { get; set; }

        public DateTime? updateAt { get; set; }

        public virtual article article { get; set; }

        public virtual user user { get; set; }
    }
}
