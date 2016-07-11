using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using System.Windows.Forms;
using ESRI.ArcGIS.SystemUI;

namespace TestSketchTool
{
    /// <summary>
    /// Summary description for tolSketchTool.
    /// </summary>
    [Guid("8d7182bc-2776-4d6b-918d-4d6ad3a314ff")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("TestSketchTool.tolSketchTool")]
    public sealed class tolSketchTool : BaseTool
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
        private ICommand m_pSketchCommand;
        private ITool m_pSketchTool;

        public tolSketchTool()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "TestSketchTool"; //localizable text 
            base.m_caption = "tolSketchTool";  //localizable text 
            base.m_message = "tolSketchTool";  //localizable text
            base.m_toolTip = "tolSketchTool";  //localizable text
            base.m_name = "TestSketchTool_TestSketchTool";   //unique id, non-localizable (e.g. "MyCategory_ArcMapTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
            //m_hookHelper.Hook = hook
            Type t = Type.GetTypeFromProgID("esriEditor.SketchTool");
            m_pSketchCommand = (ICommand)Activator.CreateInstance(t);
            m_pSketchCommand.OnCreate(hook);
            m_pSketchTool = (ITool)m_pSketchCommand;

            //If m_hookHelper.ActiveView Is Nothing Then m_hookHelper = Nothing
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add tolSketchTool.OnClick implementation
            m_pSketchCommand.OnClick();
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add tolSketchTool.OnMouseDown implementation
            m_pSketchTool.OnMouseDown(Button, Shift, X, Y);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add tolSketchTool.OnMouseMove implementation
            m_pSketchTool.OnMouseMove(Button, Shift, X, Y);
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add tolSketchTool.OnMouseUp implementation
            m_pSketchTool.OnMouseUp(Button, Shift, X, Y);
        }
        #endregion
    }
}
