using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenseKey
{
    public partial class frmGenerate : Form
    {
        public frmGenerate()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        const int ProductCode = 1;
        private void frmGenerate_Load(object sender, EventArgs e)
        {
            cboLicenseType.SelectedIndex = 0;
            //Get computer id, it's unique for each computer
            txtProductID.Text = ComputerInfo.GetComputerId();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProductID.Text);
            KeyValuesClass kv;
            string productKey = string.Empty;
            if (cboLicenseType.SelectedIndex == 0)
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.FULL,
                    Header = Convert.ToByte(10),
                    Footer = Convert.ToByte(9),
                    ProductCode = (byte)ProductCode,//As order of your software
                    Edition = Edition.ENTERPRISE,                   
                    Version = 1
                };
                if (!km.GenerateKey(kv, ref productKey))//Generate full license key
                    txtProductKey.Text = "ERROR";
            }
            else
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.TRIAL,
                    Header = Convert.ToByte(10),
                    Footer = Convert.ToByte(9),
                    ProductCode = (byte)ProductCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1,
                    Expiration = DateTime.Now.Date.AddDays(Convert.ToInt32(txtExperience.Text))
                };
                if (!km.GenerateKey(kv, ref productKey))//Generate trial license key
                    txtProductKey.Text = "ERROR";
            }
            txtProductKey.Text = productKey;
        }
    }
}
