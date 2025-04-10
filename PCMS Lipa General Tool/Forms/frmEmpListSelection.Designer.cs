namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmEmpListSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpListSelection));
            this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnLoadEmp = new Telerik.WinControls.UI.RadButton();
            this.chkLoadmyData = new Telerik.WinControls.UI.RadCheckBox();
            this.cmbEmployeeName = new Telerik.WinControls.UI.RadDropDownList();
            this.lblError = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLoadEmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLoadmyData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEmployeeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(12, 12);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(108, 19);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Select Employee:";
            this.radLabel1.ThemeName = "Crystal";
            // 
            // btnLoadEmp
            // 
            this.btnLoadEmp.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadEmp.Location = new System.Drawing.Point(12, 67);
            this.btnLoadEmp.Name = "btnLoadEmp";
            this.btnLoadEmp.Size = new System.Drawing.Size(160, 28);
            this.btnLoadEmp.TabIndex = 9;
            this.btnLoadEmp.Text = "Load Collector Notes";
            this.btnLoadEmp.ThemeName = "Crystal";
            this.btnLoadEmp.Click += new System.EventHandler(this.btnLoadEmp_Click);
            // 
            // chkLoadmyData
            // 
            this.chkLoadmyData.Location = new System.Drawing.Point(247, 44);
            this.chkLoadmyData.Name = "chkLoadmyData";
            this.chkLoadmyData.Size = new System.Drawing.Size(108, 18);
            this.chkLoadmyData.TabIndex = 10;
            this.chkLoadmyData.Text = "Load my Data";
            this.chkLoadmyData.ThemeName = "Crystal";
            this.chkLoadmyData.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkLoadmyData_ToggleStateChanged);
            // 
            // cmbEmployeeName
            // 
            this.cmbEmployeeName.Location = new System.Drawing.Point(12, 38);
            this.cmbEmployeeName.Name = "cmbEmployeeName";
            this.cmbEmployeeName.Size = new System.Drawing.Size(229, 24);
            this.cmbEmployeeName.TabIndex = 11;
            this.cmbEmployeeName.ThemeName = "Crystal";
            this.cmbEmployeeName.PopupOpened += new System.EventHandler(this.cmbEmployeeName_PopupOpened);
            // 
            // lblError
            // 
            this.lblError.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(12, 111);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(88, 19);
            this.lblError.TabIndex = 12;
            this.lblError.Text = "Status: Ready";
            this.lblError.ThemeName = "Crystal";
            // 
            // frmEmpListSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 128);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.cmbEmployeeName);
            this.Controls.Add(this.chkLoadmyData);
            this.Controls.Add(this.btnLoadEmp);
            this.Controls.Add(this.radLabel1);
            this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmpListSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDBUtility";
            this.ThemeName = "Crystal";
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLoadEmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLoadmyData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEmployeeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadButton btnLoadEmp;
        public Telerik.WinControls.UI.RadCheckBox chkLoadmyData;
        public Telerik.WinControls.UI.RadDropDownList cmbEmployeeName;
        private Telerik.WinControls.UI.RadLabel lblError;
    }
}
