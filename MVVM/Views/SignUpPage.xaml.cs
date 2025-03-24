using JoseNi.MVVM.ViewModels;

namespace JoseNi.MVVM.Views;

public partial class SignUpPage : ContentPage
{
	private Registration _registration = new Registration();
    public SignUpPage()
	{
		InitializeComponent();
	}

    private async void btnCreateAcc_Clicked(object sender, EventArgs e)
    {
        string result = _registration.Register(Fname.Text, Lname.Text, regEmail.Text, regUserName.Text, regPass.Text);

        switch (result)
        {
            case "Registration successful":
                await DisplayAlert("Success", result, "OK");
                ClearForm();
                await Navigation.PushAsync(new WelcomePage());
                break;

            case "Username already exists":
                await DisplayAlert("Error", "This username is already taken. Please choose another.", "OK");
                regPass.Text = string.Empty;
                break;

            case "Please fill in all fields":
                await DisplayAlert("Error", "All fields are required. Please fill them in.", "OK");
                regPass.Text = string.Empty;
                break;

            case "Registration failed":
                await DisplayAlert("Error", "An error occurred while creating your account. Please try again.", "OK");
                regPass.Text = string.Empty;
                break;
        }
    }
    private void ClearForm()
    {
        Fname.Text = string.Empty;
        Lname.Text = string.Empty;
        regEmail.Text = string.Empty;
        regUserName.Text = string.Empty;
        regPass.Text = string.Empty;
    }
}