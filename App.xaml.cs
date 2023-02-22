namespace MauiCalculator;

using Android.Content;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

        //MobileAds.Initialize(Context); // Replace Context with your Android activity or iOS root view controller
    }
}
