using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Microsoft.Maui.Dispatching;
using System.Reflection;

namespace JoseNi.MVVM.Views;

public partial class SplashPage : ContentPage
{
	public SplashPage()
	{
		InitializeComponent();
        //NavigationPage.SetHasNavigationBar(this, false);
        LoadEmbeddedVideo();

    }
    private void LoadEmbeddedVideo()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "JoseNi.Resources.Raw.qqqq.mp4";
        using(var stream= assembly.GetManifestResourceStream(resourceName))
        {
            if (stream != null)
            {
                var filepath = Path.Combine(FileSystem.CacheDirectory, "qqqq.mp4");
                using(var fileStream = File.Create(filepath))
                {
                    stream.CopyTo(fileStream);
                }
                SplashVideo.Source = filepath;
            }
            else
            {
                Console.WriteLine("not found");
            }
        }
    }

    private async void SplashVideo_MediaEnded(object sender, EventArgs e)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            // Ensure navigation happens on the UI thread
            await Navigation.PushAsync(new WelcomePage());
        });
    }
}