using System;
using System.Timers;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;

namespace JoseNi.MVVM.Views;

public partial class WelcomePage : ContentPage
{
    private int _currentPosition = 0;
    private System.Timers.Timer _timer;

    public WelcomePage()
	{
		InitializeComponent();
        // Initialize Timer

        // Delay execution until UI is fully loaded
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
    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    private async void btnSignUp_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}