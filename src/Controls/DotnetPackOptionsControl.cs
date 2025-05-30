using System;
using CnSharp.VisualStudio.NuPack.Models;
using System.Windows.Forms;

namespace CnSharp.VisualStudio.NuPack.Controls
{
    public partial class DotnetPackOptionsControl : UserControl
    {
        public DotnetPackOptionsControl()
        {
            InitializeComponent();
        }

        public event EventHandler SymbolsCheckedChanged;

        public PackArgs PackArgs
        {
            set
            {
                checkBoxIncludeSymbols.DataBindings.Add("Checked", value, "IncludeSymbols");
                checkBoxIncludeSource.DataBindings.Add("Checked", value, "IncludeSource");
                checkBoxNoBuild.DataBindings.Add("Checked", value, "NoBuild");
                checkBoxNoDependencies.DataBindings.Add("Checked", value, "NoDependencies");
            }
        }

        private void checkBoxIncludeSymbols_CheckedChanged(object sender, System.EventArgs e)
        {
            SymbolsCheckedChanged?.Invoke(checkBoxIncludeSymbols, e);
        }
    }
}
