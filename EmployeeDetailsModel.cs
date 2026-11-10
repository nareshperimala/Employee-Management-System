namespace EmployeeDetailsAPI.Models
{
    public class EmployeeDetailsModel
    {
        public int EmpId {  get; set; }
        public string Fn { get; set; }
        public string Ln {  get; set; }
        public int Age {  get; set; }
        public int Salary {  get; set; }
        public string Depart {  get; set; }
        public DateTime DOJ { get; set; }
    }
}
