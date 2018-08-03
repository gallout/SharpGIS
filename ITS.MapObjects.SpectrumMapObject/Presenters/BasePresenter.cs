using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.MapObjects.SpectrumMapObject.IPresenters;
using ITS.MapObjects.SpectrumMapObject.IViews;

namespace ITS.MapObjects.SpectrumMapObject.Presenters
{
    /// <summary>
    /// Базовый представитель.
    /// </summary>
    public abstract class BasePresenter : IBasePresenter
    {
        #region IBaserPresenter Members

        public abstract void Dispose();

        #endregion
    }

    /// <summary>
    /// Базовый типизированный представитель.
    /// </summary>
    /// <typeparam name="TView">Тип вида.</typeparam>
    /// <typeparam name="TModel">Тип модели.</typeparam>
    public abstract class BasePresenter<TView, TModel> : BasePresenter, IBasePresenter<TView, TModel>
    {
        /// <summary>
        /// Модель (backing storage).
        /// </summary>
        private TModel _model;

        /// <summary>
        /// Вид (backing storage).
        /// </summary>
        private TView _view;

        #region IBasePresenter<TView,TModel> Members

        /// <summary>
        /// Вид.
        /// </summary>
        //[Dependency]
        public TView View
        {
            get { return _view; }
            set
            {
                if (value == null) throw new ArgumentNullException("View");
                if (_view != null) RemoveViewEventHandlers();
                _view = value;
                AddViewEventHandlers();
            }
        }

        /// <summary>
        /// Модель.
        /// </summary>
        //[Dependency]
        public TModel Model
        {
            get { return _model; }
            set
            {
                if (value == null) throw new ArgumentNullException("Model");
                if (_model != null) RemoveModelEventHandlers();
                _model = value;
                AddModelEventHandlers();
            }
        }

        public void Init(TView view, TModel model)
        {
            Model = model;
            View = view;
            ((IBaseView)View).ViewClosing += Dispose;
        }

        public override void Dispose()
        {
            if (_view != null)
            {
                RemoveViewEventHandlers();
                _view = default(TView);
            }
            if (_model != null)
            {
                RemoveModelEventHandlers();
                _model = default(TModel);
            }
        }
        #endregion

        /// <summary>
        /// Подписывается на события вида.
        /// </summary>
        protected abstract void AddViewEventHandlers();

        /// <summary>
        /// Отписывается от событий вида.
        /// </summary>
        protected abstract void RemoveViewEventHandlers();

        /// <summary>
        /// Подписывается на события модели.
        /// </summary>
        protected abstract void AddModelEventHandlers();

        /// <summary>
        /// Отписывается от событий модели.
        /// </summary>
        protected abstract void RemoveModelEventHandlers();
    }
}
