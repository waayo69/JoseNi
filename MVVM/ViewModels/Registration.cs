using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace JoseNi.MVVM.ViewModels
{
    public class Registration
    {
        // Connection string to the cloud database
        private string connectionString = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=waayo69_Clients;User ID=waayo69_Clients;Password=kris123asd;Encrypt=False; Connection Timeout=30";

        // Method to register a new user
        public string Register(string firstName, string lastName, string email, string username, string password)
        {
            // Validate that all input fields are filled
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return "Please fill in all fields";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if the username already exists in the database
                string checkUserQuery = "SELECT COUNT(*) FROM JoseNi WHERE Username = @Username";
                using (SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, conn))
                {
                    checkUserCmd.Parameters.AddWithValue("@Username", username);
                    int userExists = (int)checkUserCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        return "Username already exists";
                    }
                }

                // Insert a new user record into the database
                string insertQuery = @"
                INSERT INTO JoseNi (FirstName, LastName, Email, Username, Password) 
                VALUES (@FirstName, @LastName, @Email, @Username, @Password)";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    // Add user details to the query parameters
                    insertCmd.Parameters.AddWithValue("@FirstName", firstName);
                    insertCmd.Parameters.AddWithValue("@LastName", lastName);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@Username", username);
                    insertCmd.Parameters.AddWithValue("@Password", password); // ⚠️ Consider hashing the password before storing

                    // Execute the query and check if the registration was successful
                    int rowsAffected = insertCmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? "Registration successful" : "Registration failed";
                }
            }
        }
    }
}
