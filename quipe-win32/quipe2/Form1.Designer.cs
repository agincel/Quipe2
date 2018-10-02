namespace quipe2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.txtID = new System.Windows.Forms.TextBox();
            this.tmrFocus = new System.Windows.Forms.Timer(this.components);
            this.lblTyping = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lbLeaderboard = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSwipes = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wb
            // 
            this.wb.Location = new System.Drawing.Point(3, 3);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.ScriptErrorsSuppressed = true;
            this.wb.Size = new System.Drawing.Size(754, 798);
            this.wb.TabIndex = 0;
            this.wb.Url = new System.Uri("https://stevens.campuslabs.com/engage/swipe", System.UriKind.Absolute);
            // 
            // txtID
            // 
            this.txtID.ForeColor = System.Drawing.SystemColors.Window;
            this.txtID.Location = new System.Drawing.Point(38, 98);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(346, 20);
            this.txtID.TabIndex = 1;
            // 
            // tmrFocus
            // 
            this.tmrFocus.Enabled = true;
            this.tmrFocus.Interval = 150;
            this.tmrFocus.Tick += new System.EventHandler(this.tmrFocus_Tick);
            // 
            // lblTyping
            // 
            this.lblTyping.Font = new System.Drawing.Font("Monofonto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTyping.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTyping.Location = new System.Drawing.Point(38, 119);
            this.lblTyping.Name = "lblTyping";
            this.lblTyping.Size = new System.Drawing.Size(346, 44);
            this.lblTyping.TabIndex = 4;
            this.lblTyping.Text = "Please Swipe your ID.";
            this.lblTyping.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Monofonto", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.Location = new System.Drawing.Point(38, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(346, 94);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Quipe 2.0";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbLeaderboard
            // 
            this.lbLeaderboard.BackColor = System.Drawing.SystemColors.Desktop;
            this.lbLeaderboard.Font = new System.Drawing.Font("Monofonto", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLeaderboard.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbLeaderboard.FormattingEnabled = true;
            this.lbLeaderboard.ItemHeight = 15;
            this.lbLeaderboard.Location = new System.Drawing.Point(526, 9);
            this.lbLeaderboard.Name = "lbLeaderboard";
            this.lbLeaderboard.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbLeaderboard.Size = new System.Drawing.Size(240, 169);
            this.lbLeaderboard.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Monofonto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(390, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "Scoreboard:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(419, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tbName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tbName.Location = new System.Drawing.Point(131, 166);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(253, 20);
            this.tbName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Monofonto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(35, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "New Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSwipes
            // 
            this.lblSwipes.Font = new System.Drawing.Font("Monofonto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSwipes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblSwipes.Location = new System.Drawing.Point(390, 5);
            this.lblSwipes.Name = "lblSwipes";
            this.lblSwipes.Size = new System.Drawing.Size(125, 17);
            this.lblSwipes.TabIndex = 11;
            this.lblSwipes.Text = "Swipes: 0";
            this.lblSwipes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.wb);
            this.panel1.Location = new System.Drawing.Point(12, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 772);
            this.panel1.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(778, 961);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblSwipes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbLeaderboard);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblTyping);
            this.Controls.Add(this.txtID);
            this.Name = "Form1";
            this.Text = "Quipe2";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Timer tmrFocus;
        private System.Windows.Forms.Label lblTyping;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox lbLeaderboard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSwipes;
        private System.Windows.Forms.Panel panel1;
    }
}

