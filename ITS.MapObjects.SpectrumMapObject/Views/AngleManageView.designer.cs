namespace ITS.MapObjects.SpectrumMapObject.Views
{
    partial class AngleManageView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AngleManageView));
            this.label1 = new System.Windows.Forms.Label();
            this.bt180Degree = new System.Windows.Forms.Button();
            this.bt270Degree = new System.Windows.Forms.Button();
            this.bt90Degree = new System.Windows.Forms.Button();
            this.bt0Degree = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.angleControl = new ITS.GIS.UIControls.Controls.AngleCtrl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Повернуть на";
            // 
            // bt180Degree
            // 
            this.bt180Degree.Location = new System.Drawing.Point(121, 241);
            this.bt180Degree.Name = "bt180Degree";
            this.bt180Degree.Size = new System.Drawing.Size(33, 23);
            this.bt180Degree.TabIndex = 26;
            this.bt180Degree.Text = "180";
            this.bt180Degree.UseVisualStyleBackColor = true;
            this.bt180Degree.Click += new System.EventHandler(this.bt180Degree_Click);
            // 
            // bt270Degree
            // 
            this.bt270Degree.Location = new System.Drawing.Point(160, 241);
            this.bt270Degree.Name = "bt270Degree";
            this.bt270Degree.Size = new System.Drawing.Size(33, 23);
            this.bt270Degree.TabIndex = 25;
            this.bt270Degree.Text = "270";
            this.bt270Degree.UseVisualStyleBackColor = true;
            this.bt270Degree.Click += new System.EventHandler(this.bt270Degree_Click);
            // 
            // bt90Degree
            // 
            this.bt90Degree.Location = new System.Drawing.Point(82, 241);
            this.bt90Degree.Name = "bt90Degree";
            this.bt90Degree.Size = new System.Drawing.Size(33, 23);
            this.bt90Degree.TabIndex = 24;
            this.bt90Degree.Text = "90";
            this.bt90Degree.UseVisualStyleBackColor = true;
            this.bt90Degree.Click += new System.EventHandler(this.bt90Degree_Click);
            // 
            // bt0Degree
            // 
            this.bt0Degree.Location = new System.Drawing.Point(7, 215);
            this.bt0Degree.Name = "bt0Degree";
            this.bt0Degree.Size = new System.Drawing.Size(185, 23);
            this.bt0Degree.TabIndex = 23;
            this.bt0Degree.Text = "Установить в 0";
            this.bt0Degree.UseVisualStyleBackColor = true;
            this.bt0Degree.Click += new System.EventHandler(this.bt0Degree_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(117, 271);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(7, 271);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // angleControl
            // 
            this.angleControl.BackColor = System.Drawing.Color.White;
            this.angleControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.angleControl.CurrentAngle = 0D;
            this.angleControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.angleControl.Location = new System.Drawing.Point(0, 0);
            this.angleControl.Margin = new System.Windows.Forms.Padding(4);
            this.angleControl.Name = "angleControl";
            this.angleControl.Size = new System.Drawing.Size(208, 208);
            this.angleControl.TabIndex = 20;
            // 
            // AngleManageView
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(202, 300);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt180Degree);
            this.Controls.Add(this.bt270Degree);
            this.Controls.Add(this.bt90Degree);
            this.Controls.Add(this.bt0Degree);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.angleControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AngleManageView";
            this.Opacity = 0.8D;
            this.Text = "Выбор угла";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AngleManageView_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt180Degree;
        private System.Windows.Forms.Button bt270Degree;
        private System.Windows.Forms.Button bt90Degree;
        private System.Windows.Forms.Button bt0Degree;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private GIS.UIControls.Controls.AngleCtrl angleControl;

    }
}