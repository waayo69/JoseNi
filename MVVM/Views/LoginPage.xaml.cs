using Microsoft.Data.SqlClient;
using System.Linq;
using JoseNi.MVVM.ViewModels;
using Microsoft.Maui.Controls;

namespace JoseNi.MVVM.Views;

public partial class LoginPage : ContentPage
{
    private int _currentPosition = 0;
    private System.Timers.Timer _timer;
    private readonly SignUpViewModel _signUpViewModel;

    public LoginPage()
	{
        InitializeComponent();
        // Delay execution until UI is fully loaded
        BindingContext = new ViewModels.LoginPageClass();
        _signUpViewModel = new SignUpViewModel();
        BindingContext = _signUpViewModel;
        this.Loaded += (s, e) => StartScrolling();
    }
    private void StartScrolling()
    {
        _timer = new System.Timers.Timer(2000); // 1 sec interval
        _timer.Elapsed += (s, e) => MoveNext();
        _timer.AutoReset = true;
        _timer.Start();

    }

    private void MoveNext()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (Cview.ItemsSource is not null)
            {
                int count = ((string[])Cview.ItemsSource).Length;
                _currentPosition = (_currentPosition + 1) % count; // Loop back to 0
                Cview.Position = _currentPosition;
            }
        });
    }

    //private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    //{
    //    PasswordEntry.IsPassword = !e.Value;
    //}

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        // Find the user by matching username and password
        var user = _signUpViewModel.Users.FirstOrDefault(u =>
            u.Username == _signUpViewModel.CurrentUser.Username &&
            u.Password == _signUpViewModel.CurrentUser.Password);

        if (user != null)
        {
            // If user is found, navigate to HomePage
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            // Show an error message if login fails
            await DisplayAlert("Login Failed", "Invalid Username or Password", "OK");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Find the user by matching username and password
        var user = _signUpViewModel.Users.FirstOrDefault(u =>
            u.Username == _signUpViewModel.CurrentUser.Username &&
            u.Password == _signUpViewModel.CurrentUser.Password);

        if (user != null)
        {
            // If user is found, navigate to HomePage
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            // Show an error message if login fails
            await DisplayAlert("Login Failed", "Invalid Username or Password", "OK");
        }
    }
}