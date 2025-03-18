using System;
using System.Timers;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using System.Collections.ObjectModel;

namespace JoseNi.MVVM.Views;

public partial class WelcomePage : ContentPage
{
    private int _currentPosition = 0;
    private System.Timers.Timer _timer;
    private System.Timers.Timer _scrollTimer;
    private int _currentIndex = 0;
    private int _totalItems = 0;

    public ObservableCollection<string> Items { get; set; }


    public WelcomePage()
	{
		InitializeComponent();
        // Initialize Timer


        Items = new ObservableCollection<string>
            {
                "Breaking News: New feature added!",
                "Welcome to UnderSec!",
                "Secure your data with privacy tools!",
                "Latest updates on cybersecurity!",
                "Join our community today!"
            };


        BindingContext = this;
        _totalItems = Items.Count;

        StartAutoScroll();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(100);
        imgLoader.IsAnimationPlaying = true;
    }
    private void StartAutoScroll()
    {
        _scrollTimer = new System.Timers.Timer(2000); // Change speed here (2 sec per item)
        _scrollTimer.Elapsed += (sender, e) => ScrollNext();
        _scrollTimer.AutoReset = true;
        _scrollTimer.Enabled = true;
    }

    private void ScrollNext()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            _currentIndex++;
            if (_currentIndex >= _totalItems)
            {
                _currentIndex = 0; // Loop back to first item
            }

            marqueeCarousel.ScrollTo(_currentIndex, position: ScrollToPosition.Center, animate: true);
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