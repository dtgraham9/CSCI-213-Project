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
        protected void check_role()
        {
            string userName = User.Identity.Name;
            AppointmentDBEntities dbconTemp = new AppointmentDBEntities();
            UserTable credentials = dbconTemp.UserTables.Find(userName);
            if(credentials.UserRole != "student")
                Response.Redirect(ResolveUrl("default.aspx"));

        }


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
            check_role();
            string userName = User.Identity.Name;
            nameLbl.Text = User.Identity.Name;
            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();

            appointmentsView.DataSource = from appointment in dbcon.AppointmentTables.Local
                            orderby appointment.AppointmentDate, appointment.AppointmentTime
                            where appointment.AppointmentDate >= DateTime.Today
                            join advisor in dbcon.AdvisorTables.Local on appointment.AdvisorID equals advisor.AdvisorID
                            join student in dbcon.StudentTables.Local on appointment.StudentID equals student.StudentID
                            select appointment;
        }
    }
}