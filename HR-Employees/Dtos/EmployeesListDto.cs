namespace HR_Employees.Dtos
{
    public class EmployeesListDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public string ManagerName { get; set; }
    }
}
