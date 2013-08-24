using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using tw.patw;
using Patw.Backend;

public partial class BM_Draw : System.Web.UI.Page
{
    PetaPoco.Database db = new PetaPoco.Database("conn");
    public PetaPoco.Sql sql = PetaPoco.Sql.Builder;
    public DataModel_a12SupauCheckin obja12SupauCheckin;

    public long PageSize = 15; // 每頁筆數
    public int MaxDrawQuota = 5; // 中獎名額
    private string TableName = "a12SupauCheckin"; // 資料表名稱
    private string PKColumn = "sID"; // 主索引鍵
    private string DistinctColumn = "sFBUID"; // 身份識別欄位
    private string WinFlagColumn = "sWin"; // 中獎識別欄位
    private string BasicCondition = " AND 1=1"; // 基本條件，如可以加入必須為有效資料 (sValid = '1')
    private bool IsGroup = false; // true: 填越多筆資料中獎機率越高; false: 眾人中獎機率皆同

    protected void Page_Load(object sender, EventArgs e)
    {
        lbMaxQuota.Text = MaxDrawQuota.ToString();
    }

    protected void patwGridView1_PreRender(object sender, EventArgs e)
    {
        if (patwGridView1.Rows.Count > 0)
        {
            // 使用 <th> 替換 <td>
            patwGridView1.UseAccessibleHeader = true;

            // 加入 <thead> 與 <tbody> 元素
            patwGridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            patwGridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    /// <summary>
    /// 主要查詢方法
    /// </summary>
    /// <param name="page">頁數（預設為 1）</param>
    /// <param name="NewDraw">重新抽獎（預設為 false）</param>
    /// <param name="ExportMode">匯出模式。（預設為 false）若為真，則取消分頁。</param>
    private void Query(long page = 1, bool NewDraw = false, bool ExportMode = false)
    {
        patwGridView1.Visible = false;
        btnSave.Visible = false;
        btnExport.Visible = false;

        string DistinctSQLStatement = "";

        if (Session["DrawIDs"] == null || NewDraw)
        {
            // 抽出結果
            DrawResult result = MakeDraw(int.Parse(tbDrawCount.Text), MaxDrawQuota, PKColumn, WinFlagColumn, DistinctColumn, TableName, BasicCondition, IsGroup);

            // 若發生異常，無法抽出
            if (!result.Result)
            {
                PatwCommon.RegisterClientScriptAlert(this, result.Msg);
                return;
            }
            else // 正常抽出
            {
                patwGridView1.Visible = true;
                btnSave.Visible = false;
                btnExport.Visible = true;

                // 排除重複後的名單
                DistinctSQLStatement = result.Msg;
                // 塞入 Session
                Session["DrawIDs"] = result.Msg;
            }
        }
        else
        {
            patwGridView1.Visible = true;
            btnSave.Visible = false;
            btnExport.Visible = true;

            // 排除重複後的名單
            DistinctSQLStatement = Convert.ToString(Session["DrawIDs"]);
        }

        // 組成 SQL 指令, 僅取上面抽出的那幾筆名單
        sql.Append(String.Format("SELECT * FROM {0}", TableName));
        sql.Append(String.Format("WHERE 1=1 AND {0} IN (SELECT MAX({0}) FROM {1} WHERE {3} IN ({2}) {4} GROUP BY {3})", PKColumn, TableName, DistinctSQLStatement, DistinctColumn, BasicCondition));

        if (!ExportMode)
        {
            var data = db.Page<DataModel_a12SupauCheckin>(page, PageSize, sql);
            patwGridView1.DataSource = data.Items;
            patwGridView1.DataBind();

            AspNetPager1.PageSize = (int)data.ItemsPerPage;
            AspNetPager1.RecordCount = (int)data.TotalItems;
            lbTotal.Text = "依據條件，目前共有 " + data.TotalItems.ToString() + " 筆";
        }
        else
        {
            var data = db.Query<DataModel_a12SupauCheckin>(sql);
            patwGridView1.DataSource = data;
            patwGridView1.DataBind();

            AspNetPager1.Visible = false;
            lbTotal.Text = "依據條件，目前共有 " + data.Count() + " 筆";
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Query(1, false, true);
        patwGridView1.Export("ExcelExport");
    }

    protected void btnDraw_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        // 強制重新抽
        Query(1, true);
    }

    /// <summary>
    /// 存入中獎名單
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        PetaPoco.Sql sql = PetaPoco.Sql.Builder;
        sql.Append(string.Format("UPDATE {0} SET {1}='1' WHERE {2} IN (SELECT MAX({2}) FROM {0} WHERE {4} IN ({3}) GROUP BY {4})", TableName, WinFlagColumn, PKColumn, Session["DrawIDs"], DistinctColumn));
        patwGridView1.Visible = false;
        btnExport.Visible = false;
        btnSave.Visible = false;
        db.Execute(sql);

        PatwCommon.RegisterClientScriptAlert(this.Page, "存入成功");
        Query();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Query(AspNetPager1.CurrentPageIndex);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    /// <summary>
    /// 抽獎方法
    /// </summary>
    /// <param name="n">欲抽出數量</param>
    /// <param name="PKColumn">主索引鍵</param>
    /// <param name="MaxDrawQuota">中獎名額</param>
    /// <param name="WinFlagColumn">中獎識別欄位</param>
    /// <param name="DistinctColumn">排除重複的欄位（判斷使用者身份的唯一值，例如：E-mail、Facebook UID 等）</param>
    /// <param name="TableName">資料表</param>
    /// <param name="BasicCondition">基本 SQL 條件</param>
    /// <param name="IsGroup">若為真，則每個人中獎機率相同；若為假，則名單越多者中獎機率越高。</param>
    /// <returns>回傳 DrawResult 類別，其下有 Result（是否成功，布林值）與 Msg（回傳訊息，若成功，則為不重複的欄位值）</returns>
    public static DrawResult MakeDraw(int n, int MaxDrawQuota, string PKColumn, string WinFlagColumn, string DistinctColumn, string TableName, string BasicCondition, bool IsGroup)
    {
        PetaPoco.Database db = new PetaPoco.Database("conn");

        int counter = 0;
        DrawResult result = new DrawResult();

        PetaPoco.Sql sql = PetaPoco.Sql.Builder;
        sql.Append(String.Format("SELECT MAX({0}) FROM {1} WHERE 1=1", PKColumn, TableName));
        sql.Append(BasicCondition);
        if (IsGroup)
        {
            sql.Append("GROUP BY [" + DistinctColumn + "]");
        }

        var data = db.Query<DataModel_a12SupauCheckin>(sql);
        counter = data.Count();

        if (counter < n)
        {
            result.Result = false;
            result.Msg = "名單不足以抽出這樣的數量喔！";

            return result;
        }
        
        if (n < 1)
        {
            result.Result = false;
            result.Msg = "數量請至少為 1。";

            return result;
        }

        if (n > MaxDrawQuota)
        {
            result.Result = false;
            result.Msg = "抽出名額不得大於中獎名額 " + MaxDrawQuota + " 名 喔！";

            return result;
        }

        #region 檢查剩餘名額

        sql = PetaPoco.Sql.Builder;
        sql.Append(String.Format("SELECT {0} FROM {1} WHERE {2}='1'", PKColumn, TableName, WinFlagColumn));
        sql.Append(BasicCondition);
        var r = db.Query<DataModel_a12SupauCheckin>(sql);

        // 若目前中獎人數大於等於中獎名額
        if (r.Count() >= MaxDrawQuota)
        {
            result.Result = false;
            result.Msg = "名額已滿";

            return result;
        }

        #endregion


        if (!IsGroup)
        {
            if (n == 1)
            {
                sql = PetaPoco.Sql.Builder;
                sql.Append(String.Format("SELECT TOP 1 {0} FROM {1} WHERE 1=1", DistinctColumn, TableName));
                sql.Append(BasicCondition);
                sql.Append("ORDER BY NEWID()");

                var a = db.SingleOrDefault<DataModel_a12SupauCheckin>(sql);
                result.Result = true;
                result.Msg = "'" + a.sFBUID.ToString() + "'";

                return result;
            }
            else
            {
                string list_column = MakeDraw(n - 1, MaxDrawQuota, PKColumn, WinFlagColumn, DistinctColumn, TableName, BasicCondition, IsGroup).Msg;

                sql = PetaPoco.Sql.Builder;
                sql.Append(String.Format("SELECT TOP 1 * FROM {0} WHERE 1=1", TableName));
                sql.Append(String.Format("{0} AND [{1}] NOT IN ({2})", BasicCondition, DistinctColumn, list_column));
                sql.Append("ORDER BY NEWID()");

                var a = db.SingleOrDefault<DataModel_a12SupauCheckin>(sql);

                result.Result = true;
                result.Msg = list_column + ",'" + a.sFBUID.ToString() + "'";

                return result;
            }
        }
        else
        {
            sql = PetaPoco.Sql.Builder;
            sql.Append(String.Format("SELECT TOP {0} {1} FROM {2} WHERE 1=1", n, DistinctColumn, TableName));
            sql.Append(BasicCondition);
            sql.Append(String.Format("GROUP BY [{0}] ORDER BY NEWID()", DistinctColumn));

            var a = db.Query<DataModel_a12SupauCheckin>(sql);
            string return_data = "";
            foreach (var item in a)
            {
                return_data += ",'" + item.sFBUID + "'";
            }
            return_data = return_data.Substring(1, return_data.Length - 1);

            result.Result = true;
            result.Msg = return_data;

            return result;
        }
    }

}
