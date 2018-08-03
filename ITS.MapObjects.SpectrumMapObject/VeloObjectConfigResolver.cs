using ITS.Core.ManagerInterfaces;
using ITS.Core.Spectrum.ManagerInterfaces;
using ITS.Core.Spectrum.ServiceInterfaces;
using ITS.GIS.MapEntities.Renderer;
using ITS.Managers.SpectrumManagers.Managers;
using ITS.MapObjects.BaseMapObject.Misc;
using ITS.MapObjects.BaseMapObject.Misc.PluginAttributes;
using ITS.MapObjects.SpectrumMapObject.Properties;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using ITS.GIS.MapEntities.Loader;
using ITS.MapObjects.SpectrumMapObject.IPresenters;
using ITS.MapObjects.SpectrumMapObject.IViews;
using ITS.MapObjects.SpectrumMapObject.Presenters;
using ITS.MapObjects.SpectrumMapObject.Views;
using ITS.ProjectBase.Utils.Container;

namespace ITS.MapObjects.SpectrumMapObject
{
    /// <summary>
    /// Регистратор компонентов плагина в IoC-контейнере.
    /// </summary>
    public class VeloObjectConfigResolver : IUnityConfigResolver
    {
        #region Implementation of IUnityConfigResolver

        public void ConfigureContainer(IUnityContainer container)
        {

            // МПВ.
            container
                .RegisterType<IAngleManageView, AngleManageView>()
                .RegisterType<IAngleManagePresenter, AngleManagePresenter>()
                .RegisterType<IZoomView, ZoomView>()
                .RegisterType<IZoomPresenter, ZoomPresenter>();

            // Менеджер ресурсов.
            container.RegisterInstance(Resources.ResourceManager);

            // Панель с кнопками.
            container.RegisterType<IMapObjectManager, VeloObjectPanel>(VeloObjectConstants.ToolName);

            // Регистрация менеджеров.           
            container.RegisterType<IVeloObjectManager, VeloObjectManager>(
                new Interceptor(new InterfaceInterceptor()),
                new InterceptionBehavior<VeloObjectMapUpdateInterception>());

            container.RegisterType<BaseView>()
                .RegisterType<IVeloObjectEditView, VeloObjectEditView>()
                .RegisterType<IVeloObjectSummaryView, VeloObjectSummaryView>()
                .RegisterType<IVeloObjectInfoView, VeloObjectInfoView>();
            container.RegisterType<IVeloObjectSummaryPresenter, VeloObjectSummaryPresenter>();

            if (VeloObjectConstants.CustomRendererEnabled)
            {
                // Кастомный отрисовщик.
                container.Resolve<ILayerRendererContainer>().AddRenderer(new VeloObjectLayerRenderer());

                Container.MainContainer.RegisterType<ICustomLayerLoader, VeloObjectLayerLoader>(Resources.VeloObjectLayerAlias);
            }

            // Пути хостов.       
            Container.MainContainer.RegisterInstance(new ClientHostConfiguration<IVeloObjectService>("vo/veloobject"));
        }

        #endregion
    }
}