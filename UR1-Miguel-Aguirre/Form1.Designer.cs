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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)VideoPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VideoPictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarMin).BeginInit();
            SuspendLayout();
            // 
            // StartStopBtn
            // 
            StartStopBtn.Location = new Point(818, 760);
            StartStopBtn.Name = "StartStopBtn";
            StartStopBtn.Size = new Size(170, 71);
            StartStopBtn.TabIndex = 0;
            StartStopBtn.Text = "Start";
            StartStopBtn.UseVisualStyleBackColor = true;
            StartStopBtn.Click += StartStopBtn_Click;
            // 
            // VideoPictureBox
            // 
            VideoPictureBox.Location = new Point(12, 12);
            VideoPictureBox.Name = "VideoPictureBox";
            VideoPictureBox.Size = new Size(874, 673);
            VideoPictureBox.TabIndex = 1;
            VideoPictureBox.TabStop = false;
            // 
            // VideoPictureBox2
            // 
            VideoPictureBox2.Location = new Point(976, 12);
            VideoPictureBox2.Name = "VideoPictureBox2";
            VideoPictureBox2.Size = new Size(874, 673);
            VideoPictureBox2.TabIndex = 2;
            VideoPictureBox2.TabStop = false;
            // 
            // TrackBarMax
            // 
            TrackBarMax.Location = new Point(600, 837);
            TrackBarMax.Maximum = 255;
            TrackBarMax.Name = "TrackBarMax";
            TrackBarMax.Size = new Size(604, 90);
            TrackBarMax.TabIndex = 3;
            TrackBarMax.Value = 255;
            TrackBarMax.Visible = false;
            TrackBarMax.Scroll += TrackBarMax_Scroll;
            // 
            // TrackBarMin
            // 
            TrackBarMin.Location = new Point(600, 933);
            TrackBarMin.Maximum = 255;
            TrackBarMin.Name = "TrackBarMin";
            TrackBarMin.Size = new Size(604, 90);
            TrackBarMin.TabIndex = 4;
            TrackBarMin.Visible = false;
            TrackBarMin.Scroll += TrackBarMin_Scroll;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(976, 691);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(170, 39);
            textBox1.TabIndex = 5;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.Visible = false;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1152, 691);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(170, 39);
            textBox2.TabIndex = 6;
            textBox2.TextAlign = HorizontalAlignment.Center;
            textBox2.Visible = false;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(1328, 691);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(170, 39);
            textBox3.TabIndex = 7;
            textBox3.TextAlign = HorizontalAlignment.Center;
            textBox3.Visible = false;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(1504, 691);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(170, 39);
            textBox4.TabIndex = 8;
            textBox4.TextAlign = HorizontalAlignment.Center;
            textBox4.Visible = false;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(1680, 691);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(170, 39);
            textBox5.TabIndex = 9;
            textBox5.TextAlign = HorizontalAlignment.Center;
            textBox5.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1862, 1035);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
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
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
    }
}