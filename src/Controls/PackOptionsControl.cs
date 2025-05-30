using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CnSharp.VisualStudio.NuPack.Config;
using CnSharp.VisualStudio.NuPack.Models;
using CnSharp.VisualStudio.NuPack.Util;

namespace CnSharp.VisualStudio.NuPack.Controls
{
    public partial class PackOptionsControl : UserControl
    {
        private bool _sdkBased;
        private List<NuGetSource> _sources;
        private List<string> _symbolServers = new List<string>();
        private DotnetPackOptionsControl _dotnetPackOptionsControl;
        private NuGetPackOptionsControl _nuGetPackOptionsControl;

        public PackOptionsControl()
        {
            InitializeComponent();
            textBoxApiKey.AttachPlaceHolder("Keep it blank if you had set apikey by NuGet command", Color.Gray);
            sourceBox.AttachPlaceHolder("Keep it blank if you just build a package", Color.Gray);
        }

        public NuPackConfig Config { get; private set; }

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
                _dotnetPackOptionsControl.PackArgs = PackArgs;
                _dotnetPackOptionsControl.SymbolsCheckedChanged += (sender, e) =>
                {
                    var checkBox = sender as CheckBox;
                    PackArgs.IncludeSymbols = checkBox?.Checked == true;
                    EnableSymbolControls();
                };
                _dotnetPackOptionsControl.Dock = DockStyle.Fill;
                panelPackOptions.Controls.Add(_dotnetPackOptionsControl);
            }
            else
            {
                _nuGetPackOptionsControl = new NuGetPackOptionsControl();
                _nuGetPackOptionsControl.PackArgs = PackArgs;
                _nuGetPackOptionsControl.SymbolsCheckedChanged += (sender, e) =>
                {
                    var checkBox = sender as CheckBox;
                    PackArgs.IncludeSymbols = checkBox?.Checked == true;
                    EnableSymbolControls();
                };
                _nuGetPackOptionsControl.Dock = DockStyle.Fill;
                panelPackOptions.Controls.Add(_nuGetPackOptionsControl);
            }
        }

        public void LoadConfig(string projectDir, string outputDir)
        {
            Config = new NuPackConfigHelper(projectDir).Read() ?? new NuPackConfig();
            PackArgs = Config.PackArgs;
            if (string.IsNullOrWhiteSpace(PackArgs.OutputDirectory))
                PackArgs.OutputDirectory = outputDir;
            folderBrowserDialog.SelectedPath = PackArgs.OutputDirectory;
            BindingPackArgs();
            BindingPushArgs();
            BindingSymbolServers(Config.SymbolServers);
        }

        private void BindingPackArgs()
        {
            textBoxOutputDir.DataBindings.Add("Text", PackArgs, "OutputDirectory");
            
        }

        private void BindingSources()
        {
            _sources.Insert(0, new NuGetSource { Name = string.Empty, Url = string.Empty });
            sourceBox.DataSource = _sources;
            sourceBox.DisplayMember = "Name";
            sourceBox.ValueMember = "Url";
        }

        private void BindingSymbolServers(List<string> servers)
        {
            _symbolServers = servers;
            _symbolServers.Insert(0, string.Empty);
            symbolServerBox.DataSource = _symbolServers;
        }


        private void BindingPushArgs()
        {
            sourceBox.DataBindings.Add("SelectedValue", PushArgs, "Source");
            textBoxApiKey.DataBindings.Add("Text", PushArgs, "ApiKey");
            symbolServerBox.DataBindings.Add("Text", PushArgs, "SymbolSource");
            textBoxSymbolServerApiKey.DataBindings.Add("Text", PushArgs, "SymbolApiKey");
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

        private void sourceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableSymbolControls();
        }

        private void EnableSymbolControls()
        {
            symbolServerBox.Enabled =
                textBoxSymbolServerApiKey.Enabled = PackArgs.IncludeSymbols && sourceBox.SelectedIndex > 0;
        }

    }
}