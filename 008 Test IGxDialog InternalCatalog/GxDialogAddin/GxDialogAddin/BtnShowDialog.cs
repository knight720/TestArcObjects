using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;

namespace GxDialogAddin
{
    public class BtnShowDialog : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public BtnShowDialog()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;

            IGxDialog gd = new GxDialog();
            gd.InternalCatalog.Close();

            IEnumGxObject ego;
            bool result = gd.DoModalOpen(0, out ego);

            int a = 0;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
