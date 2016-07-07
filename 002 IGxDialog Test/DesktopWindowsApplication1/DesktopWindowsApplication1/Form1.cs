using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DesktopWindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IEnumGxObject  ego;

            IGxDialog gd = new GxDialogClass();

            gd.ObjectFilter = new GxFilterFileGeodatabases();
            gd.AllowMultiSelect = false;
            //無法選擇路徑
            //gd.InternalCatalog.Close();

            gd.DoModalOpen(0, out ego);

        }
    }
}