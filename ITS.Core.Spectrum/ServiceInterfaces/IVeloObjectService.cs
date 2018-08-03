using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using ITS.ProjectBase.Utils.ExceptionHandling;
using ITS.Core.Spectrum.Domain;
using ITS.Core.ServiceInterfaces;
using ITS.ProjectBase.Utils.WCF.FaultContracts;

namespace ITS.Core.Spectrum.ServiceInterfaces
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    [ExceptionShielding(Policies.AbstractServicePolicy)]
    public interface IVeloObjectService : IAbstractService<VeloObject, long>
    {
        [UseNetDataContractSerializer]
        [OperationContract(Action = "GetVeloObjectsByFeatureObjectID")]
        [FaultContract(typeof(BaseFault))]
        List<VeloObject> GetVeloObjectsByFeatureObjectIDs(List<long> ids);

        [UseNetDataContractSerializer]
        [OperationContract(Action = "GetByFeature")]
        [FaultContract(typeof(BaseFault))]
        VeloObject GetByFeature(long featureId);

        [UseNetDataContractSerializer]
        [OperationContract(Action = "FilterVeloObjects")]
        [FaultContract(typeof(BaseFault))]
        [FaultContract(typeof(GeoAccessDeniedFault))]
        IEnumerable<VeloObject> FilterVeloObjects(ICollection<ITS.Core.Domain.Filters.Filter> veloobjectFilters);
    }
}
