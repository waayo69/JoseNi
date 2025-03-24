using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JoseNi.MVVM.ViewModels
{
    public class LoginPageClass
    {
        // Connection string for the cloud database
        private readonly string _connectionString = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=waayo69_Clients;User ID=waayo69_Clients;Password=kris123asd;Encrypt=False; Connection Timeout=30";

        // Properties for storing user credentials
        public string Username { get; set; }
        public string Password { get; set; }

        // Command that triggers the login process
        public ICommand LoginCommand { get; }

        // Constructor initializes the LoginCommand with an asynchronous login method
        public LoginPageClass()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        // Asynchronous method to handle user login by validating credentials from the database
        private async Task LoginAsync()
        {
            // Show an error alert if either username or password fields are empty
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            // Open a connection to the cloud database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                // SQL query to check if the username and password match any record in the database
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM JoseNi WHERE Username = @Username AND Password = @Password", conn))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    // Execute the query and check if any matching record exists
                    if ((int)await cmd.ExecuteScalarAsync() > 0)
                    {
                        // Successful login: Display success message and navigate to HomePage
                        await Application.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
                        Application.Current.MainPage = new NavigationPage(new Views.HomePage());
                    }
                    else
                    {
                        // Failed login: Show an error message
                        await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password", "OK");
                    }
                }
            }
        }
    }
}
