using System.Configuration;
using System.Linq;
using ITS.MapObjects.SpectrumMapObject.Properties;
using Microsoft.Practices.Unity;

namespace ITS.MapObjects.SpectrumMapObject
{
    /// <summary>
    /// Предоставляет доступ к константам и свойствам плагина.
    /// </summary>
    internal static class VeloObjectConstants
    {
        public static bool IsDrawSet = true;
        public static bool IsDrawRequired = true;
        public static bool IsDrawDismantle = true;
        public static bool IsDrawMobile = true;
        public static bool IsDrawCelebrate = true;

        /// <summary>
        /// для масштабирования
        /// </summary>
      
        private static string _toolName;
        private static bool? _customRendererEnabled;
        
        #region Private Fields

        #endregion

        #region Properties

        internal static bool IsNight { get; set; }

        /// <summary>
        /// Масштаб 
        /// </summary>
        
        public const float DefaultVeloPostZoom = 5.25f;

        static VeloObjectConstants()
        {
            Zoom = DefaultVeloPostZoom;
        }

        /// <summary>
        /// Название плагина дружественное пользователю.
        /// </summary>
        internal static string ToolName
        {
            get
            {
                if (string.IsNullOrEmpty(_toolName))
                {
                    _toolName = Resources.PluginToolName;
                }
                return _toolName;
            }
        }

        

        /// <summary>
        /// Включен ли кастомный рендерер.
        /// </summary>
        internal static bool CustomRendererEnabled
        {
            get
            {
                if (!_customRendererEnabled.HasValue)
                {
                    bool res = false;
                    if (ConfigurationManager.AppSettings.AllKeys.Contains("VeloObjectCustomRendererEnabled"))
                        bool.TryParse(ConfigurationManager.AppSettings["VeloObjectCustomRendererEnabled"], out res);
                    _customRendererEnabled = res;
                }
                return _customRendererEnabled.Value;
            }
        }

        /// <summary>
        /// IoC-контейнер плагина.
        /// </summary>
        internal static IUnityContainer Container
        {
            get
            {
               
                return
              ProjectBase.Utils.Container.Container.PluginContainer(ToolName);
            }
        }

        internal static string VeloObjectLayerAlias
        {
            get
            {
                return Resources.VeloObjectLayerAlias;
            }
        }

        internal static string VeloObjectAttributeName
        {
            get { return "VeloObject"; }
        }
        /// Масштаб иконок остановок
        /// </summary>
        internal static double Zoom { get; set; }

        /// <summary>
        /// Показывает нужно ли рендереру перерисовывать остановки
        /// </summary>
        internal static bool RenderChanged { get; set; }


    }
    #endregion
}