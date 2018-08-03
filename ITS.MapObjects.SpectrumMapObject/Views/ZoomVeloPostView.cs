using System;
using System.Windows.Forms;
using ITS.MapObjects.SpectrumMapObject.IViews;
namespace ITS.MapObjects.SpectrumMapObject.Views
{
    public partial class ZoomVeloPostView : Form, IZoomVeloPostView
    {
        #region Implementation of IZoomLampPostView

        public new bool ShowDialog()
        {
            return (base.ShowDialog() == DialogResult.OK);
        }
       
        public float CurrentZoom
        {
            get { return (trackBarVeloPostZoom.Value / 4f); }
            set
            {
                var zoom = (int)(value * 4);
                if (zoom > trackBarVeloPostZoom.Maximum) zoom = trackBarVeloPostZoom.Maximum;
                if (zoom < trackBarVeloPostZoom.Minimum) zoom = trackBarVeloPostZoom.Minimum;
                trackBarVeloPostZoom.Value = zoom;
            }  
        } 

        #endregion // Implementation of IZoomLampPostView

        public ZoomVeloPostView()
        {
            InitializeComponent();
        }

        private void buttonDefaultZoom_Click(object sender, EventArgs e)
        {
            CurrentZoom = VeloObjectConstants.DefaultVeloPostZoom;
        }
    } 
}
