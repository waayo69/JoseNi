using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using JoseNi.MVVM.Models;


namespace JoseNi.MVVM.ViewModels
{
    public class LoginPageClass : BindableObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; set; }

        // Static list to simulate a database for login
        public static ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public LoginPageClass()
        {
            // Command to handle login logic
            LoginCommand = new Command(OnLogin);
        }

        private void OnLogin()
        {
            // Check if the username and password match
            var user = Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                // Redirect to the main page or dashboard
                Application.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
                // Example: Navigation to another page
                // await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                // Display error message if login fails
                Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
            }
        }
    }
}
