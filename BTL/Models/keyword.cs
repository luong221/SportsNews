namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("keyword")]
    public partial class keyword
    {
        public long id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public long articleId { get; set; }

        public virtual article article { get; set; }
    }
}
