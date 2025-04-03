using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JoseNi.MVVM.Data;
using JoseNi.MVVM.Models;
using JoseNi.MVVM.Views;
using JoseNi.MVVM.ViewModels;

namespace JoseNi.MVVM.ViewModels
{
    public class AppViewModel
    {
        // ViewModel class that handles the logic for the app's login and registration processes
        public class AppViewModel : INotifyPropertyChanged
        {
            // Property to hold the app model (likely for accessing shared data or services)
            public AppModel appmodel { get; set; }

            // Private backing fields for username, password, and confirm password properties
            private string _username;
            private string _password;
            private string _confirmPassword;

            // Public property for Username, implements INotifyPropertyChanged to update UI bindings
            public string Username
            {
                get => _username;
                set
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username)); // Notifies that the Username property has changed
                }
            }

            // Public property for Password, implements INotifyPropertyChanged
            public string Password
            {
                get => _password;
                set
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password)); // Notifies that the Password property has changed
                }
            }

            // Public property for ConfirmPassword, implements INotifyPropertyChanged
            public string ConfirmPassword
            {
                get => _confirmPassword;
                set
                {
                    _confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword)); // Notifies that the ConfirmPassword property has changed
                }
            }

            // ICommand properties for login and registration actions (commands are linked to buttons or other UI elements)
            public ICommand LoginCommand { get; }
            public ICommand RegisterCommand { get; }

            // Constructor that initializes the commands and appmodel
            public AppViewModel()
            {
                // Initializing commands with RelayCommand (action executed when the command is invoked)
                LoginCommand = new RelayCommand(Login); // Command to invoke Login method
                RegisterCommand = new RelayCommand(Register); // Command to invoke Register method

                // Initialize appmodel (could be a data container or service for the app)
                appmodel = new AppModel();
            }

            // Method to handle login logic
            private async void Login()
            {
                // Check if any registered user matches the input username and password
                if (ListData.RegisteredNiggers.Any(niggers => niggers.Username == Username && niggers.Password == Password))
                {
                    // Show success message and navigate to a new page on successful login
                    Application.Current.MainPage.DisplayAlert("Success", "Login Successful", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new Rickyrolly()); // Navigate to Rickyrolly page
                }
                else
                {
                    // Show error message on failed login attempt
                    Application.Current.MainPage.DisplayAlert("Error", "Invalid Credentials", "OK");
                }
            }

            // Method to handle registration logic
            private async void Register()
            {
                // Ensure username is not empty and the password matches the confirmation password
                if (!string.IsNullOrEmpty(Username) && Password == ConfirmPassword)
                {
                    // Check if the username already exists in the registered users list
                    bool userExists = ListData.RegisteredNiggers.Any(user => user.Username == Username);

                    if (userExists)
                    {
                        // Show error if username already exists
                        Application.Current.MainPage.DisplayAlert("Error", "Username already exists", "OK");
                    }
                    else
                    {
                        // Add the new user to the registered users list
                        ListData.RegisteredNiggers.Add(new AppModel { Username = Username, Password = Password });
                        Application.Current.MainPage.DisplayAlert("Success", "Registration Successful", "OK");
                        await Application.Current.MainPage.Navigation.PopAsync(); // Go back to the previous page
                    }
                }
                else
                {
                    // Show error message if registration details are invalid
                    Application.Current.MainPage.DisplayAlert("Error", "Invalid Registration Details", "OK");
                }
            }

            // Event to notify property changes, implements INotifyPropertyChanged interface
            public event PropertyChangedEventHandler PropertyChanged;

            // Method to raise the PropertyChanged event for data binding
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
