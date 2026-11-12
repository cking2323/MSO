using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO.GUI
{
    internal class OutputControl : Control
    {
        public TextBox textBox;
        public FlowLayoutPanel flowLayoutPanel;
        FacadeGUI FacadeGUI;


        public OutputControl(FacadeGUI facadeGUI)
        {
            FacadeGUI = facadeGUI;

            textBox = new TextBox();
            flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.Height = 30;
            flowLayoutPanel.Location = new Point(0, 0);

            RoundedButton run = new RoundedButton();
            run.Text = "Run";
            run.BackColor = Color.FromArgb(59, 250, 52);
            run.Name = "run";
            flowLayoutPanel.Controls.Add(run);

            RoundedButton met = new RoundedButton();
            met.Text = "Metrics";
            met.BackColor = Color.FromArgb(255, 159, 41);
            met.Name = "met";
            flowLayoutPanel.Controls.Add(met);

            textBox.Location = new Point(0, flowLayoutPanel.Bottom + 5);
            textBox.Width = this.Width;
            textBox.Height = this.Height - flowLayoutPanel.Height - 15;
            textBox.Size = new Size(350, 140);
            textBox.BackColor = Color.Lavender;
            textBox.Multiline = true;
            textBox.PlaceholderText = "<output>";
            textBox.ReadOnly = true;


            this.Controls.Add(flowLayoutPanel);
            this.Controls.Add(textBox);
            FacadeGUI = facadeGUI;
        }

        public void Display(string output)
        {
            this.textBox.Text = output;
        }

        public void SetText(string text)
        {
            this.textBox.Clear();
            UI.Print(text.Replace("\n", Environment.NewLine), this.textBox);
        }
    }

    public class RoundedButton : Button
    {
        public int Radius { get; set; } = 15;
        protected override void OnPaint(PaintEventArgs pevent)  
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(Parent.BackColor);


            using var path = new System.Drawing.Drawing2D.GraphicsPath();
            int d = Radius * 2;
            path.AddArc(0, 0, d, d, 180, 90);
            path.AddArc(Width - d - 1, 0, d, d, 270, 90);
            path.AddArc(Width - d - 1, Height - d - 1, d, d, 0, 90);
            path.AddArc(0, Height - d - 1, d, d, 90, 90);
            path.CloseFigure();

            using var brush = new SolidBrush(BackColor);
            pevent.Graphics.FillPath(brush, path);

            TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, ForeColor);
        }
    }


}
