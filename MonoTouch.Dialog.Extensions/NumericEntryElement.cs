using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
namespace MonoTouch.Dialog.Extensions
{
	public class NumericEntryElement: TextEntryElement
	{
		public NumericEntryElement (string caption, string placeholder, string value): base(caption, placeholder, value)
		{
			this.KeyboardType = UIKeyboardType.DecimalPad;
		}
		
		public int DecimalPlaces {get;set;}
		
		protected override UITextField CreateTextField (UITableView tv)
		{
			var field = base.CreateTextField (tv);
			field.ShouldChangeCharacters += (tf, rg, s) => {
				bool ok = true;
				foreach (Char c in s) {
					if (Char.IsControl(c)) continue;
					if (Char.IsDigit(c)) continue;
					if (DecimalPlaces>0 && c=='.' && (tf.Text==null || !tf.Text.Contains("."))) continue;
					ok = false;
				}
				return ok;	
			};
			
			return field;
		}
	}
}

