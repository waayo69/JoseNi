using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace JoseNi.MVVM.Views;

public partial class SplashPage : ContentPage
{
	public SplashPage()
	{
		InitializeComponent();
	}
    private async void OnMediaEnded(object sender, EventArgs e)
    {
        // Navigate to the main page after video ends
        await Navigation.PushAsync(new MainPage());
    }
    private void OnMediaOpened(object sender, EventArgs e)
    {
        Console.WriteLine("✅ Video Loaded Successfully!");
    }

    private void OnMediaFailed(object sender, EventArgs e)
    {
        Console.WriteLine("❌ Failed to Load Video!");
    }
}