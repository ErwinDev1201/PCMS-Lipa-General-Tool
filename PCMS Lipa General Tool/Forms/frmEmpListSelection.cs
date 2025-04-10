using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System.Data;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using PCMS_Lipa_General_Tool__WinForm_;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using ClosedXML.Excel;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmEmpListSelection : Telerik.WinControls.UI.RadForm
	{

		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private readonly Database db = new();
        private readonly User user = new();



        public string _empName;
        public string _accessLevel;
        public string _userName;
        public string _employeeID;
        public string _officeLoc;
        public string _employeeStat;
        //public string _tableName;

        //public string ThemeName;
        public string _position;
        //public string _empName;
        //public string _accessLevel;
        //

        //private readonly string _empName;

        public frmEmpListSelection(string empName, string userName, string userAccess, string empID, string officeLoc, string empStat, string position)
        {
            InitializeComponent();

            _empName = empName ?? throw new ArgumentNullException(nameof(empName));
            //EmpName = empName;
            _userName = userName;
            _accessLevel = userAccess;
            _employeeID = empID;
            _officeLoc = officeLoc;
            _employeeStat = empStat;
            _position = position;
            // Other fields like userName, userAccess etc. can be stored if needed
            LoadEmployeeList();
        }

        private void chkLoadmyData_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            // Fix the logic: enable the combo when unchecked, disable when checked
            cmbEmployeeName.Enabled = !chkLoadmyData.Checked;
        }

        private void cmbEmployeeName_PopupOpened(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }

        private void LoadEmployeeList()
        {
            cmbEmployeeName.Items.Clear();
            var items = user.GetEmployeeList(_empName);
            if (items == null || items.Count == 0)
            {
                cmbEmployeeName.Text = _empName;
                //RadMessageBox.Show(
                //    this,
                //    "No employees found for the current user.",
                //    "Notice",
                //    MessageBoxButtons.OK,
                //    RadMessageIcon.Info
                //);
                //return;
            }
            items.ForEach(item => cmbEmployeeName.Items.Add(item));
        }

        private void btnLoadEmp_Click(object sender, EventArgs e)
        {

            var collectorNotes = new frmCollectors(cmbEmployeeName.Text, _empName, _userName, _accessLevel, _employeeID, _officeLoc, _employeeStat, _position);
            collectorNotes.mnumainCollector.Visible = false;
            if (!string.IsNullOrWhiteSpace(collectorNotes.ErrorMessage))
            {
                lblError.Text = collectorNotes.ErrorMessage;
                lblError.Visible = true; // optional
            }
            else
            {
                collectorNotes.ShowDialog();
            }
            // collectorNotes.tableName = cmbEmployeeName.Text;
           
        }  //
    }
}