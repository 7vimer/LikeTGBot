namespace Bot
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SectionParams
    {
        public int id { get; set; }

        public int SectionID { get; set; }

        [StringLength(50)]
        public string ParamName { get; set; }

        public virtual Section Section { get; set; }
    }
}
