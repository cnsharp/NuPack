using CnSharp.VisualStudio.NuPack.Models;
using CnSharp.VisualStudio.NuPack.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CnSharp.VisualStudio.NuPack.Controls
{
    public partial class NuGetServerOptionsControl : UserControl
    {
        private List<NuGetSource> _sources;
        private List<string> _symbolServers = new List<string>();
        private Control _apiKeyAttachment;
        private Control _sourceAttachment;

        public NuGetServerOptionsControl()
        {
            InitializeComponent();
            BindingPushArgs();
        }

        public bool ShowApiKeyPlaceHolder
        {
            set
            {
                if (value && _sourceAttachment == null)
                {
                    _sourceAttachment = textBoxApiKey.AttachPlaceHolder("Keep it blank if you had set apikey by NuGet command", Color.Gray);
                    return;
                }

                if (!value && _sourceAttachment != null)
                {
                    textBoxApiKey.Parent.Controls.Remove(_sourceAttachment);
                    _sourceAttachment = null;
                }
            }
        }

        public bool ShowSourcePlaceHolder
        {
            set
            {
                if (value && _apiKeyAttachment == null)
                {
                    _apiKeyAttachment = sourceBox.AttachPlaceHolder("Keep it blank if you just build a package", Color.Gray);
                    return;
                }

                if (!value && _apiKeyAttachment != null)
                {
                    textBoxApiKey.Parent.Controls.Remove(_apiKeyAttachment);
                    _apiKeyAttachment = null;
                }
            }
        }

        public bool IncludeSymbols { get; set; }
        public PushArgs PushArgs { get; private set; }

        public event EventHandler OnSourceChanged;

        public void BindingSources(string slnDir, string projectDir)
        {
            _sources = new NuGetConfigReader(slnDir, projectDir).GetNuGetSources();
            BindingSources();
        }

        private void BindingSources()
        {
            _sources.Insert(0, new NuGetSource { Name = string.Empty, Url = string.Empty });
            sourceBox.DataSource = _sources;
            sourceBox.DisplayMember = "Name";
            sourceBox.ValueMember = "Url";
        }

        public void BindingSymbolServers(List<string> servers)
        {
            _symbolServers = servers;
            _symbolServers.Insert(0, string.Empty);
            symbolServerBox.DataSource = _symbolServers;
        }


        private void BindingPushArgs()
        {
            PushArgs = new PushArgs();
            sourceBox.DataBindings.Add("SelectedValue", PushArgs, "Source", false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxApiKey.DataBindings.Add("Text", PushArgs, "ApiKey", false, DataSourceUpdateMode.OnPropertyChanged);
            symbolServerBox.DataBindings.Add("Text", PushArgs, "SymbolSource", false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxSymbolServerApiKey.DataBindings.Add("Text", PushArgs, "SymbolApiKey", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void sourceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableSymbolControls();
            OnSourceChanged?.Invoke(sender, e);
        }

        public void EnableSymbolControls()
        {
            symbolServerBox.Enabled =
                textBoxSymbolServerApiKey.Enabled = IncludeSymbols && sourceBox.SelectedIndex > 0;
        }

    }
}
