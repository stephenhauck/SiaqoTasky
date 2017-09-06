using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace SiaqoTasky.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            DevExpress.Mobile.Forms.Init();
            Corcav.Behaviors.Infrastructure.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
