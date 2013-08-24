using System;
using System.Collections.Generic;

using System.Web;

/// <summary>
/// ErrorMsg 的摘要描述
/// </summary>
public class ErrorMsg
{
    public ErrorMsg(string columnName, string msg)
    {
        this._columnName = columnName;
        this._msg = msg;
    }

    private string _columnName;
    public string ColumnName
    {
        get { return _columnName; }
        set { _columnName = value; }
    }

    private string _msg;
    public string Msg
    {
        get { return _msg; }
        set { _msg = value; }
    }
}
