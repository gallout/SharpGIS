namespace ITS.MapObjects.SpectrumMapObject.Views
{
    partial class VeloObjectInfoView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VeloObjectInfoView));
            this.bindingType = new System.Windows.Forms.BindingSource(this.components);
            this.tabControlRoadRepair = new System.Windows.Forms.TabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.btnOK = new System.Windows.Forms.Button();
            this.orgBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serviceDate = new System.Windows.Forms.DateTimePicker();
            this.installDate = new System.Windows.Forms.DateTimePicker();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.lengthBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sectionBox = new System.Windows.Forms.TextBox();
            this.labelField = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.bindingType)).BeginInit();
            this.tabControlRoadRepair.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingType
            // 
            this.bindingType.DataSource = typeof(ITS.Core.Spectrum.Domain.Enums.VeloTypeStrings);
            // 
            // tabControlRoadRepair
            // 
            this.tabControlRoadRepair.Controls.Add(this.tabPageInfo);
            this.tabControlRoadRepair.Controls.Add(this.tabPage2);
            this.tabControlRoadRepair.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlRoadRepair.Location = new System.Drawing.Point(0, 0);
            this.tabControlRoadRepair.Name = "tabControlRoadRepair";
            this.tabControlRoadRepair.SelectedIndex = 0;
            this.tabControlRoadRepair.Size = new System.Drawing.Size(402, 378);
            this.tabControlRoadRepair.TabIndex = 5;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.btnOK);
            this.tabPageInfo.Controls.Add(this.orgBox);
            this.tabPageInfo.Controls.Add(this.label7);
            this.tabPageInfo.Controls.Add(this.label4);
            this.tabPageInfo.Controls.Add(this.label3);
            this.tabPageInfo.Controls.Add(this.serviceDate);
            this.tabPageInfo.Controls.Add(this.installDate);
            this.tabPageInfo.Controls.Add(this.textBox3);
            this.tabPageInfo.Controls.Add(this.textBox2);
            this.tabPageInfo.Controls.Add(this.textBox1);
            this.tabPageInfo.Controls.Add(this.widthBox);
            this.tabPageInfo.Controls.Add(this.lengthBox);
            this.tabPageInfo.Controls.Add(this.label6);
            this.tabPageInfo.Controls.Add(this.label5);
            this.tabPageInfo.Controls.Add(this.label2);
            this.tabPageInfo.Controls.Add(this.label1);
            this.tabPageInfo.Controls.Add(this.sectionBox);
            this.tabPageInfo.Controls.Add(this.labelField);
            this.tabPageInfo.Controls.Add(this.labelType);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(394, 352);
            this.tabPageInfo.TabIndex = 0;
            this.tabPageInfo.Text = "Информация";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(297, 320);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 22);
            this.btnOK.TabIndex = 45;
            this.btnOK.Text = "Закрыть";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // orgBox
            // 
            this.orgBox.Location = new System.Drawing.Point(244, 283);
            this.orgBox.Name = "orgBox";
            this.orgBox.ReadOnly = true;
            this.orgBox.Size = new System.Drawing.Size(127, 20);
            this.orgBox.TabIndex = 64;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label7.Location = new System.Drawing.Point(19, 283);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 16);
            this.label7.TabIndex = 63;
            this.label7.Text = "Организация-владелец";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label4.Location = new System.Drawing.Point(19, 246);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 16);
            this.label4.TabIndex = 62;
            this.label4.Text = "Дата обслуживания";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.Location = new System.Drawing.Point(19, 211);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 61;
            this.label3.Text = "Дата установки";
            // 
            // serviceDate
            // 
            this.serviceDate.Enabled = false;
            this.serviceDate.Location = new System.Drawing.Point(244, 242);
            this.serviceDate.Name = "serviceDate";
            this.serviceDate.Size = new System.Drawing.Size(127, 20);
            this.serviceDate.TabIndex = 60;
            this.serviceDate.Value = new System.DateTime(2018, 1, 1, 10, 24, 0, 0);
            // 
            // installDate
            // 
            this.installDate.Enabled = false;
            this.installDate.Location = new System.Drawing.Point(244, 207);
            this.installDate.Name = "installDate";
            this.installDate.Size = new System.Drawing.Size(127, 20);
            this.installDate.TabIndex = 59;
            this.installDate.Value = new System.DateTime(2018, 1, 1, 10, 19, 0, 0);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(242, 86);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(127, 20);
            this.textBox3.TabIndex = 53;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(242, 57);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(127, 20);
            this.textBox2.TabIndex = 52;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(242, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 51;
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(242, 147);
            this.widthBox.Name = "widthBox";
            this.widthBox.ReadOnly = true;
            this.widthBox.Size = new System.Drawing.Size(127, 20);
            this.widthBox.TabIndex = 50;
            // 
            // lengthBox
            // 
            this.lengthBox.Location = new System.Drawing.Point(242, 121);
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.ReadOnly = true;
            this.lengthBox.Size = new System.Drawing.Size(127, 20);
            this.lengthBox.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label6.Location = new System.Drawing.Point(19, 122);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 16);
            this.label6.TabIndex = 48;
            this.label6.Text = "Длина велопарковки, м";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label5.Location = new System.Drawing.Point(19, 148);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 16);
            this.label5.TabIndex = 47;
            this.label5.Text = "Ширина велопарковки, м";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.Location = new System.Drawing.Point(19, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 16);
            this.label2.TabIndex = 41;
            this.label2.Text = "Вид велопарковки";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(19, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 40;
            this.label1.Text = "Статус объекта";
            // 
            // sectionBox
            // 
            this.sectionBox.Location = new System.Drawing.Point(242, 174);
            this.sectionBox.Name = "sectionBox";
            this.sectionBox.ReadOnly = true;
            this.sectionBox.Size = new System.Drawing.Size(127, 20);
            this.sectionBox.TabIndex = 5;
            // 
            // labelField
            // 
            this.labelField.AutoSize = true;
            this.labelField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelField.Location = new System.Drawing.Point(19, 175);
            this.labelField.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelField.Name = "labelField";
            this.labelField.Size = new System.Drawing.Size(135, 16);
            this.labelField.TabIndex = 38;
            this.labelField.Text = "Количество секций";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelType.Location = new System.Drawing.Point(19, 28);
            this.labelType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(130, 16);
            this.labelType.TabIndex = 25;
            this.labelType.Text = "Тип велопарковки";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(394, 352);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Фотография";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // VeloObjectInfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 376);
            this.Controls.Add(this.tabControlRoadRepair);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VeloObjectInfoView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Информация о велопарковке";
            ((System.ComponentModel.ISupportInitialize)(this.bindingType)).EndInit();
            this.tabControlRoadRepair.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingType;
        private System.Windows.Forms.TabControl tabControlRoadRepair;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelField;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.TextBox lengthBox;
        private System.Windows.Forms.TextBox sectionBox;
        private System.Windows.Forms.TextBox orgBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker serviceDate;
        private System.Windows.Forms.DateTimePicker installDate;
        private System.Windows.Forms.Button btnOK;
    }
}