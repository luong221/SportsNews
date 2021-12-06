namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("journalist")]
    public partial class journalist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public journalist()
        {
            articles = new HashSet<article>();
        }

        [StringLength(50)]
        public string id { get; set; }

        public int workExperience { get; set; }

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

        [StringLength(255)]
        public string img { get; set; }

        [StringLength(30)]
        public string status { get; set; }

        public long salary { get; set; }

        public DateTime? createAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<article> articles { get; set; }

        public virtual role role { get; set; }
    }
}
