using System.Resources;
using ITS.MapObjects.BaseMapObject.Misc.PluginAttributes;
using ITS.MapObjects.SpectrumMapObject.Properties;

namespace ITS.MapObjects.SpectrumMapObject
{
    /// <summary>
    /// Предоставляет доступ к ресурсам плагина.
    /// </summary>
    internal class VeloObjectPanelResourceHelper : ResourceHelperAbstract, IResourceHelper
    {
        /// <summary>
        /// Создает класс, который предоставляет доступ к ресурсам плагина.
        /// </summary>
        /// <param name="toolName">Имя инструмента.</param>
        public VeloObjectPanelResourceHelper(string toolName)
            : base(toolName)
        {
        }

        protected override ResourceManager ResManager
        {
            get { return Resources.ResourceManager; }
        }
    }
}