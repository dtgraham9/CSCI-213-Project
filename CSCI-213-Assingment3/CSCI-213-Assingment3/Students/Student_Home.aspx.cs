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
            AppointmentDBEntities1 dbconTemp = new AppointmentDBEntities1();
            UserTable credentials = dbconTemp.UserTables.Find(userName);
            if(credentials.UserRole != "student")
                Response.Redirect(ResolveUrl("default.aspx"));

        }


        AppointmentDBEntities1 dbcon = new AppointmentDBEntities1();

        int userID, advisorID;
        public void NewBDcon()
        {
            dbcon.StudentTables.Load();
            
            if (dbcon != null)
            {
                dbcon.Dispose();
                dbcon = new AppointmentDBEntities1();
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

             var results= from appointment in dbcon.AppointmentTables.Local
                                          orderby appointment.AppointmentDate, appointment.AppointmentTime
                                          where appointment.AppointmentDate >= DateTime.Today
                                          join advisor in dbcon.AdvisorTables.Local on appointment.AdvisorID equals advisor.AdvisorID
                                          join student in dbcon.StudentTables.Local on appointment.StudentID equals student.StudentID
                                          select new
                                          {
                                              appointment.AppointmentID,
                                              appointment.AppointmentDate,
                                              appointment.AppointmentTime,
                                              advisor.AdvisorLastName,
                                              advisor.AdvisorFirstName,
                                              student.StudentLastName,
                                              student.StudentFirstName,
                                              appointment.AppointmentReason
                                          };
            var formatedResults = from result in results
                                  select new
                                  {
                                      AppointmentID = result.AppointmentID,
                                      Date = result.AppointmentDate.ToShortDateString(),
                                      Time = result.AppointmentDate.Add(result.AppointmentTime).ToString("hh:mm tt"),
                                      AdvisorName = result.AdvisorFirstName + " " + result.AdvisorLastName,
                                      StudentName = result.StudentFirstName + " " + result.StudentLastName,
                                      Reason = result.AppointmentReason
                                  };
            appointmentsView.DataSource = formatedResults;
            appointmentsView.DataBind();
        }
    }
}