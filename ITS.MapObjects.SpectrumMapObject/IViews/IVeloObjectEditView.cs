using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Core.Spectrum.Domain;

namespace ITS.MapObjects.SpectrumMapObject.IViews
{
   public interface IVeloObjectEditView : IBaseView
    {
        VeloObject VeloObject { get; set; }
    }
}
