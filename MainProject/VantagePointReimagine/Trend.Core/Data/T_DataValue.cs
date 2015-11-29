namespace Trend.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_DataValue
    {
        public int Id { get; set; }

        public int T_DataPoint { get; set; }

        public decimal Value { get; set; }

        public DateTime DateTime { get; set; }

        public virtual T_DataPoint T_DataPoint1 { get; set; }
    }
}
