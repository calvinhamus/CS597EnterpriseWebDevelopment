namespace Trend.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_SavedChart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_SavedChart()
        {
            T_ChartData = new HashSet<T_ChartData>();
        }

        public int Id { get; set; }

        public int T_UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ChartData> T_ChartData { get; set; }

        public virtual T_User T_User { get; set; }
    }
}
