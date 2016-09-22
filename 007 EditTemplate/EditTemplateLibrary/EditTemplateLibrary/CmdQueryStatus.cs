using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Editor;
using System.Windows.Forms;

namespace EditTemplateLibrary
{
    /// <summary>
    /// Summary description for CmdQueryStatus.
    /// </summary>
    [Guid("e7e93026-5675-4aba-bb17-be05e5e87df9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("EditTemplateLibrary.CmdQueryStatus")]
    public sealed class CmdQueryStatus : BaseCommand
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
        public CmdQueryStatus()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "EditTemplateLibrary"; //localizable text
            base.m_caption = "CmdQueryStatus";  //localizable text
            base.m_message = "CmdQueryStatus";  //localizable text 
            base.m_toolTip = "CmdQueryStatus";  //localizable text 
            base.m_name = "EditTemplateLibrary_CmdQueryStatus";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
            CmdQueryStatus.SwitchOption(m_application,true);
        }

        #endregion

        /// <summary>
        /// 開關設定值
        /// </summary>
        /// <param name="application"></param>
        public static void SwitchOption(IApplication application, bool isChecked)
        {
            // TODO: Add CmdQueryStatus.OnClick implementation
            //Editor\Options\General\Use classic snapping
            UID uid = new UID();
            uid.Value = "esriEditor.Editor";
            IEditor editor = application.FindExtensionByCLSID(uid) as IEditor;
            IEditProperties4 ep = (IEditProperties4)editor;
            //無效
            //MessageBox.Show(ep.ClassicSnapping.ToString());

            //ep.ClassicSnapping = !ep.ClassicSnapping;
            ep.ClassicSnapping = isChecked;
        }
    }
}
