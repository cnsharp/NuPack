using System;
using System.ComponentModel;
using System.Windows.Forms;
using CnSharp.VisualStudio.NuPack.Config;
using CnSharp.VisualStudio.NuPack.Models;

namespace CnSharp.VisualStudio.NuPack.Controls
{
    public partial class PackOptionsControl : UserControl
    {
        private bool _sdkBased;
        private DotnetPackOptionsControl _dotnetPackOptionsControl;
        private NuGetPackOptionsControl _nuGetPackOptionsControl;
        private PackArgs _packArgs = new PackArgs();

        public PackOptionsControl()
        {
            InitializeComponent();
            nuGetServerOptionsControl.ShowApiKeyPlaceHolder = true;
            nuGetServerOptionsControl.ShowSourcePlaceHolder = true;
        }

        public NuPackConfig Config { get; private set; }

        public PackArgs PackArgs
        {
            get => _packArgs;
            private set
            {
                _packArgs = value;
                if (_dotnetPackOptionsControl != null)
                    _dotnetPackOptionsControl.PackArgs = value;
                if (_nuGetPackOptionsControl != null)
                    _nuGetPackOptionsControl.PackArgs = value;
                nuGetServerOptionsControl.IncludeSymbols = value.IncludeSymbols;
            }
        }


        public PushArgs PushArgs => nuGetServerOptionsControl.PushArgs;

        public ErrorProvider ErrorProvider { get; set; }

        public bool SdkBased
        {
            set
            {
                _sdkBased = value;
                SetPackArgsControls();
            }
        }

        private void SetPackArgsControls()
        {
            panelPackOptions.Controls.Clear();
            if (_sdkBased)
            {
                _dotnetPackOptionsControl = new DotnetPackOptionsControl();
                _dotnetPackOptionsControl.SymbolsCheckedChanged += (sender, e) =>
                {
                    ChangeSymbolsStatus(sender);
                };
                _dotnetPackOptionsControl.Dock = DockStyle.Fill;
                panelPackOptions.Controls.Add(_dotnetPackOptionsControl);
            }
            else
            {
                _nuGetPackOptionsControl = new NuGetPackOptionsControl();
                _nuGetPackOptionsControl.SymbolsCheckedChanged += (sender, e) =>
                {
                    ChangeSymbolsStatus(sender);
                };
                _nuGetPackOptionsControl.Dock = DockStyle.Fill;
                panelPackOptions.Controls.Add(_nuGetPackOptionsControl);
            }
        }

        private void ChangeSymbolsStatus(object sender)
        {
            var checkBox = sender as CheckBox;
            PackArgs.IncludeSymbols = checkBox?.Checked == true;
            nuGetServerOptionsControl.IncludeSymbols = PackArgs.IncludeSymbols;
            nuGetServerOptionsControl.EnableSymbolControls();
        }

        public void LoadConfig(string solutionDir, string projectDir, string outputDir)
        {
            Config = new NuPackConfigHelper(projectDir).Read() ?? new NuPackConfig();
            PackArgs = Config.PackArgs;
            if (string.IsNullOrWhiteSpace(PackArgs.OutputDirectory))
                PackArgs.OutputDirectory = outputDir;
            folderBrowserDialog.SelectedPath = PackArgs.OutputDirectory;
            BindingPackArgs();
            nuGetServerOptionsControl.BindingSources(solutionDir, projectDir);
            nuGetServerOptionsControl.BindingSymbolServers(Config.SymbolServers);
        }

        private void BindingPackArgs()
        {
            textBoxOutputDir.DataBindings.Add("Text", PackArgs, "OutputDirectory");
        }

       
        private void btnOpenOutputDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                textBoxOutputDir.Text = folderBrowserDialog.SelectedPath;
        }

        private void textBoxOutputDir_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOutputDir.Text))
            {
                ErrorProvider.SetError(textBoxOutputDir, "*");
                e.Cancel = true;
            }
        }

        private void textBoxOutputDir_Validated(object sender, EventArgs e)
        {
            ErrorProvider.SetError(textBoxOutputDir, null);
        }

    }
}