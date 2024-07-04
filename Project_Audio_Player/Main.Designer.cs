namespace Project_Audio_Player
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            button1 = new Button();
            button2 = new Button();
            waveformPainter1 = new NAudio.Gui.WaveformPainter();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(waveformPainter1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 164);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button2);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 385);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 65);
            panel2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(12, 0);
            button1.Name = "button1";
            button1.Size = new Size(68, 65);
            button1.TabIndex = 0;
            button1.Text = "Play";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(115, 0);
            button2.Name = "button2";
            button2.Size = new Size(68, 65);
            button2.TabIndex = 0;
            button2.Text = "Pause";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // waveformPainter1
            // 
            waveformPainter1.Location = new Point(12, 12);
            waveformPainter1.Name = "waveformPainter1";
            waveformPainter1.Size = new Size(776, 99);
            waveformPainter1.TabIndex = 0;
            waveformPainter1.Text = "waveformPainter1";
            waveformPainter1.Paint += waveformPainter1_Paint;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "Main";
            Text = "Form1";
            Load += Main_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Button button1;
        private Button button2;
        private NAudio.Gui.WaveformPainter waveformPainter1;
    }
}
