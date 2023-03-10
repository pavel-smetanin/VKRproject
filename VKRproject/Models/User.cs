namespace VKRproject.Models
{
    public class User
    {
        public Employee Employee { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
