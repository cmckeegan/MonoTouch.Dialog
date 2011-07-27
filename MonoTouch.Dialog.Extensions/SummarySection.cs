using System;
namespace MonoTouch.Dialog.Extensions
{
	public class SummarySection: Section
	{
		public SummarySection (string caption, string footer): base(caption, footer)
		{
		}
		
		public override string Summary ()
		{
			string summary = "";
			foreach (Element e in this) {
				var cb = e as CheckboxElement;
				if (cb == null) continue;
				
				if (cb.Value)
					summary +=  ((summary=="")?"":", ") + cb.Caption;
			}
			
			return summary;
		}
	}
}

