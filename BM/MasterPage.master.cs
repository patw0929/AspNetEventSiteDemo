using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BM_MasterPage : System.Web.UI.MasterPage
{
    string strBackendSession = "" + System.Configuration.ConfigurationManager.AppSettings["BackendSession"];

    protected void Page_Load(object sender, EventArgs e)
    {
        checkSession();
    }

    private void checkSession()
    {
        if (!ValidatorFuncs.IsNotEmpty(Convert.ToString(Session[strBackendSession])))
        {
            Response.Write("<script>alert('登入逾時，請重新登入。');location.href='login.aspx';</script>");
            Response.End();
        }
    }
}
