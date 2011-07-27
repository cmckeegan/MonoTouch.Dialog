using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace MonoTouch.Dialog.Extensions
{
	public class MultiLineTextEntryElement : EditorEntryElement {
		
		public MultiLineTextEntryElement (string caption, string value) : base (caption, value)	{
		}	
		
		protected override EditorViewController CreateEditorController (DialogViewController dvc)
		{
			return new MultiLineTextEditorController (this);
		}
		
		private class MultiLineTextEditorController: EditorViewController<MultiLineTextEntryElement, UITextView> {
			public MultiLineTextEditorController(MultiLineTextEntryElement container): base(container) {
				
			}
			
			NSObject keyBoardShowObserver;
			NSObject keyboardHideObserver;
			bool keyboardPopped;
			RectangleF keyRect;
			
			public override void ViewDidLoad ()
			{
				keyBoardShowObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, (ns)=>{
					if (keyboardPopped) return;
					keyboardPopped = true;
					keyRect = UIKeyboard.BoundsFromNotification(ns);	
					this.LayoutEditor(Editor);
				});
				keyboardHideObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, (ns)=> {
					if (!keyboardPopped) return;
					keyboardPopped = false;
					keyRect = UIKeyboard.BoundsFromNotification(ns);	
					this.LayoutEditor(Editor);
					
				});
				base.ViewDidLoad ();
			}
			
			public override void ViewDidUnload ()
			{
				base.ViewDidUnload ();
				NSNotificationCenter.DefaultCenter.RemoveObserver(keyBoardShowObserver);
				NSNotificationCenter.DefaultCenter.RemoveObserver(keyboardHideObserver);
			}
			
			protected override void CopyValueFromEditor (UITextView editor, MultiLineTextEntryElement element)
			{
				element.Value = editor.Text;
			}
			
			protected override void SetupEditor (UITextView editor, MultiLineTextEntryElement element)
			{
				base.SetupEditor (editor, element);
				editor.Layer.CornerRadius = 10f;
				editor.Font = UIFont.SystemFontOfSize(UIFont.LabelFontSize);
				editor.BecomeFirstResponder();
				editor.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
				editor.Text = element.Value;
			}
			
			protected override void LayoutEditor (UITextView editor)
			{				
				var rect = new RectangleF(10,10,View.Frame.Width - 20, View.Frame.Height - 20 - keyRect.Height);
				
				editor.Frame = rect;
			} 
		}
	}
}

