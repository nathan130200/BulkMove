namespace BulkMove
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
            this.lbSource = new System.Windows.Forms.Label();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.btnSelectSource = new System.Windows.Forms.Button();
            this.lbTarget = new System.Windows.Forms.Label();
            this.txtDestPath = new System.Windows.Forms.TextBox();
            this.btnSelectTarget = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbSource
            // 
            this.lbSource.AutoSize = true;
            this.lbSource.Location = new System.Drawing.Point(18, 15);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(43, 14);
            this.lbSource.TabIndex = 0;
            this.lbSource.Text = "Fonte:";
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.AllowDrop = true;
            this.txtSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourcePath.Location = new System.Drawing.Point(67, 12);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(279, 22);
            this.txtSourcePath.TabIndex = 1;
            this.txtSourcePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.txtSourcePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            // 
            // btnSelectSource
            // 
            this.btnSelectSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectSource.Location = new System.Drawing.Point(352, 11);
            this.btnSelectSource.Name = "btnSelectSource";
            this.btnSelectSource.Size = new System.Drawing.Size(28, 23);
            this.btnSelectSource.TabIndex = 2;
            this.btnSelectSource.Text = "...";
            this.btnSelectSource.UseVisualStyleBackColor = true;
            // 
            // lbTarget
            // 
            this.lbTarget.AutoSize = true;
            this.lbTarget.Location = new System.Drawing.Point(9, 43);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(52, 14);
            this.lbTarget.TabIndex = 3;
            this.lbTarget.Text = "Destino:";
            // 
            // txtDestPath
            // 
            this.txtDestPath.AllowDrop = true;
            this.txtDestPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestPath.Location = new System.Drawing.Point(67, 40);
            this.txtDestPath.Name = "txtDestPath";
            this.txtDestPath.Size = new System.Drawing.Size(279, 22);
            this.txtDestPath.TabIndex = 4;
            this.txtDestPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.txtDestPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            // 
            // btnSelectTarget
            // 
            this.btnSelectTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectTarget.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectTarget.Location = new System.Drawing.Point(352, 39);
            this.btnSelectTarget.Name = "btnSelectTarget";
            this.btnSelectTarget.Size = new System.Drawing.Size(28, 23);
            this.btnSelectTarget.TabIndex = 5;
            this.btnSelectTarget.Text = "...";
            this.btnSelectTarget.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(306, 68);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(74, 27);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "OK";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(392, 107);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnSelectTarget);
            this.Controls.Add(this.txtDestPath);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.btnSelectSource);
            this.Controls.Add(this.txtSourcePath);
            this.Controls.Add(this.lbSource);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bulk Move";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSource;
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.Button btnSelectSource;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.TextBox txtDestPath;
        private System.Windows.Forms.Button btnSelectTarget;
        private System.Windows.Forms.Button btnConfirm;
    }
}

