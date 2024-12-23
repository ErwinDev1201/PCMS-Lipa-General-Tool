namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmManageproduct
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
			Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageproduct));
			this.paneltable = new Telerik.WinControls.UI.RadPanel();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.lblSearchCount = new Telerik.WinControls.UI.RadLabel();
			this.dgPantryProduct = new Telerik.WinControls.UI.RadGridView();
			this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
			this.txtIntID = new Telerik.WinControls.UI.RadTextBox();
			this.btnCancel = new Telerik.WinControls.UI.RadButton();
			this.btnDelete = new Telerik.WinControls.UI.RadButton();
			this.btnSave = new Telerik.WinControls.UI.RadButton();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.txtRemarks = new Telerik.WinControls.UI.RadTextBoxControl();
			this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
			this.txtPrice = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.txtProductName = new Telerik.WinControls.UI.RadTextBox();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			((System.ComponentModel.ISupportInitialize)(this.paneltable)).BeginInit();
			this.paneltable.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgPantryProduct)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgPantryProduct.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
			this.radPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtIntID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPrice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtProductName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// paneltable
			// 
			this.paneltable.Controls.Add(this.txtSearch);
			this.paneltable.Controls.Add(this.radLabel1);
			this.paneltable.Controls.Add(this.lblSearchCount);
			this.paneltable.Controls.Add(this.dgPantryProduct);
			this.paneltable.Location = new System.Drawing.Point(12, 12);
			this.paneltable.Name = "paneltable";
			this.paneltable.Size = new System.Drawing.Size(511, 441);
			this.paneltable.TabIndex = 0;
			this.paneltable.ThemeName = "Crystal";
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(112, 18);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.ShowClearButton = true;
			this.txtSearch.Size = new System.Drawing.Size(249, 23);
			this.txtSearch.TabIndex = 9;
			this.txtSearch.ThemeName = "Crystal";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(20, 21);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(84, 19);
			this.radLabel1.TabIndex = 8;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// lblSearchCount
			// 
			this.lblSearchCount.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSearchCount.Location = new System.Drawing.Point(18, 409);
			this.lblSearchCount.Name = "lblSearchCount";
			this.lblSearchCount.Size = new System.Drawing.Size(66, 19);
			this.lblSearchCount.TabIndex = 3;
			this.lblSearchCount.Text = "radLabel5";
			this.lblSearchCount.ThemeName = "Crystal";
			// 
			// dgPantryProduct
			// 
			this.dgPantryProduct.AllowShowFocusCues = true;
			this.dgPantryProduct.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgPantryProduct.Location = new System.Drawing.Point(18, 48);
			// 
			// 
			// 
			this.dgPantryProduct.MasterTemplate.AllowAddNewRow = false;
			this.dgPantryProduct.MasterTemplate.AllowCellContextMenu = false;
			this.dgPantryProduct.MasterTemplate.AllowColumnChooser = false;
			this.dgPantryProduct.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgPantryProduct.MasterTemplate.AllowColumnReorder = false;
			this.dgPantryProduct.MasterTemplate.AllowColumnResize = false;
			this.dgPantryProduct.MasterTemplate.AllowDeleteRow = false;
			this.dgPantryProduct.MasterTemplate.AllowDragToGroup = false;
			this.dgPantryProduct.MasterTemplate.AllowEditRow = false;
			this.dgPantryProduct.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgPantryProduct.MasterTemplate.AllowRowResize = false;
			this.dgPantryProduct.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgPantryProduct.Name = "dgPantryProduct";
			this.dgPantryProduct.Size = new System.Drawing.Size(475, 354);
			this.dgPantryProduct.TabIndex = 2;
			this.dgPantryProduct.ThemeName = "Crystal";
			this.dgPantryProduct.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgPantryProduct_MouseDoubleClick);
			// 
			// radPanel2
			// 
			this.radPanel2.Controls.Add(this.txtIntID);
			this.radPanel2.Controls.Add(this.btnCancel);
			this.radPanel2.Controls.Add(this.btnDelete);
			this.radPanel2.Controls.Add(this.btnSave);
			this.radPanel2.Controls.Add(this.btnNew);
			this.radPanel2.Controls.Add(this.txtRemarks);
			this.radPanel2.Controls.Add(this.radLabel4);
			this.radPanel2.Controls.Add(this.radLabel3);
			this.radPanel2.Controls.Add(this.txtPrice);
			this.radPanel2.Controls.Add(this.radLabel2);
			this.radPanel2.Controls.Add(this.txtProductName);
			this.radPanel2.Location = new System.Drawing.Point(542, 12);
			this.radPanel2.Name = "radPanel2";
			this.radPanel2.Size = new System.Drawing.Size(331, 441);
			this.radPanel2.TabIndex = 1;
			this.radPanel2.ThemeName = "Crystal";
			// 
			// txtIntID
			// 
			this.txtIntID.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtIntID.Location = new System.Drawing.Point(24, 112);
			this.txtIntID.Name = "txtIntID";
			this.txtIntID.Size = new System.Drawing.Size(84, 23);
			this.txtIntID.TabIndex = 3;
			this.txtIntID.ThemeName = "Crystal";
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(24, 363);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(285, 54);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.ThemeName = "Crystal";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.Location = new System.Drawing.Point(164, 288);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(145, 54);
			this.btnDelete.TabIndex = 4;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.ThemeName = "Crystal";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Location = new System.Drawing.Point(24, 288);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(123, 54);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "&Save";
			this.btnSave.ThemeName = "Crystal";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnNew
			// 
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Location = new System.Drawing.Point(24, 18);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(285, 54);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&New";
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// txtRemarks
			// 
			this.txtRemarks.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRemarks.Location = new System.Drawing.Point(152, 181);
			this.txtRemarks.Multiline = true;
			this.txtRemarks.Name = "txtRemarks";
			this.txtRemarks.Size = new System.Drawing.Size(157, 89);
			this.txtRemarks.TabIndex = 7;
			this.txtRemarks.ThemeName = "Crystal";
			// 
			// radLabel4
			// 
			this.radLabel4.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel4.Location = new System.Drawing.Point(152, 156);
			this.radLabel4.Name = "radLabel4";
			this.radLabel4.Size = new System.Drawing.Size(66, 19);
			this.radLabel4.TabIndex = 6;
			this.radLabel4.Text = "Remarks: ";
			this.radLabel4.ThemeName = "Crystal";
			// 
			// radLabel3
			// 
			this.radLabel3.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel3.Location = new System.Drawing.Point(24, 156);
			this.radLabel3.Name = "radLabel3";
			this.radLabel3.Size = new System.Drawing.Size(44, 19);
			this.radLabel3.TabIndex = 5;
			this.radLabel3.Text = "Price: ";
			this.radLabel3.ThemeName = "Crystal";
			// 
			// txtPrice
			// 
			this.txtPrice.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPrice.Location = new System.Drawing.Point(24, 180);
			this.txtPrice.Name = "txtPrice";
			this.txtPrice.Size = new System.Drawing.Size(98, 23);
			this.txtPrice.TabIndex = 4;
			this.txtPrice.ThemeName = "Crystal";
			this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(24, 88);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(96, 19);
			this.radLabel2.TabIndex = 3;
			this.radLabel2.Text = "Product Name:";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// txtProductName
			// 
			this.txtProductName.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtProductName.Location = new System.Drawing.Point(114, 112);
			this.txtProductName.Name = "txtProductName";
			this.txtProductName.Size = new System.Drawing.Size(195, 23);
			this.txtProductName.TabIndex = 2;
			this.txtProductName.ThemeName = "Crystal";
			this.txtProductName.TextChanged += new System.EventHandler(this.txtProductName_TextChanged);
			// 
			// frmManageproduct
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(885, 465);
			this.Controls.Add(this.radPanel2);
			this.Controls.Add(this.paneltable);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmManageproduct";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmManageproduct";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmManageproduct_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmManageproduct_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.paneltable)).EndInit();
			this.paneltable.ResumeLayout(false);
			this.paneltable.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgPantryProduct.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgPantryProduct)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
			this.radPanel2.ResumeLayout(false);
			this.radPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtIntID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPrice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtProductName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.UI.RadPanel paneltable;
		private Telerik.WinControls.UI.RadPanel radPanel2;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadGridView dgPantryProduct;
		private Telerik.WinControls.UI.RadTextBoxControl txtRemarks;
		private Telerik.WinControls.UI.RadLabel radLabel4;
		private Telerik.WinControls.UI.RadLabel radLabel3;
		private Telerik.WinControls.UI.RadTextBox txtPrice;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		private Telerik.WinControls.UI.RadTextBox txtProductName;
		private Telerik.WinControls.UI.RadButton btnSave;
		private Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadButton btnDelete;
		private Telerik.WinControls.UI.RadButton btnCancel;
		private Telerik.WinControls.UI.RadTextBox txtIntID;
		private Telerik.WinControls.UI.RadLabel lblSearchCount;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel1;
	}
}
