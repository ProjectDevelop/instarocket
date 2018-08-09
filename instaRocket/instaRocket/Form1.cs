using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace instaRocket
{
    public partial class Form1 : Form
    {
        instagram d;
        public Form1()
        {
            InitializeComponent();

           d= new instagram("denemem122", "q1w2e3r4t5");
          d.login();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            d.likeprofile("sofiavergara");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            d.CloseChrome();
        }
    }
}
