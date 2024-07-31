using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DigitalPersona_app.DP_Enterance;

namespace DigitalPersona_app
{
    public partial class capture_Form : Form
    {
        public string? testString = null;
        private DP_Enterance _parentForm;
        public capture_Form()
        {
            InitializeComponent();
        }
        public capture_Form(DP_Enterance parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        private void Capture_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label2.Text = _parentForm.found;
        }
        public void _displayCapture(Bitmap bitmap)
        {
            if (bitmap != null)
            {

                pictureBox1.Image = bitmap;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                Console.WriteLine(bitmap.GetPixel(100, 100));

            }
            else
            {
                MessageBox.Show("Empty Bitmap");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

            //if (dP_Enterance == null || dP_Enterance.IsDisposed)
            //{
            //    dP_Enterance = new DP_Enterance();
            //}
            //dP_Enterance.Show();
            _parentForm.Show();
            _parentForm.reader.Dispose();
        }
        public void _Found(string message)
        {
            testString = message;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(testString))
            {
                if (testString == "Finger Already Enrolled")
                {
                    label2.ForeColor = Color.Red;
                    label2.Text = testString;
                    timer2.Start();
                }
                if (testString == "New Finger Enrolled")
                {
                    label2.ForeColor = Color.Green;
                    label2.Text = testString;
                    timer2.Start();
                }
                
            }
             testString = null;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = null;
            timer2.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
