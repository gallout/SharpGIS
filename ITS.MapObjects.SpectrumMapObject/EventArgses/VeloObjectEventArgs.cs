using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Core.Spectrum.Domain;

namespace ITS.MapObjects.SpectrumMapObject.EventArgses
{
   public class VeloObjectEventArgs : EventArgs
    {
        public VeloObjectEventArgs(VeloObject veloObject)
        {
            VeloObject = veloObject;
        }

        public VeloObject VeloObject { get; private set; }

    }
}
