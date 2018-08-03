using System;

namespace ITS.MapObjects.SpectrumMapObject.IPresenters
{
    /// <summary>
    /// Базовый интерфейс представителя.
    /// </summary>
    public interface IBasePresenter : IDisposable
    {

    }

    /// <summary>
    /// Базовый типизированный интерфейс представителя.
    /// </summary>
    public interface IBasePresenter<TView, TModel> : IBasePresenter
    {
        void Init(TView view, TModel model);

        /// <summary>
        /// Вид, с которым работает презентор.
        /// </summary>
        TView View { get; set; }

        /// <summary>
        /// Модель, с которой работает презентор.
        /// </summary>
        TModel Model { get; set; }
    }
}