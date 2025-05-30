using System;
using CnSharp.VisualStudio.NuPack.Models;
using System.Windows.Forms;

namespace CnSharp.VisualStudio.NuPack.Controls
{
    public partial class NuGetPackOptionsControl : UserControl
    {
        public NuGetPackOptionsControl()
        {
            InitializeComponent();
        }

        public event EventHandler SymbolsCheckedChanged;

        public PackArgs PackArgs
        {
            set
            {
                checkBoxSymbols.DataBindings.Add("Checked", value, "IncludeSymbols");
                checkBoxIncludeReferencedProjects.DataBindings.Add("Checked", value, "IncludeReferencedProjects");
            }
        }

        private void checkBoxSymbols_CheckedChanged(object sender, System.EventArgs e)
        {
            SymbolsCheckedChanged?.Invoke(checkBoxSymbols, e);
        }
    }
}
