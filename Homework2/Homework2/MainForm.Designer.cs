namespace Homework2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.btnFrom = new System.Windows.Forms.Button();
            this.btnTo = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Откуда";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Куда";
            // 
            // tbFrom
            // 
            this.tbFrom.Location = new System.Drawing.Point(85, 29);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(526, 22);
            this.tbFrom.TabIndex = 2;
            this.tbFrom.TextChanged += new System.EventHandler(this.tbFrom_TextChanged);
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(85, 72);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(526, 22);
            this.tbTo.TabIndex = 3;
            this.tbTo.TextChanged += new System.EventHandler(this.tbFrom_TextChanged);
            this.tbTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTo_KeyDown);
            // 
            // btnFrom
            // 
            this.btnFrom.Location = new System.Drawing.Point(626, 28);
            this.btnFrom.Name = "btnFrom";
            this.btnFrom.Size = new System.Drawing.Size(106, 25);
            this.btnFrom.TabIndex = 4;
            this.btnFrom.Text = "Файл...";
            this.btnFrom.UseVisualStyleBackColor = true;
            this.btnFrom.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(626, 70);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(106, 26);
            this.btnTo.TabIndex = 5;
            this.btnTo.Text = "Путь...";
            this.btnTo.UseVisualStyleBackColor = true;
            this.btnTo.Click += new System.EventHandler(this.btn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(26, 119);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(585, 33);
            this.progressBar.TabIndex = 6;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(626, 119);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(106, 33);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "Копировать";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 209);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnTo);
            this.Controls.Add(this.btnFrom);
            this.Controls.Add(this.tbTo);
            this.Controls.Add(this.tbFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFrom;
        private System.Windows.Forms.TextBox tbTo;
        private System.Windows.Forms.Button btnFrom;
        private System.Windows.Forms.Button btnTo;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCopy;
    }
}

