namespace UR1_Miguel_Aguirre
{
    partial class Form1
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
            StartStopBtn = new Button();
            VideoPictureBox = new PictureBox();
            VideoPictureBox2 = new PictureBox();
            TrackBarMax = new TrackBar();
            TrackBarMin = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)VideoPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VideoPictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarMin).BeginInit();
            SuspendLayout();
            // 
            // StartStopBtn
            // 
            StartStopBtn.Location = new Point(628, 439);
            StartStopBtn.Name = "StartStopBtn";
            StartStopBtn.Size = new Size(197, 71);
            StartStopBtn.TabIndex = 0;
            StartStopBtn.Text = "Start";
            StartStopBtn.UseVisualStyleBackColor = true;
            StartStopBtn.Click += StartStopBtn_Click;
            // 
            // VideoPictureBox
            // 
            VideoPictureBox.Location = new Point(118, 21);
            VideoPictureBox.Name = "VideoPictureBox";
            VideoPictureBox.Size = new Size(582, 392);
            VideoPictureBox.TabIndex = 1;
            VideoPictureBox.TabStop = false;
            // 
            // VideoPictureBox2
            // 
            VideoPictureBox2.Location = new Point(748, 21);
            VideoPictureBox2.Name = "VideoPictureBox2";
            VideoPictureBox2.Size = new Size(640, 392);
            VideoPictureBox2.TabIndex = 2;
            VideoPictureBox2.TabStop = false;
            // 
            // TrackBarMax
            // 
            TrackBarMax.Location = new Point(435, 561);
            TrackBarMax.Maximum = 255;
            TrackBarMax.Name = "TrackBarMax";
            TrackBarMax.Size = new Size(567, 90);
            TrackBarMax.TabIndex = 3;
            TrackBarMax.Value = 255;
            TrackBarMax.Scroll += TrackBarMax_Scroll;
            // 
            // TrackBarMin
            // 
            TrackBarMin.Location = new Point(435, 657);
            TrackBarMin.Maximum = 255;
            TrackBarMin.Name = "TrackBarMin";
            TrackBarMin.Size = new Size(567, 90);
            TrackBarMin.TabIndex = 4;
            TrackBarMin.Scroll += TrackBarMin_Scroll;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1514, 765);
            Controls.Add(TrackBarMin);
            Controls.Add(TrackBarMax);
            Controls.Add(VideoPictureBox2);
            Controls.Add(VideoPictureBox);
            Controls.Add(StartStopBtn);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)VideoPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)VideoPictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarMin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartStopBtn;
        private PictureBox VideoPictureBox;
        private PictureBox VideoPictureBox2;
        private TrackBar TrackBarMax;
        private TrackBar TrackBarMin;
    }
}