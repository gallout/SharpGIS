using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITS.MapObjects.SpectrumMapObject.IViews;

namespace ITS.MapObjects.SpectrumMapObject.Views
{
   public class BaseView : Form, IBaseView
    {
        #region Implementation of IView

        public DialogResult ShowViewDialog()
        {
            if (ViewShowing != null) ViewShowing();
            IsViewVisible = true;
            return ShowDialog();
        }

        public void ShowView()
        {
            if (ViewShowing != null) ViewShowing();
            IsViewVisible = true;
            Show();
        }

        public void CloseView()
        {
            IsViewVisible = false;
            if (ViewClosing != null) ViewClosing();
            Close();
        }

        public event Action ViewShowing;
        public event Action ViewClosing;

        public bool IsViewVisible { get; private set; }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseView
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseView";
            this.ResumeLayout(false);

        }
    }
}
