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
        protected string check_role()
        {
            string userName = User.Identity.Name;
            AppointmentDBEntities1 dbconTemp = new AppointmentDBEntities1();
            UserTable credentials = dbconTemp.UserTables.Find(userName);
            /*if(credentials.UserRole != "student")
                Response.Redirect(ResolveUrl("default.aspx"));*/
            return credentials.UserRole;

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

        protected void appointmentsView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)appointmentsView.Rows[e.RowIndex];
        }

        protected void appointmentsView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            int appointmentIdentifier = Convert.ToInt32(appointmentsView.SelectedRow.Cells[1].Text);
            NewBDcon();
            dbcon.AppointmentTables.Load();
            dbcon.MessagesTables.Load();
            AppointmentTable entry = dbcon.AppointmentTables.Find(appointmentIdentifier);
            string advisorEmail = appointmentsView.SelectedRow.Cells[5].Text;
            string studentEmail = appointmentsView.SelectedRow.Cells[7].Text;
            string message = "Appointment Cancel: " + appointmentsView.SelectedRow.Cells[2].Text + " " +
                appointmentsView.SelectedRow.Cells[3].Text;
            string role = check_role();
            MessagesTable messageEntry;
            if (role == "advisor")
            {
                messageEntry = new MessagesTable
                {
                    EmailDate = DateTime.Today,
                    EmailFrom = advisorEmail,
                    EmailText = message,
                    EmailTime = DateTime.Today.TimeOfDay,
                    EmailTo = studentEmail
                };
            }
            else
            {
                messageEntry = new MessagesTable
                {
                    EmailDate = DateTime.Today,
                    EmailFrom = studentEmail,
                    EmailText = message,
                    EmailTime = DateTime.Now.TimeOfDay,
                    EmailTo = advisorEmail
                };
            }
            dbcon.MessagesTables.Add(messageEntry);
            dbcon.AppointmentTables.Remove(entry);
            dbcon.SaveChanges();
            
            View_Own_Appointments();
        }
        
        protected void Fill_Emails()
        {
            string userName = User.Identity.Name;
            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();
            dbcon.MessagesTables.Load();

            var emails = from email in dbcon.MessagesTables.Local
                         join user in dbcon.UserTables on email.EmailTo equals user.UserEmail
                         where user.UserName == userName
                         select email;

            var formatSingleEmail = from email in emails
                                    select new
                                    {
                                        EmailID = email.EmailID,
                                        From = email.EmailFrom,
                                        Date = email.EmailDate.ToShortDateString() + " " + email.EmailDate.Add(email.EmailTime).ToString("hh:mm tt"),
                                        Message = email.EmailText
                                    };

            var formatEmails = from email in emails
                               select new
                               {
                                   emailID = email.EmailID,
                                   emailDate = email.EmailDate.ToShortDateString(),
                                   emailTime = email.EmailDate.Add(email.EmailTime).ToString("hh:mm tt"),
                                   From = email.EmailFrom
                               };

            DetailEmailView.DataSource = formatSingleEmail;
            
            DetailEmailView.DataBind();
            EmailView.DataSource = formatEmails;
            EmailView.DataBind();

            if(EmailView.Rows.Count !=0)
                EmailView.SelectRow(0);
        }

        protected void EmailView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int emailIdentifier = Convert.ToInt32(EmailView.SelectedRow.Cells[1].Text);
            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();
            dbcon.MessagesTables.Load();

            var emails = from email in dbcon.MessagesTables.Local
                         where email.EmailID == emailIdentifier
                         select email;

            var formatSingleEmail = from email in emails
                                    select new
                                    {
                                        EmailID = email.EmailID,
                                        From = email.EmailFrom,
                                        Date = email.EmailDate.ToShortDateString() + " " + email.EmailDate.Add(email.EmailTime).ToString("hh:mm tt"),
                                        Message = email.EmailText
                                    };

            DetailEmailView.DataSource = formatSingleEmail;
            DetailEmailView.DataBind();
        }

        protected void deleteEmBtn_Click(object sender, EventArgs e)
        {
            int emailIdentifier = Convert.ToInt32(EmailView.SelectedRow.Cells[1].Text);
            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();
            dbcon.MessagesTables.Load();
            MessagesTable entry = dbcon.MessagesTables.Find(emailIdentifier);
            dbcon.MessagesTables.Remove(entry);
            dbcon.SaveChanges();
            Fill_Emails();
        }

        protected void SendMsgBtn_Click(object sender, EventArgs e)
        {

            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();
            dbcon.MessagesTables.Load();
            string userName = User.Identity.Name;
            string role = check_role();
            string advisorEmail, studentEmail;

            if ("advisor" == role)
            {
                advisorEmail = (from advisor in dbcon.AdvisorTables.Local
                                where advisor.AdvisorUserName == userName
                                join user in dbcon.UserTables.Local on advisor.AdvisorUserName equals user.UserName
                                select user.UserEmail).First();
                studentEmail = StudentsView.SelectedRow.Cells[3].Text;
            }
            else
            {
                studentEmail = (from students in dbcon.StudentTables.Local
                                where students.StudentUserName == userName
                                join user in dbcon.UserTables.Local on students.StudentUserName equals user.UserName
                                select user.UserEmail).First();

                advisorEmail = (from students in dbcon.StudentTables.Local
                                where students.StudentUserName == userName
                                join advisors in dbcon.AdvisorTables.Local on students.StudentAdvisorID equals advisors.AdvisorID
                                join user in dbcon.UserTables.Local on advisors.AdvisorUserName equals user.UserName
                                select user.UserEmail).First();
            }

            MessagesTable message;

            if (TextBox1.Text != "")
            {
                string emailMessage = TextBox1.Text;
                message = new MessagesTable
                {
                    EmailTime = DateTime.Now.TimeOfDay,
                    EmailDate = DateTime.Today,
                    EmailTo = advisorEmail,
                    EmailFrom = studentEmail,
                    EmailText = emailMessage
                };

                dbcon.MessagesTables.Add(message);
                dbcon.SaveChanges();
            }
            

            
            TextBox1.Text = "";

        }

        protected void Fill_Students_Email()
        {
            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();
            dbcon.MessagesTables.Load();
            string userName = User.Identity.Name;

            var students = from advisors in dbcon.AdvisorTables.Local
                              where advisors.AdvisorUserName == userName
                              join student in dbcon.StudentTables.Local on advisors.AdvisorID equals student.StudentAdvisorID
                              join user in dbcon.UserTables.Local on student.StudentUserName equals user.UserName

                              select new
                              {
                                  StudentID = student.StudentID,
                                  StudentName = student.StudentFirstName + " " + student.StudentLastName,
                                  StudentEmail = user.UserEmail
                              };

            StudentsView.DataSource = students;
            StudentsView.DataBind();
            if(StudentsView.Rows.Count != 0)
            {
                StudentsView.SelectRow(0);
                messageLb.Text = "New Message To " + StudentsView.SelectedRow.Cells[2].Text;
            }           
        }

        protected void Loading_Page()
        {
            string role = check_role();
            if (role == "advisor")
            {
                messageLb.Text = "New Message To ";
                StudentsView.Visible = true;
                Fill_Students_Email();
                AdvisorViewAll.Visible = true;
                AdvisorViewOwn.Visible = true;

            }
            else
            {
                messageLb.Text = "New Message To Advisor";
                StudentsView.Visible = false;
                AdvisorViewAll.Visible = false;
                AdvisorViewOwn.Visible = false;
            }

            string userName = User.Identity.Name;
            nameLbl.Text = User.Identity.Name;
            Fill_Emails();
            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();
            View_Own_Appointments();
        }

        protected void StudentsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            messageLb.Text = "New Message To " + StudentsView.SelectedRow.Cells[2].Text;
        }

        protected void View_All_Appointments()
        {
            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();
            var results = from appointment in dbcon.AppointmentTables.Local
                          orderby appointment.AppointmentDate, appointment.AppointmentTime
                          where appointment.AppointmentDate >= DateTime.Today
                          join advisor in dbcon.AdvisorTables.Local on appointment.AdvisorID equals advisor.AdvisorID
                          join student in dbcon.StudentTables.Local on appointment.StudentID equals student.StudentID
                          join user in dbcon.UserTables.Local on advisor.AdvisorUserName equals user.UserName
                          join user2 in dbcon.UserTables.Local on student.StudentUserName equals user2.UserName
                          select new
                          {
                              appointment.AppointmentID,
                              appointment.AppointmentDate,
                              appointment.AppointmentTime,
                              advisor.AdvisorLastName,
                              advisor.AdvisorFirstName,
                              AdvisorEmail = user.UserEmail,
                              student.StudentLastName,
                              student.StudentFirstName,
                              StudentEmail = user2.UserEmail,
                              advisor.AdvisorLocation,
                              appointment.AppointmentReason
                          };
            var formatedResults = from result in results
                                  select new
                                  {
                                      AppointmentID = result.AppointmentID,
                                      Date = result.AppointmentDate.ToShortDateString(),
                                      Time = result.AppointmentDate.Add(result.AppointmentTime).ToString("hh:mm tt"),
                                      AdvisorName = result.AdvisorFirstName + " " + result.AdvisorLastName,
                                      result.AdvisorEmail,
                                      StudentName = result.StudentFirstName + " " + result.StudentLastName,
                                      result.StudentEmail,
                                      result.AdvisorLocation,
                                      Reason = result.AppointmentReason
                                  };
            appointmentsView.DataSource = formatedResults;
            appointmentsView.DataBind();
            if (appointmentsView.Rows.Count != 0)
                appointmentsView.SelectRow(0);
        }

        protected void AdvisorViewOwn_Click(object sender, EventArgs e)
        {
            View_All_Appointments();
        }

        protected void View_Own_Appointments()
        {
            NewBDcon();
            dbcon.StudentTables.Load();
            dbcon.UserTables.Load();
            dbcon.AdvisorTables.Load();
            dbcon.AppointmentTables.Load();
            string role = check_role();
            string userName = User.Identity.Name;
            if ("advisor" == role)
            {
                var results = from appointment in dbcon.AppointmentTables.Local
                              orderby appointment.AppointmentDate, appointment.AppointmentTime
                              where appointment.AppointmentDate >= DateTime.Today
                              join advisor in dbcon.AdvisorTables.Local on appointment.AdvisorID equals advisor.AdvisorID
                              join student in dbcon.StudentTables.Local on appointment.StudentID equals student.StudentID
                              where advisor.AdvisorUserName == userName
                              join user in dbcon.UserTables.Local on advisor.AdvisorUserName equals user.UserName
                              join user2 in dbcon.UserTables.Local on student.StudentUserName equals user2.UserName
                              select new
                              {
                                  appointment.AppointmentID,
                                  appointment.AppointmentDate,
                                  appointment.AppointmentTime,
                                  advisor.AdvisorLastName,
                                  advisor.AdvisorFirstName,
                                  AdvisorEmail = user.UserEmail,
                                  student.StudentLastName,
                                  student.StudentFirstName,
                                  StudentEmail = user2.UserEmail,
                                  advisor.AdvisorLocation,
                                  appointment.AppointmentReason
                              };

                var formatedResults = from result in results
                                      select new
                                      {
                                          AppointmentID = result.AppointmentID,
                                          Date = result.AppointmentDate.ToShortDateString(),
                                          Time = result.AppointmentDate.Add(result.AppointmentTime).ToString("hh:mm tt"),
                                          AdvisorName = result.AdvisorFirstName + " " + result.AdvisorLastName,
                                          result.AdvisorEmail,
                                          StudentName = result.StudentFirstName + " " + result.StudentLastName,
                                          result.StudentEmail,
                                          result.AdvisorLocation,
                                          Reason = result.AppointmentReason
                                      };
                appointmentsView.DataSource = formatedResults;
            }
            else
            {
                var results = from appointment in dbcon.AppointmentTables.Local
                              orderby appointment.AppointmentDate, appointment.AppointmentTime
                              where appointment.AppointmentDate >= DateTime.Today
                              join advisor in dbcon.AdvisorTables.Local on appointment.AdvisorID equals advisor.AdvisorID
                              join student in dbcon.StudentTables.Local on appointment.StudentID equals student.StudentID
                              where student.StudentUserName == userName
                              join user in dbcon.UserTables.Local on advisor.AdvisorUserName equals user.UserName
                              join user2 in dbcon.UserTables.Local on student.StudentUserName equals user2.UserName
                              select new
                              {
                                  appointment.AppointmentID,
                                  appointment.AppointmentDate,
                                  appointment.AppointmentTime,
                                  advisor.AdvisorLastName,
                                  advisor.AdvisorFirstName,
                                  AdvisorEmail = user.UserEmail,
                                  student.StudentLastName,
                                  student.StudentFirstName,
                                  StudentEmail = user2.UserEmail,
                                  advisor.AdvisorLocation,
                                  appointment.AppointmentReason
                              };
                var formatedResults = from result in results
                                      select new
                                      {
                                          AppointmentID = result.AppointmentID,
                                          Date = result.AppointmentDate.ToShortDateString(),
                                          Time = result.AppointmentDate.Add(result.AppointmentTime).ToString("hh:mm tt"),
                                          AdvisorName = result.AdvisorFirstName + " " + result.AdvisorLastName,
                                          result.AdvisorEmail,
                                          StudentName = result.StudentFirstName + " " + result.StudentLastName,
                                          result.StudentEmail,
                                          result.AdvisorLocation,
                                          Reason = result.AppointmentReason
                                      };
                appointmentsView.DataSource = formatedResults;
            }
            
            appointmentsView.DataBind();
            if (appointmentsView.Rows.Count != 0)
                appointmentsView.SelectRow(0);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {
                Loading_Page();
            }
            
        }
    }
}