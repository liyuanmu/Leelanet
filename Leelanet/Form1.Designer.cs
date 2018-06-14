namespace Leelanet
{
    partial class frmMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBoard = new System.Windows.Forms.PictureBox();
            this.btnStartLeela = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnBackMove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.picBoard);
            this.panel1.Location = new System.Drawing.Point(2, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 463);
            this.panel1.TabIndex = 2;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // picBoard
            // 
            this.picBoard.Location = new System.Drawing.Point(66, 24);
            this.picBoard.Name = "picBoard";
            this.picBoard.Size = new System.Drawing.Size(100, 50);
            this.picBoard.TabIndex = 0;
            this.picBoard.TabStop = false;
            this.picBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoard_Paint);
            this.picBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBoard_MouseMove);
            this.picBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBoard_MouseUp);
            // 
            // btnStartLeela
            // 
            this.btnStartLeela.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStartLeela.Location = new System.Drawing.Point(198, 12);
            this.btnStartLeela.Name = "btnStartLeela";
            this.btnStartLeela.Size = new System.Drawing.Size(75, 23);
            this.btnStartLeela.TabIndex = 3;
            this.btnStartLeela.Text = "启动leela";
            this.btnStartLeela.UseVisualStyleBackColor = true;
            this.btnStartLeela.Click += new System.EventHandler(this.btnStartLeela_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Location = new System.Drawing.Point(279, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Pass";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // BtnBackMove
            // 
            this.BtnBackMove.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnBackMove.Location = new System.Drawing.Point(332, 11);
            this.BtnBackMove.Name = "BtnBackMove";
            this.BtnBackMove.Size = new System.Drawing.Size(47, 23);
            this.BtnBackMove.TabIndex = 5;
            this.BtnBackMove.Text = "悔棋";
            this.BtnBackMove.UseVisualStyleBackColor = true;
            this.BtnBackMove.Click += new System.EventHandler(this.BtnBackMove_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 15);
            this.label1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button2.Location = new System.Drawing.Point(385, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "关闭引擎";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 509);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnBackMove);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStartLeela);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "Leela";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.Button btnStartLeela;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnBackMove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}

