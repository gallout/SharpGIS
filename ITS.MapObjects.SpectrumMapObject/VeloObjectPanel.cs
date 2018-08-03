using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GeoAPI.Geometries;
using ITS.Client.Interface.StateMachine;
using ITS.Client.Interface.StateMachine.Selected;
using ITS.Client.Interface.StateMachine.Selected.Interfaces;
using ITS.Core.Domain.FeatureObjects;
using ITS.Core.Domain.Geometries;
using ITS.Core.ManagerInterfaces.Features;
using ITS.Core.Spectrum.Domain;
using ITS.Core.Spectrum.ManagerInterfaces;
using ITS.GIS.CoreToGeoTranslators;
using ITS.GIS.MapEntities;
using ITS.GIS.MapEntities.Renderer;
using ITS.GIS.MapEntities.Styles;
using ITS.GIS.UIControls.Tools;
using ITS.MapObjects.BaseMapObject.Misc;
using ITS.MapObjects.BaseMapObject.Misc.PluginAttributes;
using ITS.Core.ManagerInterfaces.UndoRedo;
using ITS.Core.Spectrum.Domain.Enums;
using ITS.MapObjects.EditGeometryPlugin.Tools;
using ITS.ProjectBase.Utils.AsyncWorking;
using ITS.ProjectBase.Utils.EventBroker.EventBrokerExtension;
using Microsoft.Practices.Unity;
using NetTopologySuite.Geometries;
using ITS.MapObjects.EditGeometryPlugin.EventArgsPlugin;
using ITS.MapObjects.EditGeometryPlugin.Tools.SelectTools;
using ITS.GIS.MapEntities.Attributes;
using ITS.MapObjects.SpectrumMapObject.Helpers;
using ITS.MapObjects.SpectrumMapObject.IViews;
using ITS.MapObjects.SpectrumMapObject.Properties;
using ITS.MapObjects.SpectrumMapObject.IPresenters;


namespace ITS.MapObjects.SpectrumMapObject
{
    /// <summary>
    /// Панель плагина.
    /// </summary>
    public class VeloObjectPanel : IMapObjectManager
    {
        #region Private fields

        private VeloObject _currentVeloObject;
        private LayerObject _VeloObjectLayer;
        private IGeometry _geometry;
        private bool _flag;
        private IStateMachine _stateMachine;
        private IMap _map;
        private ILayerManager _layerManager;
        private IUndoRedoManager _undoRedoManager;
        private IToolManager _toolManager;
        private IVeloObjectManager _VeloObjectManager;
        private readonly List<ISelectedFeature> _selectedVeloObjectFeatures; /*= new List<ISelectedFeature>();*/
        private InfoOfFeature _saveFeature;
        private List<InfoOfFeature> _saveFeatures = new List<InfoOfFeature>();
        /// <summary>
        /// Признак редактирования фич.
        /// </summary>
        private bool _isEdit;
        private InfoOfFeature _beginEdit;
        /// <summary>
        /// Список геометрий фич до их редактирования.
        /// </summary>
        private Dictionary<InfoOfFeature, IGeometry> _oldGeometries = new Dictionary<InfoOfFeature, IGeometry>();


        private VeloObject _selectedVeloObject;
        private IFeature _firstVeloObject;
        private IFeature _veloObject;


        #endregion

        #region Constructor

        /// <summary>
        /// Создает панель плагина.
        /// </summary>
        public VeloObjectPanel()
        {
            Subscrubies();
        }

        private void Subscrubies()
        {
            UndoRedoManager.CanBackEvent += SubscribeToBack;
            UndoRedoManager.CanForwardEvent += SubscribeToForward;
        }

        #endregion

        #region Lazy Properties

        /// <summary>
        /// Менеджер инструментов.
        /// </summary>
        private IToolManager ToolManager
        {
            get { return _toolManager ?? (_toolManager = VeloObjectConstants.Container.Resolve<IToolManager>()); }
        }


        /// <summary>
        /// Машина состояний.
        /// </summary>
        private IStateMachine StateMachine
        {
            get { return _stateMachine ?? (_stateMachine = VeloObjectConstants.Container.Resolve<IStateMachine>()); }
        }

        private IUndoRedoManager UndoRedoManager
        {
            get { return _undoRedoManager ?? (_undoRedoManager = VeloObjectConstants.Container.Resolve<IUndoRedoManager>()); }
        }

        /// <summary>
        /// Карта.
        /// </summary>
        private IMap Map
        {
            get { return _map ?? (_map = VeloObjectConstants.Container.Resolve<IMap>()); }
        }

        /// <summary>
        /// Менеджер слоёв.
        /// </summary>
        private ILayerManager LayerManager
        {
            get { return _layerManager ?? (_layerManager = VeloObjectConstants.Container.Resolve<ILayerManager>()); }
        }


        private IVeloObjectManager VeloObjectManager
        {
            get
            {
                return _VeloObjectManager ??
                       (_VeloObjectManager = VeloObjectConstants.Container.Resolve<IVeloObjectManager>());
            }
        }

        private IVectorLayer VeloObjectLayerOnMap
        {
            get { return Map.Layers.FirstOrDefault(l => l.Alias == Resources.VeloObjectLayerAlias) as IVectorLayer; }
        }

        private LayerObject VeloObjectLayer
        {
            get
            {
                return _VeloObjectLayer ?? (_VeloObjectLayer = LayerManager.GetByAlias(Map.Alias, Resources.VeloObjectLayerAlias));
            }
        }

        #endregion

        #region IMapObjectManager Members

        public string ToolName
        {
            get { return VeloObjectConstants.ToolName; }
        }

        public string ToolVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(2); }
        }

        #endregion

        #region Create Tool Methods

        private SelectTool CreateSelectTool()
        {
            return VeloObjectConstants.Container.Resolve<SelectTool>();
        }

        private EditGeometryTool CreateEditGeometryTool()
        {
            return VeloObjectConstants.Container.Resolve<EditGeometryTool>();
        }

        private PointTool CreatePointTool()
        {
            return VeloObjectConstants.Container.Resolve<PointTool>();
        }

        /*private LineTool CreateLineTool()
        {
            return VeloObjectConstants.Container.Resolve<LineTool>();
        }

        private PolygonTool CreatePolygonTool()
        {
            return VeloObjectConstants.Container.Resolve<PolygonTool>();
        }*/

        private RotateTool CreateRotateTool()
        {
            return VeloObjectConstants.Container.Resolve<RotateTool>();
        }

        #endregion

        #region Spectrum Panel Buttons

        #region Выбрать

        /// <summary>
        /// Обрабатывает нажатие кнопки "Выбрать геометрию".
        /// </summary>
        [Functional("PluginBaseSelectVeloObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.EndEdit", "ITS.PluginBaseVeloObject.BeginEdit")]
        [ChangeCheckedOn("PluginBaseSelectVeloObject", "ChangeMapTool")]
        [ToolAction("PluginBaseSelectVeloObject", "MapDown")]
        public void SelectVeloObjectMapDown(object sender, EventArgs e)
        {
            var args = e as MapClickEventArgs;
            if (args == null) return;
            SelectedFeature(args.ClickCoordinate);
        }

        /// <summary>
        /// Обрабатывает событие выбора геометрии.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [SubscribesTo("ITS.Client.MapInterfaceController.FeatureSelected")]
        public void OnFeatureSelected(object sender, EventArgs e)
        {
            if (_selectedVeloObjectFeatures != null)
            {
                foreach (var feat in _selectedVeloObjectFeatures)
                {
                    if (feat.Feature.Attributes.Exists(a => a.AttrType.Name == "Selected"))
                    {
                        VeloObjectPanelHelper.SetSelected(feat.Feature, false);
                    }
                }
                _selectedVeloObjectFeatures.Clear();
            }

            if (StateMachine.SelectedFeatures.Count == 1)
            {
                var selected = StateMachine.SelectedFeatures.FirstOrDefault();
                if (selected.Feature.Attributes.Exists(x => x.AttrType.Name == "VeloObject"))
                {
                    _selectedVeloObjectFeatures.Add(selected);
                    VeloObjectPanelHelper.SetSelected(selected.Feature, true);
                    if (HasSelectedVeloObject != null)
                        HasSelectedVeloObject(sender, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Обрабатывает событие прекращения выбора геометрии.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [SubscribesTo("ITS.Client.MapInterfaceController.FeatureNone")]
        public void OnFeatureNone(object sender, EventArgs e)
        {
            if (_selectedVeloObjectFeatures != null)
            {
                foreach (var feat in _selectedVeloObjectFeatures)
                {
                    if (feat.Feature.Attributes.Exists(a => a.AttrType.Name == "Selected"))
                    {
                        VeloObjectPanelHelper.SetSelected(feat.Feature, false);
                    }
                }
                _selectedVeloObjectFeatures.Clear();
            }
            if (HasNoSelectedVeloObject != null)
                HasNoSelectedVeloObject(sender, EventArgs.Empty);
        }

        #endregion

        #region Добавление велопарковки(точки)

        [Functional("PluginBaseAddPointObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeCheckedOn("PluginBaseAddPointObject", "ChangeMapTool")]
        [ToolAction("PluginBaseAddPointObject", "MapClick")]
        public void OnAddPoint(object sender, EventArgs e)
        {
            var args = (MapClickEventArgs)e;
            CreatePointObject(args);
        }

        [ToolAction("PluginBaseAddPointObject", "ItemChange")]
        public void OnAddPointChange(object sender, EventArgs e)
        {
            DeselectAll();
            Map.BeginRedraw();
        }

        #endregion

        #region Добавление линии

        /*[Functional("PluginBaseAddLineObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.EndEdit", "ITS.PluginBaseVeloObject.BeginEdit")]
        [ChangeCheckedOn("PluginBaseAddLineObject", "ChangeMapTool")]
        [ToolAction("PluginBaseAddLineObject", "ItemChange")]
        public void AddRoadMarkingItemChange(object sender, EventArgs e)
        {
            DeselectAll();
            Map.BeginRedraw();

            var tool = CreateLineTool();
            tool.LineCreated += LineTool_OnLineCreated;
            ToolManager.TurnOn(tool);
        }

        [ToolAction("PluginBaseAddLineObject", "ClickOnOtherTool")]
        public void AddRoadMarkingClickOnOtherTool(object sender, EventArgs e)
        {
            var tool = ToolManager.CurrentTool as LineTool;
            if (tool != null)
            {
                tool.FinishedOrCancel();
                ToolManager.TurnOff();
                tool.LineCreated -= LineTool_OnLineCreated;
            }
        }

        private void LineTool_OnLineCreated(IFeature feature)
        {
            var tool = ToolManager.CurrentTool as LineTool;
            if (tool != null)
            {
                ToolManager.TurnOff();
                if (feature != null)
                {
                    CreateNewVeloObject(feature);
                    
                    //var featureObject = feature.GetFeatureObject();

                    //var lo = LayerManager.GetByAlias(Map.Alias, VeloObjectConstants.VeloObjectLayerAlias);
                    //lo.AddFeature(featureObject);

                    //IRoadMarkingModel model = new RoadMarkingModel(RoadMarkingManager, MaterialManager,
                    //    TypeOfRoadMarkingManager, FeatureManager);
                    //var presenter = new AddRoadMarkingPresenter();
                    //IAddRoadMarkingView view = new AddRoadMarkingView(presenter, model, featureObject);
                    //if (!view.ShowForm())
                    //{
                    //    var deleteTool = CreateDeleteTool();
                    //    deleteTool.Delete();
                    //}

                    //StateMachine.SelectNone();
                    //Map.RemoveFromServiceLayer(feature);
                    //Map.RemoveFromTempLayer(feature);
                    //Map.BeginRedraw();
                }
                ToolManager.TurnOn(tool);
            }
        }
        */
        #endregion

        #region Добавление полигона
        /*
        [Functional("PluginBaseAddPolygonObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.EndEdit", "ITS.PluginBaseVeloObject.BeginEdit")]
        [ChangeCheckedOn("PluginBaseAddPolygonObject", "ChangeMapTool")]
        [ToolAction("PluginBaseAddPolygonObject", "ItemClick")]
        public void MapClickForPolygon(object sender, EventArgs e)
        {

            var tool = CreatePolygonTool();
            tool.PolygonCreated += PolygonTool_OnPolygonCreated;
            ToolManager.TurnOn(tool);
        }

        [ToolAction("PluginBaseAddPolygonObject", "ClickOnOtherTool")]
        public void ClickOnOtherToolPolygon(object sender, EventArgs e)
        {
            var tool = ToolManager.CurrentTool as PolygonTool;
            if (tool != null)
            {
                tool.FinishedOrCancel();
                ToolManager.TurnOff();
                tool.PolygonCreated -= PolygonTool_OnPolygonCreated;
            }
        }

        private void PolygonTool_OnPolygonCreated(IFeature feature)
        {
            var tool = ToolManager.CurrentTool as PolygonTool;
            if (tool != null)
            {
                ToolManager.TurnOff();
                if (feature != null)
                {
                    CreateNewVeloObject(feature);
                }
                ToolManager.TurnOn(tool);
            }
        }
        */
        #endregion

        #region Редактирование семантики

        [Functional("PluginBaseEditVeloObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.EndEdit", "ITS.PluginBaseVeloObject.BeginEdit")]
        [ChangeCheckedOn("PluginBaseEditVeloObject", "ChangeMapTool")]
        [ToolAction("PluginBaseEditVeloObject", "MapClick")]
        public void MapClickVeloObjectEdit(object sender, EventArgs e)
        {
            var args = e as MapClickEventArgs;
            if (args == null) return;
            var info = GetFeatureByClickCoordinate(args.ClickCoordinate);
            if (info != null)
            {
                EditVeloObject(info);
            }
        }

        #endregion

        #region Кнопка "Копирование"

        [ToolAction("PluginBaseCopyPasteVeloObject", "ItemChange")]
        public void CopyPasteItemChange(object sender, EventArgs e)
        {
            Map.BeginRedraw();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Копирование опоры".
        /// </summary>
        [Functional("PluginBaseCopyPasteVeloObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeCheckedOn("PluginBaseCopyPasteVeloObject", "ChangeMapTool")]
        [ToolAction("PluginBaseCopyPasteVeloObject", "MapClick")]
        public void OnCopyPastVeloObject(object sender, EventArgs e)
        {
            var clickArgs = (MapClickEventArgs)e;
            var selectedVeloObjects = VeloObjectPanelHelper.GetVeloObjects(StateMachine);
            if (Control.ModifierKeys == Keys.Control || !selectedVeloObjects.Any())
            {
                var args = e as MapClickEventArgs;
                InfoOfFeature info = GetFeatureByClickCoordinate(args.ClickCoordinate);
                if (info != null)
                {
                    StateMachine.SelectFeatureAdd(info);
                    selectedVeloObjects.Add(VeloObjectManager.GetByFeature(info.FeatureId));
                }
            }
            else
            {
                var firstVeloObject = selectedVeloObjects.First();
                var infos = StateMachine.SelectedFeatures.Select(s => s.Info).ToList();
                foreach (var info in infos)
                {
                    VeloObjectPanelHelper.SetVisiblity(info.Feature, true);
                    VeloObjectPanelHelper.SetSelected(info.Feature, false);
                }
                infos.Clear();
                var dx = clickArgs.ClickCoordinate.X - firstVeloObject.FeatureObject.Geometry.Coordinate.X;
                var dy = clickArgs.ClickCoordinate.Y - firstVeloObject.FeatureObject.Geometry.Coordinate.Y;

                var lo = LayerManager.GetByAlias(Map.Alias, VeloObjectConstants.VeloObjectLayerAlias);

                // var style = PointStyle.Default.GetStyleObject();
                foreach (var ps in selectedVeloObjects)
                {
                    var newVeloObject = ps.GetCopy();
                    if (newVeloObject == null) continue;
                    var coord = ps.FeatureObject.Geometry.Coordinate;

                    newVeloObject.FeatureObject = new PointDomain(new NetTopologySuite.Geometries.Point(coord.X + dx, coord.Y + dy))
                    {
                        Style = GIS.CoreToGeoTranslators.StyleTranslator.GetStyleObject((IPointStyle)GIS.CoreToGeoTranslators.StyleTranslator.GetStyle(firstVeloObject.FeatureObject.Style).Clone()),
                        Layer = lo
                    };

                    newVeloObject = VeloObjectManager.AddNew(newVeloObject);
                    var layer = Map.Layers.FirstOrDefault(x => x.Alias == VeloObjectConstants.VeloObjectLayerAlias) as IVectorLayer;
                    if (layer != null)
                    {
                        // Вручную засовываем фичу в лаер. Если этого не делать, 
                        // лоадер добавит ее сам и мы не сможем назначить ей никакие атрибуты.
                        var feature = newVeloObject.FeatureObject.GetFeature();
                        layer.Features.Add(newVeloObject.FeatureObject.ID, feature);
                        feature.Attributes.Add(new Attribute<VeloObject>(new AttributeType<VeloObject>("VeloObject"), newVeloObject));
                        VeloObjectPanelHelper.SetVisiblity(feature, true);
                        VeloObjectPanelHelper.SetSelected(feature, true);
                        infos.Add(
                            Map.GetAllFeature(feature.Geometry.Coordinate, VeloObjectConstants.VeloObjectLayerAlias)
                                .First(f => f.FeatureId == newVeloObject.FeatureObject.ID));
                    }
                }
                StateMachine.SelectFeatureAdd(infos, false);
                Map.BeginRedraw();
            }
        }

        [ToolAction("PluginBaseCopyePastVeloObject", "MapRightClick")]
        public void MapRightClickCopyPastVeloObject(object sender, EventArgs e)
        {
            DeselectAll();
        }

        #endregion

        #region Удаление объекта

        [Functional("PluginBaseDeleteObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.EndEdit", "ITS.PluginBaseVeloObject.BeginEdit")]
        [ChangeCheckedOn("PluginBaseDeleteObject", "ChangeMapTool")]
        [ToolAction("PluginBaseDeleteObject", "MapClick")]
        public void MapClickDeleteVeloObject(object sender, EventArgs e)
        {
            var args = e as MapClickEventArgs;
            if (args == null) return;

            var info = GetFeatureByClickCoordinate(args.ClickCoordinate);

            if (info != null)
            {
                DeleteVeloObject(info);
            }
        }

        private void DeleteVeloObject(InfoOfFeature args)
        {
            if (MessageBox.Show("Удалить выбранную велопарковку?", "Подтвердите удаление", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var VeloObject = VeloObjectManager.GetByFeature(args.FeatureId);
                    if (VeloObject == null) return;

                    VeloObjectManager.Delete(VeloObject);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        string.Format("Ошибка удаления {0}{1}", Environment.NewLine, ex.Message),
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Редактирование геометрии
        /*
        [Functional("PluginBaseEditGeometryVeloObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeCheckedOn("PluginBaseEditGeometryVeloObject", "ChangeMapTool")]
        [ToolAction("PluginBaseEditGeometryVeloObject", "MapDown")]
        public void MapDownSelectEditGeometryVeloObject(object sender, EventArgs e)
        {
            var args = e as MapClickEventArgs;
            if (args == null)
                return;

            if (!_flag)
            {
                StateMachine.SelectNone();

                InfoOfFeature info = GetFeatureByClickCoordinate(args.ClickCoordinate);

                try
                {
                    if (info != null)
                        AsyncLoaderForm.ShowMarquee(
                            (s, ee) => { _currentVeloObject = VeloObjectManager.GetByFeature(info.FeatureId); },
                            "Загрузка для редактирования");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        string.Format("Ошибка редактирования {0}{1}", Environment.NewLine, ex.Message),
                        "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (info != null)
                {
                    if (info.Feature.Geometry is IPolygon)
                    {
                        _geometry = (IGeometry)info.Feature.Geometry.Clone();

                        if (BeginEdit != null)
                        {
                            BeginEdit(this, EventArgs.Empty);
                        }
                        var infoFeature = Map.AddToTempLayer(info.Feature);
                        StateMachine.SelectFeature(infoFeature, false);
                        //RendererSettings.NonDrawingFeatures.Add(info.FeatureId);

                        var tool = CreateEditGeometryTool();
                        ToolManager.TurnOn(tool);

                        _flag = true;
                    }
                }
            }
        }
        */
        #endregion

        #region Перемещение велопарковки

        /// <summary>
        /// Перемещение на карте выбранной остановки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Событие клика по карте</param>

        [Functional("PluginBaseMoveVeloObject", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeCheckedOn("PluginBaseMoveVeloObject", "ChangeMapTool")]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.EndEdit", "ITS.PluginBaseVeloObject.BeginEdit")]
        [ToolAction("PluginBaseMoveVeloObject", "MapClick")]
        public void MapLeftClickVeloMove(object sender, EventArgs e)
        {
            StateMachine.SelectNone();
            var clickArgs = (MapClickEventArgs)e;
            //Если остановка для перемещения не выбрана
            if (_selectedVeloObject == null)
            {
                var veloObjectInfo = GetFeatureByClickCoordinate(clickArgs.ClickCoordinate);

                // var veloObjectInfo = GetVeloObjectInfoByFeature(clickArgs.ClickCoordinate);

                _selectedVeloObject = GetVeloObjectByFeature(veloObjectInfo.Feature);

                StateMachine.SelectFeature(veloObjectInfo);

                LineString[] ls = null;
                var attrBoundary = veloObjectInfo.Feature.Attributes.Find(a => a.AttrType.Name == "Boundary");
                if (attrBoundary != null)
                {
                    ls = new[] { new LineString(((Attribute<IGeometry>)attrBoundary).AttrValue.Coordinates) };
                }
                else
                {
                    var centre = veloObjectInfo.Feature.Geometry.Centroid;
                    var halfSizeX = 6;
                    var halfSizeY = 6;
                    ls = new[]{ new LineString(new []{
                        new Coordinate(centre.X - halfSizeX, centre.Y + halfSizeY),
                        new Coordinate(centre.X + halfSizeX, centre.Y + halfSizeY),
                        new Coordinate(centre.X + halfSizeX, centre.Y - halfSizeY),
                        new Coordinate(centre.X - halfSizeX, centre.Y - halfSizeY)
                    })};
                }
                var mls = new MultiLineString(ls);
                var g = new MultiLineDomain(mls);
                g.Style = new LineStyleObject(3, System.Drawing.Color.Red, System.Drawing.Drawing2D.DashStyle.Solid);
                var feature = g.GetFeature();
                Map.AddToTempLayer(feature);
                Map.BeginRedrawRegion(feature.Envelope);
                _firstVeloObject = feature;
                //BeginEdit?.Invoke(this, EventArgs.Empty);

            }
            //Если остановка выбрана
            else
            {
                //Находим новый адрес для перемещённой остановки
                //   _selectedVeloObject.Address = LoadAddress(new NetTopologySuite.Geometries.Point(clickArgs.ClickCoordinate.X, clickArgs.ClickCoordinate.Y), 30);
                var geom = new NetTopologySuite.Geometries.Point(clickArgs.ClickCoordinate);
                _selectedVeloObject.FeatureObject.Geometry = geom;
                VeloObjectManager.Edit(_selectedVeloObject);
                StateMachine.SelectNone();
                _selectedVeloObject = null;
                Map.RemoveFromTempLayer(_firstVeloObject);
                Map.BeginRedrawRegion(_firstVeloObject.Envelope);
            }
        }

        /// <summary>
        /// Отменяем выбор при смене инструмента или нажати правой кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [ToolAction("PluginBaseMoveVeloObject", "ClickOnOtherTool")]

        public void ClickOnOtherToolVeloObjectMove(object sender, EventArgs e)
        {
            _selectedVeloObject = null;
            Map.RemoveFromTempLayer(_veloObject);
            Map.BeginRedrawRegion(_firstVeloObject.Envelope);
        }

        [ToolAction("PluginBaseMoveVeloObject", "MapRightClick")]
        public void MapRightClickVeloObjectMove(object sender, EventArgs e)
        {
            _selectedVeloObject = null;
            Map.RemoveFromTempLayer(_firstVeloObject);
            Map.BeginRedrawRegion(_firstVeloObject.Envelope);
        }

        [ToolAction("PluginBaseMoveVeloObject", "MapMove")]
        public void MapMoveVeloObjectMove(object sender, EventArgs e)
        {
            var clickArgs = (MapClickEventArgs)e;
            if (clickArgs != null && _selectedVeloObject != null)
            {
                Map.RemoveFromTempLayer(_firstVeloObject);

                _selectedVeloObject.FeatureObject.Geometry = new Point(clickArgs.ClickCoordinate);

                var newCoordinates = new Coordinate[_firstVeloObject.Geometry.Coordinates.Count()];
                for (int i = 0; i < newCoordinates.Length; i++)
                {
                    newCoordinates[i] = new Coordinate(clickArgs.ClickCoordinate.X + _firstVeloObject.Geometry.Coordinates[i].X - _firstVeloObject.Geometry.Centroid.X,
                                                      clickArgs.ClickCoordinate.Y + _firstVeloObject.Geometry.Coordinates[i].Y - _firstVeloObject.Geometry.Centroid.Y);
                }
                LineString[] ls = { new LineString(newCoordinates) };
                var mls = new MultiLineString(ls);
                var g = new MultiLineDomain(mls);
                g.Style = new LineStyleObject(3, System.Drawing.Color.Red, System.Drawing.Drawing2D.DashStyle.Solid);
                var feature = g.GetFeature();
                Map.AddToTempLayer(feature);
                Map.BeginRedrawRegion(feature.Envelope);
                _firstVeloObject = feature;
            }
        }


        #endregion

        #region Вращение геометрии

        /// <summary>
        /// Изменение угла выбранной на карте остановки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Событие клика по карте</param>
        [Functional("PluginBaseRotateGeometry", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeCheckedOn("PluginBaseRotateGeometry", "ChangeMapTool")]
        [ToolAction("PluginBaseRotateGeometry", "MapClick")]
        public void RotateGeometryImageChange(object sender, EventArgs e)
        {
            var clickArgs = (MapClickEventArgs)e;
            InfoOfFeature info;
            var veloObject = GetSelectedVeloObject(clickArgs.ClickCoordinate, out info);
            if (veloObject != null)
            {
                System.Drawing.Point point =
                    Map.CoordSys.ToControl(new NetTopologySuite.Geometries.Point(veloObject.FeatureObject.Geometry.Centroid.X,
                                                            veloObject.FeatureObject.Geometry.Centroid.Y));

                var angleManageView = VeloObjectConstants.Container.Resolve<IAngleManageView>(new ParameterOverride("point", point));
                VeloObjectConstants.Container.Resolve<IAngleManagePresenter>(new ParameterOverride("view", angleManageView));
                angleManageView.InitView(veloObject.Angle);

                if (angleManageView.DisplayView())
                {
                    veloObject.Angle = 360 - angleManageView.CurrentAngle;
                    VeloObjectManager.Edit(veloObject);
                }
            }
        }
        #endregion

        #endregion

 
        #region Вкладка Ведомости

        #region Информация о велопарковке


        [Functional("PluginBaseVeloObjectInfo", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.EndEdit", "ITS.PluginBaseVeloObject.BeginEdit")]
        [ChangeCheckedOn("PluginBaseVeloObjectInfo", "ChangeMapTool")]
        [ToolAction("PluginBaseVeloObjectInfo", "MapClick")]
        public void MapClickVeloObjectInfo(object sender, EventArgs e)
        {
            var args = e as MapClickEventArgs;
            if (args == null) return;
            var info = GetFeatureByClickCoordinate(args.ClickCoordinate);
            if (info != null)
            {
                ShowVeloObjectInfo(info);
            }
        }

        #endregion

        #region Сводная ведомость

        [Functional("PluginBaseSummary", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.EndEdit", "ITS.PluginBaseVeloObject.BeginEdit")]
        [ChangeCheckedOn("PluginBaseSummary", "ChangeMapTool")]
        [ToolAction("PluginBaseSummary", "ItemClick")]
        public void ClickFindRoadRepair(object sender, EventArgs e)
        {
            var add = VeloObjectConstants.Container.Resolve<IVeloObjectSummaryView>();
            add.View();
            Map.ClearTempLayer();
            Map.BeginRedraw();
        }

        #endregion

        #endregion

        #region Вкладка Инструменты

        #region Масштабирование велопарковки

        [Functional("PluginBaseVeloObjectZoom", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeCheckedOn("PluginBaseVeloObjectZoom", "ChangeMapTool")]
        [ToolAction("PluginBaseVeloObjectZoom", "ItemClick")]
        /// <summary>
        /// Изменение масштаба иконок остановок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void MapLeftClickVeloObjectZoom(object sender, EventArgs e)
        {
            var view = VeloObjectConstants.Container.Resolve<IZoomView>();
            view.CurrentZoom = VeloObjectConstants.Zoom;
            if (view.ShowDialog())
            {
                if (VeloObjectConstants.Zoom != view.CurrentZoom)
                {
                    VeloObjectConstants.Zoom = view.CurrentZoom;
                    VeloObjectConstants.RenderChanged = true;
                }
                Map.BeginRedraw();
            }
        }
        #endregion


        #endregion

        #region Вкладка История изменений

        #region UndoRedo Buttons

        [Functional("PluginBaseBack", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("PluginBaseBackEnable", "PluginBaseBackNonEnable")]
        [ChangeEnableOn("", "ITS.MapObjects.EditGeometryPlugin.TaskClear")]
        [ToolAction("PluginBaseBack", "ItemClick")]
        public void BackItemClick(object sender, EventArgs e)
        {
            //UndoRedoManager.Back();
        }

        [Functional("PluginBaseForward", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper))]
        [ChangeEnableOn("PluginBaseForwardEnable", "PluginBaseForwardNonEnable")]
        [ChangeEnableOn("", "ITS.MapObjects.EditGeometryPlugin.TaskClear")]
        [ToolAction("PluginBaseForward", "ItemClick")]
        public void ForwardItemClick(object sender, EventArgs e)
        {
            //UndoRedoManager.Forward();
        }

        #endregion

        #region UndoRedo Methods

        private void SubscribeToBack(object sender, EventArgs args)
        {
            if (((BoolEventArgs)args).Flag)
            {
                if (BackEnable != null)
                    BackEnable(this, EventArgs.Empty);
            }
            else
            {
                if (BackNonEnable != null)
                    BackNonEnable(this, EventArgs.Empty);
            }
        }

        private void SubscribeToForward(object sender, EventArgs args)
        {
            if (((BoolEventArgs)args).Flag)
            {
                if (ForwardEnable != null)
                    ForwardEnable(this, EventArgs.Empty);
            }
            else
            {
                if (ForwardNonEnable != null)
                    ForwardNonEnable(this, EventArgs.Empty);
            }
        }

        #endregion

        #region UndoRedo Events

        [Publishes("PluginBaseBackEnable")]
        public event EventHandler<EventArgs> BackEnable;

        [Publishes("PluginBaseBackNonEnable")]
        public event EventHandler<EventArgs> BackNonEnable;

        [Publishes("PluginBaseForwardEnable")]
        public event EventHandler<EventArgs> ForwardEnable;

        [Publishes("PluginBaseForwardNonEnable")]
        public event EventHandler<EventArgs> ForwardNonEnable;

        #endregion

        #endregion

        #region Вкладка Сохранение изменений

        #region Кнопка "Применить"

        [Functional("PluginBaseAccept", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper), true)]
        [ChangeEnableOn("", "ITS.Client.MapInterfaceController.FeatureNone")]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.BeginEdit", "ITS.PluginBaseVeloObject.EndEdit")]
        [ToolAction("PluginBaseAccept", "ItemClick")]
        public void Accept(object sender, EventArgs e)
        {
            try
            {
                Map.RemoveFromTempLayer(StateMachine.SelectedFeatures.FirstOrDefault().Feature);
                var changedFeature = StateMachine.SelectedFeatures.FirstOrDefault().Feature;

                if (_currentVeloObject != null &&
                    VeloObjectLayerOnMap.Features.ContainsKey(_currentVeloObject.FeatureObject.ID))
                {
                    RendererSettings.NonDrawingFeatures.Remove(_currentVeloObject.FeatureObject.ID);

                    var startCoord = _currentVeloObject.FeatureObject.Geometry.Coordinate;
                    var endCoord = changedFeature.Geometry.Coordinate;
                    var resCoord = new Coordinate(endCoord.X - startCoord.X, endCoord.Y - startCoord.Y);

                    _currentVeloObject.FeatureObject.Geometry = changedFeature.Geometry;
                    _currentVeloObject = VeloObjectManager.Edit(_currentVeloObject);
                }
                                    
              
                ToolManager.TurnOff();
                StateMachine.SelectNone();
                _flag = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Ошибка редактирования{0}{1}", Environment.NewLine, ex.Message),
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (EndEdit != null)
            {
                EndEdit(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Кнопка "Отмена"

        [Functional("PluginBaseCancel", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper), true)]
        [ChangeEnableOn("", "ITS.Client.MapInterfaceController.FeatureNone")]
        [ChangeEnableOn("ITS.PluginBaseVeloObject.BeginEdit", "ITS.PluginBaseVeloObject.EndEdit")]
        [ToolAction("PluginBaseCancel", "ItemClick")]
        public void Cancel(object sender, EventArgs e)
        {
            Map.RemoveFromTempLayer(StateMachine.SelectedFeatures.FirstOrDefault().Feature);

            if (_currentVeloObject != null && VeloObjectLayerOnMap.Features.ContainsKey(_currentVeloObject.FeatureObject.ID))
            {
                RendererSettings.NonDrawingFeatures.Remove(_currentVeloObject.FeatureObject.ID);
            }
           

            ToolManager.TurnOff();

            StateMachine.SelectedFeatures.FirstOrDefault().Feature.Geometry = _geometry;
            Map.BeginRedrawRegion(StateMachine.SelectedFeatures.FirstOrDefault().Feature.Envelope);

            _flag = false;

            if (EndEdit != null)
            {
                EndEdit(this, EventArgs.Empty);
            }
            StateMachine.SelectNone();
        }

        #endregion
        #endregion

        #region Вкладка Статус

        [Functional("PluginBaseSelectVeloObjectSet", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper), selected: true)]
        [ChangeCheckedOn("PluginBaseIsVeloObjectDrawSetOn", "PluginBaseIsVeloObjectDrawSetOff")]
        [ToolAction("PluginBaseSelectVeloObjectSet", "ItemClick")]
        public void OnVeloObjectSetShow(object sender, EventArgs e)
        {
            VeloObjectConstants.IsDrawSet = !VeloObjectConstants.IsDrawSet;
            if (VeloObjectConstants.IsDrawSet)
            {
                if (IsVeloObjectDrawSetOn != null)
                    IsVeloObjectDrawSetOn(sender, e);
            }
            else
            {
                if (IsVeloObjectDrawSetOff != null)
                    IsVeloObjectDrawSetOff(sender, e);
            }
            Map.BeginRedraw();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Требуются".
        /// </summary>
        [Functional("PluginBaseSelectVeloObjectRequired", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper), selected: true)]
        [ChangeCheckedOn("PluginBaseIsVeloObjectDrawRequiredOn", "PluginBaseIsVeloObjectDrawRequiredOff")]
        [ToolAction("PluginBaseSelectVeloObjectRequired", "ItemClick")]
        public void OnVeloObjectRequiredShow(object sender, EventArgs e)
        {
            VeloObjectConstants.IsDrawRequired = !VeloObjectConstants.IsDrawRequired;
            if (VeloObjectConstants.IsDrawRequired)
            {
                if (IsVeloObjectDrawRequiredOn != null)
                    IsVeloObjectDrawRequiredOn(sender, e);
            }
            else
            {
                if (IsVeloObjectDrawRequiredOff != null)
                    IsVeloObjectDrawRequiredOff(sender, e);
            }
            Map.BeginRedraw();
        }
        /// <summary>
        /// Обрабатывает нажатие кнопки "Демонтировать".
        /// </summary>
        [Functional("PluginBaseSelectVeloObjectDismantle", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper), selected: true)]
        [ChangeCheckedOn("PluginBaseIsVeloObjectDrawDismantleOn", "PluginBaseIsVeloObjectDrawDismantleOff")]
        [ToolAction("PluginBaseSelectVeloObjectDismantle", "ItemClick")]
        public void OnVeloObjectDismantleShow(object sender, EventArgs e)
        {
            VeloObjectConstants.IsDrawDismantle = !VeloObjectConstants.IsDrawDismantle;
            if (VeloObjectConstants.IsDrawDismantle)
            {
                if (IsVeloObjectDrawDismantleOn != null)
                    IsVeloObjectDrawDismantleOn(sender, e);
            }
            else
            {
                if (IsVeloObjectDrawDismantleOff != null)
                    IsVeloObjectDrawDismantleOff(sender, e);
            }
            Map.BeginRedraw();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки "Мобильный".
        /// </summary>
        [Functional("PluginBaseSelectVeloObjectMobile", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper), selected: true)]
        [ChangeCheckedOn("PluginBaseIsVeloObjectDrawMobileOn", "PluginBaseIsVeloObjectDrawMobileOff")]
        [ToolAction("PluginBaseSelectVeloObjectMobile", "ItemClick")]
        public void OnVeloObjectMobileShow(object sender, EventArgs e)
        {
            VeloObjectConstants.IsDrawMobile = !VeloObjectConstants.IsDrawMobile;
            if (VeloObjectConstants.IsDrawMobile)
            {
                if (IsVeloObjectDrawMobileOn != null)
                    IsVeloObjectDrawMobileOn(sender, e);
            }
            else
            {
                if (IsVeloObjectDrawMobileOff != null)
                    IsVeloObjectDrawMobileOff(sender, e);
            }
            Map.BeginRedraw();
        }
        /// <summary>
        /// Обрабатывает нажатие кнопки "Праздничный".
        /// </summary>
        [Functional("PluginBaseSelectVeloObjectCelebrate", FunctionalType.Tool, typeof(VeloObjectPanelResourceHelper), selected: true)]
        [ChangeCheckedOn("PluginBaseIsVeloObjectDrawCelebrateOn", "PluginBaseIsVeloObjectDrawCelebrateOff")]
        [ToolAction("PluginBaseSelectVeloObjectCelebrate", "ItemClick")]
        public void OnVeloObjectCelebrateShow(object sender, EventArgs e)
        {
            VeloObjectConstants.IsDrawCelebrate = !VeloObjectConstants.IsDrawCelebrate;
            if (VeloObjectConstants.IsDrawCelebrate)
            {
                if (IsVeloObjectDrawCelebrateOn != null)
                    IsVeloObjectDrawCelebrateOn(sender, e);
            }
            else
            {
                if (IsVeloObjectDrawCelebrateOff != null)
                    IsVeloObjectDrawCelebrateOff(sender, e);
            }
            Map.BeginRedraw();
        }

        #endregion

        #region Events

        private event Action<InfoOfFeature> FeatureSelected;
        private event Action<InfoOfFeature> FeatureDeselected;

        [Publishes("PluginBaseIsVeloObjectDrawSetOn")]
        public event EventHandler<EventArgs> IsVeloObjectDrawSetOn;

        [Publishes("PluginBaseIsVeloObjectDrawSetOff")]
        public event EventHandler<EventArgs> IsVeloObjectDrawSetOff;

        [Publishes("PluginBaseIsVeloObjectDrawRequiredOn")]
        public event EventHandler<EventArgs> IsVeloObjectDrawRequiredOn;

        [Publishes("PluginBaseIsVeloObjectDrawRequiredOff")]
        public event EventHandler<EventArgs> IsVeloObjectDrawRequiredOff;

        [Publishes("PluginBaseIsVeloObjectDrawDismantleOn")]
        public event EventHandler<EventArgs> IsVeloObjectDrawDismantleOn;

        [Publishes("PluginBaseIsVeloObjectDrawDismantleOff")]
        public event EventHandler<EventArgs> IsVeloObjectDrawDismantleOff;

        [Publishes("PluginBaseIsVeloObjectDrawMobileOn")]
        public event EventHandler<EventArgs> IsVeloObjectDrawMobileOn;

        [Publishes("PluginBaseIsVeloObjectDrawMobileOff")]
        public event EventHandler<EventArgs> IsVeloObjectDrawMobileOff;

        [Publishes("PluginBaseIsVeloObjectDrawCelebrateOn")]
        public event EventHandler<EventArgs> IsVeloObjectDrawCelebrateOn;

        [Publishes("PluginBaseIsVeloObjectDrawCelebrateOff")]
        public event EventHandler<EventArgs> IsVeloObjectDrawCelebrateOff;

        [Publishes("PluginBaseHasSelectedVeloObject")]
        public event EventHandler<EventArgs> HasSelectedVeloObject;

        [Publishes("PluginBaseHasNoSelectedVeloObject")]
        public event EventHandler<EventArgs> HasNoSelectedVeloObject;

        [Publishes("ITS.PluginBaseVeloObject.BeginEdit")]
        public event EventHandler<EventArgs> BeginEdit;

        [Publishes("ITS.PluginBaseVeloObject.EndEdit")]
        public event EventHandler<EventArgs> EndEdit;

        #endregion

        #region Event Handlers

        

        private EventHandler<EventArgs> OnPointCreatedDelegate;

        #endregion

        #region Private Methods

        private void DeselectAll()
        {
            var infos = StateMachine.SelectedFeatures.Select(x => x.Info).ToList();
            StateMachine.SelectNone();
            infos.ForEach(
                i =>
                {
                    var attr = ((IAttribute<bool>)i.Feature.Attributes.Find(a => a.AttrType.Name == "Selected"));
                    if (attr != null) attr.AttrValue = false;
                    if (FeatureDeselected != null)
                        FeatureDeselected(i);
                });
        }

        /// <summary>
        /// Выделение фич, используется всегда когда нужно выделить фичу.
        /// </summary>
        /// <param name="args">Координаты фичи.</param>
        private void SelectedFeature(Coordinate args)
        {
            //сохранение изменёных фич.
            if (_saveFeature != null && _isEdit)
            {
                _saveFeatures.Add(_saveFeature);
            }

            InfoOfFeature inf = null;
            VeloObject selectedVeloObject = null;
            if (_isEdit)
            {
                if (_beginEdit != null)
                {
                    selectedVeloObject = GetVeloObjectByFeature(_beginEdit.Feature);
                    inf = _beginEdit;
                }
                else
                {
                    if (StateMachine.SelectedFeatures != null)
                    {
                        inf = StateMachine.SelectedFeatures.Select(x => x.Info).First();
                    }
                    if (inf != null)
                        selectedVeloObject = GetVeloObjectByFeature(inf.Feature);
                }
            }
            else
            {
                selectedVeloObject = GetSelectedVeloObject(args, out inf);
            }
            _saveFeature = inf;

            if (selectedVeloObject == null)
            {
                Map.BeginRedraw();
            }
            else
            {
                if (_isEdit)
                    //сохранение геометрии фичи.
                    if (!_oldGeometries.ContainsKey(inf))
                    {
                        _oldGeometries.Add(inf, (IGeometry)inf.Feature.Geometry.Clone());
                    }

                _currentVeloObject = selectedVeloObject;

                if (inf.Feature.Geometry is ILineString && _isEdit)
                {
                    var selectedLine = (SelectedLine)StateMachine.SelectFeature(inf);
                    VeloObjectPanelHelper.SetSelected(inf.Feature, false);
                    selectedLine.PointableIsolation.ShowPoint = true;
                    selectedLine.PointableIsolation.ShowHidePoint = true;
                }
                else
                {
                    StateMachine.SelectFeature(inf, false);
                    VeloObjectPanelHelper.SetSelected(inf.Feature, true);
                }
                if (HasSelectedVeloObject != null)
                    HasSelectedVeloObject(null, EventArgs.Empty);
                Map.BeginRedraw();
            }
        }

        private VeloObject GetSelectedVeloObject(Coordinate coord, out InfoOfFeature info)
        {
            return VeloObjectPanelHelper.GetSelectedVeloObject(Map, VeloObjectManager, coord, _currentVeloObject, out info);
        }

        private VeloObject GetVeloObjectByFeature(IFeature feature)
        {
            var attr = (Attribute<VeloObject>)GetAttribute(feature, VeloObjectConstants.VeloObjectAttributeName);
            return attr == null ? null : attr.AttrValue;
        }

        private IAttribute GetAttribute(IFeature feature, string name)
        {
            return feature.Attributes.Find(a => a.AttrType.Name == name);
        }

        private InfoOfFeature GetFeatureByClickCoordinate(Coordinate coord)
        {
            var infos = Map.GetAllFeature(coord, 20);
            if (infos.Any())
            {
                InfoOfFeature info = null;
                NetTopologySuite.Geometries.Point coordPoint = new NetTopologySuite.Geometries.Point(coord);
                infos = infos.Where(a =>
                    a.Feature != null && a.LayerAlias == VeloObjectConstants.VeloObjectLayerAlias &&
                    a.Feature != null && IsVisible(GetVeloObjectByFeature(a.Feature)) &&
                    a.Feature.Geometry.Distance(coordPoint) <= 10).ToArray();
                if (infos.Length > 1)
                {
                    var listVeloObject = new List<string>();
                    AsyncLoaderForm.ShowMarquee((s, ee) =>
                    {
                        listVeloObject = infos.Select(info2 => VeloObjectManager.GetByFeature(info2.FeatureId).ToString()).ToList();
                    }, "Загрузка");

                    var form = new SelectFeatureForm(listVeloObject);
                    if (form.ShowDialog() == DialogResult.OK)
                        info = form.SelectedItem >= 0 ? infos[form.SelectedItem] : null;
                    else return null;
                }
                else
                {
                    if (infos.Length == 1)
                    {
                        info = infos.Single();
                    }
                }
                return info;
            }
            return null;
        }

        private bool IsVisible(VeloObject VeloObject)
        {
            if (VeloObject == null)
                return false;
            
            switch (VeloObject.VeloObjectStatus)
            {
                case VeloObjectStatus.Set:
                    return VeloObjectConstants.IsDrawSet;
                case VeloObjectStatus.Required:
                    return VeloObjectConstants.IsDrawRequired;
                case VeloObjectStatus.Dismantle:
                    return VeloObjectConstants.IsDrawDismantle;
                case VeloObjectStatus.Mobile:
                    return VeloObjectConstants.IsDrawMobile;
                case VeloObjectStatus.Celebrate:
                    return VeloObjectConstants.IsDrawCelebrate;
                default:
                    return true;
            }
        }

        private void CreatePointObject(MapClickEventArgs args)
        {
            IPoint point = new NetTopologySuite.Geometries.Point(args.ClickCoordinate.X, args.ClickCoordinate.Y);
            var pointFeatureObject =
                new PointDomain(point)
                {
                    Layer = LayerManager.GetByAlias(Map.Alias, VeloObjectConstants.VeloObjectLayerAlias),
                    Style = PointStyle.Default.GetStyleObject()
                };
            VeloObjectLayer.AddFeature(pointFeatureObject);

            var pointVeloObject = new VeloObject
            {
                FeatureObject = pointFeatureObject
            };
            
            var addView =
                VeloObjectConstants.Container.Resolve<IVeloObjectEditView>(new ParameterOverride("veloobject", pointVeloObject), new ParameterOverride("soManager", VeloObjectManager));

            addView.ShowViewDialog();
        }

        private void CreateNewVeloObject(IFeature VeloObjectFeature)
        {
            if (VeloObjectFeature == null)
                throw new ArgumentNullException("VeloObjectFeature");

            var featureObject = VeloObjectFeature.GetFeatureObject();
            featureObject.Layer = LayerManager.GetByAlias(Map.Alias, VeloObjectConstants.VeloObjectLayerAlias);
            VeloObjectLayer.AddFeature(featureObject);

            var VeloObject = new VeloObject
            {
                FeatureObject = featureObject,
            };

            var addView = VeloObjectConstants.Container.Resolve<IVeloObjectEditView>(
                new ParameterOverride("VeloObject", VeloObject), new ParameterOverride("VeloObjectManager", VeloObjectManager));

            addView.ShowViewDialog();
        }

        private void EditVeloObject(InfoOfFeature args)
        {
            try
            {
                VeloObject veloobject = null;
                AsyncLoaderForm.ShowMarquee((s, ee) => { veloobject = VeloObjectManager.GetByFeature(args.FeatureId); },
                    "Загрузка для редактирования");
                var addView =
                    VeloObjectConstants.Container.Resolve<IVeloObjectEditView>(new ParameterOverride("veloobject",
                        veloobject), new ParameterOverride("soManager", VeloObjectManager));
                addView.ShowViewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Ошибка редактирования {0}{1}", Environment.NewLine, ex.Message),
                    "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ShowVeloObjectInfo(InfoOfFeature args)
        {
            try
            {
                // Получили объект.
                VeloObject veloobject = null;
                AsyncLoaderForm.ShowMarquee((s, ee) => { veloobject = VeloObjectManager.GetByFeature(args.FeatureId); },
                    "Загрузка информации об объекте");

                // Если не получили, выходим.
                if (veloobject == null) return;

               //  Отображаем информацию.
                var addView =
                    VeloObjectConstants.Container.Resolve<IVeloObjectInfoView>(new ParameterOverride("veloobject",
                        veloobject), new ParameterOverride("soManager", VeloObjectManager));
                addView.ShowViewDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Ошибка просмотра информации{0}{1}", Environment.NewLine,
                        ex.Message), "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}