using MSO.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO
{
    public class CodeBoxControl : UserControl
    {
        private TextBox textBox;

        internal CodeBoxControl()
        {
            this.Size = new Size(350, 450);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;

            textBox = new TextBox();
            textBox.Multiline = true;
            textBox.Dock = DockStyle.Fill;
            textBox.ScrollBars = ScrollBars.Both;
            textBox.AcceptsTab = true;

            this.Controls.Add(textBox);
        }

        internal void SetText(string text)
        {
            textBox.Text = text;
        }

        internal string GetText()
        {
            return textBox.Text;
        }
    }
}
