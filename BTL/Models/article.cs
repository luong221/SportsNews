namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("article")]
    public partial class article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public article()
        {
            comments = new HashSet<comment>();
            keywords = new HashSet<keyword>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string journalistId { get; set; }

        [Required]
        [StringLength(50)]
        public string categoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        public int? totalView { get; set; }

        [Required]
        [StringLength(100)]
        public string thumbnail { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string description { get; set; }

        [StringLength(30)]
        public string status { get; set; }

        public DateTime createAt { get; set; }

        public DateTime? updateAt { get; set; }

        public virtual category category { get; set; }

        public virtual journalist journalist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<keyword> keywords { get; set; }
    }
}
