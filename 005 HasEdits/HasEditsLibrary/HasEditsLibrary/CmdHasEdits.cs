using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Editor;
using System.Diagnostics;

namespace HasEditsLibrary
{
    /// <summary>
    /// Summary description for CmdHasEdits.
    /// </summary>
    [Guid("94126614-9bbd-4415-bc0e-bfa9ea0d2c78")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("HasEditsLibrary.CmdHasEdits")]
    public sealed class CmdHasEdits : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        public CmdHasEdits()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CmdHasEdits"; //localizable text
            base.m_caption = "CmdHasEdits";  //localizable text
            base.m_message = "CmdHasEdits";  //localizable text 
            base.m_toolTip = "CmdHasEdits";  //localizable text 
            base.m_name = "HasEditsLibrary_CmdHasEdits";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // OK
            UseStore();

            // OK
            //UseUpdateFeature();
        }

        public void UseStore()
        {
            // TODO: Add CmdHasEdits.OnClick implementation
            UID uid = new UID();
            uid.Value = "esriEditor.Editor";
            IEditor3 editor = (IEditor3)m_application.FindExtensionByCLSID(uid);

            Debug.WriteLine(string.Format("before {0}", editor.HasEdits()));

            IMxDocument mxDocument = (IMxDocument)m_application.Document;
            IMap map = mxDocument.FocusMap;

            if (map.LayerCount == 0)
            {
                MessageBox.Show("Layer Count = 0");
                return;
            }

            ILayer layer = map.Layer[0];
            if (layer is IFeatureLayer)
            {
                IFeatureLayer fl = (IFeatureLayer)layer;
                IFeatureClass fc = fl.FeatureClass;
                IDataset dataset = (IDataset)fc;

                if (! editor.EditState.Equals(esriEditState.esriStateEditing))
                {
                    editor.StartEditing(dataset.Workspace);
                }
                editor.StartOperation();


                int index = fc.FindField("DisplayValue");

                IFeature feature = fc.GetFeature(1);

                int value = (int)feature.get_Value(index);
                feature.set_Value(fc.FindField("DisplayValue"), ++value);

                feature.Store();

                editor.StopOperation("count");

                mxDocument.ActiveView.Refresh();
                Debug.WriteLine(string.Format("after {0}", editor.HasEdits()));
            }
        }

        public void UseUpdateFeature()
        {
            // TODO: Add CmdHasEdits.OnClick implementation
            UID uid = new UID();
            uid.Value = "esriEditor.Editor";
            IEditor3 editor = (IEditor3)m_application.FindExtensionByCLSID(uid);

            Debug.WriteLine(string.Format("before {0}", editor.HasEdits()));

            IMxDocument mxDocument = (IMxDocument)m_application.Document;
            IMap map = mxDocument.FocusMap;

            if (map.LayerCount == 0)
            {
                MessageBox.Show("Layer Count = 0");
                return;
            }

            ILayer layer = map.Layer[0];
            if (layer is IFeatureLayer)
            {
                IFeatureLayer fl = (IFeatureLayer)layer;
                IFeatureClass fc = fl.FeatureClass;
                IDataset dataset = (IDataset)fc;

                //editor.StartEditing(dataset.Workspace);

                int index = fc.FindField("DisplayValue");

                IFeatureCursor fCursor = fc.Update(null, false);
                IFeature feature = fCursor.NextFeature();

                int value = (int)feature.get_Value(index);
                feature.set_Value(fc.FindField("DisplayValue"), ++value);

                fCursor.UpdateFeature(feature);

                mxDocument.ActiveView.Refresh();
                Debug.WriteLine(string.Format("after {0}", editor.HasEdits()));
            }
        }
        
        #endregion
    }
}
