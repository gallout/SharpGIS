using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Core.Spectrum.Domain;

namespace ITS.MapObjects.SpectrumMapObject.IViews
{
    public interface IVeloObjectInfoView : IBaseView
    {
        VeloObject VeloObject { get; set; }
    }
}
