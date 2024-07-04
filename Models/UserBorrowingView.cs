using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class UserRequestView
    {
        public DateTime BorrowedOn {get; set;} = default!;
        public string RequestMessage {get; set;} = default!;
    }
}