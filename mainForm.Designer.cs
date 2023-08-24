namespace music_player
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.playlist1 = new System.Windows.Forms.ListBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.labelElapsedTime = new System.Windows.Forms.Label();
            this.labelTotalTime = new System.Windows.Forms.Label();
            this.LabelArtistSong = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.BackColor = System.Drawing.Color.LightCoral;
            this.buttonBrowse.Location = new System.Drawing.Point(27, 12);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(140, 40);
            this.buttonBrowse.TabIndex = 0;
            this.buttonBrowse.Text = "browse";
            this.buttonBrowse.UseVisualStyleBackColor = false;
            this.buttonBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(485, 352);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.Text = "play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(647, 352);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(323, 352);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(75, 23);
            this.buttonPrevious.TabIndex = 3;
            this.buttonPrevious.Text = "previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(404, 352);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(566, 352);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 23);
            this.buttonPause.TabIndex = 5;
            this.buttonPause.Text = "pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.button6_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // playlist1
            // 
            this.playlist1.BackColor = System.Drawing.Color.Snow;
            this.playlist1.ForeColor = System.Drawing.Color.Black;
            this.playlist1.FormattingEnabled = true;
            this.playlist1.ItemHeight = 16;
            this.playlist1.Location = new System.Drawing.Point(12, 67);
            this.playlist1.Name = "playlist1";
            this.playlist1.Size = new System.Drawing.Size(227, 356);
            this.playlist1.TabIndex = 6;
            this.playlist1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(143, 98);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(24, 21);
            this.checkedListBox1.TabIndex = 7;
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(314, 281);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(408, 56);
            this.trackBar.TabIndex = 9;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // labelElapsedTime
            // 
            this.labelElapsedTime.AutoSize = true;
            this.labelElapsedTime.Location = new System.Drawing.Point(270, 281);
            this.labelElapsedTime.Name = "labelElapsedTime";
            this.labelElapsedTime.Size = new System.Drawing.Size(38, 16);
            this.labelElapsedTime.TabIndex = 10;
            this.labelElapsedTime.Text = "00:00";
            this.labelElapsedTime.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.AutoSize = true;
            this.labelTotalTime.Location = new System.Drawing.Point(728, 281);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(38, 16);
            this.labelTotalTime.TabIndex = 11;
            this.labelTotalTime.Text = "00:00";
            this.labelTotalTime.Click += new System.EventHandler(this.label2_Click);
            // 
            // LabelArtistSong
            // 
            this.LabelArtistSong.AutoSize = true;
            this.LabelArtistSong.Location = new System.Drawing.Point(401, 237);
            this.LabelArtistSong.Name = "LabelArtistSong";
            this.LabelArtistSong.Size = new System.Drawing.Size(252, 16);
            this.LabelArtistSong.TabIndex = 12;
            this.LabelArtistSong.Text = "THIS IS WHATS CURRETNLY PLAYING";
            this.LabelArtistSong.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(91, 429);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(138, 37);
            this.buttonRemove.TabIndex = 13;
            this.buttonRemove.Text = "remove song";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(804, 478);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.LabelArtistSong);
            this.Controls.Add(this.labelTotalTime);
            this.Controls.Add(this.labelElapsedTime);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.playlist1);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonBrowse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "lalala this plays music";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox playlist1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label labelElapsedTime;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Label LabelArtistSong;
        private System.Windows.Forms.Button buttonRemove;
    }
}

