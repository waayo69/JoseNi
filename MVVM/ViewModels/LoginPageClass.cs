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
        private readonly string _connectionString = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=waayo69_Clients;User ID=waayo69_Clients;Password=kris123asd;Encrypt=False; Connection Timeout=30";

        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; }

        public LoginPageClass()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM JoseNi WHERE Username = @Username AND Password = @Password", conn))
                {
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    if ((int)await cmd.ExecuteScalarAsync() > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
                        Application.Current.MainPage = new NavigationPage(new Views.HomePage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password", "OK");
                    }
                }
            }
        }
    }
}
