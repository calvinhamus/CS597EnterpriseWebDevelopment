namespace Trend.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_User()
        {
            T_SavedChart = new HashSet<T_SavedChart>();
            T_UserPlc = new HashSet<T_UserPlc>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string username { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        public int T_UserLevel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SavedChart> T_SavedChart { get; set; }

        public virtual T_UserLevel T_UserLevel1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_UserPlc> T_UserPlc { get; set; }
    }
}
