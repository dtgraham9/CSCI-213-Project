using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
namespace CSCI_213_Assingment3.Students
{
    public partial class Apointment_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Calendar1.SelectedDate = DateTime.Today;
            string userName = User.Identity.Name;
        }
        AppointmentDBEntities dbconDate = new AppointmentDBEntities();
        public void NewBDcon()
        {
            dbconDate.StudentTables.Load();

            if (dbconDate != null)
            {
                dbconDate.Dispose();
                dbconDate = new AppointmentDBEntities();
            }

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            List<TimeSpan> availableTimes = new List<TimeSpan>() {
                new TimeSpan(8, 0, 0), new TimeSpan(8,30,0), new TimeSpan(9,0,0),
                new TimeSpan(14,30,0), new TimeSpan(15,0,0), new TimeSpan(15,30,0)};
            NewBDcon();
            string userName = User.Identity.Name;
            dbconDate.StudentTables.Load();
            dbconDate.UserTables.Load();
            dbconDate.AdvisorTables.Load();
            dbconDate.AppointmentTables.Load();
            UserTable credentials = dbconDate.UserTables.Find(userName);
            int advisor = (from students in dbconDate.StudentTables.Local
                          where students.StudentUserName == userName
                          select students.StudentAdvisorID).First();

            var bookedTimes = (from appointment in dbconDate.AppointmentTables.Local
                              where appointment.AppointmentDate == Calendar1.SelectedDate && appointment.AdvisorID == advisor
                              select appointment.AppointmentTime).ToList();
            var remainingTimes = from time in availableTimes
                                 where !bookedTimes.Contains(time)
                                 select time;
            
            foreach (TimeSpan time in remainingTimes)
            {
                DateTime holdTime = DateTime.Today.Add(time);
                string displayTime = holdTime.ToString("hh:mm tt");
                DropDownList1.Items.Add(displayTime);
            }
            
        }
    }
}