namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("administrative")]
    public partial class administrative
    {
        [StringLength(50)]
        public string id { get; set; }

        public long roleId { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        [Required]
        [StringLength(128)]
        public string name { get; set; }

        [Required]
        [StringLength(128)]
        public string lastname { get; set; }

        public bool gender { get; set; }

        public DateTime birthday { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        public virtual role role { get; set; }
    }
}
