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

        private int minTrackBarValue = 0, minTrackBarValueH = 0, minTrackBarValueS = 0, minTrackBarValueV = 0;
        private int maxTrackBarValue = 255, maxTrackBarValueH = 255, maxTrackBarValueS = 255, maxTrackBarValueV = 255;

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
                TrackBarMax.Visible = false; // make the max track bar invisible
                TrackBarMin.Visible = false; // make the min track bar invisible
                TrackBarMaxH.Visible = false; // make the max track bar H invisible
                TrackBarMinH.Visible = false; // make the min track bar H invisible
                TrackBarMaxS.Visible = false; // make the max track bar S invisible
                TrackBarMinS.Visible = false; // make the min track bar S invisible
                TrackBarMaxV.Visible = false; // make the max track bar V invisible
                TrackBarMinV.Visible = false; // make the min track bar V invisible
            }
            else
            {
                mCancellationToken = new(); // reinitialize
                mCaptureThread = new(() => DisplayWebcam(mCancellationToken.Token)); // initialize new thread
                mCaptureThread.Start(); // start it
                mIsCapturing = true; // indicate new state
                StartStopBtn.Text = "Stop"; // inform accordingly
                TrackBarMax.Visible = true; // make the max track bar visible
                TrackBarMin.Visible = true; // make the min track bar visible
                TrackBarMaxH.Visible = true; // make the max track bar H visible
                TrackBarMinH.Visible = true; // make the min track bar H visible
                TrackBarMaxS.Visible = true; // make the max track bar S visible
                TrackBarMinS.Visible = true; // make the min track bar S visible
                TrackBarMaxV.Visible = true; // make the max track bar V visible
                TrackBarMinV.Visible = true; // make the min track bar V visible
                textBox1.Visible = true; // make textBox1 visible
                textBox2.Visible = true; // make textBox2 visible
                textBox3.Visible = true; // make textBox3 visible
                textBox4.Visible = true; // make textBox4 visible
                textBox5.Visible = true; // make textBox5 visible
                label1.Visible = true; // make label1 visible
                label2.Visible = true; // make label2 visible
                label3.Visible = true; // make label3 visible
                label4.Visible = true; // make label4 visible
                label5.Visible = true; // make label5 visible
                label6.Visible = true; // make label6 visible
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
                CvInvoke.Flip(frame, frame, FlipType.Horizontal); // flip display frame horizontally
 
                VideoPictureBox.Image = frame.ToBitmap(); // display current raw frame

                Mat hsvFrame = new Mat();
                CvInvoke.CvtColor(frame, hsvFrame, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv);

                Mat[] hsvChannels = hsvFrame.Split();

                Mat hueFilter = new Mat();
                CvInvoke.InRange(hsvChannels[0], new ScalarArray(minTrackBarValueH), new ScalarArray(maxTrackBarValueH), hueFilter);
                
                VideoPictureBox3.Image = hueFilter.ToBitmap(); // display current hue frame

                Mat saturationFilter = new Mat();
                CvInvoke.InRange(hsvChannels[1], new ScalarArray(minTrackBarValueS), new ScalarArray(maxTrackBarValueS), saturationFilter);
                
                VideoPictureBox4.Image = saturationFilter.ToBitmap(); // display current saturation frame

                Mat valueFilter = new Mat();
                CvInvoke.InRange(hsvChannels[2], new ScalarArray(minTrackBarValueV), new ScalarArray(maxTrackBarValueV), valueFilter);
                
                VideoPictureBox5.Image = valueFilter.ToBitmap(); // display current value frame

                Mat mergedImage = new Mat();
                CvInvoke.BitwiseAnd(hueFilter, saturationFilter, mergedImage);
                CvInvoke.BitwiseAnd(mergedImage, valueFilter, mergedImage);
                VideoPictureBox6.Image = mergedImage.ToBitmap(); // display current merged frame

                CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Gray); // changing the image to gray
                CvInvoke.Threshold(frame, frame, minTrackBarValue, maxTrackBarValue, ThresholdType.Binary); // apply binary thresholding

                VideoPictureBox2.Image = frame.ToBitmap(); // display current binary frame

                int columnWidth = frame.Width / 5; // divide the frame width into 5 sections to create 5 columns
                Mat[] columns = new Mat[5]; // create an array to store the individual columns of the image
                for (int i = 0; i < 5; i++) // iterate through each column
                {
                    int startX = i * columnWidth; // calculate the starting x-coordinate of the current column
                    int endX = (i == 4) ? frame.Width : (i + 1) * columnWidth; // calculate the ending x-coordinate of the current column
                    Rectangle roi = new Rectangle(startX, 0, endX - startX, frame.Height); // define a region of interest (ROI) representing the current column
                    columns[i] = new Mat(frame, roi); // extract the current column from the frame using the defined ROI
                }

                int[] whitePixelsCounts = new int[5]; // int array to apply the corresponding pixel count to the corresponding column section
                for (int i = 0; i < 5; i++) // for loop for int array
                {
                    whitePixelsCounts[i] = CvInvoke.CountNonZero(columns[i]); // count the number of white pixels
                }

                Invoke(new Action(() => // apply the right int array so that the pixel count for each section is on the right text box
                {
                    textBox1.Text = $"{whitePixelsCounts[0]}";
                    textBox2.Text = $"{whitePixelsCounts[1]}";
                    textBox3.Text = $"{whitePixelsCounts[2]}";
                    textBox4.Text = $"{whitePixelsCounts[3]}";
                    textBox5.Text = $"{whitePixelsCounts[4]}";
                }));

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

        private void TrackBarMaxH_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinH.Value > TrackBarMaxH.Value)
            {
                TrackBarMinH.Value = TrackBarMaxH.Value;
            }

            maxTrackBarValueH = TrackBarMaxH.Value;
        }

        private void TrackBarMinH_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinH.Value > TrackBarMaxH.Value)
            {
                TrackBarMaxH.Value = TrackBarMinH.Value;
            }

            minTrackBarValueH = TrackBarMinH.Value;
        }

        private void TrackBarMaxS_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinS.Value > TrackBarMaxS.Value)
            {
                TrackBarMinS.Value = TrackBarMaxS.Value;
            }

            maxTrackBarValueS = TrackBarMaxS.Value;
        }

        private void TrackBarMinS_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinS.Value > TrackBarMaxS.Value)
            {
                TrackBarMaxS.Value = TrackBarMinS.Value;
            }

            minTrackBarValueS = TrackBarMinS.Value;
        }

        private void TrackBarMaxV_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinV.Value > TrackBarMaxV.Value)
            {
                TrackBarMinV.Value = TrackBarMaxV.Value;
            }

            maxTrackBarValueV = TrackBarMaxV.Value;
        }

        private void TrackBarMinV_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinV.Value > TrackBarMaxV.Value)
            {
                TrackBarMaxV.Value = TrackBarMinV.Value;
            }

            minTrackBarValueV = TrackBarMinV.Value;
        }
    }
}