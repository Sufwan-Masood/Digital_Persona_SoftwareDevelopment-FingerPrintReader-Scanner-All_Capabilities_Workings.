using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalPersona_app
{
    public partial class DBView : Form
    {
        DP_Enterance _parentForm;
        public DBView()
        {
            InitializeComponent();
        }
        public DBView(DP_Enterance parentForm)
        {

            _parentForm = parentForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _parentForm.Show();
        }
        private void sqlDataBind()
        {
            try
            {
                string cs = "Data Source=DESKTOP-1907SQ5;Initial Catalog=DigitalPersona;Integrated Security=True";
                SqlConnection con = new SqlConnection(cs);
                String query = "select Id,Decoded_XML from FingerprintData";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch
            {

            }


        }

        private void DBView_Load(object sender, EventArgs e)
        {
            sqlDataBind();
        }
    }
}
