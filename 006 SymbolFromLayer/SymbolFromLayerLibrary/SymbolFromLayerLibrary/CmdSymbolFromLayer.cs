using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Diagnostics;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace SymbolFromLayerLibrary
{
    /// <summary>
    /// Summary description for CmdSymbolFromLayer.
    /// </summary>
    [Guid("8a9db33b-51c1-4c6b-bae4-7d67e5ea8a0a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("SymbolFromLayerLibrary.CmdSymbolFromLayer")]
    public sealed class CmdSymbolFromLayer : BaseCommand
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
        public CmdSymbolFromLayer()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CmdSymbolFromLayer"; //localizable text
            base.m_caption = "CmdSymbolFromLayer";  //localizable text
            base.m_message = "CmdSymbolFromLayer";  //localizable text 
            base.m_toolTip = "CmdSymbolFromLayer";  //localizable text 
            base.m_name = "SymbolFromLayerLibrary_CmdSymbolFromLayer";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

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
            // TODO: Add CmdSymbolFromLayer.OnClick implementation
            IMxDocument mxDocument = (IMxDocument)m_application.Document;
            IMap map = mxDocument.FocusMap;
            ILayer layer = map.Layer[0];

            
            IGeoFeatureLayer gfl = (IGeoFeatureLayer)layer;
            IUniqueValueRenderer uvr = (IUniqueValueRenderer)gfl.Renderer;
            for (int i =0; i< uvr.ValueCount ; ++i)
            {
                Debug.WriteLine(uvr.Value[i]);
            }

            string fieldName = uvr.Field[0];

            IGraphicsContainer gc = (IGraphicsContainer)mxDocument.ActiveView;

            IFeatureLayer fl = (IFeatureLayer)layer;
            IFeatureClass fc = fl.FeatureClass;

            int index = fc.FindField(fieldName);

            IFeatureCursor fCursor = fc.Search(null, false);
            IFeature feature = fCursor.NextFeature();
            while (feature != null)
            {
                string value = (string)feature.get_Value(index);
                
                IPolygonElement pe = new PolygonElementClass();
                IFillShapeElement fse = (IFillShapeElement)pe;
                fse.Symbol = (IFillSymbol)uvr.Symbol[value];
                IElement e = (IElement)pe;
                e.Geometry = feature.ShapeCopy;

                gc.AddElement(e, 0);

                feature = fCursor.NextFeature();
            }
        }

        #endregion
    }
}
