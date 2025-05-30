using System.Windows.Forms;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public class MessageBoxHelper
    {
        public static DialogResult ShowErrorMessageBox(string message, string caption)
        {
            return ShowMessageBox(message, caption, MessageBoxIcon.Error);
        }

        public static DialogResult ShowErrorMessageBox(string message)
        {
            return ShowErrorMessageBox(message, Common.ProductName);
        }

        public static DialogResult ShowWarningMessageBox(string message, string caption)
        {
            return ShowMessageBox(message, caption, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowWarningMessageBox(string message)
        {
            return ShowWarningMessageBox(message, Common.ProductName);
        }


        public static DialogResult ShowMessageBox(string message, string caption, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
        }

        public static DialogResult ShowMessageBox(string message)
        {
            return ShowMessageBox(message, Common.ProductName, MessageBoxIcon.Information);
        }


        public static DialogResult ShowQuestionMessageBox(string message)
        {
            return MessageBox.Show(message, Common.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}