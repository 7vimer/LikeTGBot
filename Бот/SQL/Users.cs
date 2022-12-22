namespace Bot
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Reports = new HashSet<Reports>();
            Reports1 = new HashSet<Reports>();
            Section = new HashSet<Section>();
            SectionAnkets = new HashSet<SectionAnkets>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [StringLength(100)]
        public string photo { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        public int? Age { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public bool VIP { get; set; }

        [StringLength(7)]
        public string Sex { get; set; }

        [StringLength(7)]
        public string TargetSex { get; set; }

        public bool Active { get; set; }

        public string Description { get; set; }

        public float? Karma { get; set; }

        public float? Rating { get; set; }

        public int? ViewCount { get; set; }

        public int? LikedCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reports> Reports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reports> Reports1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Section> Section { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SectionAnkets> SectionAnkets { get; set; }
    }
}
