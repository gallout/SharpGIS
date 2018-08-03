using ITS.Core.ServiceInterfaces;
using ITS.Core.Spectrum.DataInterfaces;
using ITS.Core.Spectrum.ServiceInterfaces;
using ITS.Data.Spectrum.DAO;
using ITS.Services.SpectrumServices.Services;
using Microsoft.Practices.Unity;

namespace ITS.Services.SpectrumServices
{
    /// <summary>
    ///     Конфигуратор сервисов.
    /// </summary>
    public class VeloObjectServiceConfigurator : IServiceConfigurator
    {
        /// <summary>
        ///     Конфигурирует сервис.
        /// </summary>
        /// <param name="container">Контейнер.</param>
        /// <param name="manager">Менеджер хостов.</param>
        public void Configure(IUnityContainer container, IServiceHostManager manager)
        {
            container.RegisterType<IVeloObjectDAO, VeloObjectDAO>();
            manager.Register(typeof(IVeloObjectService), typeof(VeloObjectService), "vo/veloobject");
        }
    }
}
