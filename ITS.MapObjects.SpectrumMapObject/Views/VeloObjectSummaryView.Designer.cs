namespace ITS.MapObjects.SpectrumMapObject.Views
{
    partial class VeloObjectSummaryView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VeloObjectSummaryView));
            this.dgVeloObjects = new System.Windows.Forms.DataGridView();
            this.ShowOnMapColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.EditVeloObjectColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.tabControlFilter = new System.Windows.Forms.TabControl();
            this.tabPageVeloObject = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBoxVeloObjectFilter = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.filterValueControlVeloObject = new ITS.MapObjects.BaseMapObject.FilterControls.FilterValueControl();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnAddVeloObjectFilter = new System.Windows.Forms.Button();
            this.flowPanelAddedVeloObjectFilters = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgVeloObjects)).BeginInit();
            this.tabControlFilter.SuspendLayout();
            this.tabPageVeloObject.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBoxVeloObjectFilter.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgVeloObjects
            // 
            this.dgVeloObjects.AllowUserToAddRows = false;
            this.dgVeloObjects.AllowUserToDeleteRows = false;
            this.dgVeloObjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgVeloObjects.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgVeloObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVeloObjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShowOnMapColumn,
            this.EditVeloObjectColumn});
            this.dgVeloObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVeloObjects.Location = new System.Drawing.Point(0, 0);
            this.dgVeloObjects.Name = "dgVeloObjects";
            this.dgVeloObjects.ReadOnly = true;
            this.dgVeloObjects.RowHeadersVisible = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgVeloObjects.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgVeloObjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVeloObjects.Size = new System.Drawing.Size(759, 558);
            this.dgVeloObjects.TabIndex = 10;
            this.dgVeloObjects.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VeloObjectsGrid_CellContentClickHandler);
            this.dgVeloObjects.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.VeloObjectGridCellFormatting);
            // 
            // ShowOnMapColumn
            // 
            this.ShowOnMapColumn.FillWeight = 1F;
            this.ShowOnMapColumn.HeaderText = "";
            this.ShowOnMapColumn.Image = ((System.Drawing.Image)(resources.GetObject("ShowOnMapColumn.Image")));
            this.ShowOnMapColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ShowOnMapColumn.MinimumWidth = 20;
            this.ShowOnMapColumn.Name = "ShowOnMapColumn";
            this.ShowOnMapColumn.ReadOnly = true;
            this.ShowOnMapColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ShowOnMapColumn.ToolTipText = "Показать объект на карте";
            // 
            // EditVeloObjectColumn
            // 
            this.EditVeloObjectColumn.FillWeight = 1F;
            this.EditVeloObjectColumn.HeaderText = "";
            this.EditVeloObjectColumn.Image = ((System.Drawing.Image)(resources.GetObject("EditVeloObjectColumn.Image")));
            this.EditVeloObjectColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.EditVeloObjectColumn.MinimumWidth = 20;
            this.EditVeloObjectColumn.Name = "EditVeloObjectColumn";
            this.EditVeloObjectColumn.ReadOnly = true;
            this.EditVeloObjectColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EditVeloObjectColumn.ToolTipText = "Редактировать группировки";
            // 
            // tabControlFilter
            // 
            this.tabControlFilter.Controls.Add(this.tabPageVeloObject);
            this.tabControlFilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControlFilter.Location = new System.Drawing.Point(759, 0);
            this.tabControlFilter.Name = "tabControlFilter";
            this.tabControlFilter.SelectedIndex = 0;
            this.tabControlFilter.Size = new System.Drawing.Size(282, 558);
            this.tabControlFilter.TabIndex = 9;
            // 
            // tabPageVeloObject
            // 
            this.tabPageVeloObject.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageVeloObject.Controls.Add(this.panel2);
            this.tabPageVeloObject.Controls.Add(this.panel3);
            this.tabPageVeloObject.Location = new System.Drawing.Point(4, 22);
            this.tabPageVeloObject.Name = "tabPageVeloObject";
            this.tabPageVeloObject.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVeloObject.Size = new System.Drawing.Size(274, 532);
            this.tabPageVeloObject.TabIndex = 0;
            this.tabPageVeloObject.Text = "Велопарковка";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.Controls.Add(this.btnApplyFilter);
            this.panel2.Controls.Add(this.btnClearFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 490);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(268, 39);
            this.panel2.TabIndex = 10;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.AutoSize = true;
            this.btnExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExport.Location = new System.Drawing.Point(186, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 23);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "Сохранить";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.ExportWord_ClickHandler);
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApplyFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnApplyFilter.Location = new System.Drawing.Point(34, 8);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(70, 23);
            this.btnApplyFilter.TabIndex = 9;
            this.btnApplyFilter.Text = "Загрузить";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.ApplyFilter_ClickHandler);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearFilter.Location = new System.Drawing.Point(110, 8);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(70, 23);
            this.btnClearFilter.TabIndex = 8;
            this.btnClearFilter.Text = "Сбросить";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.DropFilter_ClickHandler);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.groupBoxVeloObjectFilter);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(268, 485);
            this.panel3.TabIndex = 1;
            // 
            // groupBoxVeloObjectFilter
            // 
            this.groupBoxVeloObjectFilter.AutoSize = true;
            this.groupBoxVeloObjectFilter.Controls.Add(this.flowLayoutPanel2);
            this.groupBoxVeloObjectFilter.Location = new System.Drawing.Point(3, 0);
            this.groupBoxVeloObjectFilter.Name = "groupBoxVeloObjectFilter";
            this.groupBoxVeloObjectFilter.Size = new System.Drawing.Size(261, 106);
            this.groupBoxVeloObjectFilter.TabIndex = 5;
            this.groupBoxVeloObjectFilter.TabStop = false;
            this.groupBoxVeloObjectFilter.Text = "Параметры фильтра";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.filterValueControlVeloObject);
            this.flowLayoutPanel2.Controls.Add(this.panel6);
            this.flowLayoutPanel2.Controls.Add(this.flowPanelAddedVeloObjectFilters);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(255, 87);
            this.flowLayoutPanel2.TabIndex = 156;
            // 
            // filterValueControlVeloObject
            // 
            this.filterValueControlVeloObject.AutoSize = true;
            this.filterValueControlVeloObject.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterValueControlVeloObject.Location = new System.Drawing.Point(3, 3);
            this.filterValueControlVeloObject.MinimumSize = new System.Drawing.Size(247, 44);
            this.filterValueControlVeloObject.Name = "filterValueControlVeloObject";
            this.filterValueControlVeloObject.Size = new System.Drawing.Size(247, 44);
            this.filterValueControlVeloObject.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnAddVeloObjectFilter);
            this.panel6.Location = new System.Drawing.Point(3, 53);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(164, 31);
            this.panel6.TabIndex = 152;
            // 
            // btnAddVeloObjectFilter
            // 
            this.btnAddVeloObjectFilter.Location = new System.Drawing.Point(3, 3);
            this.btnAddVeloObjectFilter.Name = "btnAddVeloObjectFilter";
            this.btnAddVeloObjectFilter.Size = new System.Drawing.Size(75, 23);
            this.btnAddVeloObjectFilter.TabIndex = 150;
            this.btnAddVeloObjectFilter.Text = "Добавить";
            this.btnAddVeloObjectFilter.UseVisualStyleBackColor = true;
            this.btnAddVeloObjectFilter.Click += new System.EventHandler(this.btnAddVeloObjectFilter_Click);
            // 
            // flowPanelAddedVeloObjectFilters
            // 
            this.flowPanelAddedVeloObjectFilters.AutoSize = true;
            this.flowPanelAddedVeloObjectFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelAddedVeloObjectFilters.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelAddedVeloObjectFilters.Location = new System.Drawing.Point(0, 87);
            this.flowPanelAddedVeloObjectFilters.Margin = new System.Windows.Forms.Padding(0);
            this.flowPanelAddedVeloObjectFilters.Name = "flowPanelAddedVeloObjectFilters";
            this.flowPanelAddedVeloObjectFilters.Size = new System.Drawing.Size(253, 0);
            this.flowPanelAddedVeloObjectFilters.TabIndex = 154;
            // 
            // VeloObjectSummaryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 558);
            this.Controls.Add(this.dgVeloObjects);
            this.Controls.Add(this.tabControlFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VeloObjectSummaryView";
            this.Text = "Сводная ведомость";
            ((System.ComponentModel.ISupportInitialize)(this.dgVeloObjects)).EndInit();
            this.tabControlFilter.ResumeLayout(false);
            this.tabPageVeloObject.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBoxVeloObjectFilter.ResumeLayout(false);
            this.groupBoxVeloObjectFilter.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgVeloObjects;
        private System.Windows.Forms.DataGridViewImageColumn ShowOnMapColumn;
        private System.Windows.Forms.DataGridViewImageColumn EditVeloObjectColumn;
        private System.Windows.Forms.TabControl tabControlFilter;
        private System.Windows.Forms.TabPage tabPageVeloObject;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBoxVeloObjectFilter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private BaseMapObject.FilterControls.FilterValueControl filterValueControlVeloObject;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnAddVeloObjectFilter;
        private System.Windows.Forms.FlowLayoutPanel flowPanelAddedVeloObjectFilters;
    }
}