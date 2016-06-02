using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FHIRUK.Resources;

namespace fhirTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.identifier = new Identifiers() 
            {
                new Identifier 
                {
                    use = EnumIdentifierUse.Usual,
                    value = "9990001234"
                }
            };

            String json = p.JSON();

            textBox1.Text = json;
        }
    }
}
