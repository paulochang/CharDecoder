using Base64Converter.Controller;
using Base64Converter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base64Converter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = HumanFriendlyKey.HumanFriendlyKeys_GetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string theOriginalString = textBox1.Text;
            int? id_result = Cs_Base64Manager.EncodeAndSaveString(theOriginalString);
            if (id_result.HasValue)
            {
                label1.Text = Cs_Base64Manager.GetEncodedString(id_result.Value);
            }
            dataGridView1.DataSource = HumanFriendlyKey.HumanFriendlyKeys_GetData();
        }
    }
}
