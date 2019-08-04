using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace CSCI_213_Assingment3
{
    public partial class Login_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            AppointmentDBEntities1 dbcon = new AppointmentDBEntities1();
            UserTable loginCred = dbcon.UserTables.Find(Login1.UserName.Trim());
            /* UserTable user = (from x in dbcon.UserTables.Local
             *                   where x.UserName.Trim().Equals(name)
             *                   && x.UserPass.Equals(pass)
             *                   select x).First();
             * if(user !=null)
             */
            if (!loginCred.Equals(null) && loginCred.UserPassword.Equals(Login1.Password.Trim()))
            {
                FormsAuthentication.RedirectFromLoginPage(loginCred.UserName, true);
                //FormsAuthentication.
                
            }
        }
    }
}