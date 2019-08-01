using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace CSCI_213_Assingment3.Students
{
    public partial class Student_Home : System.Web.UI.Page
    {

        AppointmentDBEntities dbcon = new AppointmentDBEntities();

        int userID, advisorID;
        public void NewBDcon()
        {
            dbcon.StudentTables.Load();
            
            if (dbcon != null)
            {
                dbcon.Dispose();
                dbcon = new AppointmentDBEntities();
            }

        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NewBDcon();
            dbcon.StudentTables.Load();

            dbcon.AppointmentTables.Load();

            ListBox1.Text = from item in dbcon.AppointmentTables.Local 
                            orderby item.AppointmentDate, item.AppointmentTime
                            where item.AppointmentDate >= GetDate() && item.userId
        }
    }
}