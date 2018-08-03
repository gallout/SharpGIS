using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Core.ManagerInterfaces;
using ITS.Core.Spectrum.Domain;

namespace ITS.Core.Spectrum.ManagerInterfaces
{
    public interface IVeloObjectManager : IManager<VeloObject, long>
    {
        List<VeloObject> GetVeloObjectsByFeatureObjectIDs(List<long> ids);
        VeloObject GetByFeature(long featureId);
        IEnumerable<VeloObject> FilterVeloObjects(ICollection<Core.Domain.Filters.Filter> veloobjectFilters);
    }
}
