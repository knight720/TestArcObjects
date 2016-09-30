using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace EditTemplateLibrary
{
    /// <summary>
    /// Summary description for CmdEditTemplate.
    /// </summary>
    [Guid("7ea3e6b9-1a7c-479a-918a-31836d4a8837")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("EditTemplateLibrary.CmdEditTemplate")]
    public sealed class CmdEditTemplate : BaseCommand
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
        public CmdEditTemplate()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "EditTemplateLibrary"; //localizable text
            base.m_caption = "CmdEditTemplate";  //localizable text
            base.m_message = "CmdEditTemplate";  //localizable text 
            base.m_toolTip = "CmdEditTemplate";  //localizable text 
            base.m_name = "EditTemplateLibrary_CmdEditTemplate";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
            // TODO: Add CmdEditTemplate.OnClick implementation
            IMxDocument mxDocuemnt = (IMxDocument)m_application.Document;
            IMap map = mxDocuemnt.FocusMap;

            //ILayer layer = map.get_Layer(0);
            //{6CA416B1-E160-11D2-9F4E-00C04F6BC78E} IDataLayer
            //{40A9E885-5533-11d0-98BE-00805F7CED21} IFeatureLayer
            //{E156D7E5-22AF-11D3-9F99-00C04F6BC78E} IGeoFeatureLayer
            //{34B2EF81-F4AC-11D1-A245-080009B6F22B} IGraphicsLayer
            //{5CEAE408-4C0A-437F-9DB3-054D83919850} IFDOGraphicsLayer
            //{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E} ICoverageAnnotationLayer
            //{EDAD6644-1810-11D1-86AE-0000F8751720} IGroupLayer
            ILayer layer = null;
            IDataset dataset = null;
            UID uid2 = new UID();
            uid2.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            IEnumLayer el = map.get_Layers(uid2, true);
            ILayer l = el.Next();
            while (l != null)
            {
                dataset = (IDataset)l;
                if (dataset.BrowseName.Equals("LandUse"))
                {
                    layer = l;
                    break;
                }
                l = el.Next();
            }

            dataset = (IDataset)layer;
            //IFeatureLayer fl = (IFeatureLayer)layer;
            //IFeatureClass fc = fl.FeatureClass;
            IWorkspace workspace = dataset.Workspace;
            
            

            //Get a reference to the editor.
            UID uid = new UIDClass();
            uid.Value = "esriEditor.Editor";
            IEditor3 editor = m_application.FindExtensionByCLSID(uid) as IEditor3;

            //Check to see if a workspace is already being edited.
            if (editor.EditState == esriEditState.esriStateNotEditing)
            {
                editor.StartEditing(workspace);
                //return true;
            }
            else
            {
                //return false;
            }


            //editor.RemoveAllTemplatesInLayer(layer);
            IArray array = new ArrayClass();

            ILayerExtensions layerExtensions;
            IEditTemplateManager editTemplateMgr;
            layerExtensions = layer as ILayerExtensions;

            //Find the EditTemplateManager extension.
            for (int j = 0; j < layerExtensions.ExtensionCount; j++)
            {
                object extension = layerExtensions.get_Extension(j);

                if (extension is IEditTemplateManager)
                {
                    //editTemplateMgr = extension as IEditTemplateManager;
                    //Use EditTemplateManager to get information about templates.

                    //editTemplateMgr.EditTemplate[1].Name 

                    IEditTemplateFactory etf = new EditTemplateFactoryClass();
                    IEditTemplate et = etf.Create("abc", layer);
                    et.SetDefaultValue("Type", 5, true);

                    array.Add(et);

                    editor.AddTemplates(array);

                    //IEditTemplate editTemplate = editTemplateMgr.get_EditTemplate(editTemplateMgr.Count-1);
                    //editTemplate.SetDefaultValue("Name", "Venice", true);

                    //editor.CurrentTemplate = editTemplate;
                    editor.CurrentTemplate = et;
                }
            }


            


        }

        #endregion
    }
}
