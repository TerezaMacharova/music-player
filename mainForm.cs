using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;
using TagLib;
using Newtonsoft.Json;

namespace music_player
{
    public enum PlaybackState
    {
        Stopped,
        Playing,
        Paused
    }

    public partial class MainForm : Form //class for the UI
    {
        private struct SongInfo  //to keep track of the current song 
        {
            public string Title;
            public string Artist;
        }

        private SongInfo currentSong = new SongInfo();
        private Playlist playlist = new Playlist();
        private MusicPlayer musicPlayer;
        private string currentFilePath = null;
        private Timer updateTimer;
        private const string PlaylistFilePath = "playlist.json";

        private void SavePlaylist()
        {
            var jsonString = JsonConvert.SerializeObject(playlist.Songs);
            System.IO.File.WriteAllText(PlaylistFilePath, jsonString);
        }

        private void LoadPlaylist()
        {
            if (System.IO.File.Exists(PlaylistFilePath))
            {
                var jsonString = System.IO.File.ReadAllText(PlaylistFilePath);
                var songs = JsonConvert.DeserializeObject<List<Song>>(jsonString);
                playlist.Songs.Clear();
                playlist.Songs.AddRange(songs);
            }
        }

        private void RefreshPlaylist(string query = "")
        {
            playlist1.Items.Clear();
            var filteredSongs = FilteredSongs(query);
            foreach (var song in playlist.Songs)
            {
                playlist1.Items.Add(song);
            }
        }

        private void InitializeTimer()
        {
            if (updateTimer == null)
            {
                updateTimer = new Timer();
                updateTimer.Interval = 1000;
                updateTimer.Tick += updateTimer_Tick;
            }

            updateTimer.Start();
        }

        private void StopTimer()
        {
            updateTimer?.Stop();
        }

        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; //ensures that the form starts in the middle
            CenterContent();
            playlist = new Playlist();
            LoadPlaylist(); 
            musicPlayer = new MusicPlayer(playlist);
            RefreshPlaylist();

            txtArtist.Visible = false;
            txtTitle.Visible = false;
            buttonSave.Visible = false;
            buttonCancel.Visible = false;
            labelTitle.Visible = false;
            labelArtist.Visible = false;
            
            musicPlayer.TrackBarUpdateEvent += HandleTrackBarUpdate;
            musicPlayer.SongStoppedEvent += MusicPlayer_SongStopped;
            musicPlayer.SongStartedEvent += MusicPlayer_SongStarted;
            playlist.CurrentSongRemoved += Playlist_CurrentSongRemoved;
            
            playlist1.DoubleClick += playlist1_DoubleClick;
            buttonSave.Click += buttonSave_Click;
            buttonCancel.Click += buttonCancel_Click;
            buttonSearch.Click += buttonSearch_Click;

        }

        private void CenterContent()
        {
            //something to center the content
        }

        //event handlers and ui stuff

        private void button1_Click(object sender, EventArgs e) //button for broesing
        {
            //file browsing logic, allowing the user to select audio
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 Files|*.mp3|WAV Files|*.wav|All Files|*.*";
                openFileDialog.Title = "Select an audio file";

                if (openFileDialog.ShowDialog() == DialogResult.OK) // Show the OpenFileDialog and check if the user selected a file
                {
                    string selectedFilePath = openFileDialog.FileName;

                    Song newSong = new Song
                    {
                        FilePath = selectedFilePath,
                        Title = Path.GetFileNameWithoutExtension(selectedFilePath) //setting a title 
                    };

                    playlist.AddSong(newSong);
                    SavePlaylist();
                    RefreshPlaylist();
                    //musicPlayer.Play(newSong.FilePath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //play btn
        {
            switch (musicPlayer.CurrentState)
            {
                case PlaybackState.Paused:
                    musicPlayer.Play(); // Resume playback
                    break;

                case PlaybackState.Stopped:
                    if (currentFilePath != null)
                    {
                        musicPlayer.Play(currentFilePath); // Play the last selected song
                    }
                    else
                    {
                        var firstSong = playlist.Songs.FirstOrDefault();
                        if (firstSong != null)
                        {
                            currentFilePath = firstSong.FilePath;
                            musicPlayer.Play(firstSong.FilePath); // If no song was previously selected, play the first song in the playlist
                        }
                    }
                    break;

                case PlaybackState.Playing:
                    // You can decide what action to perform if a song is already playing when the play button is pressed.
                    break;
            }
        }


        private void button3_Click(object sender, EventArgs e) //next btn
        {
            Song nextSong = playlist.GetNextSong();
            if (nextSong != null)
            {
                currentFilePath = nextSong.FilePath;
                musicPlayer.Play(nextSong.FilePath);
            }

        }

        private void button4_Click(object sender, EventArgs e) //previous btn
        {
            Song prevSong = playlist.GetPreviousSong();
            if (prevSong != null)
            {
                currentFilePath = prevSong.FilePath;
                musicPlayer.Play(prevSong.FilePath);
            }
        }

        private void button5_Click(object sender, EventArgs e) //stop btn
        {
            musicPlayer.Stop();
        }

        private void button6_Click(object sender, EventArgs e) //pause btn
        {
            musicPlayer.Pause();
        }
        /*this method is triggered when form loads */
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = playlist1.SelectedIndex;

            if (playlist1.SelectedItem == null)
            {
                return;
            }

            if (playlist == null)
            {
                return;
            }

            if (selectedIndex >= 0)
            {
                playlist.CurrentSongIndex = selectedIndex;
                Song selectedSong = playlist.GetCurrentSong();

                if (selectedSong == null)
                {
                    MessageBox.Show("Selected song is null!");
                    return;
                }

                // Check #2: Ensure musicPlayer has been initialized
                if (musicPlayer == null)
                {
                    MessageBox.Show("Music player is not initialized!");
                    return;
                }

                // Check #3: Ensure selectedSong's FilePath is not null or empty
                if (string.IsNullOrEmpty(selectedSong.FilePath))
                {
                    MessageBox.Show("Selected song's file path is invalid!");
                    return;
                }

                musicPlayer.Play(selectedSong.FilePath);

            }
        }

        private void UpdateButtonStates()
        {
            buttonPlay.Enabled = currentFilePath != null;
            buttonNext.Enabled = playlist.CurrentSongIndex < playlist.Songs.Count - 1;
            buttonPrevious.Enabled = playlist.CurrentSongIndex > 0; // Enable previous if there are songs before
            buttonStop.Enabled = musicPlayer.CurrentState == PlaybackState.Playing; // Enable stop if playing
            buttonPause.Enabled = musicPlayer.CurrentState == PlaybackState.Playing; // Enable pause if playing
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (musicPlayer.CurrentState == PlaybackState.Playing || musicPlayer.CurrentState == PlaybackState.Paused)
            {
                int newSeconds = trackBar.Value;
                musicPlayer.Seek(newSeconds);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (musicPlayer.CurrentState == PlaybackState.Playing)
            {
                TimeSpan currentTime = musicPlayer.CurrentTime;
                TimeSpan totalTime = musicPlayer.TotalTime;

                labelElapsedTime.Text = currentTime.ToString(@"mm\:ss");
                labelTotalTime.Text = totalTime.ToString(@"mm\:ss");
                trackBar.Value = (int)currentTime.TotalSeconds; // totalTime.TotalSeconds) * trackBar.Maximum);

            }
        }

        private void HandleTrackBarUpdate(object sender, TrackBarUpdateEventArgs e)
        {
            trackBar.Maximum = e.Maximum;
            trackBar.Value = e.Value;
        }

        /* this method is an event handler for SongStartedEvent of MusicPlayer class
         this method is called when SongStartedEvent is raised*/
        /* updating the artist - song label and initializing the timer bar */
        private void MusicPlayer_SongStarted(object sender, EventArgs e)
        {
            InitializeTimer();

            Song currentSong = playlist.GetCurrentSong();
            if (currentSong != null)
            {
                LabelArtistSong.Text = $"{currentSong.Artist} - {currentSong.Title}";
            }
        }

        private void MusicPlayer_SongStopped(object sender, EventArgs e)
        {
            StopTimer();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonRemove_Click_1(object sender, EventArgs e)
        {

            int selectedIndex = playlist1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < playlist.Songs.Count)
            {
                if (playlist.CurrentSongIndex == selectedIndex)
                {
                    musicPlayer.Stop();
                }
                playlist.RemoveSongAt(selectedIndex);

                if (selectedIndex <= playlist.CurrentSongIndex)
                {
                    playlist.CurrentSongIndex--;
                }

                if (playlist.CurrentSongIndex == playlist.Songs.Count)
                {
                    musicPlayer.Stop();
                    playlist.CurrentSongIndex = 0;
                }

                SavePlaylist();
                RefreshPlaylist();
                
            }
        }
        private void Playlist_CurrentSongRemoved(object sender, EventArgs e)
        { 
            musicPlayer.Play();
        }

        private void playlist1_DoubleClick(object sender, EventArgs e)
        {
            if (playlist1.SelectedIndex >= 0)
            {
                Song selectedSong = (Song)playlist1.SelectedItem;

                txtArtist.Text = selectedSong.Artist;
                txtTitle.Text = selectedSong.Title;

                txtArtist.Visible = true;
                txtTitle.Visible = true;
                buttonSave.Visible = true;
                buttonCancel.Visible = true;
                labelArtist.Visible = true; 
                labelTitle.Visible = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            txtArtist.Visible = false;
            txtTitle.Visible = false;
            buttonSave.Visible = false;
            buttonCancel.Visible = false;
            labelArtist.Visible = false;
            labelTitle.Visible = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (playlist1.SelectedIndex >= 0)
            {
                Song selectedSong = (Song)playlist1.SelectedItem;
                selectedSong.Artist = txtArtist.Text;
                selectedSong.Title = txtTitle.Text;

                Song currentSong = playlist.GetCurrentSong();  // Assuming this method fetches the current song being played.

                if (currentSong != null && currentSong == selectedSong)
                {
                    LabelArtistSong.Text = currentSong.ToString();
                }

                RefreshPlaylist();
                SavePlaylist();

                txtArtist.Visible = false;
                txtTitle.Visible = false;
                buttonCancel.Visible = false;
                buttonSave.Visible = false;
                labelArtist.Visible = false;
                labelTitle.Visible = false;
            }
        }

        private List<Song> FilteredSongs (string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return playlist.Songs;

            return playlist.Songs.Where(s => s.Title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 || s.Artist.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Button clicked!");
            RefreshPlaylist(txtSearch.Text);
        }
    }

    public class Song //class representing songs
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string FilePath { get; set; }
        public byte[] Artwork { get; set; }

        public override string ToString()
        {
            return $"{Artist} - {Title}";
        }
    }

    public class Playlist
    {
        public event EventHandler SongStoppedEvent;
        public event EventHandler CurrentSongRemoved;

        public List<Song> Songs { get; } = new List<Song>();
        public int CurrentSongIndex = -1; // deafault to -1 if no song is selected

        public Song GetCurrentSong()
        {
            if (CurrentSongIndex >= 0 && CurrentSongIndex < Songs.Count)
            {
                return Songs[CurrentSongIndex];
            }
            return null;
        }

        public Song GetNextSong()
        {
            if (CurrentSongIndex + 1 < Songs.Count)
            {
                CurrentSongIndex++;
            }
            else
            {
                CurrentSongIndex = 0;
            }
            return GetCurrentSong();
        }

        public Song GetPreviousSong()
        {
            if (CurrentSongIndex - 1 >= 0)
            {
                CurrentSongIndex--;
            }
            else
            {
                CurrentSongIndex = Songs.Count - 1;
            }
            return GetCurrentSong();
        }

        public void AddSong(Song song)
        {
            Songs.Add(song);
            CurrentSongIndex = Songs.Count - 1;
        }

        public void RemoveSongAt(int index)
        {
            bool isCurrentSong = (index == CurrentSongIndex);
            if (index >= 0 && index < Songs.Count)
            {
                Songs.RemoveAt(index);

                if (index <= CurrentSongIndex)
                {
                    CurrentSongIndex--;
                }
            }
            if (isCurrentSong)
            {
               CurrentSongRemoved?.Invoke(this, EventArgs.Empty); ///or play next song, that would be better probably ??????
            }
        }

    }

    public class Artist
    {
        public string Name { get; set; }
        private List<Song> songs = new List<Song>();
        public List<Song> Songs { get { return songs; } }

        public void AddSong(Song newSong)
        {
            songs.Add(newSong);
        }
    }

    public class TrackBarUpdateEventArgs : EventArgs
    {
        public int Maximum { get; set; }
        public int Value { get; set; }
    }

    public delegate void TrackBarUpdateHandler(object sender, TrackBarUpdateEventArgs e);

    public class MusicPlayer
    {
        public event TrackBarUpdateHandler TrackBarUpdateEvent;
        public event EventHandler SongStartedEvent;
        public event EventHandler RequestStopTimer;
        public event EventHandler SongStoppedEvent;

        private IWavePlayer waveOutDevice; // Represents the device used for audio playback.
        private AudioFileReader audioFileReader; //Used to read the audio file specified by its path.
        private Playlist playlist;

        public MusicPlayer(Playlist playlist)
        {
            this.playlist = playlist;
            waveOutDevice = new WaveOut();
            waveOutDevice.PlaybackStopped += WaveOutDevice_PlaybackStopped;
        }

        private void WaveOutDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (CurrentState == PlaybackState.Playing)
            {
                Song nextSong = playlist.GetNextSong();
                if (nextSong != null)
                {
                    Play(nextSong.FilePath);
                }
            }
        }
        public PlaybackState CurrentState { get; private set; } = PlaybackState.Stopped;  //the logic of music playing goes here

        public void Play()  //method to play from playlist
        {
            if (CurrentState == PlaybackState.Paused)
            {
                waveOutDevice.Play();
                CurrentState = PlaybackState.Playing;
            }
            else if (CurrentState == PlaybackState.Stopped)
            {
                Song currentSong = playlist.GetCurrentSong();

                if (currentSong != null)
                {
                    Play(currentSong.FilePath);
                }
            }
            ///// UpdateButtonsStates();
        }

        public void Play(string filePath)  //method to play specific file
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("File path is null or empty");
                return;
            }

            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show($"File does not exist: {filePath}");
                return;
            }
            try
            {
                Stop();

                waveOutDevice = new WaveOut();
                audioFileReader = new AudioFileReader(filePath);
                waveOutDevice.PlaybackStopped += OnPlaybackStopped;
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
                CurrentState = PlaybackState.Playing;

                SongStartedEvent?.Invoke(this, EventArgs.Empty);
                TrackBarUpdateEvent?.Invoke(this, new TrackBarUpdateEventArgs
                {
                    Maximum = (int)audioFileReader.TotalTime.TotalSeconds,
                    Value = 0
                }); //this raises the event
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing the file: {ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                audioFileReader.Dispose();
                waveOutDevice?.Dispose();
                audioFileReader = null;
                waveOutDevice = null;
            }
        }

        public void Stop()
        {
            waveOutDevice?.Stop();
            if (waveOutDevice != null)
            {
                waveOutDevice.PlaybackStopped -= OnPlaybackStopped;
                waveOutDevice.PlaybackStopped -= WaveOutDevice_PlaybackStopped;
            }

            audioFileReader?.Dispose();
            waveOutDevice?.Dispose();
            audioFileReader = null;
            waveOutDevice = null;

            CurrentState = PlaybackState.Stopped;
            SongStoppedEvent?.Invoke(this, EventArgs.Empty);
            RequestStopTimer?.Invoke(this, EventArgs.Empty);
        }

        public void Pause()
        {
            if (CurrentState == PlaybackState.Playing)
            {
                waveOutDevice?.Pause();
                CurrentState = PlaybackState.Paused;
            }
        }

        public void Skip()
        {

        }

        public void GoBack()
        {

        }
        public void Seek(int seconds)
        {
            if (audioFileReader != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(seconds);
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            var nextSong = playlist.GetNextSong();
            if (nextSong != null)
            {
                Play(nextSong.FilePath);
            }
            else
            {
                playlist.CurrentSongIndex = 0;
                var firstSong = playlist.GetCurrentSong();
                if (firstSong != null)
                {
                    Play(firstSong.FilePath);
                }
            }
        }

        public TimeSpan TotalTime
        {
            get
            {
                return audioFileReader?.TotalTime ?? TimeSpan.Zero;
            }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                return audioFileReader?.CurrentTime ?? TimeSpan.Zero;
            }
        }
    }
}