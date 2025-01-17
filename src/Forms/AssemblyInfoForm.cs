using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.NuPack.Extensions;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack.Forms
{
    public partial class AssemblyInfoForm : Form
    {
        private readonly List<Project> _projects;
        private  List<ProjectAssemblyInfo> _projectInfos;
        private  ProjectAssemblyInfo[] _projectOriginalInfos;
        private CommonAssemblyInfo _commonInfo;
        private string _fileToLink;
        private static readonly string[] commonInfoFields = typeof(CommonAssemblyInfo).GetProperties().Select(p => p.Name).ToArray();
        private BackgroundWorker _loadingWorker;
        private BackgroundWorker _savingWorker;
        
        public AssemblyInfoForm(IEnumerable<Project> refProjects)
        {
            InitializeComponent();
            Icon = Resource.logo;

            projectGrid.AutoGenerateColumns = false;
            RegisterEvents();

             _projects = refProjects.ToList();

            _loadingWorker = new BackgroundWorker();
            _loadingWorker.DoWork += (sender, e) =>
            {
                var projects = (e.Argument as IEnumerable<Project>).ToList();
                e.Result = projects.Select(p => p.GetProjectAssemblyInfo()).ToList();
            };
            _loadingWorker.RunWorkerCompleted += (sender, e) =>
            {
                _projectInfos = e.Result as List<ProjectAssemblyInfo>;
                projectBindingSource.DataSource = _projectInfos;
                projectGrid.DataSource = projectBindingSource;
                _projectOriginalInfos = _projectInfos.Select(p => p.Copy()).ToArray();
            };
            _loadingWorker.RunWorkerAsync(_projects);
            new LoadingForm {BackgroundWorker = _loadingWorker,ReloadEnabled = false}.ShowDialog(this);
        }

        void ScanCommonInfoFiles()
        {
            var i = 0;
            _projects.ForEach(p =>
            {
                var commonInfoFile = p.GetCommonAssemblyInfoFilePath();
                if (commonInfoFile == null)
                {
                    i++;
                    return;
                }
                if (_fileToLink == null)
                {
                    _fileToLink = commonInfoFile;
                    _commonInfo = AssemblyInfoUtil.ReadCommonAssemblyInfo(_fileToLink);
                    BindCommonInfo();
                }
                if (_fileToLink == commonInfoFile)
                    projectGrid.Rows[i].Cells[0].Value = true;
                i++;
            });
        }

        private void RegisterEvents()
        {
            txtCompany.TextChanged += CommonInfoTextChanged;
            txtCopyright.TextChanged += CommonInfoTextChanged;
            txtProduct.TextChanged += CommonInfoTextChanged;
            txtVersion.TextChanged += CommonInfoTextChanged;
            txtTrademark.TextChanged += CommonInfoTextChanged;
        }

        private void CommonInfoTextChanged(object sender, EventArgs e)
        {
            var box = sender as TextBox;
           
            var cellName = box.Name.Replace("txt", "Col");
            foreach (DataGridViewRow row in projectGrid.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    row.Cells[cellName].Value = box.Text.Trim();
                }
            }
            projectGrid.EndEdit();
        }


        
        public IServiceProvider ServiceProvider { get; set; }

        
        private void FormVersionLoad(object sender, EventArgs e)
        {
            ActiveControl = projectGrid;
        }


        public static bool Contains(Enum keys, Enum flag)
        {
            ulong keysVal = Convert.ToUInt64(keys);
            ulong flagVal = Convert.ToUInt64(flag);

            return (keysVal & flagVal) == flagVal;
        }
        
        private void projectGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
           ScanCommonInfoFiles();
        }

        private void projectGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if(projectGrid.Columns[e.ColumnIndex] == ColTrademark)
                return;
            projectGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
            if (e.FormattedValue == null || string.IsNullOrEmpty(e.FormattedValue.ToString()) ||
                e.FormattedValue.ToString().Trim().Length == 0)
            {
                projectGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "*";
                e.Cancel = true;
            }
        }

        private void projectGrid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            projectGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
        }

        private void CopyCellValues(int row)
        {
            foreach (var field in commonInfoFields)
            {
                var cellName = "Col" + field;
                var textbox = groupBoxCommonInfo.Controls.Find("txt" + field, false).FirstOrDefault() as TextBox;
                if (textbox != null)
                    projectGrid.Rows[row].Cells[cellName].Value = textbox.Text.Trim();
            }
            projectGrid.EndEdit();
        }


        private void RevertCellValue(int row)
        {
            foreach (var field in commonInfoFields)
            {
                var cellName = "Col" + field;
                var info = _projectOriginalInfos[row];
                var v = typeof(ProjectAssemblyInfo).GetProperty(field).GetValue(info, null);
                projectGrid.Rows[row].Cells[cellName].Value = v;
            }
            projectGrid.EndEdit();
        }


        private void projectGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var field = projectGrid.Columns[e.ColumnIndex].DataPropertyName;
            if (commonInfoFields.Contains(field))
            {
                var textbox = groupBoxCommonInfo.Controls.Find("txt" + field,false).FirstOrDefault() as TextBox;
                var selected = (bool?) projectGrid.CurrentRow.Cells[0].Value ?? false;
                if (!string.IsNullOrWhiteSpace(textbox?.Text) && selected)
                    e.Cancel = true;
            }
        }

        private void projectGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }


        private void projectGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0 && _projectOriginalInfos != null)
            {
                var selected = (bool?)projectGrid.CurrentCell.Value ?? false;
                if (selected) CopyCellValues(e.RowIndex);
                else RevertCellValue(e.RowIndex);
            }
        }


        private void projectGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid.IsCurrentCellDirty && grid.CurrentCell.ColumnIndex == 0)
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private string DefaultExt => _projects[0].GetCodeFileExtension();

        private void btnNewOrChange_Click(object sender, EventArgs e)
        {
            saveAssemblyInfoFileDialog.DefaultExt = DefaultExt;
            saveAssemblyInfoFileDialog.InitialDirectory = Path.GetDirectoryName(Host.Instance.DTE.Solution.FileName);
            saveAssemblyInfoFileDialog.FileName = _fileToLink ?? typeof(CommonAssemblyInfo).Name;
            if(saveAssemblyInfoFileDialog.ShowDialog() != DialogResult.OK) return;
            var info = _projectOriginalInfos[0];
            _commonInfo = new CommonAssemblyInfo
            {
                Company = info.Company,
                Copyright = info.Copyright,
                Product = info.Product,
                Trademark = info.Trademark
            };
            _commonInfo.Save(saveAssemblyInfoFileDialog.FileName);
            _fileToLink = saveAssemblyInfoFileDialog.FileName;
            BindCommonInfo();
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            openAssemblyInfoFileDialog.DefaultExt = DefaultExt;
            openAssemblyInfoFileDialog.InitialDirectory = _fileToLink != null ? Path.GetDirectoryName(_fileToLink) : Path.GetDirectoryName(Host.Instance.DTE.Solution.FileName);
            if (openAssemblyInfoFileDialog.ShowDialog() != DialogResult.OK) return;
            _commonInfo = AssemblyInfoUtil.ReadCommonAssemblyInfo(openAssemblyInfoFileDialog.FileName);
            _fileToLink = openAssemblyInfoFileDialog.FileName;
            BindCommonInfo();
        }

        void BindCommonInfo()
        {
            foreach (Control control in groupBoxCommonInfo.Controls)
            {
                if (control is TextBox)
                {
                    var box = control as TextBox;
                    box.DataBindings.Clear();
                    box.DataBindings.Add("Text", _commonInfo, box.Name.Replace("txt", ""),false,DataSourceUpdateMode.OnPropertyChanged);
                }
            }
        }

        private string[] GetIgnoreFields()
        {
            var fields = new List<string>();
            if(string.IsNullOrWhiteSpace(txtTrademark.Text))
                fields.Add("Trademark");
            if (string.IsNullOrWhiteSpace(txtVersion.Text))
                fields.Add("Version");
            return fields.ToArray();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!projectGrid.EndEdit())
                return;
            if (_commonInfo != null)
            {
                if (string.IsNullOrWhiteSpace(_commonInfo.Company))
                {
                    ServiceProvider.ShowError("Company required.",Common.ProductName);
                    txtCompany.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(_commonInfo.Product))
                {
                    ServiceProvider.ShowError("Product required.", Common.ProductName);
                    txtProduct.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(_commonInfo.Copyright))
                {
                    ServiceProvider.ShowError("Copyright required.", Common.ProductName);
                    txtCopyright.Focus();
                    return;
                }
                _commonInfo.Save(_fileToLink);
            }
           

            //projectBindingSource.ResetBindings(false);

            toolStripStatusLabel.Text = "Saving...";
            if (_fileToLink != null) Host.Instance.Solution2.AddSolutionItem(_fileToLink);
            var i = 0;
            var ignoreFields = GetIgnoreFields();
            var savingProjects = new List<AssemblyInfoGridViewRow>();
            foreach (DataGridViewRow row in projectGrid.Rows)
            {
                var assemblyInfo = _projectInfos[i];
                if (_commonInfo != null && row.Cells[0].Value != null && (bool) row.Cells[0].Value)
                {
                    savingProjects.Add(new AssemblyInfoGridViewRow{AssemblyInfo = assemblyInfo,LinkToCommonInfo = true,Row = i});
                }
                else if (assemblyInfo.CompareTo(_projectOriginalInfos[i]) != 0)
                {
                    savingProjects.Add(new AssemblyInfoGridViewRow { AssemblyInfo = assemblyInfo, LinkToCommonInfo = false, Row = i });
                }
                i++;
            }
            if (savingProjects.Count == 0)
            {
                DialogResult = DialogResult.OK;
                return;
            }
         
            _savingWorker= new BackgroundWorker{WorkerReportsProgress = true};
            _savingWorker.DoWork += (o, args) =>
            {
                var projects = args.Argument as List<AssemblyInfoGridViewRow>;
                var j = 1;
                var n = projects.Count;
                foreach (var pair in projects)
                {
                    if (pair.LinkToCommonInfo)
                    {
                        pair.AssemblyInfo.Project.LinkCommonAssemblyInfoFile(_fileToLink);
                        pair.AssemblyInfo.Project.RemoveCommonAssemblyInfoAnnotations(ignoreFields);
                    }
                    pair.AssemblyInfo.Save();
                    _savingWorker.ReportProgress(j == n  ? 100 : (int)Math.Ceiling(Math.Round((decimal)j / n,2,MidpointRounding.AwayFromZero)) * 100,pair);
                    j++;
                }
            };
            _savingWorker.RunWorkerCompleted += (o, args) =>
            {
                DialogResult = DialogResult.OK;
            };
            _savingWorker.ProgressChanged += (o, args) =>
            {
                var row = args.UserState as AssemblyInfoGridViewRow;
                toolStripStatusLabel.Text = $"{row.AssemblyInfo.ProjectName} saved.";
                projectGrid.Rows[row.Row].DefaultCellStyle.ForeColor = Color.DarkCyan;
            };
            panel1.Enabled = false;
            projectGrid.Enabled = false;
            _savingWorker.RunWorkerAsync(savingProjects);
        }

        class AssemblyInfoGridViewRow
        {
            public ProjectAssemblyInfo AssemblyInfo { get; set; }
            public bool LinkToCommonInfo { get; set; }
            public int Row { get; set; }
        }

    }
}