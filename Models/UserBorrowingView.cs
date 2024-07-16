using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Library_Management_System.Models
{
    public class UserRequestView
    {
        public DateTime DueDate {get; set;} = default!;
        public DateTime BorrowedOn {get; set;} = default!;
        public string RequestMessage {get; set;} = default!;
    }
}