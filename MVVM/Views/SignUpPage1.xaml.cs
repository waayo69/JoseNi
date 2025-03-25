namespace JoseNi.MVVM.Views;

public partial class SignUpPage1 : ContentPage
{
	public SignUpPage1()
	{
		InitializeComponent();
	}

	private async void btnCreateAcc_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
    }
}