
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonoTouch.Dialog.Extensions;

namespace Sample.Extensions
{
	public class Application
	{
		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			
			window.AddSubview (navigation.View);

			var menu = new RootElement ("Extensions"){
				new Section ("Elements"){
					new TextEntryElement ("Type in some text to respond to this overly wordy question?", "Type here", null),
					new NumericEntryElement("Enter a number", "Type here", null),
					new NumericEntryElement("Enter a decimal", "Type here", null) {DecimalPlaces = 2},
					
					new MultiLineTextEntryElement("This one allows you to enter more text than you might imagine", null),
					new DateEntryElement("Enter your Date of Birth", null),
					new DateEntryElement("Enter your Time of Birth", null) {Mode = DateEntryElementMode.Time},
					new DateEntryElement("Enter your Date & Time of Birth", null) {Mode = DateEntryElementMode.DateTime},
					new MultiSelectEntryElement("Pick something cool from this list or very cool things", "Ice", "Fish", "Water", "Dogs", "Monkey", "Cat", "Walrus", "Parrot"),
					new MultipleChoiceEntryElement("Which of these things would you like to choose only one of", "Monkey", "Cat", "Walrus", "Parrot", "Slot", "Mouse")
				}
			};

			var dv = new DialogViewController (menu) {
				Autorotate = true
			};
			navigation.PushViewController (dv, true);
			
			window.MakeKeyAndVisible ();
			
			return true;
		}

		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{
		}
	}
}

