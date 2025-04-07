using JoseNi.MVVM.ViewModels;

namespace JoseNi.MVVM.Views;

public partial class SignUpPage : ContentPage
{
	//private Registration _registration = new Registration();
    public SignUpPage()
	{
		InitializeComponent();
	}

    private async void btnNext_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage1());
    }

    private void btnNext_Clicked_1(object sender, EventArgs e)
    {

    }
}