using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using GeoAPI.Geometries;
using ITS.Core.Domain;
using ITS.Core.Enums;
using ITS.Core.Spectrum.Domain;
using ITS.Core.Spectrum.Domain.Enums;
using ITS.GIS.MapEntities;
using ITS.GIS.MapEntities.Attributes;
using ITS.GIS.MapEntities.Renderer;
using ITS.GIS.MapEntities.Styles;
using ITS.MapObjects.SpectrumMapObject.Properties;
using Microsoft.Practices.Unity;
using NetTopologySuite.Geometries;
using NHibernate.Criterion;
using ITS.ProjectBase.Utils.Container;
using ITS.Core;
using ITS.ProjectBase.Utils.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Drawing.Text;
using ITS.MapObjects.SpectrumMapObject.Locators;

namespace ITS.MapObjects.SpectrumMapObject
{
    public interface IVeloObjectLayerRender : ICustomLayerRenderer
    {

    }
    public class VeloObjectLayerRenderer : IVeloObjectLayerRender
    {

        private readonly SimpleLayerRenderer _simpleLayerRenderer;
        private Matrix _transformMatrix = new Matrix();
        private readonly IMap _map; //для масштабирования

        public string Alias
        {
            get { return Resources.VeloObjectLayerAlias; }
        }

        public string PluginName { get { return Resources.PluginToolName; } }
        public bool IsEnabled { get; set; }
        public string Description { get { return "Стандартный"; } }

        public VeloObjectLayerRenderer()
        {
            _simpleLayerRenderer = VeloObjectConstants.Container.Resolve<SimpleLayerRenderer>();
            IsEnabled = true;
            _map = Container.MainContainer.Resolve<IMap>();
            SelectedVeloPosts = new Dictionary<long, Color>();
        }

        private ExceptionManager _exceptionManager;

        private ExceptionManager ExceptionManager
        {
            get
            {
                if (_exceptionManager == null)
                {
                    _exceptionManager = ApplicationService.GetExceptionManager();
                }
                return _exceptionManager;
            }
        }
        /// <summary>
        /// Список выделенных велопарковок. Key - id знака, Value - цвет выделения.
        /// </summary>
        public Dictionary<long, Color> SelectedVeloPosts { get; set; }

        /// <summary>
        /// Включена ли подсветка велопарковок.
        /// </summary>
        public bool VeloPostsHighlightingEnabled { get; set; }


        private IAttribute GetAttribute(IFeature feature, string name)
        {
            return feature.Attributes.Find(a => a.AttrType.Name == name);
        }

        

          private VeloObject GetVeloObjectByFeature(IFeature feature)
          {
              var attr = (Attribute<VeloObject>)GetAttribute(feature, VeloObjectConstants.VeloObjectAttributeName);
              return attr == null ? null : attr.AttrValue;
          }


        public void Render(Graphics g, ILayer layer, Envelope envelope, Matrix transformMatrix)
        {
            var vlayer = layer as IVectorLayer;
            if (vlayer != null)
            { //Получили все геометрии заданной области
                var features = vlayer.Features.Where(f => envelope.Intersects(f.Value.Geometry.EnvelopeInternal)).
                    Select(f => f.Value).ToList();
                // Переходим к отрисовке списка ремонтов.
                Render(g, features, transformMatrix);
            }
        }

        public void Render(Graphics g, IEnumerable<IFeature> features, Matrix transformMatrix)
        {
            // Открываем контейнер для отрисовки.
            var gc = g.BeginContainer();

            // Применяем зум и смещения к контейнеру.
            g.Transform = transformMatrix;
           
            // Задаем максимальное качество отрисовки.
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //foreach (var feature in features)
            //{
            //    DrawVeloObject(g, feature);
            //    //DrawVeloObject(feature,g);
            //}
            DrawVeloObjects(g,features);

            // Закрываем контейнер.
            g.EndContainer(gc);
        }

   


        protected bool IsVisible(VeloObject VeloObject)
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
        
        //protected void DrawVeloObject(Graphics g, IFeature feature)
        //{
        //    var VeloObject = GetVeloObjectByFeature(feature);
        //    if (VeloObject == null) return;

        //    var ls = feature.Geometry;
        //    if (ls == null) return;

        //    if (!IsVisible(VeloObject))
        //        return;

        //    if (ls.GeometryType == "Point")
        //    {
        //        var img = GetImage(VeloObject.VeloView, VeloObject.VeloType);

        //        double ratio = 0.5;     // множитель размера отрисовываемого изображения
        //        int imgWidth = (int)Math.Round(img.Width * ratio);
        //        int imgHeight = (int)Math.Round(img.Height * ratio);

        //        if (imgWidth < 1) imgWidth = 1;
        //        if (imgHeight < 1) imgHeight = 1;

        //        IPicture pic = new Picture(ls.Coordinate.X - imgWidth / 2, ls.Coordinate.Y - imgHeight / 2,
        //            imgWidth, imgHeight, img);
        //        PointF[] destPoints = { pic.P1, pic.P2, pic.P3 };
        //        g.DrawImage(pic.Image, destPoints);
        //    }
        //    else if (ls.GeometryType == "LineString")
        //    {
        //        Paint(VeloObject.FeatureObject.Geometry, g, new LineStyle(Color.Purple, 2f, DashStyle.Dash));
        //    }
        //    else if (ls.GeometryType == "Polygon")
        //    {
        //        Paint(VeloObject.FeatureObject.Geometry, g, new AreaStyle(new InteriorStyle(Color.BlueViolet), new LineStyle(Color.Purple, 1f)));
        //    }
        //}

        protected Image GetImage(VeloView VeloView, VeloType VeloType)
        {

            switch (VeloView)
            {

                case (VeloView.View1):
                    switch (VeloType)
                    {
                        case (VeloType.Type1):
                            return Resources.k1;
                        case (VeloType.Type2):
                            return Resources.k2;
                        case (VeloType.Type3):
                            return Resources.k3;
                        case (VeloType.Type4):
                            return Resources.k4;
                        case (VeloType.Type5):
                            return Resources.k5;
                        case (VeloType.Type6):
                            return Resources.k6;
                        case (VeloType.Type7):
                            return Resources.k7;
                        case (VeloType.Type8):
                            return Resources.k8;
                        default:
                            return Resources.knone;
                    }

                case (VeloView.View2):

                    switch (VeloType)
                    {
                        case (VeloType.Type1):
                            return Resources.d1;
                        case (VeloType.Type2):
                            return Resources.d2;
                        case (VeloType.Type3):
                            return Resources.d3;
                        case (VeloType.Type4):
                            return Resources.d4;
                        case (VeloType.Type5):
                            return Resources.d5;
                        case (VeloType.Type6):
                            return Resources.d6;
                        case (VeloType.Type7):
                            return Resources.d7;
                        case (VeloType.Type8):
                            return Resources.d8;
                        default:
                            return Resources.dnone;
                    }

                case (VeloView.None):

                   switch (VeloType)
                    {
                        case (VeloType.Type1):
                            return Resources.n1;
                        case (VeloType.Type2):
                            return Resources.n2;
                        case (VeloType.Type3):
                            return Resources.n3;
                        case (VeloType.Type4):
                            return Resources.n4;
                        case (VeloType.Type5):
                            return Resources.n5;
                        case (VeloType.Type6):
                            return Resources.n6;
                        case (VeloType.Type7):
                            return Resources.n7;
                        case (VeloType.Type8):
                            return Resources.n8;
                        default:
                            return Resources.none1;
                    } 
                default:
                    return Resources.none1;

            }               
        }


        protected virtual void Paint(IGeometry geometry, Graphics g, IStyle style)
        {
            if (!geometry.IsEmpty)
                _simpleLayerRenderer.Paint(geometry, g, style, _transformMatrix);
        }


        /// <summary>
        /// Максимальный размер элемента.
        /// </summary>
        private double _itemMaxSizePx;

        /// <summary>
        /// Четверть размера элемента.
        /// </summary>
        private double _itemSizePxDiv4;

        /// <summary>
        /// Толщина линий (например, опоры-растяжки).
        /// </summary>
        private double _lineWidth;

        /// <summary>
        /// Универсальный стиль.
        /// </summary>
        private IStyle _style;

        /// <summary>
        /// Толщина линии растяжки.
        /// </summary>
        private float _wireWidth;

        protected void CreateFastVariables()
        {
            _itemMaxSizePx = VeloObjectConstants.Zoom;
            _itemSizePxDiv4 = _itemMaxSizePx / 6d;

            _lineWidth = _itemMaxSizePx / 100d;
            _wireWidth = (float)_itemMaxSizePx / 20f;

            _style = new CompositeStyle(new InteriorStyle(),
                                        new LineStyle(Color.Black, (float)_lineWidth),
                                        new PointStyle(Color.Black),
                                        new TextStyle(new Font("Arial", (float)_itemSizePxDiv4),
                                                      Color.Black), new ImageStyle());
        }

      

        /// <summary>
        /// Список выделенных остановок. Key - id знака, Value - цвет выделения.
        /// </summary>
        public Dictionary<long, Color> SelectedVeloObjects { get; set; }

        /// <summary>
        /// Включена ли подсветка остановок.
        /// </summary>
        public bool VeloObjectsHighlightingEnabled { get; set; }

        #region Implementation of ICustomLayerRenderer

        protected void DrawVeloObjects(Graphics g, IEnumerable<IFeature> features)
        {
            //Получили все геометрии заданной области
            foreach (var feature in features)
            {
                var attrVeloObject = (Attribute<VeloObject>)GetAttribute(feature, "VeloObject");
                VeloObject veloObject = null;
                var prevAngle = Double.NaN;
                if (attrVeloObject != null)
                {
                    veloObject = attrVeloObject.AttrValue;
                    prevAngle = veloObject.Angle;
                    var attr = GetAttribute(feature, "Angle");
                    if (attr != null)
                    {
                        veloObject.Angle += -((Attribute<double>)attr).AttrValue;
                    }
                }
                if (veloObject != null)
                {
                    var drawable = true;
                    switch (veloObject.VeloObjectStatus)
                    {
                        case VeloObjectStatus.Set:
                            {
                                if (!VeloObjectConstants.IsDrawSet)
                                    drawable = !drawable;
                                break;
                            }
                        case VeloObjectStatus.Required:
                            {
                                if (!VeloObjectConstants.IsDrawRequired)
                                    drawable = !drawable;
                                break;
                            }
                        case VeloObjectStatus.Dismantle:
                            {
                                if (!VeloObjectConstants.IsDrawDismantle)
                                    drawable = !drawable;
                                break;
                            }
                        case VeloObjectStatus.Mobile:
                            {
                                if (!VeloObjectConstants.IsDrawMobile)
                                    drawable = !drawable;
                                break;
                            }
                        case VeloObjectStatus.Celebrate:
                        {
                            if (!VeloObjectConstants.IsDrawCelebrate)
                                drawable = !drawable;
                            break;
                        }
                    }
                    if (drawable)
                        DrawVeloObject(feature, veloObject, g);
                }

                if (veloObject != null && VeloObjectsHighlightingEnabled && SelectedVeloObjects.ContainsKey(veloObject.ID))
                {
                    var rectPoints = GetRectanglePoints(new Coordinate(feature.Geometry.Centroid.X, feature.Geometry.Centroid.Y),
                        veloObject.Angle, VeloObjectConstants.Zoom + 1);
                    using (var pen = new Pen(SelectedVeloObjects[veloObject.ID], 0.5f)
                    { StartCap = LineCap.Square, EndCap = LineCap.Square })
                    {
                        g.DrawLines(pen, rectPoints.Select(c => new PointF((float)c.X, (float)c.Y)).ToArray());
                    }
                }
                if (veloObject != null && !Double.IsNaN(prevAngle)) veloObject.Angle = prevAngle;
            }
            VeloObjectConstants.RenderChanged = false;
        }

        protected void DrawVeloObject(IFeature feature, VeloObject veloObject, Graphics g)
        {
            var itemHalfSizePx = VeloObjectConstants.Zoom;
           
            //TODO: доделать смещение картинки к границе остановки.

            var angle = (veloObject != null ? (360 - veloObject.Angle) % 360 : 0) * Math.PI / 180;
            //var angle = (veloObject != null ? (veloObject.Angle) % 360 : 0) * Math.PI / 180;
            var sinAngle = Math.Sin(angle);
            var cosAngle = Math.Cos(angle);

            var center = new Coordinate(feature.Geometry.Centroid.X, feature.Geometry.Centroid.Y,
              feature.Geometry.Centroid.Z);

            Image veloObjectImage = GetImage(veloObject.VeloView, veloObject.VeloType);

            var picture =
                new Picture(
                    center.X - itemHalfSizePx * cosAngle - itemHalfSizePx * sinAngle,
                    center.Y - itemHalfSizePx * sinAngle + itemHalfSizePx * cosAngle,
                    center.X + itemHalfSizePx * cosAngle - itemHalfSizePx * sinAngle,
                    center.Y + itemHalfSizePx * sinAngle + itemHalfSizePx * cosAngle,
                    center.X + itemHalfSizePx * cosAngle + itemHalfSizePx * sinAngle,
                    center.Y + itemHalfSizePx * sinAngle - itemHalfSizePx * cosAngle,
                    center.X - itemHalfSizePx * cosAngle + itemHalfSizePx * sinAngle,
                    center.Y - itemHalfSizePx * sinAngle - itemHalfSizePx * cosAngle, veloObjectImage);

            var attr = (Attribute<IGeometry>)GetAttribute(feature, "Boundary");
            if (attr == null)
            {
                attr = new Attribute<IGeometry>(new AttributeType<IGeometry>("Boundary"), picture.Boundary);
                feature.Attributes.Add(attr);
            }
            else
            {
                attr.AttrValue = picture.Boundary;
            }


            //var conturColor = Color.DeepSkyBlue;

            //if (veloObject.VeloObjectStatus == VeloObjectStatus.Mobile)
            //{
            //    conturColor = Color.ForestGreen;
            //}
            //var pen = new Pen(conturColor, 0.5f);
            try
            {
                DrawImage(g, picture, (veloObject.Angle - 90) % 360);
                // g.DrawLines(pen, lines);
            }
            catch (OutOfMemoryException)
            {
            }


            //var dl = new Coordinate(picture.P1.X, picture.P1.Y); // нижняя левая точка
            //var dr = new Coordinate(picture.P2.X, picture.P2.Y); // нижняя правая точка
            //var ul = new Coordinate(picture.P3.X, picture.P3.Y); // верхняя левая точка

            //var middle = LocateUtils.GetCenter(new[] { ul, dr });

            /*if (veloObject != null)
            {
                DrawText(g, middle,
                   veloObject.Name, (veloObject.Angle) % 360);
            }*/
        }

        /// <summary>
        /// Нанесение Text на Graphics.
        /// </summary>
        /// <param name="g">Graphics.</param>
        /// <param name="text">Текст надписи.</param>
        /// <param name="angle">Угол поворота текста.</param>
        /// <param name="coord">Координата, где начинается надпись.</param>
        protected virtual void DrawText(Graphics g, Coordinate coord, string text, double angle)
        {
            var center = new Coordinate(coord.X, coord.Y);

            var l = new List<Coordinate> { new Coordinate(center.X + _itemMaxSizePx, center.Y) };

            var coordText = LocateUtils.RotateTransform(l, -((angle - 90) * Math.PI / 180), center)[0];

            var x = (float)coordText.X;
            var y = (float)coordText.Y;

            var textStyle = _style as ITextStyle;
            if (textStyle != null)
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                var size = g.MeasureString(text, textStyle.Font, 1000000, StringFormat.GenericTypographic);
                var cont = g.BeginContainer();

                // Сдвигаем строку на место отрисовки.
                g.TranslateTransform(x, y);

                // Делаем надпись зеркальной.
                g.ScaleTransform(1, -1);

                //Сдвиг по размеру текста.
                x = -size.Width / 2;
                y = -size.Height / 2;

                // Поворачиваем строку на заданный угол.
                g.RotateTransform((float)(angle));

                // Отрисовка белой каёмочки вокруг текста.
                const float dx = 0.02f;
                using (var brushWhite = new SolidBrush(Color.White))
                {
                    g.DrawString(text, textStyle.Font, brushWhite,
                        new PointF(x - dx, y - dx));
                    g.DrawString(text, textStyle.Font, brushWhite,
                        new PointF(x + dx, y + dx));
                }
                using (var brushStyled = new SolidBrush(textStyle.TextColor))
                {
                    g.DrawString(text, textStyle.Font, brushStyled, x, y);
                }
                g.EndContainer(cont);
            }
        }

        /// <summary>
        /// Получить атрибут геометрии по его названию.
        /// </summary>
        /// <param name="feature">Геометрия</param>
        /// <param name="name">Название атрибута</param>
        /// <returns></returns>
       

        protected virtual void DrawImage(Graphics g, IPicture picture, double angle)
        {
            if (picture == null) return;

            PointF[] destPoints = { picture.P1, picture.P2, picture.P3 };
            try
            {
                g.DrawImage(picture.Image, destPoints);
            }
            catch (OutOfMemoryException)
            {
            }
        }

       


        #endregion

        private static Coordinate[] GetRectanglePoints(Coordinate center, double angle, double halfBoundary)
        {
            var angleRad = ((360 - angle) % 360) * Math.PI / 180;
            Coordinate[] rect = new Coordinate[]
            {
                new Coordinate(center.X - halfBoundary, center.Y + halfBoundary),
                new Coordinate(center.X + halfBoundary, center.Y + halfBoundary),
                new Coordinate(center.X + halfBoundary, center.Y - halfBoundary),
                new Coordinate(center.X - halfBoundary, center.Y - halfBoundary),
                new Coordinate(center.X - halfBoundary, center.Y + halfBoundary)
            };
            return LocateUtils.RotateTransform(rect, angleRad, center);
        }
    }

}
 


