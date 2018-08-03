using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITS.Core.Enums;
using ITS.Core.Spectrum.Domain;
using ITS.Core.Spectrum.Domain.Enums;
using ITS.Core.Spectrum.ManagerInterfaces;
using ITS.MapObjects.SpectrumMapObject.IViews;

namespace ITS.MapObjects.SpectrumMapObject.Views
{
    public partial class VeloObjectInfoView : BaseView, IVeloObjectInfoView, IVeloObjectEditView
    {
        public VeloObjectInfoView(VeloObject veloobject, IVeloObjectManager soManager)
        {
            InitializeComponent();
            _veloobjectManager = soManager;
            VeloObject = veloobject;
            _isEdit = !VeloObject.IsTransient();
        }

        #region Fields

        private VeloObject _veloobject;
        private readonly IVeloObjectManager _veloobjectManager;
        private bool _isEdit;

        #endregion

        #region Properties

        public VeloObject VeloObject
        {
            get
            {  /// <summary>
               /// Тип велопарковки.
               /// </summary>
                _veloobject.VeloType = VeloTypeStrings.GetValue(textBox1.Text as string);
                
                /// <summary>
                /// Вид велопарковки.
                /// </summary>
                _veloobject.VeloView = VeloViewStrings.GetValue(textBox2.Text as string);

                /// <summary>
                /// Статус.
                /// </summary>
                _veloobject.VeloObjectStatus = VeloObjectStatusStrings.GetStatusByName(textBox3.Text as string);

                /// <summary>
                /// Количество секций.
                /// </summary>
                int sectionField = 0;
                int.TryParse(sectionBox.Text, out sectionField);
                _veloobject.VeloSection = sectionField;

                /// <summary>
                /// Длина.
                /// </summary>
                int lengthField = 0;
                int.TryParse(lengthBox.Text, out lengthField);
                _veloobject.VeloLength = lengthField;

                /// <summary>
                /// Ширина.
                /// </summary>
                int widthField = 0;
                int.TryParse(widthBox.Text, out widthField);
                _veloobject.VeloWidth = widthField;

                /// <summary>
                /// Дата установки.
                /// </summary>
                _veloobject.DataSet = installDate.Value;
                /// <summary>
                /// Дата обслуживания.
                /// </summary>
                _veloobject.DataCheck = serviceDate.Value;



                return _veloobject;
            }
            set
            {
                _veloobject = value;
                if (_veloobject != null)
                {
                    /// <summary>
                    /// Тип велопарковки.
                    /// </summary>
                    textBox1.Text = VeloTypeStrings.GetString(_veloobject.VeloType);
                    /// <summary>
                    /// Вид велопарковки.
                    /// </summary>
                    textBox2.Text = VeloViewStrings.GetString(_veloobject.VeloView);
                    /// <summary>
                    /// Статус.
                    /// </summary>
                    textBox3.Text = VeloObjectStatusStrings.GetStatusName(_veloobject.VeloObjectStatus);
                    /// <summary>
                    /// Количество секций.
                    /// </summary>
                    sectionBox.Text = _veloobject.VeloSection.ToString();
                    /// <summary>
                    /// Длина.
                    /// </summary>
                    lengthBox.Text = _veloobject.VeloLength.ToString();
                    /// <summary>
                    /// Ширина.
                    /// </summary>
                    widthBox.Text = _veloobject.VeloWidth.ToString();
                    /// <summary>
                    /// Дата установки.
                    /// </summary>
                    installDate.Value = _veloobject.DataSet ?? DateTime.Now;
                    /// <summary>
                    /// Дата обслуживания.
                    /// </summary>
                    serviceDate.Value = _veloobject.DataCheck ?? DateTime.Now;

                }
            }
        }



        #endregion
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_veloobjectManager != null)
            {
                if (!_isEdit)
                {
                    _veloobjectManager.AddNew(VeloObject);
                }
                else
                {
                    _veloobjectManager.Edit(VeloObject);
                }
            }
            DialogResult = DialogResult.OK;
            CloseView();
        }

        private void tbField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

    }
}
