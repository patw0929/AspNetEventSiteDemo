using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tw.patw;

public partial class BM_ValidateCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidateCode vc = new ValidateCode();
        Session["ValidateCode"] = vc.DrawImage();
    }
}
