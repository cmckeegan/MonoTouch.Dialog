using System;
namespace MonoTouch.Dialog.Extensions
{
	
    public class RadioEntryElement: RadioElement {
        public RadioEntryElement(string caption): base(caption) {}
        public override void Selected (DialogViewController dvc, MonoTouch.UIKit.UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
        {
            base.Selected (dvc, tableView, path);
            dvc.NavigationController.PopViewControllerAnimated(true);
        }
    }
}

