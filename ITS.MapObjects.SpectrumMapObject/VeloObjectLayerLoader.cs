using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Core.Spectrum.Domain;
using ITS.GIS.MapEntities;
using ITS.GIS.MapEntities.Attributes;
using ITS.GIS.MapEntities.Loader;
using ITS.MapObjects.SpectrumMapObject.Properties;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;
using Microsoft.Practices.Unity;
using ITS.Core.Spectrum.ManagerInterfaces;

namespace ITS.MapObjects.SpectrumMapObject
{
    public class VeloObjectLayerLoader : ICustomLayerLoader
    {
        /// <summary>
        /// Менеджер.
        /// </summary>
        private readonly IVeloObjectManager _veloobjectManager;

        public VeloObjectLayerLoader()
        {
            _veloobjectManager = VeloObjectConstants.Container.Resolve<IVeloObjectManager>();
        }

        public string LayerName
        {
            get { return Resources.VeloObjectLayerAlias; }
        }

        public string AttributeName
        {
            get { return VeloObjectConstants.VeloObjectAttributeName; }
        }

        public void Load(List<long> listIds, IMap map)
        {
            if (listIds.Count == 0) return;

            List<long> idsInLayer;
            lock (map.SyncRoot)
            {
                var layer = (IVectorLayer)map.Layers[Resources.VeloObjectLayerAlias];
                idsInLayer = listIds.Where(id => layer.Features.ContainsKey(id)).ToList();
            }
            var someobjects = LoadVeloObjectsByIds(idsInLayer);
            
            lock (map.SyncRoot)
            {
                var layer = (IVectorLayer)map.Layers[Resources.VeloObjectLayerAlias];
                foreach (var so in someobjects)
                {
                    if (layer.Features.ContainsKey(so.FeatureObject.ID))
                    {
                        var f = layer.Features[so.FeatureObject.ID];
                        var attrRR = (Attribute<VeloObject>)GetAttribute(f, AttributeName);
                        if (attrRR == null)
                        {
                            attrRR = new Attribute<VeloObject>(new AttributeType<VeloObject>(AttributeName), so);
                            f.Attributes.Add(attrRR);
                        }
                        else
                        {
                            attrRR.AttrValue = so;
                        }
                    }
                }
            }
        }

        private IEnumerable<VeloObject> LoadVeloObjectsByIds(List<long> ids)
        {
            return _veloobjectManager.GetVeloObjectsByFeatureObjectIDs(ids);
        }

        /// <summary>
        /// Получить атрибут геометрии по его названию.
        /// </summary>
        /// <param name="feature">Геометрия</param>
        /// <param name="name">Название атрибута</param>
        /// <returns></returns>
        private IAttribute GetAttribute(IFeature feature, string name)
        {
            return feature.Attributes.Find(a => a.AttrType.Name == name);
        }
    }
}
