using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace HR_Employees.Entities
{
    [Table("WorkingHours", Schema = "MasterData")]
    public class WorkingHour
    {
        public int Id { get; set; }
        public DateTime SigninTime { get; set; }
        public DateTime? SignoutTime { get; set; }
        public TimeSpan? WorkingHours { get; set; }


        //public float? WorkingHoursInt { get; set; } 
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

    }
}
