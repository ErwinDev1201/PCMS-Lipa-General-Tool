namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmITTask
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
			Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.crystalTheme2 = new Telerik.WinControls.Themes.CrystalTheme();
			this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
			this.btnNewTicket = new Telerik.WinControls.UI.RadButton();
			this.radLabel12 = new Telerik.WinControls.UI.RadLabel();
			this.cmbEmployeeStat = new Telerik.WinControls.UI.RadDropDownList();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
			this.lblSearchCount = new Telerik.WinControls.UI.RadLabel();
			this.dgTask = new Telerik.WinControls.UI.RadGridView();
			((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
			this.radGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnNewTicket)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel12)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbEmployeeStat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgTask)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgTask.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radGroupBox1
			// 
			this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this.radGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radGroupBox1.Controls.Add(this.btnNewTicket);
			this.radGroupBox1.Controls.Add(this.radLabel12);
			this.radGroupBox1.Controls.Add(this.cmbEmployeeStat);
			this.radGroupBox1.Controls.Add(this.txtSearch);
			this.radGroupBox1.Controls.Add(this.radLabel11);
			this.radGroupBox1.Controls.Add(this.lblSearchCount);
			this.radGroupBox1.Controls.Add(this.dgTask);
			this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
			this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(1);
			this.radGroupBox1.HeaderText = "Task Detail";
			this.radGroupBox1.Location = new System.Drawing.Point(12, 20);
			this.radGroupBox1.Name = "radGroupBox1";
			this.radGroupBox1.Size = new System.Drawing.Size(875, 557);
			this.radGroupBox1.TabIndex = 13;
			this.radGroupBox1.Text = "Task Detail";
			this.radGroupBox1.ThemeName = "Crystal";
			// 
			// btnNewTicket
			// 
			this.btnNewTicket.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNewTicket.Location = new System.Drawing.Point(726, 52);
			this.btnNewTicket.Name = "btnNewTicket";
			this.btnNewTicket.Size = new System.Drawing.Size(131, 39);
			this.btnNewTicket.TabIndex = 12;
			this.btnNewTicket.Text = "&Assign New Task";
			this.btnNewTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNewTicket.ThemeName = "Crystal";
			this.btnNewTicket.Click += new System.EventHandler(this.btnNewTicket_Click);
			// 
			// radLabel12
			// 
			this.radLabel12.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel12.Location = new System.Drawing.Point(24, 69);
			this.radLabel12.Name = "radLabel12";
			this.radLabel12.Size = new System.Drawing.Size(87, 19);
			this.radLabel12.TabIndex = 11;
			this.radLabel12.Text = "Task Status:  ";
			this.radLabel12.ThemeName = "Crystal";
			// 
			// cmbEmployeeStat
			// 
			this.cmbEmployeeStat.DropDownAnimationEnabled = true;
			this.cmbEmployeeStat.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			radListDataItem1.Text = "To Do";
			radListDataItem2.Text = "In Progress";
			radListDataItem3.Text = "Done";
			radListDataItem4.Text = "Pending";
			this.cmbEmployeeStat.Items.Add(radListDataItem1);
			this.cmbEmployeeStat.Items.Add(radListDataItem2);
			this.cmbEmployeeStat.Items.Add(radListDataItem3);
			this.cmbEmployeeStat.Items.Add(radListDataItem4);
			this.cmbEmployeeStat.Location = new System.Drawing.Point(116, 67);
			this.cmbEmployeeStat.Name = "cmbEmployeeStat";
			this.cmbEmployeeStat.Size = new System.Drawing.Size(181, 24);
			this.cmbEmployeeStat.TabIndex = 10;
			this.cmbEmployeeStat.ThemeName = "Crystal";
			this.cmbEmployeeStat.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbEmployeeStat_SelectedIndexChanged);
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(116, 38);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.ShowClearButton = true;
			this.txtSearch.Size = new System.Drawing.Size(249, 23);
			this.txtSearch.TabIndex = 9;
			this.txtSearch.ThemeName = "Crystal";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// radLabel11
			// 
			this.radLabel11.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel11.Location = new System.Drawing.Point(24, 41);
			this.radLabel11.Name = "radLabel11";
			this.radLabel11.Size = new System.Drawing.Size(84, 19);
			this.radLabel11.TabIndex = 8;
			this.radLabel11.Text = "Search here: ";
			this.radLabel11.ThemeName = "Crystal";
			// 
			// lblSearchCount
			// 
			this.lblSearchCount.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSearchCount.Location = new System.Drawing.Point(24, 533);
			this.lblSearchCount.Name = "lblSearchCount";
			this.lblSearchCount.Size = new System.Drawing.Size(74, 19);
			this.lblSearchCount.TabIndex = 9;
			this.lblSearchCount.Text = "radLabel12";
			this.lblSearchCount.ThemeName = "Crystal";
			// 
			// dgTask
			// 
			this.dgTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgTask.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgTask.Location = new System.Drawing.Point(24, 97);
			// 
			// 
			// 
			this.dgTask.MasterTemplate.AllowAddNewRow = false;
			this.dgTask.MasterTemplate.AllowCellContextMenu = false;
			this.dgTask.MasterTemplate.AllowColumnChooser = false;
			this.dgTask.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgTask.MasterTemplate.AllowColumnReorder = false;
			this.dgTask.MasterTemplate.AllowColumnResize = false;
			this.dgTask.MasterTemplate.AllowDeleteRow = false;
			this.dgTask.MasterTemplate.AllowDragToGroup = false;
			this.dgTask.MasterTemplate.AllowEditRow = false;
			this.dgTask.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgTask.MasterTemplate.AllowRowResize = false;
			this.dgTask.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgTask.Name = "dgTask";
			this.dgTask.Size = new System.Drawing.Size(833, 430);
			this.dgTask.TabIndex = 1;
			this.dgTask.ThemeName = "Crystal";
			this.dgTask.TitleText = "Tasks";
			this.dgTask.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgTask_MouseDoubleClick);
			// 
			// frmITTask
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 596);
			this.Controls.Add(this.radGroupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmITTask";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmITTask";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmITTask_Load);
			((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
			this.radGroupBox1.ResumeLayout(false);
			this.radGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnNewTicket)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel12)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbEmployeeStat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgTask.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgTask)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme2;
		private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
		public Telerik.WinControls.UI.RadButton btnNewTicket;
		private Telerik.WinControls.UI.RadLabel radLabel12;
		private Telerik.WinControls.UI.RadDropDownList cmbEmployeeStat;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel11;
		private Telerik.WinControls.UI.RadLabel lblSearchCount;
		public Telerik.WinControls.UI.RadGridView dgTask;
	}
}
