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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;
using System.CodeDom;

namespace DigitalPersona_app
{

    public partial class DP_Enterance : Form
    {
        private const int PROBABILITY_ONE = 0x7fffffff;
        private capture_Form captureForm;
        private Capabilites_form capabilitesForm;
        private List<Fmd> fmdList;
        private const int sampleCount = 1;

        private List<Fmd> preEnrollment;
        int count;
        public string found = null;

        public delegate void updateCapabilites(Reader reader);
        public delegate void DisplayCapture(Bitmap bitmap);
        public delegate void _foundDelegate(string message);
        
        [DllImport("kernel32.dll")] private static extern bool AllocConsole();

        public Reader reader;
        public DP_Enterance()
        {
            AllocConsole();
            fmdList = new List<Fmd>();
            preEnrollment = new List<Fmd>();
            count = 0;
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
        //private void OnCaptured(CaptureResult captureResult)
        //{

        //    if (captureForm == null || captureForm.IsDisposed)
        //    {
        //        captureForm = new capture_Form(this);
        //    }

        //    if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
        //    {
        //        MessageBox.Show("Error: " + captureResult.ResultCode);
        //        return;
        //    }

        //    // Process the captured fingerprint
        //    if (captureResult.Data != null && captureResult.Data.Views.Count > 0)
        //    {
              
        //       Console.WriteLine() ;
        //        foreach (Fid.Fiv view in captureResult.Data.Views)
        //        {
                   
        //            // Use the provided CreateBitmap method to create the bitmap
        //            Bitmap bitmap = CreateBitmap(view.RawImage, view.Width, view.Height);

        //            //pictureBox1.Image = bitmap; // checking of Bitmap on the same form
        //            DisplayCapture displayCapture = captureForm._displayCapture;
        //            displayCapture(bitmap);

        //            // Convert the fingerprint to FMD
        //            Fmd fmd = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI).Data;
        //            // Add the FMD to the list
        //            fmdList.Add(fmd);

        //            if (fmdList.Count == sampleCount) {
        //                StoreFmdInDatabase(fmdList[0].Bytes);
        //                fmdList.Clear();
        //            }
                    
        //        }
        //    }
        //    //reader.Dispose();   // if on then only one capture will be taken
        //}



        private void StoreFmdInDatabase(byte[] fmdBytes,string Xml)
        {
            bool is_enrolled=false;
            string cs = "Data Source=DESKTOP-1907SQ5;Initial Catalog=DigitalPersona;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);
            string query = "INSERT INTO FingerprintData (Fmd,Decoded_XML) VALUES (@Fmd , @Xml)";
            string query2 = "Select Decoded_XML from FingerprintData";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd.Parameters.AddWithValue("@Fmd", fmdBytes);
            cmd.Parameters.AddWithValue("@Xml", Xml);
            //cmd2.Parameters.AddWithValue("@Fmd", fmdBytes);
            //cmd2.Parameters.AddWithValue("@Xml", Xml);
            con.Open();

            // Retrieve all FMDs from the database
            SqlDataReader dr = cmd2.ExecuteReader();  
            List<Fmd>existingFmds = new List<Fmd>(); 
            
            while (dr.Read())
            {
                string xmlData = dr["Decoded_XML"].ToString();
                existingFmds.Add(Fmd.DeserializeXml(xmlData));
            }
            dr.Close();
            int checkNo = 0;
            foreach (var existingFmd in existingFmds)
            {
                CompareResult compareResult = Comparison.Compare(existingFmd, 0, Fmd.DeserializeXml(Xml), 0);
                Console.WriteLine($"Checking {++checkNo} FMD in DB || Dissimilarity Score => {compareResult.Score} ");
                if(compareResult.Score <= PROBABILITY_ONE/ 100000)
                {
                    is_enrolled = true;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\t\t\t\t\t*MATCHED");
                    Console.ResetColor();
                }

            }
            checkNo = 0;
            con.Close();

            con.Open();
            if (!is_enrolled) {
                
                int nq = cmd.ExecuteNonQuery();
                if (nq > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Data Entered in SqlServer Base");
                    Console.WriteLine($"Rows Affected: {nq}");
                    Console.WriteLine($"Data: {fmdBytes.LongLength}");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.ResetColor();
                    _foundDelegate found = captureForm._Found;
                    found("New Finger Enrolled");
                }
            }
            else
            {
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Already Enrolled in Data Base");
                Console.ResetColor();
                _foundDelegate found = captureForm._Found;
                found("Finger Already Enrolled");
            }
            
            con.Close();
        }



        //Helper method to create a bitmap from the raw image data
        //Provided CreateBitmap method
        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];

            }

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }
        //public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        //{
        //    Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

        //    // Lock the bitmap's bits.
        //    BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
        //                                    ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        //    int bytesPerPixel = Image.GetPixelFormatSize(data.PixelFormat) / 8;
        //    byte[] rgbBytes = new byte[data.Stride * bmp.Height];

        //    for (int y = 0; y < bmp.Height; y++)
        //    {
        //        for (int x = 0; x < bmp.Width; x++)
        //        {
        //            byte grayscaleValue = bytes[y * width + x];

        //            // Apply color transformation
        //            Color color = GetColorForValue(grayscaleValue);

        //            int pixelIndex = (y * data.Stride) + (x * bytesPerPixel);

        //            rgbBytes[pixelIndex] = color.B;     // Blue component
        //            rgbBytes[pixelIndex + 1] = color.G; // Green component
        //            rgbBytes[pixelIndex + 2] = color.R; // Red component
        //        }
        //    }

        //    Marshal.Copy(rgbBytes, 0, data.Scan0, rgbBytes.Length);

        //    // Unlock the bits.
        //    bmp.UnlockBits(data);

        //    return bmp;
        //}

        //private Color GetColorForValue(byte value)
        //{
        //    // Map grayscale value to a minimalist gradient.
        //    // Example: from soft blue to soft green.

        //    int r = Math.Min(100 + (value / 4), 255); // Soft Red
        //    int g = Math.Min(150 + (value / 2), 255); // Soft Green
        //    int b = Math.Min(180 + (value / 3), 255); // Soft Blue

        //    return Color.FromArgb(r, g, b);
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
        private void OnCaptured(CaptureResult captureResult)
        {
            DisplayCapture displayCapture = captureForm._displayCapture; // delegate subscription
            DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
           
            foreach(Fid.Fiv view in captureResult.Data.Views)
            {
                Bitmap bitmap = CreateBitmap(view.RawImage,view.Width,view.Height);
                displayCapture(bitmap);
            }
            //Console.WriteLine($"        --{resultConversion.Data.ViewCount}");// means that result conversion has only one view
            //Console.WriteLine($"       - --{resultConversion.Data.Views.Count}");
            if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show(resultConversion.ResultCode.ToString());
                return;
            }

            if (captureForm == null || captureForm.IsDisposed)
            {
                captureForm = new capture_Form(this);
            }

            if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                MessageBox.Show("-Error: " + captureResult.ResultCode);
                return;
            }

            preEnrollment.Add(resultConversion.Data);
            count++;
            //if (count >= 2) // Create enrollment does not need it, as I will not create an Enrollment Till the Data Minutae is Enough
            {
                DataResult<Fmd> resultEnrollment = Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preEnrollment);
   
                if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                {
                    string Xml = Fmd.SerializeXml(resultEnrollment.Data);
                    Console.WriteLine(Xml);
                    Fmd check = Fmd.DeserializeXml(Xml);
                    Console.WriteLine($"{check.Width} = {resultEnrollment.Data.Width}");
                    StoreFmdInDatabase(resultEnrollment.Data.Bytes, Xml);

                    preEnrollment.Clear();
                    count= 0;                
                }
                else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                {
                    Console.WriteLine($"-Error in Enrollment");
                    preEnrollment.Clear();
                    count = 0;
                }
            }   
        }
    }
}