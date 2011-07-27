using System;
using MonoTouch.UIKit;
using System.Drawing;
namespace MonoTouch.Dialog.Extensions
{
	public abstract class EditorViewController: UIViewController {
		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
		}
		
		public bool Autorotate { get; set; }
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return Autorotate;
		}	
	}
	
	public abstract class EditorViewController<TElement, TEditorView> : EditorViewController where TElement : Element where TEditorView : UIView, new() {
		protected TElement Element {get; private set;}
		protected TEditorView Editor {get; private set;}
		
		public EditorViewController (TElement element)
		{
			this.Element = element;
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.GroupTableViewBackgroundColor;
			Editor = new TEditorView();
			SetupEditor(Editor, Element);
			LayoutEditor(Editor);
			this.View.AddSubview(Editor);
		}
		
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (disposing) {
				if (Editor == null) return;
				
				Editor.RemoveFromSuperview();
				Editor.Dispose();
				Editor = null;
			}
		}
		
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			
			if (confirmed)
				CopyValueFromEditor(Editor, Element);
		}
		
		
		UIBarButtonItem doneButton;
		bool confirmed;
		
		protected abstract void CopyValueFromEditor(TEditorView editor, TElement element);
		protected abstract void LayoutEditor(TEditorView editor);
		protected virtual void SetupEditor(TEditorView editor, TElement element) {
			
				doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done);
				doneButton.Clicked += (s,e) => {
					confirmed = true;
					this.NavigationController.PopViewControllerAnimated(true);
				};
					
				this.NavigationItem.SetRightBarButtonItem(doneButton, false);
		}
	}
}

