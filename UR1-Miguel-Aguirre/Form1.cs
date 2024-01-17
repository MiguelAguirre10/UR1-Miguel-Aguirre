using Emgu.CV;
using System.Drawing.Text;

namespace UR1_Miguel_Aguirre
{
    public partial class Form1 : Form
    {
        // main capture object from Emgu.Cv
        VideoCapture mCapture;

        // video thread for multi-threading
        Thread mCaptureThread;

        // for requesting thread termination
        CancellationTokenSource mCancellationToken = new();

        // capturing state indicator
        bool mIsCapturing = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // initialize with ifany plugged camera
                mCapture = new VideoCapture(0);

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

                // initialize new thread
                mCaptureThread = new(() => DisplayWebcam(mCancellationToken.Token));

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

                // ~60fps -> 1000ms/60 = 16.6
                Task.Delay(16);

                VideoPictureBox.Image = frame.ToBitmap(); // display current frame
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
    }
}