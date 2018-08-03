using ITS.MapObjects.SpectrumMapObject.IPresenters;
using ITS.MapObjects.SpectrumMapObject.IViews;


namespace ITS.MapObjects.SpectrumMapObject.Presenters
{
    public class AngleManagePresenter : IAngleManagePresenter
    {
        #region Private fields

        private readonly IAngleManageView _view;

        #endregion

        #region Constructor

        public AngleManagePresenter(IAngleManageView view)
        {
            _view = view;
            Init();
        }

        #endregion

        #region Private methods

        private void Init()
        {
            _view.ResetAngleToZero += OnResetAngleToZero;
            _view.InitialAngleChanging += OnInitialAngleChanging;
        }

        #endregion

        #region EventHandlers

        private void OnResetAngleToZero()
        {
            _view.SetCurrentAngle(0);
        }

        private void OnInitialAngleChanging(double angle)
        {
            _view.SetCurrentAngle((_view.CurrentAngle + angle) % 360);
        }

        #endregion
    }
}