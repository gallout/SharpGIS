using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Security;
using ITS.Core.Spectrum.Domain;
using ITS.Core.Spectrum.ManagerInterfaces;
using ITS.Core.Spectrum.ServiceInterfaces;
using ITS.Managers.BaseManagers;
using ITS.ProjectBase.Utils.ExceptionHandling;
using ITS.ProjectBase.Utils.WCF.FaultContracts;

namespace ITS.Managers.SpectrumManagers.Managers
{
    public sealed class VeloObjectManager : AbstractManager<VeloObject, long, IVeloObjectService>,
        IVeloObjectManager
    {
        #region Члены IVeloObjectManager

        public VeloObject GetByFeature(long featureId)
        {
            VeloObject res = null;
            IVeloObjectService channel = GetChannel();
            using (channel as IDisposable)
            {
                try
                {
                    res = channel.GetByFeature(featureId);
                }
                catch (FaultException<BaseFault> ex)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(ex, Policies.AbstractManagerPolicy);
                }
                catch (MessageSecurityException e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.SecurityPolicy);
                }
                catch (SecurityAccessDeniedException e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.AccessPolicy);
                }
                catch (Exception e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.ChannelCreatorPolicy);
                }
            }
            return res;
        }

        public List<VeloObject> GetVeloObjectsByFeatureObjectIDs(List<long> ids)
        {
            {
                IVeloObjectService channel = GetChannel();
                try
                {
                    using (channel as IDisposable)
                    {
                        return channel.GetVeloObjectsByFeatureObjectIDs(ids);
                    }
                }
                catch (FaultException<BaseFault> ex)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(ex, Policies.AbstractManagerPolicy);
                }
                catch (MessageSecurityException e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.SecurityPolicy);
                }
                catch (SecurityAccessDeniedException e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.AccessPolicy);
                }
                catch (Exception e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.ChannelCreatorPolicy);
                }
                throw new Exception("Не удалось выполнить запрос к серверу.");
            }
        }

        public IEnumerable<VeloObject> FilterVeloObjects(ICollection<ITS.Core.Domain.Filters.Filter> veloobjectFilters)
        {
            IEnumerable<VeloObject> res = null;
            IVeloObjectService channel = GetChannel();
            using ((IDisposable)channel)
            {
                try
                {
                    res = channel.FilterVeloObjects(veloobjectFilters);
                }
                catch (FaultException<BaseFault> ex)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(ex, Policies.AbstractManagerPolicy);
                }
                catch (MessageSecurityException e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.SecurityPolicy);
                }
                catch (SecurityAccessDeniedException e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.AccessPolicy);
                }
                catch (Exception e)
                {
                    if (ExceptionManager != null) ExceptionManager.HandleException(e, Policies.ChannelCreatorPolicy);
                }
            }

            return res;
        }

        #endregion
    }
}
