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
        public void _displayCapture2(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                pictureBox2.Image = bitmap;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
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
        //public void UpdatePictureBox(Bitmap fingerprintImage)
        //{
        //    if (fingerprintImage != null)
        //    {
        //        pictureBox1.Image = fingerprintImage;
        //        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        //    }
        //    else
        //    {
        //        MessageBox.Show("No image to display.");
        //    }
        //}
        public void hidelabel()
        {
            label1.Text = string.Empty;
        }
    }
}
