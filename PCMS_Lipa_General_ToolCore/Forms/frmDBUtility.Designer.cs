namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmDBUtility
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBUtility));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.cmbTable = new Telerik.WinControls.UI.RadDropDownList();
			this.btnChangeSeq = new Telerik.WinControls.UI.RadButton();
			this.txtSequence = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
			this.lblcurrentVal = new Telerik.WinControls.UI.RadLabel();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbTable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnChangeSeq)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSequence)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblcurrentVal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(13, 27);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(92, 19);
			this.radLabel1.TabIndex = 0;
			this.radLabel1.Text = "Database List:";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// cmbTable
			// 
			this.cmbTable.DropDownAnimationEnabled = true;
			this.cmbTable.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTable.Location = new System.Drawing.Point(159, 23);
			this.cmbTable.Name = "cmbTable";
			this.cmbTable.Size = new System.Drawing.Size(217, 24);
			this.cmbTable.TabIndex = 1;
			this.cmbTable.ThemeName = "Crystal";
			this.cmbTable.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbTable_SelectedIndexChanged);
			// 
			// btnChangeSeq
			// 
			this.btnChangeSeq.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnChangeSeq.Location = new System.Drawing.Point(241, 87);
			this.btnChangeSeq.Name = "btnChangeSeq";
			this.btnChangeSeq.Size = new System.Drawing.Size(135, 39);
			this.btnChangeSeq.TabIndex = 2;
			this.btnChangeSeq.Text = "Change Sequence";
			this.btnChangeSeq.ThemeName = "Crystal";
			this.btnChangeSeq.Click += new System.EventHandler(this.btnChangeSeq_Click);
			// 
			// txtSequence
			// 
			this.txtSequence.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSequence.Location = new System.Drawing.Point(159, 92);
			this.txtSequence.Name = "txtSequence";
			this.txtSequence.Size = new System.Drawing.Size(68, 23);
			this.txtSequence.TabIndex = 3;
			this.txtSequence.ThemeName = "Crystal";
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(13, 93);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(121, 19);
			this.radLabel2.TabIndex = 4;
			this.radLabel2.Text = "Start Sequence No:";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// radLabel3
			// 
			this.radLabel3.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel3.Location = new System.Drawing.Point(13, 61);
			this.radLabel3.Name = "radLabel3";
			this.radLabel3.Size = new System.Drawing.Size(140, 19);
			this.radLabel3.TabIndex = 5;
			this.radLabel3.Text = "Current Sequence No: ";
			this.radLabel3.ThemeName = "Crystal";
			// 
			// lblcurrentVal
			// 
			this.lblcurrentVal.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblcurrentVal.Location = new System.Drawing.Point(159, 61);
			this.lblcurrentVal.Name = "lblcurrentVal";
			this.lblcurrentVal.Size = new System.Drawing.Size(14, 19);
			this.lblcurrentVal.TabIndex = 6;
			this.lblcurrentVal.Text = "0";
			this.lblcurrentVal.ThemeName = "Crystal";
			// 
			// frmDBUtility
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(389, 133);
			this.Controls.Add(this.lblcurrentVal);
			this.Controls.Add(this.radLabel3);
			this.Controls.Add(this.radLabel2);
			this.Controls.Add(this.txtSequence);
			this.Controls.Add(this.btnChangeSeq);
			this.Controls.Add(this.cmbTable);
			this.Controls.Add(this.radLabel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmDBUtility";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmDBUtility";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbTable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnChangeSeq)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSequence)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblcurrentVal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadDropDownList cmbTable;
		private Telerik.WinControls.UI.RadButton btnChangeSeq;
		private Telerik.WinControls.UI.RadTextBox txtSequence;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		private Telerik.WinControls.UI.RadLabel radLabel3;
		private Telerik.WinControls.UI.RadLabel lblcurrentVal;
	}
}
