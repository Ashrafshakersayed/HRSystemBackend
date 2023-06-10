namespace HR_Employees.Dtos
{
    public class WorkingHourDto
    {
        public int Id { get; set; }
        public DateTime SigninTime { get; set; }
        public DateTime? SignoutTime { get; set; }
        public TimeSpan? WorkingHours { get; set; }
    }
}
