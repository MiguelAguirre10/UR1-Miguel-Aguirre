using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing.Text;
using System.IO.Ports;

namespace UR1_Miguel_Aguirre
{
    public partial class Form1 : Form
    {
        VideoCapture mCapture; // main capture object from Emgu.Cv
        Thread mCaptureThread; // video thread for multi-threading
        CancellationTokenSource mCancellationToken = new(); // for requesting thread termination
        SerialPort port;

        bool mIsCapturing = false; // capturing state indicator

        private int minTrackBarValue = 190, minTrackBarValueHR = 0, minTrackBarValueSR = 120, minTrackBarValueVR = 90, minTrackBarValueHY = 15, minTrackBarValueSY = 45, minTrackBarValueVY = 140;
        private int maxTrackBarValue = 255, maxTrackBarValueHR = 180, maxTrackBarValueSR = 185, maxTrackBarValueVR = 155, maxTrackBarValueHY = 35, maxTrackBarValueSY = 150, maxTrackBarValueVY = 220;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                mCapture = new VideoCapture(1); // initialize with ifany plugged camera

                if (mCapture.Height == 0)
                    throw new Exception("No Cameras Found");

                if (port == null)
                {
                    port = new SerialPort("COM24", 9600); // Set your board COM
                    port.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
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
                TrackBarMaxHR.Visible = false; // make the max track bar HR invisible
                TrackBarMinHR.Visible = false; // make the min track bar HR invisible
                TrackBarMaxSR.Visible = false; // make the max track bar SR invisible
                TrackBarMinSR.Visible = false; // make the min track bar SR invisible
                TrackBarMaxVR.Visible = false; // make the max track bar VR invisible
                TrackBarMinVR.Visible = false; // make the min track bar VR invisible
                TrackBarMaxHY.Visible = false; // make the max track bar HY invisible
                TrackBarMinHY.Visible = false; // make the min track bar HY invisible
                TrackBarMaxSY.Visible = false; // make the max track bar SY invisible
                TrackBarMinSY.Visible = false; // make the min track bar SY invisible
                TrackBarMaxVY.Visible = false; // make the max track bar VY invisible
                TrackBarMinVY.Visible = false; // make the min track bar VY invisible
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
                TrackBarMaxHR.Visible = true; // make the max track bar HR visible
                TrackBarMinHR.Visible = true; // make the min track bar HR visible
                TrackBarMaxSR.Visible = true; // make the max track bar SR visible
                TrackBarMinSR.Visible = true; // make the min track bar SR visible
                TrackBarMaxVR.Visible = true; // make the max track bar VR visible
                TrackBarMinVR.Visible = true; // make the min track bar VR visible
                TrackBarMaxHY.Visible = true; // make the max track bar HY visible
                TrackBarMinHY.Visible = true; // make the min track bar HY visible
                TrackBarMaxSY.Visible = true; // make the max track bar SY visible
                TrackBarMinSY.Visible = true; // make the min track bar SY visible
                TrackBarMaxVY.Visible = true; // make the max track bar VY visible
                TrackBarMinVY.Visible = true; // make the min track bar VY visible
                textBox1.Visible = true; // make textBox1 visible
                textBox2.Visible = true; // make textBox2 visible
                textBox3.Visible = true; // make textBox3 visible
                textBox4.Visible = true; // make textBox4 visible
                textBox5.Visible = true; // make textBox5 visible
                textBox6.Visible = true; // make textBox6 visible
                textBox7.Visible = true; // make textBox7 visible
                textBox8.Visible = true; // make textBox8 visible
                textBox9.Visible = true; // make textBox9 visible
                textBox10.Visible = true; // make textBox10 visible
                textBox11.Visible = true; // make textBox11 visible
                textBox12.Visible = true; // make textBox12 visible
                textBox13.Visible = true; // make textBox13 visible
                label1.Visible = true; // make label1 visible
                label2.Visible = true; // make label2 visible
                label3.Visible = true; // make label3 visible
                label4.Visible = true; // make label4 visible
                label5.Visible = true; // make label5 visible
                label6.Visible = true; // make label6 visible
                label7.Visible = true; // make label7 visible
                label8.Visible = true; // make label8 visible
                label9.Visible = true; // make label9 visible
                label10.Visible = true; // make label10 visible
                label11.Visible = true; // make label11 visible
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

                VideoPictureBox.Image = frame.Clone().ToBitmap(); // display current raw frame

                Mat hsvFrame = new Mat();
                CvInvoke.CvtColor(frame, hsvFrame, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv); // convert the image to an HSV image

                Mat[] hsvChannels = hsvFrame.Split(); // split the HSV image into an array of Mat objects, one per channel:

                Mat hueFilter = new Mat();
                CvInvoke.InRange(hsvChannels[0], new ScalarArray(minTrackBarValueHR), new ScalarArray(maxTrackBarValueHR), hueFilter); // filter out all but "the color you want"?

                VideoPictureBox3.Image = hueFilter.Clone().ToBitmap(); // display current hue frame

                Mat saturationFilter = new Mat();
                CvInvoke.InRange(hsvChannels[1], new ScalarArray(minTrackBarValueSR), new ScalarArray(maxTrackBarValueSR), saturationFilter); // use the saturation channel to filter out all but certain saturations

                VideoPictureBox4.Image = saturationFilter.Clone().ToBitmap(); // display current saturation frame

                Mat valueFilter = new Mat();
                CvInvoke.InRange(hsvChannels[2], new ScalarArray(minTrackBarValueVR), new ScalarArray(maxTrackBarValueVR), valueFilter); // use the calue channel to filter out all but brighter colors

                VideoPictureBox5.Image = valueFilter.Clone().ToBitmap(); // display current value frame

                // now combine the filters together:
                Mat mergedImage = new Mat();
                CvInvoke.BitwiseAnd(hueFilter, saturationFilter, mergedImage);
                CvInvoke.BitwiseAnd(mergedImage, valueFilter, mergedImage);

                VideoPictureBox6.Image = mergedImage.Clone().ToBitmap(); // display current merged frame

                int redPixelsCounts; // int array to apply the corresponding pixel count to the corresponding column section
                redPixelsCounts = CvInvoke.CountNonZero(mergedImage); // count the number of red pixels

                Mat hsvFrame2 = new Mat();
                CvInvoke.CvtColor(frame, hsvFrame2, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv); // convert the image to an HSV image

                Mat[] hsvChannels2 = hsvFrame2.Split(); // split the HSV image into an array of Mat objects, one per channel:

                Mat hueFilter2 = new Mat();
                CvInvoke.InRange(hsvChannels2[0], new ScalarArray(minTrackBarValueHY), new ScalarArray(maxTrackBarValueHY), hueFilter2); // filter out all but "the color you want"?

                VideoPictureBox7.Image = hueFilter2.Clone().ToBitmap(); // display current hue frame

                Mat saturationFilter2 = new Mat();
                CvInvoke.InRange(hsvChannels2[1], new ScalarArray(minTrackBarValueSY), new ScalarArray(maxTrackBarValueSY), saturationFilter2); // use the saturation channel to filter out all but certain saturations

                VideoPictureBox8.Image = saturationFilter2.Clone().ToBitmap(); // display current saturation frame

                Mat valueFilter2 = new Mat();
                CvInvoke.InRange(hsvChannels2[2], new ScalarArray(minTrackBarValueVY), new ScalarArray(maxTrackBarValueVY), valueFilter2); // use the calue channel to filter out all but brighter colors

                VideoPictureBox9.Image = valueFilter2.Clone().ToBitmap(); // display current value frame

                // now combine the filters together:
                Mat mergedImage2 = new Mat();
                CvInvoke.BitwiseAnd(hueFilter2, saturationFilter2, mergedImage2);
                CvInvoke.BitwiseAnd(mergedImage2, valueFilter2, mergedImage2);

                VideoPictureBox10.Image = mergedImage2.Clone().ToBitmap(); // display current merged frame
               
                int columnWidthY = mergedImage2.Width / 5; // divide the yellow merged frame width into 5 sections to create 5 columns
                Mat[] columnsY = new Mat[5]; // create an array to store the individual columns of the image
                for (int i = 0; i < 5; i++) // iterate through each column
                {
                    int startXY = i * columnWidthY; // calculate the starting x-coordinate of the current column
                    int endXY = (i == 4) ? mergedImage2.Width : (i + 1) * columnWidthY; // calculate the ending x-coordinate of the current column
                    Rectangle roiY = new Rectangle(startXY, 0, endXY - startXY, mergedImage2.Height); // define a region of interest (ROI) representing the current column
                    columnsY[i] = new Mat(mergedImage2, roiY); // extract the current column from the frame using the defined ROI
                }

                int[] yellowPixelsCounts = new int[5]; // int array to apply the corresponding pixel count to the corresponding column section
                for (int i = 0; i < 5; i++) // for loop for int array
                {
                    yellowPixelsCounts[i] = CvInvoke.CountNonZero(columnsY[i]); // count the number of yellow pixels
                }

                CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Gray); // changing the image to gray
                CvInvoke.Threshold(frame, frame, minTrackBarValue, maxTrackBarValue, ThresholdType.Binary); // apply binary thresholding

                VideoPictureBox2.Image = frame.Clone().ToBitmap(); // display current binary frame

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

                int dataP = 2;
                int stateNumber = 0;
                string state = "NULL";

                if (yellowPixelsCounts[2] > yellowPixelsCounts[0] && yellowPixelsCounts[2] > yellowPixelsCounts[1] && yellowPixelsCounts[2] > yellowPixelsCounts[3] && yellowPixelsCounts[2] > yellowPixelsCounts[4])
                {
                    dataP = 2;
                    stateNumber = 2;
                    state = "FORWARD";
                }
                else if (yellowPixelsCounts[0] > yellowPixelsCounts[1] && yellowPixelsCounts[0] > yellowPixelsCounts[2] && yellowPixelsCounts[0] > yellowPixelsCounts[3] && yellowPixelsCounts[0] > yellowPixelsCounts[4])
                {
                    dataP = 4;
                    stateNumber = 4;
                    state = "HARD RIGHT";
                }
                else if (yellowPixelsCounts[1] > yellowPixelsCounts[0] && yellowPixelsCounts[1] > yellowPixelsCounts[2] && yellowPixelsCounts[1] > yellowPixelsCounts[3] && yellowPixelsCounts[1] > yellowPixelsCounts[4])
                {
                    dataP = 3;
                    stateNumber = 3;
                    state = "RIGHT";
                }
                else if (yellowPixelsCounts[3] > yellowPixelsCounts[0] && yellowPixelsCounts[3] > yellowPixelsCounts[1] && yellowPixelsCounts[3] > yellowPixelsCounts[2] && yellowPixelsCounts[3] > yellowPixelsCounts[4])
                {
                    dataP = 1;
                    stateNumber = 1;
                    state = "LEFT";
                }
                else if (yellowPixelsCounts[4] > yellowPixelsCounts[0] && yellowPixelsCounts[4] > yellowPixelsCounts[1] && yellowPixelsCounts[4] > yellowPixelsCounts[2] && yellowPixelsCounts[4] > yellowPixelsCounts[3])
                {
                    dataP = 0;
                    stateNumber = 0;
                    state = "HARD LEFT";
                }
                if (redPixelsCounts > 20000)
                {
                    dataP = 5;
                    stateNumber = 5;
                    state = "STOP";
                }

                Invoke(new Action(() => // apply the right int array so that the pixel count for each section is on the right text box
                {
                    textBox1.Text = $"{whitePixelsCounts[0]}";
                    textBox2.Text = $"{whitePixelsCounts[1]}";
                    textBox3.Text = $"{whitePixelsCounts[2]}";
                    textBox4.Text = $"{whitePixelsCounts[3]}";
                    textBox5.Text = $"{whitePixelsCounts[4]}";
                    textBox6.Text = $"{yellowPixelsCounts[0]}";
                    textBox7.Text = $"{yellowPixelsCounts[1]}";
                    textBox8.Text = $"{yellowPixelsCounts[2]}";
                    textBox9.Text = $"{yellowPixelsCounts[3]}";
                    textBox10.Text = $"{yellowPixelsCounts[4]}";
                    textBox11.Text = $"{redPixelsCounts}";
                    textBox12.Text = $"{stateNumber}";
                    textBox13.Text = state;

                    PortWrite(dataP);
                }));

                Task.Delay(16); // ~60fps -> 1000ms/60 = 16.6
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Dispose all processing threads to avoid orphaned processes
            if (mIsCapturing)
            {
                mCancellationToken.Cancel();
            }

            mCapture.Dispose();
            mCancellationToken.Dispose();

            if (port != null && port.IsOpen)
            {
                port.Close();
            }
        }

        private void PortWrite(int data)
        {
            byte[] buffer = new byte[1]
            {
                Convert.ToByte(data),
            };
            port.Write(buffer, 0, buffer.Length);
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

        private void TrackBarMaxHR_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinHR.Value > TrackBarMaxHR.Value)
            {
                TrackBarMinHR.Value = TrackBarMaxHR.Value;
            }

            maxTrackBarValueHR = TrackBarMaxHR.Value;
        }

        private void TrackBarMinHR_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinHR.Value > TrackBarMaxHR.Value)
            {
                TrackBarMaxHR.Value = TrackBarMinHR.Value;
            }

            minTrackBarValueHR = TrackBarMinHR.Value;
        }

        private void TrackBarMaxSR_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinSR.Value > TrackBarMaxSR.Value)
            {
                TrackBarMinSR.Value = TrackBarMaxSR.Value;
            }

            maxTrackBarValueSR = TrackBarMaxSR.Value;
        }

        private void TrackBarMinSR_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinSR.Value > TrackBarMaxSR.Value)
            {
                TrackBarMaxSR.Value = TrackBarMinSR.Value;
            }

            minTrackBarValueSR = TrackBarMinSR.Value;
        }

        private void TrackBarMaxVR_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinVR.Value > TrackBarMaxVR.Value)
            {
                TrackBarMinVR.Value = TrackBarMaxVR.Value;
            }

            maxTrackBarValueVR = TrackBarMaxVR.Value;
        }

        private void TrackBarMinVR_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinVR.Value > TrackBarMaxVR.Value)
            {
                TrackBarMaxVR.Value = TrackBarMinVR.Value;
            }

            minTrackBarValueVR = TrackBarMinVR.Value;
        }

        private void TrackBarMaxHY_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinHY.Value > TrackBarMaxHY.Value)
            {
                TrackBarMinHY.Value = TrackBarMaxHY.Value;
            }

            maxTrackBarValueHY = TrackBarMaxHY.Value;
        }

        private void TrackBarMinHY_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinHY.Value > TrackBarMaxHY.Value)
            {
                TrackBarMaxHY.Value = TrackBarMinHY.Value;
            }

            minTrackBarValueHY = TrackBarMinHY.Value;
        }

        private void TrackBarMaxSY_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinSY.Value > TrackBarMaxSY.Value)
            {
                TrackBarMinSY.Value = TrackBarMaxSY.Value;
            }

            maxTrackBarValueSY = TrackBarMaxSY.Value;
        }

        private void TrackBarMinSY_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinSY.Value > TrackBarMaxSY.Value)
            {
                TrackBarMaxSY.Value = TrackBarMinSY.Value;
            }

            minTrackBarValueSY = TrackBarMinSY.Value;
        }

        private void TrackBarMaxVY_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinVY.Value > TrackBarMaxVY.Value)
            {
                TrackBarMinVY.Value = TrackBarMaxVY.Value;
            }

            maxTrackBarValueVY = TrackBarMaxVY.Value;
        }

        private void TrackBarMinVY_Scroll(object sender, EventArgs e)
        {
            if (TrackBarMinVY.Value > TrackBarMaxVY.Value)
            {
                TrackBarMaxVY.Value = TrackBarMinVY.Value;
            }

            minTrackBarValueVY = TrackBarMinVY.Value;
        }
    }
}