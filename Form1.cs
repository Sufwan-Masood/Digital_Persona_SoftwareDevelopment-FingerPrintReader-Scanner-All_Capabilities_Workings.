using DPUruNet;
using DPCtlXUru;
using System.Text.RegularExpressions;
using System.Reflection.PortableExecutable;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DPXUru;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace DigitalPersona_app
{

    public partial class DP_Enterance : Form
    {
        private capture_Form captureForm;
        private Capabilites_form capabilitesForm;
        public delegate void updateCapabilites(Reader reader);
        public delegate void DisplayCapture(Bitmap bitmap);
        public delegate void openEnterance();
        [DllImport("kernel32.dll")] private static extern bool AllocConsole();

        public Reader reader;
        public DP_Enterance()
        {
            AllocConsole();

            ReaderCollection readerCollection = ReaderCollection.GetReaders();
            try
            {
                InitializeComponent();
                foreach (var _readers in readerCollection)
                {
                    comboBox1.Items.Add(_readers.Description.SerialNumber);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error " + e);
            }
        }



        //public void Capabilities()
        //{
        //    if (reader != null)
        //    {
        //        string label1.Text = $"Can Capture: {reader.Capabilities.CanIdentify}";
        //        string label3.Text = $"Can Identify: {reader.Capabilities.CanCapture.ToString}";
        //        string label6.Text = $"Can Match: {reader.Capabilities.CanMatch.ToString}";
        //        string label4.Text = $"Can Stream: {reader.Capabilities.CanStream.ToString}";
        //        string label2.Text = $"Extract Features: {reader.Capabilities.ExtractFeatures.ToString}";
        //        string label5.Text = $"Has Calibration: {reader.Capabilities.HasCalibration.ToString}";
        //        string label7.Text = $"Has Fingerprint Storage: {reader.Capabilities.HasFingerprintStorage.ToString}"; ;
        //        string label8.Text = $"Has Power Management: {reader.Capabilities.HasPowerManagement.ToString}"; ;
        //        string label9.Text = $"Indicator Type: {reader.Capabilities.IndicatorType.ToString}";
        //        string label10.Text = $"PIV Complaint: {reader.Capabilities.PIVCompliant.ToString}";
        //        string label11.Text = $"Image Resolutions: {reader.Capabilities.Resolutions.ToString}";

        //    }
        //    else
        //    {
        //        MessageBox.Show("Reader not initialized.");
        //    }
        //}


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkLabel.Text = comboBox1.SelectedItem.ToString();
            checkLabel.Text = $"Selected Reader: " + comboBox1.SelectedItem.ToString();

        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox1.SelectedItem.ToString()))
            {
                button1.Visible = true;
                button2.Visible = true;
                ReaderCollection readerCollection = ReaderCollection.GetReaders();
                reader = readerCollection[comboBox1.SelectedIndex];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            if (capabilitesForm == null || capabilitesForm.IsDisposed)
            {
                capabilitesForm = new Capabilites_form(this);
            }
            updateCapabilites updateCapabilites = capabilitesForm.updatecap;
            updateCapabilites(reader);
            capabilitesForm.Show();

        }

        private void startCapture()
        {
            if (reader == null)
            {
                MessageBox.Show("Reader not initialized.");
                return;
            }

            reader.Open(Constants.CapturePriority.DP_PRIORITY_EXCLUSIVE);

            reader.On_Captured += new Reader.CaptureCallback(OnCaptured);
            // event is subscribed to event handler( callback delegate )=>as onCaptured has same sig as of the delegate CaptureCallback
            reader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, reader.Capabilities.Resolutions[0]);

            //Hidelabel hidelabel = captureForm.hidelabel;
            //hidelabel();                                                              here


        }


        //event handler of capture
        //private void onCaptured(CaptureResult captureResult)
        //{
        //    if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
        //    {
        //        MessageBox.Show("Error: " + captureResult.ResultCode);
        //        return;
        //    }
        //    if (captureResult.Data != null)
        //    {
        //        foreach (Fid.Fiv view in captureResult.Data.Views)
        //        {
        //             Bitmap bitmap = CreateBitmap(view.RawImage);
        //            Capture_form capture_Form = new Capture_form();
        //            capture_Form.pictureBox1 = new PictureBox();
        //            capture_Form.pictureBox1.Image = bitmap;
        //        }
        //    }
        //}
        private void OnCaptured(CaptureResult captureResult)
        {
            if (captureForm == null || captureForm.IsDisposed)
            {
                captureForm = new capture_Form(this);
            }

            if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("Error: " + captureResult.ResultCode);
                return;
            }

            // Process the captured fingerprint
            if (captureResult.Data != null && captureResult.Data.Views.Count > 0)
            {
                foreach (Fid.Fiv view in captureResult.Data.Views)
                {


                    // Use the provided CreateBitmap method to create the bitmap
                    Bitmap bitmap = CreateBitmap(view.RawImage, view.Width, view.Height);

                    //// Display the bitmap in capture_Form's PictureBox
                    //if (captureForm == null || captureForm.IsDisposed)
                    //{
                    //    captureForm = new capture_Form();
                    //}

                    //captureForm.UpdatePictureBox(bitmap);
                    //captureForm.Show();
                    //captureForm.BringToFront();

                    //pictureBox1.Image = bitmap; // checking of Bitmap on the same form
                    DisplayCapture displayCapture = captureForm._displayCapture;
                    displayCapture(bitmap);
                }
            }
            //reader.Dispose();
        }




        // Helper method to create a bitmap from the raw image data
        // Provided CreateBitmap method
        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format16bppArgb1555);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        // the below is working
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //capture_Form form = new capture_Form();
        //    this.Hide();
        //    if (captureForm == null || captureForm.IsDisposed)
        //    {
        //        captureForm = new capture_Form();
        //    }
        //    startCapture();
        //    captureForm.Show();
        //}
        private void button2_Click(object sender, EventArgs e)
        {
            //capture_Form form = new capture_Form();
            this.Hide();
            if (captureForm == null || captureForm.IsDisposed)
            {
                captureForm = new capture_Form(this);
            }
            startCapture();
            captureForm.Show();
        }
    }
}