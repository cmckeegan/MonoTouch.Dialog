using System;
namespace MonoTouch.Dialog.Extensions
{
	public class MultiSelectEntryElement: SummaryRootElement {
        public MultiSelectEntryElement (string caption, params string[] options): base(caption) {
            var section = new SummarySection(caption, "Select all that apply");
            foreach(var s in options) {
                section.Add(new CheckboxElement(s));
            }
            Add(section);
        }
    }
}

