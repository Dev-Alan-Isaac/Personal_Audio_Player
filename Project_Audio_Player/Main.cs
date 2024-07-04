using NAudio.Wave;

namespace Project_Audio_Player
{
    public partial class Main : Form
    {
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;

        public Main()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load your audio file
            string audioFilePath = "path/to/your/audio.wav";
            audioFileReader = new AudioFileReader(audioFilePath);

            // Initialize WaveOutEvent for audio playback
            waveOut = new WaveOutEvent();
            waveOut.Init(audioFileReader);

            // Subscribe to PositionChanged event
            waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
            waveOut.Play();
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Handle playback stopped (e.g., reset visualizer position)
        }

        private void pictureBoxWaveform_Paint(object sender, PaintEventArgs e)
        {
            // Calculate visualizer position based on audio playback position
            float playbackPosition = (float)audioFileReader.CurrentTime.TotalSeconds;
            float totalDuration = (float)audioFileReader.TotalTime.TotalSeconds;
            float visualizerWidth = pictureBoxWaveform.Width;

            float visualizerX = playbackPosition / totalDuration * visualizerWidth;

            // Draw a vertical line (you can customize this)
            using (var pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawLine(pen, visualizerX, 0, visualizerX, pictureBoxWaveform.Height);
            }
        }

    }
}
