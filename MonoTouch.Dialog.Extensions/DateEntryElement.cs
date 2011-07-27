using System;
using MonoTouch.UIKit;
namespace MonoTouch.Dialog.Extensions
{
	
	public enum DateEntryElementMode {
		Date,
		Time,
		DateTime
	}
	public class DateEntryElement: EditorEntryElement
		
	{
		
		
		
		private DateTime? dateValue;
		
		public DateTime? DateValue {
			get {
				return dateValue;
			}
			set {
				dateValue = value;
				Value = ConvertToString(dateValue);
			}
		}
		
		public DateEntryElementMode Mode {get; set;}
		
		private string ConvertToString(DateTime? value) {
			if (!value.HasValue) return null;
			
			switch (Mode) {
			case DateEntryElementMode.Date:
				return value.Value.ToShortDateString();
			case DateEntryElementMode.Time:
				return value.Value.ToShortTimeString();
			default:
				return value.Value.ToShortDateString() + " " + value.Value.ToShortTimeString();
			}
		}
		
		public DateEntryElement (string caption, DateTime? value): base(caption, null)
		{
			DateValue = value;
		}
		
		protected override EditorViewController CreateEditorController (DialogViewController dvc)
		{
			UIDatePickerMode pickerMode;
			switch (Mode) {
				case DateEntryElementMode.Date:
					pickerMode = UIDatePickerMode.Date;
				break;
				case DateEntryElementMode.Time:
					pickerMode = UIDatePickerMode.Time;
					break;
				default:
					pickerMode = UIDatePickerMode.DateAndTime;
					break;
			}
			
			return new DateEntryEditorController(this) {Mode = pickerMode};
		}
		
		private class DateEntryEditorController : EditorViewController<DateEntryElement, UIDatePicker> {
			public UIDatePickerMode Mode {get;set;}
			public DateEntryEditorController(DateEntryElement element): base(element) {
			}
			
			
			protected override void SetupEditor (UIDatePicker editor, DateEntryElement element)
			{
				base.SetupEditor (editor, element);
				editor.Mode = Mode;
				if (Element.DateValue.HasValue) {
					editor.Date = Element.DateValue;
				}
				editor.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleBottomMargin;
			}
			
			protected override void CopyValueFromEditor (UIDatePicker editor, DateEntryElement element)
			{
				element.DateValue = editor.Date;	
			}
			
			public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
			{
				base.WillRotate (toInterfaceOrientation, duration);
				//LayoutEditor(Editor);
			}
			
			protected override void LayoutEditor (UIDatePicker editor)
			{
				editor.Frame = new System.Drawing.RectangleF(0, (this.View.Frame.Height - Editor.Frame.Height)/2, this.View.Frame.Width, editor.Frame.Height);
			}	
		}
	}
}

