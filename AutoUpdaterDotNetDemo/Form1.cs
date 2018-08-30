using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoUpdaterDotNET;

namespace AutoUpdaterDotNetDemo
{
    public partial class Form1 : Form
    {
        string updateUrl = "http://172.18.34.182:8001/updates/UpdateConfig.xml";
        public Form1()
        {
            InitializeComponent();
            AutoUpdater.Start(updateUrl);

            
            version.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start(updateUrl);
        }
    }
}