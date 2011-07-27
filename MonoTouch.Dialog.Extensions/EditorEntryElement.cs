using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
namespace MonoTouch.Dialog.Extensions
{
	public abstract class EditorEntryElement : VerticalLayoutElement
	{
		
		public EditorEntryElement (string caption, string value) : base (caption, value)	{
			CellStyle = UITableViewCellStyle.Default;
		}	
		
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = base.GetCell (tv);
			cell.SelectionStyle = UITableViewCellSelectionStyle.Blue;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			return cell;
		}
		
		protected abstract EditorViewController CreateEditorController(DialogViewController dvc);
 
 		
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
			var vc = CreateEditorController(dvc);
			vc.Autorotate = true; 
			dvc.ActivateController (vc);
		}
	}
}

