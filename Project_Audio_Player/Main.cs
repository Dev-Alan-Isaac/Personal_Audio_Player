using NAudio.Gui;
using NAudio.Wave;

namespace Project_Audio_Player
{
    public partial class Main : Form
    {
        private string selectedFilePath; // Variable para almacenar la ruta del archivo seleccionado
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private float playbackPosition = 0; // Initialize playback position

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Mostrar el cuadro de diálogo de archivo
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de audio|*.wav;*.aiff;*.mp3;*.flac|Todos los archivos|*.*";
                openFileDialog.Title = "Selecciona un archivo de audio";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtener la ruta del archivo seleccionado
                    selectedFilePath = openFileDialog.FileName;

                    if (audioFile == null)
                    {
                        audioFile = new AudioFileReader(selectedFilePath);
                    }
                }
            }
        }

        // Play

        private void button1_Click(object sender, EventArgs e)
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
            }
            outputDevice.Init(audioFile);

            // Subscribe to the PlaybackStopped event
            outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
            outputDevice.Play();

            // Start a timer or use a separate thread to update playbackPosition
            //Example:
            timer1.Tick += (s, args) => { playbackPosition = GetCurrentPlaybackPosition(); };
        }

        private float GetCurrentPlaybackPosition()
        {
            // Calculate the current playback position based on audioFile.Position
            if (audioFile != null)
            {
                long currentPositionBytes = audioFile.Position;
                double seconds = (double)currentPositionBytes / audioFile.WaveFormat.AverageBytesPerSecond;
                return (float)seconds;
            }
            else
            {
                // Return 0 if audioFile is not initialized
                return 0f;
            }
        }

        private void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Dispose of resources when playback stops
            outputDevice.Dispose();
            audioFile.Dispose();
            outputDevice = null;
            audioFile = null;
        }

        // Pause
        private void button2_Click(object sender, EventArgs e)
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Pause();
            }
        }

        private void waveformPainter1_Paint(object sender, PaintEventArgs e)
        {
            if (audioFile != null && !string.IsNullOrEmpty(selectedFilePath))
            {
                int width = waveformPainter1.Width;
                int height = waveformPainter1.Height;
                int samplesPerPixel = audioFile.WaveFormat.SampleRate / width;
                int bufferSize = samplesPerPixel * audioFile.WaveFormat.Channels;
                float[] buffer = new float[bufferSize];

                // Calculate the current playback position in seconds
                playbackPosition = (float)audioFile.Position / audioFile.WaveFormat.AverageBytesPerSecond;

                // Draw the waveform
                for (int x = 0; x < width; x++)
                {
                    int bytesRead = audioFile.Read(buffer, 0, bufferSize);
                    if (bytesRead == 0) break;

                    float maxSample = buffer.Take(bytesRead).Max();
                    float minSample = buffer.Take(bytesRead).Min();

                    int yTop = (int)(height * (1 - maxSample));
                    int yBottom = (int)(height * (1 - minSample));

                    e.Graphics.DrawLine(Pens.Blue, x, yTop, x, yBottom);
                }

                // Calculate the current playback position in pixels
                int playbackX = (int)(width * playbackPosition);

                // Draw the playback position line
                e.Graphics.DrawLine(Pens.Red, playbackX, 0, playbackX, height);
            }
        }
    }
}
