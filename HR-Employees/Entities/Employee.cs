using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Employees.Entities
{
    [Table("Employees", Schema = "MasterData")]
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public int? ManagerID { get; set; }

        public Employee Manager { get; set; }
        public List<WorkingHour> WorkingHours { get; set; }

    }
}
