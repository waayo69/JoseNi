using Microsoft.Data.SqlClient;

namespace JoseNi.MVVM.Views;

public partial class LoginPage : ContentPage
{
    private string connectionString = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=waayo69_Clients;User ID=waayo69_Clients;Password=kris123asd;Encrypt=False; Connection Timeout=30";
    private int _currentPosition = 0;
    private System.Timers.Timer _timer;
    public LoginPage()
	{
		InitializeComponent();
        // Delay execution until UI is fully loaded
        BindingContext = new ViewModels.LoginPageClass();
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

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        PasswordEntry.IsPassword = !e.Value;
    }
}