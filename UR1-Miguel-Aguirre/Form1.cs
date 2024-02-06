using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing.Text;

namespace UR1_Miguel_Aguirre
{
    public partial class Form1 : Form
    {
        VideoCapture mCapture; // main capture object from Emgu.Cv
        Thread mCaptureThread; // video thread for multi-threading
        CancellationTokenSource mCancellationToken = new(); // for requesting thread termination

        bool mIsCapturing = false; // capturing state indicator

        private int minTrackBarValue = 0;
        private int maxTrackBarValue = 255;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                mCapture = new VideoCapture(0); // initialize with ifany plugged camera

                if (mCapture.Height == 0)
                    throw new Exception("No Cameras Found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartStopBtn_Click(object sender, EventArgs e)
        {
            if (mIsCapturing) // if live, stop it
            {
                mCancellationToken.Cancel(); // request a stop
                mIsCapturing = false; // indicate new state
                StartStopBtn.Text = "Start"; // inform accordingly
            }
            else
            {
                mCancellationToken = new(); // reinitialize
                mCaptureThread = new(() => DisplayWebcam(mCancellationToken.Token)); // initialize new thread
                mCaptureThread.Start(); // start it
                mIsCapturing = true; // indicate new state
                StartStopBtn.Text = "Stop"; // inform accordingly
            }
        }

        private void DisplayWebcam(CancellationToken token)
        {
            while (!token.IsCancellationRequested) // white no requested cancellation
            {
                Mat frame = mCapture.QueryFrame(); // grab a new frame

                // resize to PictureBox aspect ratio
                int newHeight = (frame.Size.Height * VideoPictureBox.Size.Width) / frame.Size.Width;
                Size newSize = new Size(VideoPictureBox.Size.Width, newHeight);
                CvInvoke.Resize(frame, frame, newSize);

                VideoPictureBox.Image = frame.ToBitmap(); // display current frame

                CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Gray); // Changing the image to gray
                CvInvoke.Threshold(frame, frame, minTrackBarValue, maxTrackBarValue, ThresholdType.Binary); // Apply binary thresholding

                VideoPictureBox2.Image = frame.ToBitmap(); // display current frame

                Task.Delay(16); // ~60fps -> 1000ms/60 = 16.6
            }
        }

        private void Form1_FormClosed(object sendet, FormClosedEventArgs e)
        {
            // Dispose all processing threads to avoid orphaned processes
            if (mIsCapturing)
            {
                mCancellationToken.Cancel();
            }
            mCapture.Dispose();
            mCancellationToken.Dispose();
        }

        private void TrackBarMin_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMin.Value > TrackBarMax.Value)
            {
                TrackBarMax.Value = TrackBarMin.Value;
            }

            minTrackBarValue = TrackBarMin.Value;
        }        
        
        private void TrackBarMax_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMin.Value > TrackBarMax.Value)
            {
                TrackBarMin.Value = TrackBarMax.Value;
            }

            maxTrackBarValue = TrackBarMax.Value;
        }
    }
}