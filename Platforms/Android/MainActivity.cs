using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Identity.Client;

namespace BlazorHybridCustomAuthentication
{
	[Activity(Theme = "@style/Maui.SplashTheme", 
        MainLauncher = true,
        SupportsPictureInPicture = true,
        HardwareAccelerated = true,
        LaunchMode = LaunchMode.SingleInstance,
        Exported = true,
        ResizeableActivity = true,
        AllowEmbedded = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]

    // Specify configuration changes the activity can handle
    public class MainActivity : MauiAppCompatActivity
	{
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            // Call the base class implementation
            base.OnActivityResult(requestCode, resultCode, data); 

            // Handle MSAL authentication continuation for Android platform
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}
