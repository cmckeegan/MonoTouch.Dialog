using System;
namespace MonoTouch.Dialog.Extensions
{
	public class MultipleChoiceEntryElement: SummaryRootElement {
        public MultipleChoiceEntryElement (string caption, params string[] options): base(caption, new RadioGroup(-1)) {
            var section = new Section(caption);
            foreach(var s in options) {
                section.Add(new RadioEntryElement(s));
            }
            Add(section);
        }
    }
}

