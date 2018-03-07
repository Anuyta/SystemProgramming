using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_4
{
    public partial class MainForm : Form
    {
        private List<string> pathesFrom = new List<string>();
        private string pathTo;
        private List<Task> tasksLst = new List<Task>();
        private CancellationTokenSource tokenSource;
        private CancellationToken token;

        public MainForm()
        {
            InitializeComponent();
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn == btnFrom)
            {
                tbFrom.Text = GetFilesName();
            }
            else if (btn == btnTo)
            {
                tbTo.Text = GetFolderName();
                pathTo = tbTo.Text;
            }
            else if (btn == btnCopy)
            {
                StartCopy();
            }
            else if(btn == btnCancel)
            {
                tokenSource.Cancel();
                CancellationCopy();
            }
        }

        private void tbFrom_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb == tbFrom)
            {
                GetFilesNames(tbFrom.Text);
            }
            else if (tb == tbTo)
            {
                pathTo = tbTo.Text;
            }
        }

        private void tbTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartCopy();
            }
        }

        #region UserMethods
        private void StartCopy()
        {
            try
            {
                progressBar.Value = 0;
                CopyFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetFilesName()
        {
            string namesFrom = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*|Txt files (*.txt)|*.txt";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if(ofd.FileNames.Length > 5)
                {
                    MessageBox.Show("Одновременно вы можете копировать не более 5 файлов", "ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return namesFrom;
                }
                foreach (var item in ofd.FileNames)
                {
                    pathesFrom.Add(item);                    
                    namesFrom += item;
                    namesFrom += "; ";
                }
            }
            return namesFrom;
        }

        private void GetFilesNames(string str)
        {
            //pathesFrom = new List<string>();
            var match = Regex.Matches(str, @"\w\:[\\\w]*\.\w*(?=\;)");

            //for (int i = 0; i < match.Count; i++)
            //{
            //    pathesFrom.Add(match[i].Value);
            //}
            foreach (var item in match)
            {
                pathesFrom.Add(item.ToString());
            }
        }

        private string GetFolderName()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return string.Empty;
        }

        private void CopyFile(string fileName)
        {
            string destFile = Path.GetFileName(fileName);
            byte[] buffer = new byte[4096];
            using (FileStream sourceStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                double fileLength = sourceStream.Length;
                using (FileStream destStream = new FileStream(String.Format("{0}\\{1}", pathTo, destFile), FileMode.Create, FileAccess.Write))
                {
                    int currentBlockSize = 0;
                    do
                    {
                        currentBlockSize = sourceStream.Read(buffer, 0, buffer.Length);
                        destStream.Write(buffer, 0, currentBlockSize);
                    } while (currentBlockSize > 0);
                    token.ThrowIfCancellationRequested();
                }
            }
        }

        private void CopyFiles()
        {
            if (string.IsNullOrEmpty(tbFrom.Text) || string.IsNullOrEmpty(tbTo.Text))
            {
                MessageBox.Show("проверьте путь", "ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            foreach (var fileName in pathesFrom)
            {
                var task1 = new Task(() => CopyFile(fileName));
                task1.Start();
                var task2 = task1.ContinueWith(t => !t.IsCanceled);
                this.Invoke(new Action<int>(ShowOnProgressBar), pathesFrom.Count);
            }        
        }

        private void ShowOnProgressBar(int value)
        {
            if (progressBar.Value == 0)
                progressBar.Maximum = value;            
            progressBar.Increment(1);
        }

        private void CancellationCopy()
        {
            foreach (var item in pathesFrom)
            {
                File.Delete(string.Format("{0}\\{1}", pathTo, Path.GetFileName(item)));
            }
            progressBar.Value = 0;
            MessageBox.Show("Копирование отменено", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        #endregion
    }
}

