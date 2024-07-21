using Microsoft.AspNetCore.Identity;

namespace Library_Management_System.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }


        public ICollection<LibarianMessage> ReceivedMessages { get; set; } = new HashSet<LibarianMessage>();
        public ICollection<LibarianMessage> SentMessages { get; set; } = new HashSet<LibarianMessage>();
    }
}
