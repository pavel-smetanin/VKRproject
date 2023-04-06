namespace VKRproject.Models
{
    public class User
    {
        public Employee Employee { get; set; }
        public string Role { get; set; }
        public User() 
        {
            Employee = new Employee();
        }
    }
}
