using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITS.Core.Spectrum.Domain;
using ITS.MapObjects.BaseMapObject.FilterControls;
using ITS.MapObjects.SpectrumMapObject.EventArgses;
using ITS.MapObjects.SpectrumMapObject.IPresenters;
using ITS.MapObjects.SpectrumMapObject.IViews;
using ITS.MapObjects.SpectrumMapObject.Presenters;

namespace ITS.MapObjects.SpectrumMapObject.Views
{
    public partial class VeloObjectSummaryView : Form, IVeloObjectSummaryView
    {
        #region Private Fields and Consts

        private const string FeatureObjectColumn = "FeatureObject";
        private const string IDColumn = "ID";
        private const string VeloTypeColumn = "VeloType";
        private const string VeloViewColumn = "VeloView";
        private const string VeloObjectStatusColumn = "VeloObjectStatus";
        private const string TypeAsStringColumn = "TypeAsString";
        private const string ViewAsStringColumn = "ViewAsString";
        private const string StatusAsStringColumn = "StatusAsString";
        private const string VeloSectionColumn = "VeloSection";
        private const string VeloLengthColumn = "VeloLength";
        private const string VeloWidthColumn = "VeloWidth";
        private const string DataSetColumn = "DataSet";
        private const string DataCheckColumn = "DataCheck";


        private static readonly Dictionary<string, string> SOColumnHeaders;
        private static readonly Dictionary<string, int> SOColumnWidths;
        private IEnumerable<VeloObject> _model;

        #endregion

        #region Constructors

        static VeloObjectSummaryView()
        {
            SOColumnHeaders = new Dictionary<string, string>();
            SOColumnWidths = new Dictionary<string, int>();

            //RRColumnHeaders[IDColumn] = "Идентификатор";
            SOColumnHeaders[TypeAsStringColumn] = "Тип велопарковки";
            SOColumnHeaders[ViewAsStringColumn] = "Вид велопарковки";
            SOColumnHeaders[StatusAsStringColumn] = "Статус";
            SOColumnHeaders[VeloSectionColumn] = "Количество секций";
            SOColumnHeaders[VeloLengthColumn] = "Длина, м";
            SOColumnHeaders[VeloWidthColumn] = "Ширина, м";
            SOColumnHeaders[DataSetColumn] = "Дата установки";
            SOColumnHeaders[DataCheckColumn] = "Дата обслуживания";
    }

        public VeloObjectSummaryView()
            : this(new VeloObjectSummaryPresenter())
        {

        }

        public VeloObjectSummaryView(IVeloObjectSummaryPresenter presenter)
        {
            InitializeComponent();
            InitVeloObjectListView(SOColumnHeaders, SOColumnWidths);
            presenter.Init(this);

        }

        #region Implementation of IVeloObjectFindView

        public IEnumerable<VeloObject> Model
        {
            get { return _model; }
            set
            {
                _model = value ?? new List<VeloObject>();
                AddItemsToVeloObjectList(_model);
                //labelCount.Text = value.Count().ToString();
            }
        }

        public ICollection<ITS.Core.Domain.Filters.Filter> VeloObjectFilters
        {
            get { return flowPanelAddedVeloObjectFilters.Controls.OfType<FilterContainer>().Select(fc => fc.GetFilter()).ToList(); }
        }

        public void FillVeloObjectFilters(IDictionary<ITS.Core.Domain.Filters.Filter, object> filterDictionary)
        {
            filterValueControlVeloObject.FillFilters(filterDictionary);
        }

        public event EventHandler<EventArgs> LoadVeloObject;

        public event EventHandler<VeloObjectEventArgs> ShowOnMap;

        public event EventHandler<VeloObjectEventArgs> EditVeloObject;

       

        public event EventHandler<EventArgs> ExportToWord;

        public void View()
        {
            ShowDialog();
        }
        #endregion


        #region Private Fields

        private void InitVeloObjectListView(Dictionary<string, string> columnHeaders,
            Dictionary<string, int> columnWidths)
        {
            dgVeloObjects.DataSource = new BindingList<VeloObject>();
            dgVeloObjects.Columns[VeloTypeColumn].Visible = false;
            dgVeloObjects.Columns[VeloViewColumn].Visible = false;
            dgVeloObjects.Columns[VeloObjectStatusColumn].Visible = false;
            dgVeloObjects.Columns[FeatureObjectColumn].Visible = false;
            dgVeloObjects.Columns[IDColumn].Visible = false;

            //dgRoadrepairs.Columns[IDColumn].HeaderText = columnHeaders[IDColumn];
            dgVeloObjects.Columns[TypeAsStringColumn].HeaderText = columnHeaders[TypeAsStringColumn];
            dgVeloObjects.Columns[ViewAsStringColumn].HeaderText = columnHeaders[ViewAsStringColumn];
            dgVeloObjects.Columns[StatusAsStringColumn].HeaderText = columnHeaders[StatusAsStringColumn];
            dgVeloObjects.Columns[VeloSectionColumn].HeaderText = columnHeaders[VeloSectionColumn];
            dgVeloObjects.Columns[VeloLengthColumn].HeaderText = columnHeaders[VeloLengthColumn];
            dgVeloObjects.Columns[VeloWidthColumn].HeaderText = columnHeaders[VeloWidthColumn];
            dgVeloObjects.Columns[DataSetColumn].HeaderText = columnHeaders[DataSetColumn];
            dgVeloObjects.Columns[DataCheckColumn].HeaderText = columnHeaders[DataCheckColumn];

        }
        private void AddItemsToVeloObjectList(IEnumerable<VeloObject> VeloObject)
        {
            dgVeloObjects.DataSource = VeloObject;
            if (VeloObject.Count() == 0)
                MessageBox.Show("Данные по критериям поиска не найдены!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработка отображения данных в таблице
        /// </summary>
        private void VeloObjectGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (dgVeloObjects.Columns[e.ColumnIndex].Name)
            {
                //case IDColumn:
                //{
                //    var id = ((RoadRepair)dgRoadrepairs.Rows[e.RowIndex].DataBoundItem).ID;
                //    e.Value = id.ToString();
                //    e.FormattingApplied = true;
                //}
                //    break;

                case VeloTypeColumn:
                    {
                        var VeloType = ((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem).VeloType;
                        e.Value = VeloType;
                        e.FormattingApplied = true;
                        break;
                    }

                case VeloViewColumn:
                    {
                        var VeloView = ((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem).VeloView;
                        e.Value = VeloView;
                        e.FormattingApplied = true;
                        break;
                    }

                case VeloObjectStatusColumn:
                    {
                        var VeloStatus = ((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem).VeloObjectStatus;
                        e.Value = VeloStatus.ToString();
                        e.FormattingApplied = true;
                        break;
                    }
                case VeloLengthColumn:
                    {
                        var VeloLength = ((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem).VeloLength;
                        e.Value = VeloLength.ToString();
                        e.FormattingApplied = true;
                        break;
                    }
                case VeloWidthColumn:
                    {
                        var VeloWidth = ((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem).VeloWidth;
                        e.Value = VeloWidth.ToString();
                        e.FormattingApplied = true;
                        break;
                    }

                case VeloSectionColumn:
                    {
                        var VeloSection = ((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem).VeloSection;
                        e.Value = VeloSection.ToString();
                        e.FormattingApplied = true;
                        break;
                    }
                case DataSetColumn:
                    {
                        var DataSet = ((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem).DataSet;
                        e.Value = DataSet.ToString();
                        e.FormattingApplied = true;
                        break;
                    }

                case DataCheckColumn:
                    {
                        var DataCheck = ((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem).DataCheck;
                        e.Value = DataCheck.ToString();
                        e.FormattingApplied = true;
                        break;
                    }


            }
        }
        private void DropFilterProperties()
        {
            filterValueControlVeloObject.AddFilterControls(flowPanelAddedVeloObjectFilters.Controls.OfType<FilterContainer>().Select(fc => fc.FilterControl));
            flowPanelAddedVeloObjectFilters.Controls.Clear();
        }
        #endregion

        #region EventHandlers

        private void ApplyFilter_ClickHandler(object sender, EventArgs e)
        {
            if (LoadVeloObject != null)
            {
                LoadVeloObject(this,EventArgs.Empty);
            }
        }

        private void DropFilter_ClickHandler(object sender, EventArgs e)
        {
            DropFilterProperties();
        }

        private void ExportWord_ClickHandler(object sender, EventArgs e)
        {
            if (ExportToWord != null)
            {
                ExportToWord(this, EventArgs.Empty);
            }
        }

        private void VeloObjectsGrid_CellContentClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            if (dgVeloObjects.Columns[e.ColumnIndex].Name == EditVeloObjectColumn.Name)
            {
                if (EditVeloObject != null)
                {
                    EditVeloObject(this, new VeloObjectEventArgs((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem));
                }
            }
            if (dgVeloObjects.Columns[e.ColumnIndex].Name == ShowOnMapColumn.Name)
            {
                if (ShowOnMap != null)
                {
                    ShowOnMap(this, new VeloObjectEventArgs((VeloObject)dgVeloObjects.Rows[e.RowIndex].DataBoundItem));
                }
            }
        }

        private void btnAddVeloObjectFilter_Click(object sender, EventArgs e)
        {
            var f = filterValueControlVeloObject.GetFilterControl();
            if (f != null)
            {
                var fc = new FilterContainer(f);
                flowPanelAddedVeloObjectFilters.Controls.Add(fc);
                fc.Delete += FcOnVeloObjectFilterDelete;
            }
        }

        private void FcOnVeloObjectFilterDelete(FilterContainer fc)
        {
            flowPanelAddedVeloObjectFilters.Controls.Remove(fc);
            filterValueControlVeloObject.AddFilterControl(fc.FilterControl);
        }

        #endregion

       

       #endregion
    }
}
