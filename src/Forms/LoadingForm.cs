using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CnSharp.VisualStudio.NuPack.Forms
{
    public partial class LoadingForm : Form
    {
        #region Constants and Fields
        private BackgroundWorker _backgroundWorker;

        private bool _isCloseBySelf;

        #endregion

        #region Constructors and Destructors

        public LoadingForm()
        {
            InitializeComponent();

            _isCloseBySelf = false;
            LoadingMessage = "Loading...";
        }

        #endregion

        #region Public Properties

        public BackgroundWorker BackgroundWorker
        {
            get { return _backgroundWorker; }
            set
            {
                if (_backgroundWorker != null && _backgroundWorker.IsBusy)
                {
                    return;
                }
                _backgroundWorker = value;
                _backgroundWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
                if (_backgroundWorker.WorkerReportsProgress)
                {
                    progressBar.Maximum = 100;
                    progressBar.Show();
                    _backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
                }
            }
        }

        public Action Action { get; set; }

        public string LoadingMessage { get; set; }

        public bool ReloadEnabled { get; set; } = true;

        #endregion

        #region Methods

        private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowError(e.Error);
                return;
            }
            _isCloseBySelf = true;
            Close();
        }

        private void ShowError(Exception error)
        {
            ShowStatus("Loading failed."+Environment.NewLine + error.Message);

           
            reloadPicture.Top = loadingPicture.Top;
            reloadPicture.Left = loadingPicture.Left - reloadPicture.Width / 2;
            reloadPicture.Visible = ReloadEnabled;

            closePicture.Visible = true;
            closePicture.Top = loadingPicture.Top;
            closePicture.Left = ReloadEnabled ? loadingPicture.Right : reloadPicture.Left;

            loadingPicture.Visible = false;
        }

        private void FrmWait_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isCloseBySelf)
            {
                e.Cancel = true;
            }
        }

        private void FrmWait_Load(object sender, EventArgs e)
        {
            var x = Width/2 - panel1.Width/2;
            var y = Height/2 - panel1.Height/2;

            var point = new Point(x, y);

            panel1.Location = point;

            FormBorderStyle = FormBorderStyle.None;

            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            WindowState = FormWindowState.Maximized;

            Refresh();

            if (Action != null)
            {
                try
                {
                    Action.Invoke();
                    _isCloseBySelf = true;
                    Close();
                }
                catch (Exception exception)
                {
                    ShowError(exception);
                }
            }
        }

        public void DoAction(Action action, bool closeWhenCompleted = true)
        {
            
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowProcess(e.ProgressPercentage);
        }

        #endregion

        #region Implementation of IBlockView

        public void Block()
        {
            ShowDialog();
        }

        public void UnBlock()
        {
            _isCloseBySelf = true;
            Close();
        }

        public void ShowProcess(int percentage)
        {
            if (!progressBar.Visible) progressBar.Visible = true;
            progressBar.Value = percentage;
        }

        public void ShowStatus(string status)
        {
            label.Visible = true;
            label.Text = status;
        }

        #endregion

        private void ReloadPicture_Click(object sender, EventArgs e)
        {
            if (BackgroundWorker == null || BackgroundWorker.IsBusy) 
                return;
            loadingPicture.Visible = true;
            reloadPicture.Visible = false;
            closePicture.Visible = false;
            ShowStatus(LoadingMessage);
            BackgroundWorker.RunWorkerAsync();
        }

        private void closePicture_Click(object sender, EventArgs e)
        {
            _isCloseBySelf = true;
            Close();
            if(Parent is Form form)
                form.Close();
            else
            {
                Owner?.Close();
            }
        }
    }
}