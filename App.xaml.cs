using JoseNi.MVVM.ViewModels;
using JoseNi.MVVM.Views;

namespace JoseNi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SplashPage());
        }
    }
}
