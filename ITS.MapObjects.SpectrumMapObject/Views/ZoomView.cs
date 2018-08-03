using System.Windows.Forms;
using ITS.MapObjects.SpectrumMapObject.IViews;

namespace ITS.MapObjects.SpectrumMapObject.Views
{
    public partial class ZoomView : Form, IZoomView
    {
        public ZoomView()
        {
            InitializeComponent();
        }

        #region Implementation of IZoomPostView

        public new bool ShowDialog()
        {
            return (base.ShowDialog() == DialogResult.OK);
        }

        public double CurrentZoom
        {
            get { return (trackBarZoomPost.Value / 2d); }
            set
            {
                var zoom = (int)(value * 2);
                if (zoom > trackBarZoomPost.Maximum) zoom = trackBarZoomPost.Maximum;
                if (zoom < trackBarZoomPost.Minimum) zoom = trackBarZoomPost.Minimum;
                trackBarZoomPost.Value = zoom;
            }
        }

        #endregion
    }
}