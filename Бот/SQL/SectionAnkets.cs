namespace Bot
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SectionAnkets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SectionAnkets()
        {
            AnketsParams = new HashSet<AnketsParams>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public long? Creator { get; set; }

        public int? SectionId { get; set; }

        [StringLength(100)]
        public string photo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnketsParams> AnketsParams { get; set; }

        public virtual Section Section { get; set; }

        public virtual Users Users { get; set; }
    }
}
