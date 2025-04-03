using JoseNi.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using JoseNi.MVVM.Models;
namespace JoseNi.MVVM.Views;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }

    private async void btnNext_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        if (BindingContext is SignUpViewModel vm)
        {
            vm.CurrentUser.DateOfBirth = e.NewDate;
        }
    }
}
