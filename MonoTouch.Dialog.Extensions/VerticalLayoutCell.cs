
using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
namespace MonoTouch.Dialog.Extensions
{
	public class VerticalLayoutCell: UITableViewCell {
		public const float BorderHeight = 8;
		public const float Padding = 4;
		public static SizeF MaxCellSize = new SizeF(260, float.MaxValue);
		
		public static readonly UIFont TextFont = UIFont.BoldSystemFontOfSize (17);
		public static readonly UIFont DetailTextFont = UIFont.SystemFontOfSize(UIFont.LabelFontSize);
		
		public VerticalLayoutCell(UITableViewCellStyle style, string reuseIdentifier): base(style, reuseIdentifier) {
			this.TextLabel.Lines = 0;
			this.TextLabel.Font = TextFont;
			
			if (this.DetailTextLabel!=null) {
				this.DetailTextLabel.Lines = 0;
				this.DetailTextLabel.TextColor = UIColor.FromRGB(0.22f, 0.33f, 0.53f);
				this.DetailTextLabel.Font = DetailTextFont;
			}
		}
		
		public VerticalLayoutCell(string reuseIdentifier): this(UITableViewCellStyle.Default, reuseIdentifier) {
		}
			
		
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();	
			
			float captionHeight = StringSize(this.TextLabel.Text, TextFont, MaxCellSize).Height;
			
			this.TextLabel.Frame = new RectangleF(this.TextLabel.Frame.X, BorderHeight ,this.TextLabel.Frame.Width, captionHeight);
			float y = this.TextLabel.Frame.Bottom;
			
			if (DetailTextLabel!=null) {
				SizeF detailSize = StringSize(this.DetailTextLabel.Text, DetailTextFont, MaxCellSize);
				this.DetailTextLabel.Frame = new RectangleF(this.TextLabel.Frame.X, y + Padding, detailSize.Width, detailSize.Height);
				
				y = this.DetailTextLabel.Frame.Bottom;
			}
			
			foreach (var v in this.ContentView.Subviews) {
				if (v==this.TextLabel) continue;
				if (v==this.DetailTextLabel) continue;
				v.Frame = new RectangleF(this.TextLabel.Frame.X, y + Padding, this.TextLabel.Frame.Width, v.Frame.Height);
				y = v.Frame.Bottom;
			}
		}
		
	}
}
