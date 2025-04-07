using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoseNi.MVVM.Models
{
    public class User
    {
        // Stores the first name of the user
        public string FirstName { get; set; }

        // Stores the last name of the user
        public string LastName { get; set; }

        // Stores the email address of the user
        public string Email { get; set; }
        public DateTime DoB {  get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        // Stores the chosen username of the user
        public string Username { get; set; }

        // Stores the user's password
        public string Password { get; set; }
    }
}
