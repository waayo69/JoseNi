using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoseNi.MVVM.Models
{
    public class Accounts
    {
        // Model class representing user account details for registration

        // Stores the first name of the user
        public string FirstName { get; set; }

        // Stores the last name of the user
        public string LastName { get; set; }

        // Stores the email address of the user
        public string Email { get; set; }

        // Stores the chosen username of the user
        public string Username { get; set; }

        // Stores the user's password
        public string Password { get; set; }
    }
}
