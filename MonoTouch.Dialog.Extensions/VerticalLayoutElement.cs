using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
namespace MonoTouch.Dialog.Extensions
{
	public class VerticalLayoutElement: Element, IElementSizing
	{
		protected UITableViewCellStyle CellStyle {get; set;}
		public virtual string Value {get; set;}
		
		public VerticalLayoutElement (string caption, string value): base(caption) {
			Value = value;	
		}
		
		public override UIKit.UITableViewCell GetCell (UITableView tv)
		{
			CellStyle = String.IsNullOrEmpty(Value) ? UITableViewCellStyle.Default : UITableViewCellStyle.Subtitle;
			string key = "vle_"+CellStyle.ToString();
			var cell = tv.DequeueReusableCell(key) as VerticalLayoutCell;
			if (cell == null) {
				cell = new VerticalLayoutCell(CellStyle, key);
			}
			
			cell.TextLabel.Text = Caption;
			
			if (!String.IsNullOrEmpty(Value))
				cell.DetailTextLabel.Text = Value;
			
			return cell;
		}
		
		protected virtual float GetHeight(UITableView tableView) {
			float captionHeight = tableView.StringSize(Caption, VerticalLayoutCell.TextFont, VerticalLayoutCell.MaxCellSize).Height;
			if (!String.IsNullOrEmpty(Value)) {
				float valueHeight = tableView.StringSize(Value, VerticalLayoutCell.DetailTextFont, VerticalLayoutCell.MaxCellSize).Height;
				if (valueHeight > 0)
					captionHeight += (valueHeight + VerticalLayoutCell.Padding) ;
			}
			return captionHeight;
		}
	

		public float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			float captionHeight = GetHeight(tableView);
			return VerticalLayoutCell.BorderHeight + captionHeight + VerticalLayoutCell.BorderHeight;
		}
	}
}

