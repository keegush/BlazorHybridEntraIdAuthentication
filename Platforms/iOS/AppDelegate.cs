using Foundation;
using Microsoft.Identity.Client;
using UIKit;

namespace BlazorHybridCustomAuthentication
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        // Handles the URL callback from the authentication flow
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            // Set the authentication continuation arguments for MSAL
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);

            // Continue with the base implementation
            return base.OpenUrl(app, url, options);
        }
    }
}
