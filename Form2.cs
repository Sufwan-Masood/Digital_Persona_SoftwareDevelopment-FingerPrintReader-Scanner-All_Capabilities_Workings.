using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalPersona_app
{

    public partial class Capabilites_form : Form
    {
    private DP_Enterance dP_Enterance;

        public Capabilites_form()
        {
            InitializeComponent();
        }

        private void Capabilites_form_Load(object sender, EventArgs e)
        {



        }
        //DP_Enterance.updateCapabilites up = (Reader reader) =>
        public void updatecap(Reader reader)
        {




            reader.Open(Constants.CapturePriority.DP_PRIORITY_EXCLUSIVE);
            if (reader != null)
            {

                label1.Text = $"Can Capture: {reader.Capabilities.CanCapture}";
                label3.Text = $"Can Identify: {reader.Capabilities.CanIdentify}";
                label6.Text = $"Can Match: {reader.Capabilities.CanMatch}";
                label4.Text = $"Can Stream: {reader.Capabilities.CanStream}";
                label2.Text = $"Extract Features: {reader.Capabilities.ExtractFeatures}";
                label5.Text = $"Has Calibration: {reader.Capabilities.HasCalibration}";
                label7.Text = $"Has Storage: {reader.Capabilities.HasFingerprintStorage}"; ;
                label8.Text = $"Power Manage: {(reader.Capabilities.HasPowerManagement)}"; ;
                label9.Text = $"Indicator Type: {reader.Capabilities.IndicatorType}";
                label10.Text = $"PIV Complaint: {reader.Capabilities.PIVCompliant}";
                label11.Text = $"Image Resolutions: {reader.Capabilities.Resolutions[0]}";



            }
            else
            {
                MessageBox.Show("Reader is null");
            }
            //reader.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            if (dP_Enterance == null || dP_Enterance.IsDisposed)
            {
                dP_Enterance = new DP_Enterance();
            }

            dP_Enterance.Show();
            
        }
    }
}
