using System;
using MonoTouch.UIKit;
namespace MonoTouch.Dialog.Extensions
{
	public class SummaryRootElement: RootElement, IElementSizing
	{
		public SummaryRootElement (string caption): base(caption, 0 , -1)
		{
		}
		
		public SummaryRootElement(string caption, Group group): base(caption, group) {
			
		}
		
		
		private const string rkey = "summary-root-"; 
		
		protected override UITableViewCell CreateCell (UIKit.UITableView tv)
		{
			string summary = GetSummary(tv);
			var cellStyle = (String.IsNullOrEmpty(summary))?UITableViewCellStyle.Default : UITableViewCellStyle.Subtitle;
			string key = rkey + (cellStyle.ToString());
			var cell = tv.DequeueReusableCell(key) as VerticalLayoutCell;
			if (cell == null) {
				cell = new VerticalLayoutCell(cellStyle, key);	
			}
			
			return cell;
		}
		
		public float GetHeight (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			
			float captionHeight = tableView.StringSize(Caption, VerticalLayoutCell.TextFont, VerticalLayoutCell.MaxCellSize).Height;
			string summary = GetSummary(tableView);
			if (!String.IsNullOrEmpty(summary)) {
				float valueHeight = tableView.StringSize(summary, VerticalLayoutCell.DetailTextFont, VerticalLayoutCell.MaxCellSize).Height;
				if (valueHeight > 0)
					captionHeight += (valueHeight + VerticalLayoutCell.Padding) ;
			}
			return VerticalLayoutCell.BorderHeight + captionHeight + VerticalLayoutCell.BorderHeight;
		}
	}
}

