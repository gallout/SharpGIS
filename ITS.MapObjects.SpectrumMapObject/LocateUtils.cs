using System;
using System.Collections.Generic;
using System.Drawing;
using GeoAPI.Geometries;

namespace ITS.MapObjects.SpectrumMapObject.Locators
{
    public class LocateUtils
    {
        /// <summary>
        /// Поворачивает точки относительно центральной точки на заданный угол.
        /// </summary>
        /// <param name="points">Точки, которые надо повернуть.</param>
        /// <param name="angle">Угол поворота.</param>
        /// <param name="center">Центр поворота.</param>
        /// <returns>Повернутые точки.</returns>
        public static Coordinate[] RotateTransform(IEnumerable<Coordinate> points, double angle, Coordinate center)
        {
            var res = new List<Coordinate>();
            foreach (var point in points)
            {
                var x = (point.X - center.X) * Math.Cos(angle) - (point.Y - center.Y) * Math.Sin(angle) + center.X;
                var y = (point.X - center.X) * Math.Sin(angle) + (point.Y - center.Y) * Math.Cos(angle) + center.Y;
                res.Add(new Coordinate(x, y));
            }
            return res.ToArray();
        }

        /// <summary>
        /// Поворачивает точки относительно центральной точки на заданный угол.
        /// </summary>
        /// <param name="points">Точки, которые надо повернуть.</param>
        /// <param name="angle">Угол поворота.</param>
        /// <param name="center">Центр поворота.</param>
        /// <returns>Повернутые точки.</returns>
        public static PointF[] RotateTransform(PointF[] points, double angle, PointF center)
        {
            var res = new List<PointF>();
            foreach (var point in points)
            {
                var x = (point.X - center.X) * Math.Cos(angle) - (point.Y - center.Y) * Math.Sin(angle) + center.X;
                var y = (point.X - center.X) * Math.Sin(angle) + (point.Y - center.Y) * Math.Cos(angle) + center.Y;
                res.Add(new PointF((float)x, (float)y));
            }
            return res.ToArray();
        }

        public static PointF[] TranslateTransform(PointF[] points, float offsetX, float offsetY)
        {
            var res = new List<PointF>();
            foreach (var point in points)
            {
                res.Add(new PointF(point.X + offsetX, point.Y + offsetY));
            }
            return res.ToArray();
        }

        /// <summary>
        /// Получает весовой центр расположения точек.
        /// </summary>
        /// <param name="points">Точки для получения центра.</param>
        /// <returns>Центр точек.</returns>
        public static Coordinate GetCenter(Coordinate[] points)
        {
            if (points == null || points.Length == 0) throw new ArgumentOutOfRangeException("points");

            double x = 0, y = 0;

            foreach (var point in points)
            {
                x += point.X;
                y += point.Y;
            }

            return new Coordinate((x / points.Length), (y / points.Length));
        }

        public static double GetAngle(Coordinate center, Coordinate coord)
        {
            return Math.Atan2(coord.Y - center.Y, coord.X - center.X);
        }

        public static double GetDistance(Coordinate c1, Coordinate c2)
        {
            var dx = c2.X - c1.X;
            var dy = c1.Y - c2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static Coordinate[] Scale(Coordinate[] points, Coordinate center, double scale)
        {
            return Scale(points, center, scale, scale);
        }

        public static Coordinate[] Scale(Coordinate[] points, Coordinate center, double scaleX, double scaleY)
        {
            foreach (var point in points)
            {
                point.X = center.X - (center.X - point.X) * scaleX;
                point.Y = center.Y - (center.Y - point.Y) * scaleY;
            }
            return points;
        }

    }
}