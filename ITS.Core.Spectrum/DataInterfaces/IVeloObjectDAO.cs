using System.Collections.Generic;
using ITS.Core.Domain.Organizations;
using ITS.Core.Spectrum.Domain;
using ITS.ProjectBase.Data;

namespace ITS.Core.Spectrum.DataInterfaces
{
    public interface IVeloObjectDAO : IDAO<VeloObject, long>
    {
        List<VeloObject> GetVeloObjectsByFeatureObjectIDs(List<long> ids);

        VeloObject GetByFeature(long featureId);

        IEnumerable<VeloObject> FilterVeloObjects(ICollection<ITS.Core.Domain.Filters.Filter> veloobjectFilters);
    }
}