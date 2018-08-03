using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ITS.Client.Core.ViewInterfaces;
using ITS.ProjectBase.Utils.Delegates;

namespace ITS.MapObjects.SpectrumMapObject.IViews
{
    public interface IAngleManageView : IEditView
    {
        #region Properties

        /// <summary>
        /// Текущий угол.
        /// </summary>
        double CurrentAngle { get; }

        /// <summary>
        /// Исходное значение угла.
        /// </summary>
        double InitialAngle { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Инициализирует отображение.
        /// </summary>
        /// <param name="angle"></param>
        void InitView(double angle);

        /// <summary>
        /// Устанавливает текущее значение угла.
        /// </summary>
        /// <param name="angle">Угол.</param>
        void SetCurrentAngle(double angle);

        #endregion

        #region Events

        /// <summary>
        /// Сброс угла в 0.
        /// </summary>
        event SimpleEventHandler ResetAngleToZero;

        /// <summary>
        /// Изменение начального значения угла.
        /// </summary>
        event GenericEventHandler<double> InitialAngleChanging;

        #endregion
    }
}