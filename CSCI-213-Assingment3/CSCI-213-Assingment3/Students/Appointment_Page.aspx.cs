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
            int advisor = (from students in dbconDate.StudentTables.Local
                           where students.StudentUserName == userName
                           select students.StudentAdvisorID).First();

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
        protected void Page_Load(object sender, EventArgs e)
        {
            Calendar1.SelectedDate = DateTime.Today;
            Fill_Drop_Down(DateTime.Today);
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
            Fill_Drop_Down(Calendar1.SelectedDate);
            
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.ParseExact(DropDownList1.SelectedItem.Value,
                                    "hh:mm tt", CultureInfo.InvariantCulture);
            NewBDcon();
            dbconDate.StudentTables.Load();
            dbconDate.UserTables.Load();
            dbconDate.AdvisorTables.Load();
            dbconDate.AppointmentTables.Load();
            TimeSpan span = dateTime.TimeOfDay;
            string userName = User.Identity.Name;
            int advisor = (from students in dbconDate.StudentTables.Local
                           where students.StudentUserName == userName
                           select students.StudentAdvisorID).First();
            int student = (from students in dbconDate.StudentTables.Local
                           where students.StudentUserName == userName
                           select students.StudentID).First();
            AppointmentTable entry = new AppointmentTable
            {
                AdvisorID = advisor,
                AppointmentDate = Calendar1.SelectedDate,
                AppointmentReason = TextBox1.Text,
                StudentID= student,
                AppointmentTime = span,
            };
            dbconDate.AppointmentTables.Add(entry);
            dbconDate.SaveChanges();
            Response.Redirect(ResolveUrl("Student_Home.aspx"));
        }
    }
}