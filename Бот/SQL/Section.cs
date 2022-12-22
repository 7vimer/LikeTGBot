namespace Bot
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Section")]
    public partial class Section
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Section()
        {
            SectionAnkets = new HashSet<SectionAnkets>();
            SectionParams = new HashSet<SectionParams>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public float? Popularity { get; set; }

        public long? Creator { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SectionAnkets> SectionAnkets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SectionParams> SectionParams { get; set; }
    }
}
