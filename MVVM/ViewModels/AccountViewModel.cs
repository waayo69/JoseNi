using JoseNi.MVVM.Models;
using JoseNi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JoseNi.MVVM.Views;
using System.Text.RegularExpressions;

namespace JoseNi.MVVM.ViewModels
{
    internal class AccountViewModel : INotifyPropertyChanged
    {
        //var dec
        public User user { get; set; }
        private string _fname {get; set;}
        private string _lname {get; set;}
        private string _email {get; set;}
        private int _age {get; set;}
        private string _gender {get; set;}
        private DateTime _dob {get; set;}
        private string _password {get; set;}
        private string _username {get; set;}

        public ICommand LoginButton => new Command(Login);
        public ICommand RegisterButton => new Command(Register);
        public ICommand ContinueButton => new Command(OnContinue);


        public AccountViewModel()
        {
            user = new User();
        }
        public DateTime DoB
        {
            get => _dob;
            set
            {
                _dob = value;
                OnPropertyChanged(nameof(DoB)); 
            }
        }

        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string FName
        {
            get => _fname;
            set
            {
                _fname = value;
                OnPropertyChanged(nameof(FName));
            }
        }
        public string LName
        {
            get => _lname;
            set
            {
                _lname = value;
                OnPropertyChanged(nameof(LName));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => _password;
            set 
            { 
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private async void OnContinue()
        {
           

            FakeDb.Users.Add(new User { FirstName = FName, LastName = LName, DoB = DoB, Gender = Gender, Age = Age});
            //DoB = DateOnly.FromDateTime(DoB.DateTime);
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage
            {
                BindingContext = this
            });
        }
        private void Login()
        {
            //Check if username and password exists
            if (FakeDb.Users.Any(u => u.Username == Username && u.Password == Password))
            {
                App.Current.MainPage.DisplayAlert("Welcome", "Wazgud Cuh", "Nigga");
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Login", "Login Unsuccessful\nInvalid Email/Phone Number or Password", "OK");
                App.Current.MainPage = new NavigationPage(new WelcomePage());
            }
        }
        private void Register()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                App.Current.MainPage.DisplayAlert("Register", "Please Enter Fields Properly", "OK");
                return; // Exit the method if fields are empty
            }

            // Validate Email/Phone
            ValidateEmail();
            if (string.IsNullOrEmpty(Email))
            {
                App.Current.MainPage.DisplayAlert("Register", "EMAIL EPMTY!", "OK");
                return; // Exit if Email/Phone validation fails
            }

            // Validate Password
            if (!ValidatePassword())
            {
                App.Current.MainPage.DisplayAlert("Register", "Invalid Password", "OK");
                return; // Exit if Password validation fails
            }

            // Check if username already exists
            if (FakeDb.Users.Any(u => u.Email == Email || u.Username== Username))
            {
                App.Current.MainPage.DisplayAlert("Register", "Account Already Exists", "OK");
                return; // Exit if account already exists
            }

            

            // Add user/register
            FakeDb.Users.Add(new User { Email = Email, Username = Username, Password = Password });
            App.Current.MainPage.DisplayAlert("Register", "Account Registered", "OK");
            App.Current.MainPage = new NavigationPage(new WelcomePage());
        }
        public void ValidateEmail()
        {
            if (string.IsNullOrEmpty(Email)) 
            {
                App.Current.MainPage.DisplayAlert("Error", "Email Cannot be Empty", "OK");
            }
            else if (IsEmail(Email))
            {
                ValidateEmail1();
            }
        }
        private void ValidateEmail1()
        {
            if (!IsEmail(Email))
            {
                App.Current.MainPage.DisplayAlert("Error", "Invalid Email Format", "OK");
            }
           
        }
        private bool ValidatePassword()
        {
            if (string.IsNullOrEmpty(Password))
            {
                App.Current.MainPage.DisplayAlert("Restriction Error", "Password cannot be empty.", "OK");
                return false;
            }

            if (Password.Length < 8)
            {
                App.Current.MainPage.DisplayAlert("Restriction Error", "Password must be at least 8 characters long.", "OK");
                
                return false;
            }

            if (!Regex.IsMatch(Password, @"[A-Z]"))
            {
                App.Current.MainPage.DisplayAlert("Restriction Error", "Password must contain at least one uppercase letter.", "OK");
                return false;
            }

            if (!Regex.IsMatch(Password, @"[a-z]"))
            {
                App.Current.MainPage.DisplayAlert("Restriction Error", "Password must contain at least one lowercase letter.", "OK");
                return false;
            }
           

            if (!Regex.IsMatch(Password, @"[0-9]"))
            {
                App.Current.MainPage.DisplayAlert("Restriction Error", "Password must contain at least one number.", "OK");
                return false;
            }

            if (!Regex.IsMatch(Password, @"[\W_]"))
            {
                App.Current.MainPage.DisplayAlert("Restriction Error", "Password must contain at least one special character.", "OK");

                return false;
            }

           
            return true;
        }

        private bool IsEmail(string input)
        {
            // A simple check for email format
            return Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
