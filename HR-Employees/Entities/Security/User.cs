using System.ComponentModel.DataAnnotations.Schema;

namespace HR_Employees.Entities
{
    [Table("Users", Schema = "Security")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
