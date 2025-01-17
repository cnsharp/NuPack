using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CnSharp.VisualStudio.NuPack.Config;
using CnSharp.VisualStudio.NuPack.Models;
using CnSharp.VisualStudio.NuPack.Util;

namespace CnSharp.VisualStudio.NuPack.Controls
{
    public partial class PackOptionsControl : UserControl
    {
        private Control _closeButton;
        private List<NuGetSource> _sources;
        public PackArgs PackArgs { get; private set; } = new PackArgs();

        public List<NuGetSource> Sources
        {
            set
            {
                _sources = value;
                BindingSources();
            }
        }

        public PushArgs PushArgs { get; } = new PushArgs();

        public ErrorProvider ErrorProvider { get; set; }

        public PackOptionsControl()
        {
            InitializeComponent();
            textBoxApiKey.AttachPlaceHolder("Keep it blank if you had set apikey by NuGet command", Color.Gray);
            sourceBox.AttachPlaceHolder("Keep it blank if you just build a package", Color.Gray);
            BindingPushArgs();
        }

        public void LoadConfig(string projectDir, string outputDir)
        {
           PackArgs =  new NuPackConfigHelper(projectDir).Read()?.PackArgs ?? new PackArgs();
           if(string.IsNullOrWhiteSpace(PackArgs.OutputDirectory))
                PackArgs.OutputDirectory = outputDir;
           folderBrowserDialog.SelectedPath = PackArgs.OutputDirectory;
           BindingPackArgs();
        }

        private void BindingPackArgs()
        {
            textBoxOutputDir.DataBindings.Add("Text", PackArgs, "OutputDirectory");
            checkBoxIncludeSymbols.DataBindings.Add("Checked", PackArgs, "IncludeSymbols");
            checkBoxIncludeSource.DataBindings.Add("Checked", PackArgs, "IncludeSource");
            checkBoxNoBuild.DataBindings.Add("Checked", PackArgs, "NoBuild");
            checkBoxNoDependencies.DataBindings.Add("Checked", PackArgs, "NoDependencies");
        }

        private void BindingSources()
        {
            _sources.Insert(0, new NuGetSource { Name = string.Empty, Url = string.Empty });
            sourceBox.DataSource = _sources;
            sourceBox.DisplayMember = "Name";
            sourceBox.ValueMember = "Url";
        }

        private void BindingPushArgs()
        {
            sourceBox.DataBindings.Add("SelectedValue", PushArgs, "Source");
            textBoxApiKey.DataBindings.Add("Text", PushArgs, "ApiKey");
            textBoxSymbolServer.DataBindings.Add("Text", PushArgs, "SymbolSource");
            textBoxSymbolServerApiKey.DataBindings.Add("Text", PushArgs, "SymbolApiKey");
        }
      
        private void btnOpenOutputDir_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                textBoxOutputDir.Text = folderBrowserDialog.SelectedPath;
        }

        private void textBoxOutputDir_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOutputDir.Text))
            {
                ErrorProvider.SetError(textBoxOutputDir, "*");
                e.Cancel = true;
            }
        }

        private void textBoxOutputDir_Validated(object sender, System.EventArgs e)
        {
            ErrorProvider.SetError(textBoxOutputDir, null);
        }
    }
}