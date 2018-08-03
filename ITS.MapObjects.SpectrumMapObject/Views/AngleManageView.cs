using ITS.MapObjects.SpectrumMapObject.IViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ITS.ProjectBase.Utils.Delegates;
using System.Drawing.Drawing2D;

namespace ITS.MapObjects.SpectrumMapObject.Views
{
    public partial class AngleManageView : Form, IAngleManageView
    {
        #region Поля

        private double _angle;

        #endregion

        #region Конструкторы

        private AngleManageView()
        {
            InitializeComponent();
        }

        public AngleManageView(Point point)
            : this()
        {
            Top = point.Y - angleControl.Height / 2 - 1 /*Ширина границы формы - 1 пиксел*/;
            Left = point.X - Width / 2;
        }

        #endregion

        #region Члены IAngleManageView

        public bool DisplayView()
        {
            StartPosition = FormStartPosition.CenterParent;
            if (ShowDialog() == DialogResult.OK)
            {
                return IsAngleChanged();
            }
            return false;
        }

        public void RefreshView()
        {
            angleControl.Invalidate();
        }

        #endregion

        #region Properties

        public double InitialAngle
        {
            get { return _angle; }
        }

        public double CurrentAngle
        {
            get { return angleControl.CurrentAngle; }
        }

        #endregion

        #region События

        public event SimpleEventHandler ResetAngleToZero;

        public event GenericEventHandler<double> InitialAngleChanging;

        #endregion

        #region Методы

        public void SetCurrentAngle(double angle)
        {
            angleControl.CurrentAngle =angle;
            RefreshView();
        }

        public void InitView(double angle)
        {
            _angle = angle;
            angleControl.CurrentAngle = _angle;
            angleControl.Initialize(360-CurrentAngle);
        }

        #endregion

        #region Private methods

        private bool IsAngleChanged()
        {
            return Math.Abs(_angle - angleControl.CurrentAngle) > 0.01f;
        }

        #endregion

        #region Interactors

        private void AngleManageView_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(
                new LinearGradientBrush(
                    new PointF(Width / 2f, 0),
                    new PointF(Width / 2f, 100),
                    Color.LightBlue, Color.White),
                new RectangleF(0, 0, Width, 100));
        }

        private void bt0Degree_Click(object sender, EventArgs e)
        {
            ResetAngleToZero();
        }

        private void bt90Degree_Click(object sender, EventArgs e)
        {
            InitialAngleChanging(90);
        }
                                                            
        private void bt180Degree_Click(object sender, EventArgs e)
        {
            InitialAngleChanging(180);
        }

        private void bt270Degree_Click(object sender, EventArgs e)
        {
            InitialAngleChanging(270);
        }

        #endregion
    }
}