using NAudio.Gui;
using NAudio.Wave;

namespace Project_Audio_Player
{
    public partial class Main : Form
    {
        private string selectedFilePath; // Variable para almacenar la ruta del archivo seleccionado

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

                    if (!string.IsNullOrEmpty(selectedFilePath))
                    {
                        try
                        {
                            using (var audioFileReader = new AudioFileReader(selectedFilePath))
                            {
                                using (var outputDevice = new WaveOutEvent())
                                {
                                    outputDevice.Init(audioFileReader);
                                    outputDevice.Play();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Manejar excepciones (por ejemplo, archivo no encontrado, formato no compatible)
                            MessageBox.Show($"Error al reproducir el audio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // Play
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                try
                {
                    using (var audioFileReader = new AudioFileReader(selectedFilePath))
                    {
                        using (var outputDevice = new WaveOutEvent())
                        {
                            outputDevice.Init(audioFileReader);
                            outputDevice.Play();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones (por ejemplo, archivo no encontrado, formato no compatible)
                    MessageBox.Show($"Error al reproducir el audio: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        // Pause
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void waveformPainter1_Paint(object sender, PaintEventArgs e)
        {

            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                // Crear un lector de audio para el archivo
                using (var audioFileReader = new AudioFileReader(selectedFilePath))
                {
                    // Configurar los parámetros para la representación de la forma de onda
                    int width = waveformPainter1.Width;
                    int height = waveformPainter1.Height;
                    int samplesPerPixel = audioFileReader.WaveFormat.SampleRate / width;
                    int bufferSize = samplesPerPixel * audioFileReader.WaveFormat.Channels;
                    float[] buffer = new float[bufferSize];

                    // Dibujar la forma de onda
                    for (int x = 0; x < width; x++)
                    {
                        int bytesRead = audioFileReader.Read(buffer, 0, bufferSize);
                        if (bytesRead == 0) break;

                        float maxSample = buffer.Take(bytesRead).Max();
                        float minSample = buffer.Take(bytesRead).Min();

                        int yTop = (int)(height * (1 - maxSample));
                        int yBottom = (int)(height * (1 - minSample));

                        e.Graphics.DrawLine(Pens.Blue, x, yTop, x, yBottom);
                    }
                }
            }
        }

    }
}
