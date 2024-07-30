using Microsoft.AspNetCore.Identity;

namespace Library_Management_System.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }


        public ICollection<Message> ReceivedMessages { get; set; } = new HashSet<Message>();
        public ICollection<Message> SentMessages { get; set; } = new HashSet<Message>();
    }
}
