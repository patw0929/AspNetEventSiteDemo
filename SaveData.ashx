<%@ WebHandler Language="C#" Class="SaveData" %>

using System;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using tw.patw;
using FluentValidation.Results;
using Patw.Backend;

public class SaveData : IHttpHandler
{
    public void ProcessRequest (HttpContext context)
    {
        var db = new PetaPoco.Database("conn");
        Dictionary<String, Object> dic = new Dictionary<string, object>();

        DataModel_a12SupauCheckin obja12SupauCheckin = new DataModel_a12SupauCheckin()
        {
            sFBUID = string.Empty,
            sFBDisplayName = string.Empty,
            sName = string.Empty,
            sGender = 1,
            sBirth = Convert.ToDateTime("2010/10/10"),
            sFBEmail = string.Empty,
            sEmail = string.Empty,
            sMobile = string.Empty,
            sLocation = 1,
            sIP = string.Empty,
            sValid = 1,
            sCreatetime = Convert.ToDateTime("2010/10/10"),
            sWin = 1
        };

        a12SupauCheckinValidator validator = new a12SupauCheckinValidator();
        ValidationResult results = validator.Validate(obja12SupauCheckin);
        string ErrorMessage = "";

        if (!results.IsValid)
        {
            foreach (var failure in results.Errors)
            {
                ErrorMessage += failure.ErrorMessage + "、";
            }

            dic["Result"] = "0";
            dic["Msg"] = ErrorMessage;

        }
        else
        {
            var result = db.Insert(obja12SupauCheckin);
            if (int.Parse(result.ToString()) > 0)
            {
                dic["Result"] = "1";
            }
        }

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        context.Response.Write(serializer.Serialize(dic));
        context.Response.End();
    }

    public bool IsReusable
    {
        get {
            return false;
        }
    }
}
