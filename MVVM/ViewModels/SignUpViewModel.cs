using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using JoseNi.MVVM.Models;

namespace JoseNi.MVVM.ViewModels
{
    public class SignUpViewModel : BindableObject
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public User CurrentUser { get; set; } = new User();
        public ICommand SignUpCommand { get; set; }

        public SignUpViewModel()
        {
            // Add gender options
            GenderOptions = new List<string> { "Male", "Female", "Other" };

            // Command to handle signup logic
            SignUpCommand = new Command(OnSignUp);
        }

        public List<string> GenderOptions { get; set; }

        private void OnSignUp()
        {
            // Check if the username already exists
            if (Users.Any(u => u.Username == CurrentUser.Username))
            {
                // Display a message if the username is taken
                Application.Current.MainPage.DisplayAlert("Error", "Username already taken", "OK");
                return;
            }

            // Add new user to the list
            Users.Add(new User
            {
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                Email = CurrentUser.Email,
                Username = CurrentUser.Username,
                Password = CurrentUser.Password, // For simplicity, no hashing in this example
                DateOfBirth = CurrentUser.DateOfBirth,
                Gender = CurrentUser.Gender
            });

            // Clear input fields after registration
            CurrentUser = new User();
            OnPropertyChanged(nameof(CurrentUser));

            // Display success message
            Application.Current.MainPage.DisplayAlert("Success", "Account successfully created!", "OK");
        }
    }
}
