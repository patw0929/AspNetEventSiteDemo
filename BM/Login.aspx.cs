using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tw.patw;

public partial class BM_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        imgCode.ImageUrl = "ValidateCode.aspx?t=" + (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
    }

    protected void btnLogin_Click(object sender, System.EventArgs e)
    {
        string strAccount = this.txtAct.Text.Trim();
        string strPassword = this.txtPWD.Text.Trim();
        string strVCode = this.VCode.Text.Trim();

        if ((!(strVCode == Convert.ToString(Session["ValidateCode"]))))
        {
            VCode.Text = "";
            PatwCommon.RegisterClientScriptAlert(this, "認證碼錯誤");
        }
        else
        {
            checkLogin(strAccount, strPassword);
        }
    }

    /// <summary>
    /// 檢查登入並設定 Session
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pw"></param>
    private void checkLogin(string acct, string pw)
    {
        // 取得 AppSettings 中後台登入相關設定 檔案:web.config
        string strDefaultAct = "" + System.Configuration.ConfigurationManager.AppSettings["BackendLoginAct"];
        string strDefaultPwd = "" + System.Configuration.ConfigurationManager.AppSettings["BackendLoginPwd"];

        string strBackendSession = "" + System.Configuration.ConfigurationManager.AppSettings["BackendSession"];

        // 檢查是否為預設帳號
        if (strDefaultAct == acct && strDefaultPwd == pw)
        {
            Session[strBackendSession] = acct;
            Response.Redirect("~/BM/Main.aspx");
        }
        else
        {
            PatwCommon.RegisterClientScriptAlert(this, "帳號或密碼錯誤！");
        }

    }
}