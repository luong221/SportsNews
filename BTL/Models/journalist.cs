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
        public long id { get; set; }

        public int workExperience { get; set; }

        public long salary { get; set; }

        public long infoId { get; set; }

        public virtual info info { get; set; }
    }
}
