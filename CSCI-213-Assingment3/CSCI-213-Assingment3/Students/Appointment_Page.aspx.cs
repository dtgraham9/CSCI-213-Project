using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Globalization;

namespace CSCI_213_Assingment3.Students
{
    public partial class Apointment_Page : System.Web.UI.Page
    {
        protected string check_role()
        {
            string userName = User.Identity.Name;
            AppointmentDBEntities1 dbconTemp = new AppointmentDBEntities1();
            UserTable credentials = dbconTemp.UserTables.Find(userName);
            /*if (credentials.UserRole != "student")
                Response.Redirect(ResolveUrl("default.aspx"));*/
            return credentials.UserRole;

        }

        protected void Fill_Students()
        {
            NewBDcon();
            dbconDate.StudentTables.Load();
            dbconDate.UserTables.Load();
            dbconDate.AdvisorTables.Load();
            dbconDate.AppointmentTables.Load();
            dbconDate.MessagesTables.Load();
            string userName = User.Identity.Name;

            var students = from advisors in dbconDate.AdvisorTables.Local
                           where advisors.AdvisorUserName == userName
                           join student in dbconDate.StudentTables.Local on advisors.AdvisorID equals student.StudentAdvisorID
                           join user in dbconDate.UserTables.Local on student.StudentUserName equals user.UserName
                           select new
                           {
                               StudentID = student.StudentID,
                               StudentName = student.StudentFirstName + " " + student.StudentLastName,
                               StudentEmail = user.UserEmail
                           };

            StudentsView.DataSource = students;
            StudentsView.DataBind();
            if (StudentsView.Rows.Count != 0)
            {
                StudentsView.SelectRow(0);
                studentLB.Text = "Appointment for " + StudentsView.SelectedRow.Cells[2].Text;
            }
        }
        protected void Fill_Drop_Down(DateTime day)
        {
            NewBDcon();
            List<TimeSpan> availableTimes = new List<TimeSpan>() {
                new TimeSpan(8, 0, 0), new TimeSpan(8,30,0), new TimeSpan(9,0,0),
                new TimeSpan(14,30,0), new TimeSpan(15,0,0), new TimeSpan(15,30,0)};
            string userName = User.Identity.Name;
            dbconDate.StudentTables.Load();
            dbconDate.UserTables.Load();
            dbconDate.AdvisorTables.Load();
            dbconDate.AppointmentTables.Load();
            int advisor;
            string role = check_role();
            if(role == "advisor")
            {
                advisor = (from advisors in dbconDate.AdvisorTables.Local
                           where advisors.AdvisorUserName == userName
                           select advisors.AdvisorID).First();
            }
            else
            {
                advisor = (from students in dbconDate.StudentTables.Local
                 where students.StudentUserName == userName
                 select students.StudentAdvisorID).First();
            }
                

            var bookedTimes = (from appointment in dbconDate.AppointmentTables.Local
                               where appointment.AppointmentDate == Calendar1.SelectedDate && appointment.AdvisorID == advisor
                               select appointment.AppointmentTime).ToList();
            var remainingTimes = from time in availableTimes
                                 where !bookedTimes.Contains(time)
                                 select time;
            DropDownList1.Items.Clear();
            foreach (TimeSpan time in remainingTimes)
            {
                DateTime holdTime = day.Add(time);
                string displayTime = holdTime.ToString("hh:mm tt");
                DropDownList1.Items.Add(displayTime);
            }
        }

        protected void Loading_Page()
        {
            string role = check_role();
            if("advisor" == role)
            {
                StudentsView.Visible = true;
                Fill_Students();
                studentLB.Visible = true;
            }
            else
            {
                StudentsView.Visible = false;
                studentLB.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            
            if (!Page.IsPostBack)
            {
                Loading_Page();
                Calendar1.SelectedDate = DateTime.Today;
                Fill_Drop_Down(DateTime.Today);
            }
            
        }
        AppointmentDBEntities1 dbconDate = new AppointmentDBEntities1();
        public void NewBDcon()
        {
            dbconDate.StudentTables.Load();

            if (dbconDate != null)
            {
                dbconDate.Dispose();
                dbconDate = new AppointmentDBEntities1();
            }

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate > DateTime.Now)
            {
                Fill_Drop_Down(Calendar1.SelectedDate);

                Label1.Visible = false;
            }
            else
            {
                Label1.Visible = true;
            }
            
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            DateTime dateTime;
            if (DropDownList1.Items.Count != 0)
            {
                dateTime = DateTime.ParseExact(DropDownList1.SelectedItem.Value,
                                    "hh:mm tt", CultureInfo.InvariantCulture);
            }
            else
            {
                return ;
            }
            string role = check_role();
            NewBDcon();
            dbconDate.StudentTables.Load();
            dbconDate.UserTables.Load();
            dbconDate.AdvisorTables.Load();
            dbconDate.AppointmentTables.Load();
            dbconDate.MessagesTables.Load();
            TimeSpan span = dateTime.TimeOfDay;
            string userName = User.Identity.Name;
            int advisor, student;
            if ("advisor" == role)
            {
                advisor = (from advisors in dbconDate.AdvisorTables.Local
                           where advisors.AdvisorUserName == userName
                           select advisors.AdvisorID).First();
                student = Convert.ToInt32(StudentsView.SelectedRow.Cells[1].Text);
            }
            else
            {
                advisor = (from students in dbconDate.StudentTables.Local
                           where students.StudentUserName == userName
                           select students.StudentAdvisorID).First();
                student = (from students in dbconDate.StudentTables.Local
                           where students.StudentUserName == userName
                           select students.StudentID).First();
            }
            AppointmentTable entry = new AppointmentTable
            {
                AdvisorID = advisor,
                AppointmentDate = Calendar1.SelectedDate,
                AppointmentReason = TextBox1.Text,
                StudentID= student,
                AppointmentTime = span,
            };

            AdvisorTable advisorEntry = dbconDate.AdvisorTables.Find(advisor);
            StudentTable studentEntry = dbconDate.StudentTables.Find(student);
            string studentEmail, advisorEmail;
            if("advisor" == role)
            {
                advisorEmail = (from advisors in dbconDate.AdvisorTables.Local
                                where advisors.AdvisorUserName == userName
                                join user in dbconDate.UserTables.Local on advisors.AdvisorUserName equals user.UserName
                                select user.UserEmail).First();
                studentEmail = StudentsView.SelectedRow.Cells[3].Text;
            }
            else
            {
                studentEmail = (from students in dbconDate.StudentTables.Local
                                where students.StudentUserName == userName
                                join user in dbconDate.UserTables.Local on students.StudentUserName equals user.UserName
                                select user.UserEmail).First();
                advisorEmail = (from students in dbconDate.StudentTables.Local
                                where students.StudentUserName == userName
                                join advisors in dbconDate.AdvisorTables.Local on students.StudentAdvisorID equals advisors.AdvisorID
                                join user in dbconDate.UserTables.Local on advisors.AdvisorUserName equals user.UserName
                                select user.UserEmail).First();
            }
            DateTime holdTime = DateTime.Today.Add(span);
            string displayTime = holdTime.ToString("hh:mm tt");
            string emailMessage = "Appointment date and Time: " + Calendar1.SelectedDate.ToShortDateString() + " " + displayTime +
                                  " \nAppointment Reason: " + TextBox1.Text;

            MessagesTable message;
            if ("advisor" == role)
            {
                message = new MessagesTable
                {
                    EmailTime = DateTime.Now.TimeOfDay,
                    EmailDate = DateTime.Today,
                    EmailTo = studentEmail,
                    EmailFrom = advisorEmail,
                    EmailText = emailMessage
                };
            }
            else
            {
                message = new MessagesTable
                {
                    EmailTime = DateTime.Now.TimeOfDay,
                    EmailDate = DateTime.Today,
                    EmailTo = advisorEmail,
                    EmailFrom = studentEmail,
                    EmailText = emailMessage
                };
            }

            dbconDate.MessagesTables.Add(message);
            dbconDate.SaveChanges();
            dbconDate.AppointmentTables.Add(entry);
            dbconDate.SaveChanges();
            Response.Redirect(ResolveUrl("Student_Home.aspx"));
        }

        protected void clearBtn_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("Student_Home.aspx"));
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void backBtn_Click1(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("Student_Home.aspx"));
        }

        protected void StudentsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            studentLB.Text = "Appointment for " + StudentsView.SelectedRow.Cells[2].Text;
        }
    }
}