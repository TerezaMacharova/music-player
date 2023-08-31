using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;
using Newtonsoft.Json;

namespace music_player
{
    /// <summary>
    /// represent the possible playback states 
    /// </summary>
    public enum PlaybackState
    {
        Stopped,
        Playing,
        Paused
    }

    /// <summary>
    /// the main form for the applications UI
    /// </summary>
    public partial class MainForm : Form 
    {
        /// <summary>
        /// contains informations about song 
        /// </summary>
        private struct SongInfo  
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

        /// <summary>
        /// saves the current playlist to a JSON file
        /// </summary>
        private void SavePlaylist()
        {
            var jsonString = JsonConvert.SerializeObject(playlist.Songs);
            System.IO.File.WriteAllText(PlaylistFilePath, jsonString);
        }

         /// <summary>
         /// loads the playlist from a JSON file, if it exist
         /// </summary>
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


        /// <summary>
        /// refreshes the playlist in the UI, can be filtered by query
        /// </summary>
        private void RefreshPlaylist(string query = "")
        {
            playlist1.Items.Clear();
            var filteredSongs = FilteredSongs(query);
            foreach (var song in filteredSongs)
            {
                
                playlist1.Items.Add(song);
            }
        }

        /// <summary>
        /// initializes the timer used for updating the UI
        /// </summary>
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

        /// <summary>
        /// stops updating the timer
        /// </summary>
        private void StopTimer()
        {
            updateTimer?.Stop();
        }
         /// <summary>
         /// initializes a new instance of the <see cref="MainForm"/> class
         /// </summary>
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
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

        /// <summary>
        /// handles the click event for the button that allows user to browse for audio
        /// </summary>
        private void button1_Click(object sender, EventArgs e) 
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 Files|*.mp3|WAV Files|*.wav|All Files|*.*";
                openFileDialog.Title = "Select an audio file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    Song newSong = new Song
                    {
                        FilePath = selectedFilePath,
                        Title = Path.GetFileNameWithoutExtension(selectedFilePath) 
                    };

                    playlist.AddSong(newSong);
                    SavePlaylist();
                    RefreshPlaylist();
                }
            }
        }

        /// <summary>
        /// handles the click event for the play button
        /// </summary>
        private void button2_Click(object sender, EventArgs e) 
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
                    break;
            }
        }

        /// <summary>
        /// handles the click event for the next button
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            Song nextSong = playlist.GetNextSong();
            if (nextSong != null)
            {
                currentFilePath = nextSong.FilePath;
                musicPlayer.Play(nextSong.FilePath);
            }
        }

        /// <summary>
        /// handles the click event for the previous button
        /// </summary>
        private void button4_Click(object sender, EventArgs e) 
        {
            Song prevSong = playlist.GetPreviousSong();
            if (prevSong != null)
            {
                currentFilePath = prevSong.FilePath;
                musicPlayer.Play(prevSong.FilePath);
            }
        }

        /// <summary>
        /// handles the click event for the stop button
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            musicPlayer.Stop();
        }

        /// <summary>
        /// handles the click event for the pause button
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            musicPlayer.Pause();
        }

        /// <summary>
        /// handles event when a song is selected from playlist (listBox)
        /// </summary>
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

                if (musicPlayer == null)
                {
                    MessageBox.Show("Music player is not initialized!");
                    return;
                }

                if (string.IsNullOrEmpty(selectedSong.FilePath))
                {
                    MessageBox.Show("Selected song's file path is invalid!");
                    return;
                }
                musicPlayer.Play(selectedSong.FilePath);
            }
        }

        /// <summary>
        /// handles the scrolling event of trackBar that seeks through song
        /// </summary>
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (musicPlayer.CurrentState == PlaybackState.Playing || musicPlayer.CurrentState == PlaybackState.Paused)
            {
                int newSeconds = trackBar.Value;
                musicPlayer.Seek(newSeconds);
            }
        }

        /// <summary>
        /// timer tick event to update the UI elements related to the songs playback status
        /// </summary>
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (musicPlayer.CurrentState == PlaybackState.Playing)
            {
                TimeSpan currentTime = musicPlayer.CurrentTime;
                TimeSpan totalTime = musicPlayer.TotalTime;

                labelElapsedTime.Text = currentTime.ToString(@"mm\:ss");
                labelTotalTime.Text = totalTime.ToString(@"mm\:ss");
                trackBar.Value = (int)currentTime.TotalSeconds;
            }
        }


        /// <summary>
        /// handles the custom event to update the trackBar based on the playback status
        /// </summary>
        private void HandleTrackBarUpdate(object sender, TrackBarUpdateEventArgs e)
        {
            trackBar.Maximum = e.Maximum;
            trackBar.Value = e.Value;
        }


        /// <summary>
        /// handles the logic when <see cref="MusicPlayer.SongStartedEvent)"/> is triggered
        /// initializes the timer and updates the artist-song label
        /// </summary>
        private void MusicPlayer_SongStarted(object sender, EventArgs e)
        {
            InitializeTimer();

            Song currentSong = playlist.GetCurrentSong();
            if (currentSong != null)
            {
                LabelArtistSong.Text = $"{currentSong.Artist} - {currentSong.Title}";
            }
        }

        /// <summary>
        /// handles the event when the song stops playing
        /// stops the timer responsibly for the song progression updates
        /// </summary>
        private void MusicPlayer_SongStopped(object sender, EventArgs e)
        {
            StopTimer();
        }

        /// <summary>
        /// handles the logic for removing a selected song from the playlist
        /// </summary>
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

        /// <summary>
        /// plays the next song when the current song is removed
        /// </summary>
        private void Playlist_CurrentSongRemoved(object sender, EventArgs e)
        { 
            musicPlayer.Play();
        }

        /// <summary>
        /// handles the double-click event on a song in the playlist that allow song detail editing
        /// </summary>
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

        /// <summary>
        /// cancel the visibility of the UI elements for song detail elements
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            txtArtist.Visible = false;
            txtTitle.Visible = false;
            buttonSave.Visible = false;
            buttonCancel.Visible = false;
            labelArtist.Visible = false;
            labelTitle.Visible = false;
        }

        /// <summary>
        /// handles the save button click event when editing song details 
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (playlist1.SelectedIndex >= 0)
            {
                Song selectedSong = (Song)playlist1.SelectedItem;
                selectedSong.Artist = txtArtist.Text;
                selectedSong.Title = txtTitle.Text;

                Song currentSong = playlist.GetCurrentSong();  

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


        /// <summary>
        /// filters the songs based on a query, matching against song title and artist 
        /// </summary>
        /// <param name="query">the string for filtering songs</param>
        /// <returns> list of songs that match the query</returns>
        private List<Song> FilteredSongs (string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return playlist.Songs;

            return playlist.Songs.Where(s => 
                (s.Title != null && s.Title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 )
                || 
                (s.Artist!= null && s.Artist.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
            ).ToList();
        }

        /// <summary>
        /// handles the button search click event and refreshing the playlist, matching whats in the search bar
        /// </summary>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            RefreshPlaylist(txtSearch.Text);
        }
    }

    /// <summary>
    /// represents a song with properties like artist, title and file path
    /// </summary>
    public class Song
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }


        /// <summary>
        /// returns the string representation of the song in the format "Artist - Title"
        /// </summary>
        public override string ToString()
        {
            return $"{Artist} - {Title}";
        }
    }

    /// <summary>
    /// represents a collection of songs and provides functionality to manage and navigate through them
    /// </summary>
    public class Playlist
    {
        public event EventHandler SongStoppedEvent;
        public event EventHandler CurrentSongRemoved;

        public List<Song> Songs { get; } = new List<Song>();
        public int CurrentSongIndex = -1; // default to -1 if no song is selected


        /// <summary>
        /// returns the current song from playlist
        /// if no song is selected returns null
        /// </summary>
        public Song GetCurrentSong()
        {
            if (CurrentSongIndex >= 0 && CurrentSongIndex < Songs.Count)
            {
                return Songs[CurrentSongIndex];
            }
            return null;
        }


        /// <summary>
        /// moves to the next song in the playlist and returns it 
        /// </summary>
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

        /// <summary>
        /// moves to the previous song in the playlist and returns it
        /// </summary>
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

        /// <summary>
        /// adds song to playlist
        /// </summary>
        public void AddSong(Song song)
        {
            Songs.Add(song);
            CurrentSongIndex = Songs.Count - 1;
        }

        /// <summary>
        /// removes a song at the specified index
        /// </summary>
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
               CurrentSongRemoved?.Invoke(this, EventArgs.Empty); 
            }
        }
    }

    /// <summary>
    /// represents a music artist and their collection of songs
    /// </summary>
    public class Artist
    {
        public string Name { get; set; }
        private List<Song> songs = new List<Song>();
        public List<Song> Songs { get { return songs; } }
    }

    /// <summary>
    /// event arguments for updating a track bar with music playback progress
    /// </summary>
    public class TrackBarUpdateEventArgs : EventArgs
    {
        public int Maximum { get; set; }
        public int Value { get; set; }
    }

    public delegate void TrackBarUpdateHandler(object sender, TrackBarUpdateEventArgs e);

    /// <summary>
    /// represents the main music player and provides functionality to play, stop and manage music playback
    /// </summary>
    public class MusicPlayer
    {
        public event TrackBarUpdateHandler TrackBarUpdateEvent;
        public event EventHandler SongStartedEvent;
        public event EventHandler RequestStopTimer;
        public event EventHandler SongStoppedEvent;

        private IWavePlayer waveOutDevice; // Represents the device used for audio playback.
        private AudioFileReader audioFileReader; //Used to read the audio file specified by its path.
        private Playlist playlist;
        
        /// <summary>
        /// initializes a new instance of the MusicPlayer class
        /// </summary>
        /// <param name="playlist"> playlist that is used by the music player </param>
        public MusicPlayer(Playlist playlist)
        {
            this.playlist = playlist;
            waveOutDevice = new WaveOut();
            waveOutDevice.PlaybackStopped += WaveOutDevice_PlaybackStopped;
        }


        /// <summary>
        /// event handler for when playback stops in the WaveOutDevice
        /// </summary>
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

        /// <summary>
        /// current playback state of the music player
        /// </summary>
        public PlaybackState CurrentState { get; private set; } = PlaybackState.Stopped;  //the logic of music playing goes here


        /// <summary>
        /// starts or resumes playback of the current song in the playlist
        /// </summary>
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
        }

        /// <summary>
        /// plays the song specified by the file path
        /// </summary>
        /// <param name="filePath"> path to the audio file</param>
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
                }); 
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

        /// <summary>
        /// stops the current music playback and releases resources 
        /// </summary>
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

        /// <summary>
        /// pauses the current playback
        /// </summary>
        public void Pause()
        {
            if (CurrentState == PlaybackState.Playing)
            {
                waveOutDevice?.Pause();
                CurrentState = PlaybackState.Paused;
            }
        }

        /// <summary>
        /// adjusts the current playback position to the specified seconds
        /// </summary>
        /// <param name="seconds"> the position to seek to in seconds </param>
        public void Seek(int seconds)
        {
            if (audioFileReader != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(seconds);
            }
        }

        /// <summary>
        /// handles the event when playback is stopped
        /// </summary>
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

        /// <summary>
        /// returns the total duration of the currently loaded song
        /// </summary>
        public TimeSpan TotalTime
        {
            get
            {
                return audioFileReader?.TotalTime ?? TimeSpan.Zero;
            }
        }


        /// <summary>
        /// returns the current playback time of the song
        /// </summary>
        public TimeSpan CurrentTime
        {
            get
            {
                return audioFileReader?.CurrentTime ?? TimeSpan.Zero;
            }
        }
    }
}