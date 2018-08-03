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
    public partial class VeloObjectEditView : BaseView, IVeloObjectEditView
    {


        public VeloObjectEditView(VeloObject veloobject,IVeloObjectManager soManager)
        {
            InitializeComponent();
            
            cB_Type.DataSource = Enum.GetValues(typeof(VeloType)).Cast<VeloType>()
                .Select(t => VeloTypeStrings.GetString(t)).ToList();
            
           /* cB_View.DataSource = Enum.GetValues(typeof(VeloView)).Cast<VeloView>()
                .Select(t => VeloViewStrings.GetString(t)).ToList(); */

           /* cB_Status.DataSource = Enum.GetValues(typeof(VeloObjectStatus)).Cast<VeloObjectStatus>()
                .Select(t => VeloObjectStatusStrings.GetStatusName(t)).ToList(); */


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
                _veloobject.VeloType = VeloTypeStrings.GetValue(cB_Type.SelectedItem as string);

                /// <summary>
                /// Вид велопарковки.
                /// </summary>
                //  _veloobject.VeloView = VeloViewStrings.GetValue(cB_View.SelectedItem as string);

                if (radioKratko.Checked)
                {
                    _veloobject.VeloView = VeloView.View1;
                }
                else if (radioDolgo.Checked)
                {
                    _veloobject.VeloView = VeloView.View2;
                }

                /// <summary>
                /// Статус.
                /// </summary>
                // _veloobject.VeloObjectStatus = VeloObjectStatusStrings.GetStatusByName(cB_Status.SelectedItem as string);


                if (radioButton1.Checked)
                {
                    _veloobject.VeloObjectStatus = VeloObjectStatus.Set;
                }
                if (radioButton2.Checked)
                {
                    _veloobject.VeloObjectStatus = VeloObjectStatus.Required;
                }
                if (radioButton3.Checked)
                {
                    _veloobject.VeloObjectStatus = VeloObjectStatus.Dismantle;
                }
                if (radioButton4.Checked)
                {
                    _veloobject.VeloObjectStatus = VeloObjectStatus.Mobile;
                }
                if (radioButton5.Checked)
                {
                    _veloobject.VeloObjectStatus = VeloObjectStatus.Celebrate;
                }


                /// <summary>
                /// Количество секций.
                /// </summary>
                int sectionField = 0;
                int.TryParse(sectionBox.Text, out sectionField);
                _veloobject.VeloSection = sectionField;

                /// <summary>
                /// Длина.
                /// </summary>
                float lengthField = 0;
                float.TryParse(lengthBox.Text, out lengthField);
                _veloobject.VeloLength = lengthField;

                /// <summary>
                /// Ширина.
                /// </summary>
                float widthField = 0;
                float.TryParse(widthBox.Text, out widthField);
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
                    cB_Type.Text = VeloTypeStrings.GetString(_veloobject.VeloType);
                    /// <summary>
                    /// Вид велопарковки.
                    /// </summary>
                    // cB_View.Text = VeloViewStrings.GetString(_veloobject.VeloView);

                    if (radioDolgo.Checked)
                    {
                        radioDolgo.Text = _veloobject.VeloObjectStatus.ToString();
                    }
                    else if (radioKratko.Checked)
                    {
                        radioKratko.Text = _veloobject.VeloObjectStatus.ToString();
                    }
                    /// <summary>
                    /// Статус.
                    /// </summary>
                    //cB_Status.SelectedItem = cB_Status.Items.OfType<string>().FirstOrDefault(x => x == _veloobject.StatusAsString);

                    if (radioButton1.Checked)
                    {
                        radioButton1.Text = _veloobject.VeloObjectStatus.ToString();
                    }
                    if (radioButton3.Checked)
                    {
                        radioButton3.Text = _veloobject.VeloObjectStatus.ToString();
                    }
                    if (radioButton2.Checked)
                    {
                        radioButton2.Text = _veloobject.VeloObjectStatus.ToString();
                    }
                    if (radioButton4.Checked)
                    {
                        radioButton4.Text = _veloobject.VeloObjectStatus.ToString();
                    }

                    if (radioButton5.Checked)
                    {
                        radioButton5.Text = _veloobject.VeloObjectStatus.ToString();
                    }

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
                    installDate.Value = _veloobject.DataSet??DateTime.Now;       
                    /// <summary>
                    /// Дата обслуживания.
                    /// </summary>
                    serviceDate.Value = _veloobject.DataCheck??DateTime.Now;

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            CloseView();
        }

        private void tbField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void lengthBox_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 50)
            {
                MessageBox.Show("Длина велопарковки не может быть больше 99");
                numericUpDown1.Value = 0;
            }
        }

        private void widthBox_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 50)
            {
                MessageBox.Show("Ширина велопарковки не может быть больше 99");
                numericUpDown1.Value = 0;
            }
        }
    }
}
