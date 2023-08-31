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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.labelElapsedTime = new System.Windows.Forms.Label();
            this.labelTotalTime = new System.Windows.Forms.Label();
            this.LabelArtistSong = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.labelArtist = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.BackColor = System.Drawing.Color.White;
            this.buttonBrowse.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonBrowse.Location = new System.Drawing.Point(24, 23);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(144, 42);
            this.buttonBrowse.TabIndex = 0;
            this.buttonBrowse.Text = "browse";
            this.buttonBrowse.UseVisualStyleBackColor = false;
            this.buttonBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonPlay.Location = new System.Drawing.Point(655, 401);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 34);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.Text = "play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonNext.Location = new System.Drawing.Point(817, 401);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 34);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonPrevious.Location = new System.Drawing.Point(486, 401);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(82, 34);
            this.buttonPrevious.TabIndex = 3;
            this.buttonPrevious.Text = "previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonStop.Location = new System.Drawing.Point(574, 401);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 34);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Font = new System.Drawing.Font("Cambria", 9F);
            this.buttonPause.Location = new System.Drawing.Point(736, 401);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 34);
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
            this.playlist1.Location = new System.Drawing.Point(12, 106);
            this.playlist1.Name = "playlist1";
            this.playlist1.Size = new System.Drawing.Size(255, 356);
            this.playlist1.TabIndex = 6;
            this.playlist1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(452, 348);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(465, 56);
            this.trackBar.TabIndex = 9;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // labelElapsedTime
            // 
            this.labelElapsedTime.AutoSize = true;
            this.labelElapsedTime.Font = new System.Drawing.Font("Candara", 10.2F);
            this.labelElapsedTime.Location = new System.Drawing.Point(396, 348);
            this.labelElapsedTime.Name = "labelElapsedTime";
            this.labelElapsedTime.Size = new System.Drawing.Size(50, 21);
            this.labelElapsedTime.TabIndex = 10;
            this.labelElapsedTime.Text = "00:00";
            this.labelElapsedTime.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.AutoSize = true;
            this.labelTotalTime.Font = new System.Drawing.Font("Candara", 10.2F);
            this.labelTotalTime.Location = new System.Drawing.Point(923, 348);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(50, 21);
            this.labelTotalTime.TabIndex = 11;
            this.labelTotalTime.Text = "00:00";
            // 
            // LabelArtistSong
            // 
            this.LabelArtistSong.AutoSize = true;
            this.LabelArtistSong.Font = new System.Drawing.Font("Cambria", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabelArtistSong.Location = new System.Drawing.Point(507, 312);
            this.LabelArtistSong.Name = "LabelArtistSong";
            this.LabelArtistSong.Size = new System.Drawing.Size(157, 33);
            this.LabelArtistSong.TabIndex = 12;
            this.LabelArtistSong.Text = "artist - song";
            this.LabelArtistSong.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonRemove.Location = new System.Drawing.Point(67, 468);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(138, 37);
            this.buttonRemove.TabIndex = 13;
            this.buttonRemove.Text = "remove song";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click_1);
            // 
            // txtArtist
            // 
            this.txtArtist.Location = new System.Drawing.Point(338, 120);
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Size = new System.Drawing.Size(214, 22);
            this.txtArtist.TabIndex = 14;
            this.txtArtist.TextChanged += new System.EventHandler(this.txtArtist_TextChanged);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(338, 148);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(214, 22);
            this.txtTitle.TabIndex = 15;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSave.Location = new System.Drawing.Point(377, 176);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(58, 29);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonCancel.Location = new System.Drawing.Point(441, 176);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(63, 29);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(13, 71);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(180, 22);
            this.txtSearch.TabIndex = 18;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSearch.Location = new System.Drawing.Point(199, 67);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(77, 30);
            this.buttonSearch.TabIndex = 19;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // labelArtist
            // 
            this.labelArtist.AutoSize = true;
            this.labelArtist.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelArtist.Location = new System.Drawing.Point(281, 122);
            this.labelArtist.Name = "labelArtist";
            this.labelArtist.Size = new System.Drawing.Size(51, 20);
            this.labelArtist.TabIndex = 20;
            this.labelArtist.Text = "Artist";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.Location = new System.Drawing.Point(289, 148);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(43, 20);
            this.labelTitle.TabIndex = 21;
            this.labelTitle.Text = "Title";
            this.labelTitle.Click += new System.EventHandler(this.labelTitle_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1091, 531);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelArtist);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtArtist);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.LabelArtistSong);
            this.Controls.Add(this.labelTotalTime);
            this.Controls.Add(this.labelElapsedTime);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.playlist1);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonBrowse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label labelElapsedTime;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Label LabelArtistSong;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.TextBox txtArtist;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelArtist;
        private System.Windows.Forms.Label labelTitle;
    }
}

