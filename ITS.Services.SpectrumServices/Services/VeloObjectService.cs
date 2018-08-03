using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.ServiceModel;
using ITS.Core;
using ITS.Core.Security;
using ITS.Core.Spectrum.DataInterfaces;
using ITS.Core.Spectrum.Domain;
using ITS.Core.Spectrum.ServiceInterfaces;
using ITS.ProjectBase.Data;
using ITS.ProjectBase.Service;
using ITS.ProjectBase.Utils.ExceptionHandling;
using ITS.ProjectBase.Utils.WCF.Compressor;
using ITS.ProjectBase.Utils.WCF.FaultContracts;


namespace ITS.Services.SpectrumServices.Services
{
    [MessageCompression(Compress.Reply | Compress.Request)]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerSession,
        UseSynchronizationContext = false,
        AutomaticSessionShutdown = true)]
    public class VeloObjectService : AbstractService<VeloObject, long>, IVeloObjectService
    {
        protected override IDAO<VeloObject, long> GetDAO()
        {
            try
            {
                return ApplicationService.GetDaoService().GetDao<IVeloObjectDAO>();
            }
            catch (Exception ex)
            {
                if (ExceptionManager != null) ExceptionManager.HandleException(ex, Policies.AbstractServicePolicy);
                throw;
            }
        }

        #region Члены IVeloObjectService

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.ReadWrite)]
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.ReadOnly)]
        public VeloObject GetByFeature(long featureId)
        {
            try
            {
                return ((IVeloObjectDAO)GetDAO()).GetByFeature(featureId);
            }
            catch (Exception ex)
            {
                if (ExceptionManager != null)
                    ExceptionManager.HandleException(ex, Policies.AbstractServicePolicy);
                var f = new BaseFault(ex, ServiceSecurityContext.Current.PrimaryIdentity.Name);
                throw new FaultException<BaseFault>(f);
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.ReadWrite)]
        public override VeloObject SaveOrUpdate(VeloObject entity)
        {
            try
            {
                var transient = entity.IsTransient();
                entity = GetDAO().Save(entity);
                HistoryManager.CreateOrUpdate(entity, entity.FeatureObject.Layer.Map.Alias, transient);
            }
            catch (Exception ex)
            {
                if (ExceptionManager != null)
                    ExceptionManager.HandleException(ex, Policies.AbstractServicePolicy);
                var f = new BaseFault(ex, ServiceSecurityContext.Current.PrimaryIdentity.Name);
                throw new FaultException<BaseFault>(f);
            }
            return entity;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.ReadWrite)]
        public override void DeleteAndCommit(VeloObject entity)
        {
            try
            {
                GetDAO().Delete(entity);
                HistoryManager.Delete(entity, entity.FeatureObject.Layer.Map.Alias);
            }
            catch (Exception ex)
            {
                if (ExceptionManager != null)
                    ExceptionManager.HandleException(ex, Policies.AbstractServicePolicy);
                var f = new BaseFault(ex, ServiceSecurityContext.Current.PrimaryIdentity.Name);
                throw new FaultException<BaseFault>(f);
            }
        }

        /// <summary>
        /// Возвращает список объектов Дорожный ремонт по списку ИД геометрий
        /// Испольхуется в LayerLoader
        /// </summary>
        /// <param name="ids">Список ИД геометрий</param>
        /// <returns>Список объектов Дорожный ремонт</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.ReadWrite)]
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.ReadOnly)]
        public List<VeloObject> GetVeloObjectsByFeatureObjectIDs(List<long> ids)
        {

            try
            {
                return ((IVeloObjectDAO)GetDAO()).GetVeloObjectsByFeatureObjectIDs(ids);
            }
            catch (Exception ex)
            {
                if (ExceptionManager != null) ExceptionManager.HandleException(ex, Policies.AbstractServicePolicy);
                var fault = new BaseFault(ex);
                throw new FaultException<BaseFault>(fault);
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.ReadWrite)]
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.ReadOnly)]
        public IEnumerable<VeloObject> FilterVeloObjects(ICollection<ITS.Core.Domain.Filters.Filter> veloobjectFilters)
        {
            try
            {
                return ((IVeloObjectDAO)GetDAO()).FilterVeloObjects(veloobjectFilters);
            }
            catch (Exception ex)
            {
                if (ExceptionManager != null)
                    ExceptionManager.HandleException(ex, Policies.AbstractServicePolicy);
                var f = new BaseFault(ex, ServiceSecurityContext.Current.PrimaryIdentity.Name);
                throw new FaultException<BaseFault>(f);
            }
        }



        #endregion
    }
    
}
