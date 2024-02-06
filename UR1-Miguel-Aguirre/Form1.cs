using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing.Text;

namespace UR1_Miguel_Aguirre
{
    public partial class Form1 : Form
    {
        // main capture object from Emgu.Cv
        VideoCapture mCapture, _capture;

        // video thread for multi-threading
        Thread mCaptureThread, _captureThread;

        // for requesting thread termination
        CancellationTokenSource mCancellationToken = new(), _CancellationToken = new();

        // capturing state indicator
        bool mIsCapturing = false, _IsCapturing = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            try
            {
                // initialize with ifany plugged camera
                mCapture = new VideoCapture(0);
                _capture = new VideoCapture(0);

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
                _CancellationToken.Cancel(); // request a stop
                mIsCapturing = false; // indicate new state
                _IsCapturing = false; // indicate new state
                StartStopBtn.Text = "Start"; // inform accordingly
            }
            else
            {
                mCancellationToken = new(); // reinitialize
                _CancellationToken = new(); // reinitialize

                // initialize new thread
                mCaptureThread = new(() => DisplayWebcam(mCancellationToken.Token));
                _captureThread = new(() => ThresholdDisplayWebcam(_CancellationToken.Token));

                mCaptureThread.Start(); // start it
                _captureThread.Start(); // strat it

                mIsCapturing = true; // indicate new state
                _IsCapturing = true; // indicate new state
                
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

                // display the image in PictureBox
            }
        }
        private void ThresholdDisplayWebcam(CancellationToken token)
        {
            while (!token.IsCancellationRequested) // wait no requested cancellation
            {
                Mat frame = mCapture.QueryFrame(); // grab a new frame

                // resize to PictureBox aspect ratio
                int newHeight = (frame.Size.Height * VideoPictureBox.Size.Width) / frame.Size.Width;
                Size newSize = new Size(VideoPictureBox.Size.Width, newHeight);
                CvInvoke.Resize(frame, frame, newSize);

                // Convert the frame to grayscale if it's not already
                if (frame.NumberOfChannels > 1)
                {
                    CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Gray);
                }

                // Apply binary thresholding
                double thresholdValue = 128; // You can adjust this threshold value as needed
                CvInvoke.Threshold(frame, frame, thresholdValue, 255, ThresholdType.Binary);

                // ~60fps -> 1000ms/60 = 16.6
                Task.Delay(16);

                // display the image in PictureBox
                emguPictureBox.Image = frame.ToBitmap();
            }
        }


        private void Form1_FormClosed(object sendet, FormClosedEventArgs e)
        {
            // Dispose all processing threads to avoid orphaned processes
            if (mIsCapturing)
            {
                mCancellationToken.Cancel();
                _CancellationToken.Cancel();
            }
            mCapture.Dispose();
            mCancellationToken.Dispose(); 
            _capture.Dispose();
            _CancellationToken.Dispose();
        }
    }
}