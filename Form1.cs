using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;
using TagLib;

namespace music_player
{
    public enum PlaybackState
    {
        Stopped,
        Playing,
        Paused
    }

    public partial class Form1 : Form //class for the UI
    {
        private struct SongInfo  //to keep track of the current song 
        {
            public string Title;
            public string Artist;
            //public string Lenght;
        }

        private SongInfo currentSong = new SongInfo();
        private Playlist playlist;
        private MusicPlayer musicPlayer;
        private string currentFilePath = null;
        private Timer updateTimer;

        private void RefreshPlaylist()
        {
            playlist1.Items.Clear();
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
       
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; //ensures that the form starts in the middle
            CenterContent();
            playlist = new Playlist();
            musicPlayer = new MusicPlayer(playlist);

            musicPlayer.TrackBarUpdateEvent += HandleTrackBarUpdate;
            musicPlayer.SongStoppedEvent += MusicPlayer_SongStopped;
            musicPlayer.SongStartedEvent += MusicPlayer_SongStarted;
    }

        private void CenterContent()
        {
            //something to center the content
        }

        //event handlers and ui stuff

        private void button1_Click(object sender, EventArgs e) //button for browsing
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = playlist1.SelectedIndex;
            if (selectedIndex >= 0)
            {
                playlist.CurrentSongIndex = selectedIndex;
                Song selectedSong = playlist.GetCurrentSong();
                musicPlayer.Play(selectedSong.FilePath);

            }
        }

        private void UpdateButtonStates()
        {
            button2.Enabled = currentFilePath != null;
            button3.Enabled = playlist.CurrentSongIndex < playlist.Songs.Count - 1;
            button4.Enabled = playlist.CurrentSongIndex > 0; // Enable previous if there are songs before
            button5.Enabled = musicPlayer.CurrentState == PlaybackState.Playing; // Enable stop if playing
            button6.Enabled = musicPlayer.CurrentState == PlaybackState.Playing; // Enable pause if playing
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

        private Song GetSongMetadata (string filepath)
        {
            Song song = new Song();
            try
            {
                var file = TagLib.File.Create(filepath);
                song.Title = file.Tag.Title ?? "title";
                song.Artist = file.Tag.FirstPerformer ?? "artist";
                song.Genre = file.Tag.FirstGenre ?? "genre";
                song.FilePath = filepath;

                if (file.Tag.Pictures.Length > 0)
                {
                    var artwork = file.Tag.Pictures[0].Data;
                    song.Artwork = artwork.Data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing the file: {ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return song;

        }
    }


    //class representing songs
    public class Song
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
                return GetCurrentSong();
            }
            return null;
        }

        public Song GetPreviousSong()
        {
            if (CurrentSongIndex - 1 >= 0)
            {
                CurrentSongIndex--;
                return GetCurrentSong();
            }
            return null;
        }

        public void AddSong(Song song)
        {
            Songs.Add(song);
            CurrentSongIndex = Songs.Count - 1;
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
        }

        public void Play(string filePath)  //method to play specific file
        {
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
        public void Seek (int seconds)
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
            } else
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

    public class MusicLibrary
    {
        //database or smth similar goes here 
    }
}