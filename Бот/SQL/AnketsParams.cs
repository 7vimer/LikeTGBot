namespace Bot
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AnketsParams
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? AnketaId { get; set; }

        [StringLength(50)]
        public string ParamName { get; set; }

        [StringLength(50)]
        public string ParamValue { get; set; }

        public virtual SectionAnkets SectionAnkets { get; set; }
    }
}
