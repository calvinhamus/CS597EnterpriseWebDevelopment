namespace Trend.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_ChartData
    {
        public int Id { get; set; }

        public int T_SavedChartId { get; set; }

        public int T_DataValueId { get; set; }

        public virtual T_DataValue T_DataValue { get; set; }

        public virtual T_SavedChart T_SavedChart { get; set; }
    }
}
