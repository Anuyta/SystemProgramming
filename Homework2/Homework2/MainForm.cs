using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework2
{
    public partial class MainForm : Form
    {
        private string pathFrom;
        private string pathTo;
        private string fileNameTo;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn == btnFrom)
            {
                tbFrom.Text = GetFileName();
                pathFrom = tbFrom.Text;
            }
            else if(btn == btnTo)
            {
                tbTo.Text = GetFolderName();
                pathTo = tbTo.Text;
            }
            else if(btn == btnCopy)
            {
                StartCopy();
            }
        }

        private void tbFrom_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb == tbFrom)
            {
                pathFrom = tbFrom.Text;
                fileNameTo = Path.GetFileName(pathFrom);
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
                new Thread(CopyFile).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetFileName()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*|Txt files (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileNameTo = Path.GetFileName(ofd.FileName);
                return ofd.FileName;
            }
            return string.Empty;
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

        private void CopyFile()
        {
            if (!File.Exists(pathFrom) || !Directory.Exists(pathTo))
            {
                MessageBox.Show("проверьте путь", "ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }

            byte[] buffer = new byte[4096];
            using (FileStream sourceStream = new FileStream(pathFrom, FileMode.Open, FileAccess.Read))
            {
                double fileLength = sourceStream.Length;
                using (FileStream destStream = new FileStream(String.Format("{0}\\{1}", pathTo, fileNameTo), FileMode.Create, FileAccess.Write))
                {
                    int currentBlockSize = 0;
                    int max = (int)Math.Round(fileLength / 4096);

                    do
                    {
                        currentBlockSize = sourceStream.Read(buffer, 0, buffer.Length);
                        destStream.Write(buffer, 0, currentBlockSize);
                        this.Invoke(new Action<int>(ShowOnProgressBar), max);
                    } while (currentBlockSize > 0);
                }
            }
        }

        private void ShowOnProgressBar(int value)
        {
            if (progressBar.Value == 0)
                progressBar.Maximum = value;

            if (progressBar.Value == progressBar.Maximum)
                MessageBox.Show("файл успешно скопирован", "завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Thread.Sleep(1000);
            progressBar.Increment(1);
        }

        #endregion
    }
}
