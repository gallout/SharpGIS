using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeoAPI.Geometries;
using GeoAPI.Operations.Buffer;
using ITS.Core.Enums;
using ITS.Core.Spectrum.Domain;
using ITS.Core.Spectrum.Domain.Enums;
using ITS.Core.Spectrum.ManagerInterfaces;
using ITS.GIS.CoreToGeoTranslators;
using ITS.GIS.MapEntities;
using ITS.GIS.MapEntities.Styles;
using ITS.MapObjects.SpectrumMapObject.EventArgses;
using ITS.MapObjects.SpectrumMapObject.IPresenters;
using ITS.MapObjects.SpectrumMapObject.IViews;
using ITS.MapObjects.SpectrumMapObject.Reports;
using ITS.ProjectBase.Utils.AsyncWorking;
using Microsoft.Practices.Unity;

namespace ITS.MapObjects.SpectrumMapObject.Presenters
{
    public class VeloObjectSummaryPresenter : IVeloObjectSummaryPresenter
    {
        private readonly IVeloObjectManager _VeloObjectManager;
        public VeloObjectSummaryPresenter()
        {
        }

        public VeloObjectSummaryPresenter(IVeloObjectManager VeloObjectManager)
        {
            _VeloObjectManager = VeloObjectManager;
        }

        [Dependency]
        public IMap Map { get; set; }

        #region IPresenter Members

        public void Init(IVeloObjectSummaryView view)
        {
            view.LoadVeloObject += Load_Handler;
            view.EditVeloObject += EditVeloObject_Handler;
           
            view.ShowOnMap += ShowOnMap_Handler;
            view.ExportToWord += Export_Handler;
            InitView(view);
        }

        #endregion

        private void InitView(IVeloObjectSummaryView view)
        {
            view.FillVeloObjectFilters(new Dictionary<Core.Domain.Filters.Filter, object>
            {
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Идентификатор", FilterType=FilterType.Id,PropertyPath="ID"},null},
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Тип велопарковки", FilterType=FilterType.Selector,PropertyPath="VeloType"},GetTypeTree()},
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Вид велопарковки", FilterType=FilterType.Selector,PropertyPath="VeloView"},GetViewTree()},
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Статус", FilterType=FilterType.Selector,PropertyPath="VeloStatus"},GetObjectStatusTree()},
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Длина, м", FilterType=FilterType.String,PropertyPath="VeloLength"},null},
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Ширина, м", FilterType=FilterType.String,PropertyPath="VeloWidth"},null},
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Количество секций", FilterType=FilterType.String,PropertyPath="VeloSection"},null},
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Дата установки", FilterType=FilterType.Date,PropertyPath="DataSet"},null},
                {new ITS.Core.Domain.Filters.Filter {PropertyName = "Дата обслуживания", FilterType=FilterType.Date,PropertyPath="DataCheck"},null},
            });
        }
        private Dictionary<TreeNode, object> GetTypeTree()
        {
            return
                Enum.GetValues(typeof(VeloType))
                    .Cast<VeloType>()
                    .ToDictionary<VeloType, TreeNode, object>(type => new TreeNode(VeloTypeStrings.GetString(type)), type => type);
        }

        private Dictionary<TreeNode, object> GetViewTree()
        {
            return
                Enum.GetValues(typeof(VeloView))
                    .Cast<VeloView>()
                    .ToDictionary<VeloView, TreeNode, object>(view => new TreeNode(VeloViewStrings.GetString(view)), view => view);
        }
        private Dictionary<TreeNode, object> GetObjectStatusTree()
        {
            return
                Enum.GetValues(typeof(VeloObjectStatus))
                    .Cast<VeloObjectStatus>()
                    .ToDictionary<VeloObjectStatus, TreeNode, object>(status => new TreeNode(VeloObjectStatusStrings.GetStatusName(status)), status => status);
        }



        private void Load_Handler(object sender, EventArgs e)
        {
            var view = (IVeloObjectSummaryView)sender;

            var VeloObjectsFilters =
                new List<ITS.Core.Domain.Filters.Filter>(new[]
                {
                    new ITS.Core.Domain.Filters.Filter
                    {
                        FilterType = FilterType.String,
                        Function = FilterFunc.Eq,
                        PropertyPath = "FeatureObject.Layer.Map.Alias",
                        Values = new[] {Map.Alias},
                        PropertyName = "Карта"
                    }
                });
            VeloObjectsFilters.AddRange(view.VeloObjectFilters);


            IEnumerable<VeloObject> filteredVeloObject = null;
            AsyncLoaderForm.ShowMarquee((a, b) =>
                filteredVeloObject = _VeloObjectManager.FilterVeloObjects(VeloObjectsFilters), "Идет загрузка объекта");
            view.Model = filteredVeloObject;
        }


        /// <summary>
        /// Обрабатывает нажатие кнопки редактирования объекта.
        /// </summary>
        private void EditVeloObject_Handler(object sender, VeloObjectEventArgs e)
        {
            VeloObject VeloObject = _VeloObjectManager.GetByFeature(e.VeloObject.FeatureObject.ID);
            var addView = VeloObjectConstants.Container.Resolve<IVeloObjectEditView>(new ParameterOverride("veloobject", VeloObject), new ParameterOverride("soManager", _VeloObjectManager));
            addView.ShowViewDialog();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки информация об объекте.
        /// </summary>
        private void InfoVeloObject_Handler(object sender, VeloObjectEventArgs e)
        {
            VeloObject VeloObject = _VeloObjectManager.GetByFeature(e.VeloObject.FeatureObject.ID);
            var addView = VeloObjectConstants.Container.Resolve<IVeloObjectInfoView>(new ParameterOverride("VeloObject", VeloObject));
            addView.ShowViewDialog();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки перемещения к месторасположению объекта.
        /// </summary>
        private void ShowOnMap_Handler(object sender, VeloObjectEventArgs e)
        {
            Map.ClearTempLayer();
            Map.CoordSys.ChangeLocationTo(e.VeloObject.FeatureObject.Geometry.Centroid.X, e.VeloObject.FeatureObject.Geometry.Centroid.Y);

            IGeometry geometry = e.VeloObject.FeatureObject.GetFeature().Geometry.Buffer(0.2, 4, EndCapStyle.Flat);
            Map.AddToTempLayer(new Feature(geometry.Difference(e.VeloObject.FeatureObject.Geometry), new AreaStyle(new InteriorStyle(Color.Green), new LineStyle(Color.Green, 2f))));

            Map.BeginRedraw();
        }

        private void Export_Handler(object sender, EventArgs e)
        {
            var view = (IVeloObjectSummaryView)sender;
            if (view.Model != null)
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = "Doc files (*.rtf)|*.rtf";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AsyncLoaderForm.ShowMarquee((s, ee) =>
                        {
                            try
                            {
                                VeloObjectFindReport.ReportMake(dialog.FileName, view.Model.ToList());
                                MessageBox.Show("Сводная ведомость успешно экспортирована в Word", "Экспорт в Word...");
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Произошла ошибка экспорта", "Экспорт в Word...");
                            }
                        },
                        "Идет формирование отчета...");

                    ReportHelper.Open(dialog.FileName);
                }
            }
            else MessageBox.Show("Сводная ведомость пуста!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
