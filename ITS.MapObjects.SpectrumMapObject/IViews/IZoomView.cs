using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.MapObjects.SpectrumMapObject.IViews
{
    public interface IZoomView
    {
        double CurrentZoom { get; set; }
        bool ShowDialog();
    }
}