namespace UnBlockMePro
{
    partial class UnBlockMe
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
            this.board = new System.Windows.Forms.PictureBox();
            this.plankNumber = new System.Windows.Forms.TextBox();
            this.btnGoToSolve = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.board)).BeginInit();
            this.SuspendLayout();
            // 
            // board
            // 
            this.board.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.board.Enabled = false;
            this.board.Location = new System.Drawing.Point(51, 46);
            this.board.Margin = new System.Windows.Forms.Padding(6);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(550, 550);
            this.board.TabIndex = 0;
            this.board.TabStop = false;
            this.board.Click += new System.EventHandler(this.board_Click);
            // 
            // plankNumber
            // 
            this.plankNumber.Enabled = false;
            this.plankNumber.Location = new System.Drawing.Point(634, 46);
            this.plankNumber.Margin = new System.Windows.Forms.Padding(6);
            this.plankNumber.Name = "plankNumber";
            this.plankNumber.ReadOnly = true;
            this.plankNumber.Size = new System.Drawing.Size(130, 30);
            this.plankNumber.TabIndex = 1;
            this.plankNumber.Text = "0";
            this.plankNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.plankNumber_KeyPress);
            // 
            // btnGoToSolve
            // 
            this.btnGoToSolve.Location = new System.Drawing.Point(634, 194);
            this.btnGoToSolve.Margin = new System.Windows.Forms.Padding(6);
            this.btnGoToSolve.Name = "btnGoToSolve";
            this.btnGoToSolve.Size = new System.Drawing.Size(138, 42);
            this.btnGoToSolve.TabIndex = 3;
            this.btnGoToSolve.Text = "解题";
            this.btnGoToSolve.UseVisualStyleBackColor = true;
            this.btnGoToSolve.Visible = false;
            this.btnGoToSolve.Click += new System.EventHandler(this.btnGoToSolve_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(138, 609);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(6);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(138, 42);
            this.btnPrev.TabIndex = 4;
            this.btnPrev.Text = "上一步 (A)";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Visible = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(321, 611);
            this.btnNext.Margin = new System.Windows.Forms.Padding(6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(138, 42);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "下一步 (D)";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(631, 247);
            this.btnReset.Margin = new System.Windows.Forms.Padding(6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(138, 42);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "重置 (R)";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(622, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 148);
            this.label1.TabIndex = 7;
            this.label1.Text = "右键单击格子来新建滑块，左键单击格子来扩展/缩减滑块。";
            // 
            // UnBlockMe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 674);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnGoToSolve);
            this.Controls.Add(this.plankNumber);
            this.Controls.Add(this.board);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnBlockMe";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "阿瞒有我良计, 走华容自是易如反掌";
            this.Load += new System.EventHandler(this.unBlockMe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.board)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox board;
        private System.Windows.Forms.TextBox plankNumber;
        private System.Windows.Forms.Button btnGoToSolve;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnReset;
        private Label label1;
    }
}

