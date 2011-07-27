using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
namespace MonoTouch.Dialog.Extensions
{
	public class TextEntryElement : Element, IElementSizing
	{

		string val;
		private float cellHeight=-1;

		public UIKeyboardType KeyboardType = UIKeyboardType.Default;

		static NSString ekey = new NSString ("TextEntryElement");
		bool isPassword, becomeResponder;
		UITextField textField;
		string placeholder;

		public event EventHandler Changed;

		public TextEntryElement (string caption, string placeholder, string value) : base(caption)
		{
			Value = value;
			this.placeholder = placeholder;
		}

		public TextEntryElement (string caption, string placeholder, string value, bool isPassword) : base(caption)
		{
			Value = value;
			this.isPassword = isPassword;
			this.placeholder = placeholder;
		}

		public string Value {
			get { return val; }
			set {
				val = value;
				if (textField != null)
					textField.Text = value;
			}
		}

		public override string Summary ()
		{
			return Value;
		}
		
		protected virtual UITextField CreateTextField(UITableView tv) {
			
			SizeF size = tv.StringSize("Wg", VerticalLayoutCell.TextFont);
			var resultField = new UITextField (new RectangleF(0,0,size.Width, size.Height)) { Tag = 1, Placeholder = placeholder ?? "", SecureTextEntry = isPassword };
			resultField.Text = Value ?? "";
			resultField.TextColor = UIColor.FromRGB(0.22f, 0.33f, 0.53f);
			//textField.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleLeftMargin;
			
			resultField.ValueChanged += delegate { FetchValue (); };
			resultField.Ended += delegate { FetchValue (); };
			resultField.ShouldReturn += delegate {
				TextEntryElement focus = null;
				foreach (var e in (Parent as Section).Elements) {
					if (e == this)
						focus = this; 
					else if (focus != null && e is TextEntryElement) {
						focus = e as TextEntryElement;
						break;
					}
				}
				if (focus != this)
					focus.textField.BecomeFirstResponder ();
				else
					focus.textField.ResignFirstResponder ();
				
				return true;
			};
			resultField.Started += delegate {
				TextEntryElement self = null;
				var returnType = UIReturnKeyType.Default;
				
				foreach (var e in (Parent as Section).Elements) {
					if (e == this)
						self = this; else if (self != null && e is EntryElement)
						returnType = UIReturnKeyType.Next;
				}
				resultField.ReturnKeyType = returnType;
			};
			return resultField;
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (ekey) as VerticalLayoutCell;
			if (cell == null) {
				cell = new VerticalLayoutCell (ekey);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			} 
			else
				RemoveTag(cell, 1);
			
			if (textField == null) {
				textField = CreateTextField(tv);
			}
						
			if (becomeResponder){
				textField.BecomeFirstResponder ();
				becomeResponder = false;
			}
			
			textField.KeyboardType = KeyboardType;
			cell.TextLabel.Text = Caption;
			cell.ContentView.AddSubview (textField);
			return cell;
		}

		/// <summary>
		///  Copies the value from the currently entry UIView to the Value property and raises the Changed event if necessary.
		/// </summary>
		public void FetchValue ()
		{
			if (textField == null)
				return;
			
			var newValue = textField.Text;
			var diff = newValue != Value;
			Value = newValue;
			
			if (diff) {
				if (Changed != null)
					Changed (this, EventArgs.Empty);
			}
		}


		public override bool Matches (string text)
		{
			return (Value != null ? Value.IndexOf (text, StringComparison.CurrentCultureIgnoreCase) != -1 : false) || base.Matches (text);
		}

		/// <summary>
		/// Makes this cell the first responder (get the focus)
		/// </summary>
		/// <param name="animated">
		/// Whether scrolling to the location of this cell should be animated
		/// </param>
		public void BecomeFirstResponder (bool animated)
		{
			becomeResponder = true;
			var tv = GetContainerTableView ();
			if (tv == null)
				return;
			tv.ScrollToRow (IndexPath, UITableViewScrollPosition.Middle, animated);
			if (textField != null) {
				textField.BecomeFirstResponder ();
				becomeResponder = false;
			}
		}

		public void ResignFirstResponder (bool animated)
		{
			becomeResponder = false;
			var tv = GetContainerTableView ();
			if (tv == null)
				return;
			tv.ScrollToRow (IndexPath, UITableViewScrollPosition.Middle, animated);
			if (textField != null)
				textField.ResignFirstResponder ();
		}
	

		public float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			if (this.cellHeight == -1) {
				cellHeight = tableView.StringSize(Caption, VerticalLayoutCell.TextFont, VerticalLayoutCell.MaxCellSize).Height;
			}
			
			SizeF size = tableView.StringSize("Wg", VerticalLayoutCell.TextFont);
			
			return VerticalLayoutCell.BorderHeight + cellHeight + VerticalLayoutCell.Padding + size.Height + VerticalLayoutCell.BorderHeight;
		}
}
	
	
	
}

