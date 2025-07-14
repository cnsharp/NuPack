using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CnSharp.VisualStudio.NuPack.Controls
{
    public static class ControlExtensions
    {
        public static void SetPlaceHolder(this TextBox textBox, string placeholder, Color foreColor)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = foreColor;
            textBox.GotFocus += (sender, args) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = string.Empty;
                    textBox.ForeColor = SystemColors.WindowText;
                }
            };
            textBox.LostFocus += (sender, args) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = foreColor;
                }
            };
        }

        public static Control AttachPlaceHolder(this Control textBox, string placeholder, Color foreColor)
        {
            var label = new Label
            {
                AutoSize = true,
                Text = placeholder,
                ForeColor = foreColor,
                BackColor = textBox.BackColor,
                Font = new Font(textBox.Font.FontFamily, textBox.Font.Size)
            };
            label.Location = new Point(textBox.Location.X + 4,
                textBox.Location.Y + (textBox.ClientSize.Height - label.Font.Height) / 2 + 2);
            label.Click += (sender, args) =>
            {
                label.Hide();
                textBox.Focus();
            };
            textBox.Parent.Controls.Add(label);
            label.BringToFront();
            textBox.GotFocus += (sender, args) =>
            {
                label.Hide();
            };
            textBox.LostFocus += (sender, args) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    label.Show();
                }
            };
            return label;
        }

        public static Control AttachCloseButton(this ComboBox comboBox)
        {
            var button = new PictureBox
            {
                Size = new Size(comboBox.ClientSize.Height - 8, comboBox.ClientSize.Height - 8),
                Image = Resource.close, 
                BackColor = comboBox.BackColor,
                BorderStyle = BorderStyle.None,
                TabStop = false,
                Cursor = Cursors.Default,
                Text = "X"
            };
            button.Location = new Point(comboBox.ClientSize.Width - button.Width - 2, 4);
            button.Click += (sender, args) => comboBox.SelectedIndex = -1;
            comboBox.Controls.Add(button);
            return button;
        }
         
        public static void AttachHelpButton(this Control control, EventHandler eventHandler)
        {
            var button = InitHelpButton(control);
            button.Click += eventHandler;
            control.Parent.Controls.Add(button);
        }

        public static void AttachHelpButton(this Control control, string url)
        {
            var button = InitHelpButton(control);
            if(!string.IsNullOrWhiteSpace(url))
                button.Click += (sender, args) => Process.Start(url);
            control.Parent.Controls.Add(button);
        }

        private static PictureBox InitHelpButton(Control control)
        {
            return new PictureBox
            {
                Size = new Size(16,16),
                Image = Resource.HelpIndexFile,
                BorderStyle = BorderStyle.None,
                TabStop = false,
                Cursor = Cursors.Hand,
                Text = "?",
                Location = new Point(control.Right + 1, control.Top)
            };
        }
    }
}