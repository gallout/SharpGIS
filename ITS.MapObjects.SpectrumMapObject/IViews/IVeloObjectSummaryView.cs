using System;
using System.Collections.Generic;
using ITS.Core.Spectrum.Domain;
using ITS.MapObjects.SpectrumMapObject.EventArgses;

namespace ITS.MapObjects.SpectrumMapObject.IViews
{
    public interface IVeloObjectSummaryView
    {
        /// <summary>
        /// Отображаемые ДТО.
        /// </summary>
        IEnumerable<VeloObject> Model { get; set; }

        void View();
        /// <summary>
        /// Заполняет фильтры .
        /// </summary>
        /// <param name="filterDictionary">Фильтры и их возможные значения.</param>
        void FillVeloObjectFilters(IDictionary<ITS.Core.Domain.Filters.Filter, object> filterDictionary);

        /// <summary>
        /// Фильтры.
        /// </summary>
        ICollection<Core.Domain.Filters.Filter> VeloObjectFilters { get; }

        /// <summary>
        /// Отобразить  на карте.
        /// </summary>
        event EventHandler<VeloObjectEventArgs> ShowOnMap;

        /// <summary>
        /// Редактировать.
        /// </summary>
        event EventHandler<VeloObjectEventArgs> EditVeloObject;

      

        /// <summary>
        /// Применить фильтр.
        /// </summary>
        event EventHandler<EventArgs> LoadVeloObject;

        /// <summary>
        /// Экспортировать сводную ведомость в MS Word.
        /// </summary>
        event EventHandler<EventArgs> ExportToWord;
    }
}