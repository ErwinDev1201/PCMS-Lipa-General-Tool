namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmTicketingTask
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
			Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
			Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
			this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
			this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
			this.pageNonIT = new Telerik.WinControls.UI.RadPageViewPage();
			this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.radTextBox2 = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
			this.radTextBoxControl1 = new Telerik.WinControls.UI.RadTextBoxControl();
			this.pageIT = new Telerik.WinControls.UI.RadPageViewPage();
			this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
			this.cmbAssignee = new Telerik.WinControls.UI.RadDropDownList();
			this.radDropDownList2 = new Telerik.WinControls.UI.RadDropDownList();
			this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
			((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
			this.radPageView1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radTextBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radTextBoxControl1)).BeginInit();
			this.pageIT.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbAssignee)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radDropDownList2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radGridView1
			// 
			this.radGridView1.Location = new System.Drawing.Point(12, 95);
			// 
			// 
			// 
			this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition3;
			this.radGridView1.Name = "radGridView1";
			this.radGridView1.Size = new System.Drawing.Size(768, 574);
			this.radGridView1.TabIndex = 0;
			this.radGridView1.ThemeName = "Crystal";
			// 
			// radLabel6
			// 
			this.radLabel6.Font = new System.Drawing.Font("Snap ITC", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel6.Location = new System.Drawing.Point(12, 27);
			this.radLabel6.Name = "radLabel6";
			this.radLabel6.Size = new System.Drawing.Size(315, 47);
			this.radLabel6.TabIndex = 13;
			this.radLabel6.Text = "PCMS HelpDesk";
			this.radLabel6.ThemeName = "Crystal";
			// 
			// radPageView1
			// 
			this.radPageView1.Controls.Add(this.pageIT);
			this.radPageView1.Controls.Add(this.pageNonIT);
			this.radPageView1.Location = new System.Drawing.Point(787, 95);
			this.radPageView1.Name = "radPageView1";
			this.radPageView1.SelectedPage = this.pageIT;
			this.radPageView1.Size = new System.Drawing.Size(429, 574);
			this.radPageView1.TabIndex = 14;
			this.radPageView1.ThemeName = "Crystal";
			// 
			// pageNonIT
			// 
			this.pageNonIT.ItemSize = new System.Drawing.SizeF(105F, 28F);
			this.pageNonIT.Location = new System.Drawing.Point(6, 33);
			this.pageNonIT.Name = "pageNonIT";
			this.pageNonIT.Size = new System.Drawing.Size(417, 534);
			this.pageNonIT.Text = "Non IT Related";
			// 
			// radTextBox1
			// 
			this.radTextBox1.Location = new System.Drawing.Point(113, 9);
			this.radTextBox1.Name = "radTextBox1";
			this.radTextBox1.Size = new System.Drawing.Size(61, 24);
			this.radTextBox1.TabIndex = 0;
			this.radTextBox1.ThemeName = "Crystal";
			// 
			// radLabel1
			// 
			this.radLabel1.Location = new System.Drawing.Point(3, 9);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(56, 20);
			this.radLabel1.TabIndex = 1;
			this.radLabel1.Text = "Task ID:";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// radTextBox2
			// 
			this.radTextBox2.Location = new System.Drawing.Point(113, 45);
			this.radTextBox2.Name = "radTextBox2";
			this.radTextBox2.Size = new System.Drawing.Size(290, 24);
			this.radTextBox2.TabIndex = 2;
			this.radTextBox2.ThemeName = "Crystal";
			// 
			// radLabel2
			// 
			this.radLabel2.Location = new System.Drawing.Point(3, 45);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(83, 20);
			this.radLabel2.TabIndex = 3;
			this.radLabel2.Text = "Task Name: ";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// radLabel3
			// 
			this.radLabel3.Location = new System.Drawing.Point(3, 89);
			this.radLabel3.Name = "radLabel3";
			this.radLabel3.Size = new System.Drawing.Size(116, 20);
			this.radLabel3.TabIndex = 5;
			this.radLabel3.Text = "Task Description: ";
			this.radLabel3.ThemeName = "Crystal";
			// 
			// radTextBoxControl1
			// 
			this.radTextBoxControl1.Location = new System.Drawing.Point(113, 86);
			this.radTextBoxControl1.Name = "radTextBoxControl1";
			this.radTextBoxControl1.Size = new System.Drawing.Size(290, 231);
			this.radTextBoxControl1.TabIndex = 6;
			this.radTextBoxControl1.ThemeName = "Crystal";
			// 
			// pageIT
			// 
			this.pageIT.Controls.Add(this.radDropDownList2);
			this.pageIT.Controls.Add(this.radLabel5);
			this.pageIT.Controls.Add(this.cmbAssignee);
			this.pageIT.Controls.Add(this.radLabel4);
			this.pageIT.Controls.Add(this.radTextBoxControl1);
			this.pageIT.Controls.Add(this.radLabel3);
			this.pageIT.Controls.Add(this.radLabel2);
			this.pageIT.Controls.Add(this.radTextBox2);
			this.pageIT.Controls.Add(this.radLabel1);
			this.pageIT.Controls.Add(this.radTextBox1);
			this.pageIT.ItemSize = new System.Drawing.SizeF(76F, 28F);
			this.pageIT.Location = new System.Drawing.Point(6, 33);
			this.pageIT.Name = "pageIT";
			this.pageIT.Size = new System.Drawing.Size(417, 534);
			this.pageIT.Text = "IT Related";
			// 
			// radLabel4
			// 
			this.radLabel4.Location = new System.Drawing.Point(3, 347);
			this.radLabel4.Name = "radLabel4";
			this.radLabel4.Size = new System.Drawing.Size(70, 20);
			this.radLabel4.TabIndex = 7;
			this.radLabel4.Text = "Assignee: ";
			this.radLabel4.ThemeName = "Crystal";
			// 
			// cmbAssignee
			// 
			this.cmbAssignee.DropDownAnimationEnabled = true;
			this.cmbAssignee.Location = new System.Drawing.Point(80, 347);
			this.cmbAssignee.Name = "cmbAssignee";
			this.cmbAssignee.Size = new System.Drawing.Size(136, 24);
			this.cmbAssignee.TabIndex = 8;
			this.cmbAssignee.Text = "radDropDownList1";
			this.cmbAssignee.ThemeName = "Crystal";
			// 
			// radDropDownList2
			// 
			this.radDropDownList2.DropDownAnimationEnabled = true;
			radListDataItem1.Text = "High";
			radListDataItem2.Text = "Normal";
			radListDataItem3.Text = "Low";
			this.radDropDownList2.Items.Add(radListDataItem1);
			this.radDropDownList2.Items.Add(radListDataItem2);
			this.radDropDownList2.Items.Add(radListDataItem3);
			this.radDropDownList2.Location = new System.Drawing.Point(310, 351);
			this.radDropDownList2.Name = "radDropDownList2";
			this.radDropDownList2.Size = new System.Drawing.Size(99, 24);
			this.radDropDownList2.TabIndex = 10;
			this.radDropDownList2.ThemeName = "Crystal";
			// 
			// radLabel5
			// 
			this.radLabel5.Location = new System.Drawing.Point(247, 353);
			this.radLabel5.Name = "radLabel5";
			this.radLabel5.Size = new System.Drawing.Size(57, 20);
			this.radLabel5.TabIndex = 9;
			this.radLabel5.Text = "Priority: ";
			this.radLabel5.ThemeName = "Crystal";
			// 
			// frmTicketingTask
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1228, 701);
			this.Controls.Add(this.radPageView1);
			this.Controls.Add(this.radLabel6);
			this.Controls.Add(this.radGridView1);
			this.Name = "frmTicketingTask";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmTicketingTask";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
			this.radPageView1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radTextBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radTextBoxControl1)).EndInit();
			this.pageIT.ResumeLayout(false);
			this.pageIT.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbAssignee)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radDropDownList2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadGridView radGridView1;
		private Telerik.WinControls.UI.RadLabel radLabel6;
		private Telerik.WinControls.UI.RadPageView radPageView1;
		public Telerik.WinControls.UI.RadPageViewPage pageNonIT;
		public Telerik.WinControls.UI.RadPageViewPage pageIT;
		private Telerik.WinControls.UI.RadDropDownList radDropDownList2;
		private Telerik.WinControls.UI.RadLabel radLabel5;
		private Telerik.WinControls.UI.RadDropDownList cmbAssignee;
		private Telerik.WinControls.UI.RadLabel radLabel4;
		private Telerik.WinControls.UI.RadTextBoxControl radTextBoxControl1;
		private Telerik.WinControls.UI.RadLabel radLabel3;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		private Telerik.WinControls.UI.RadTextBox radTextBox2;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadTextBox radTextBox1;
	}
}
