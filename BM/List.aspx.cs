using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Patw.Backend;

public partial class BM_List : System.Web.UI.Page
{
    PetaPoco.Database db = new PetaPoco.Database("conn");
    public PetaPoco.Sql sql = PetaPoco.Sql.Builder;
    public long PageSize = 15;
    public IList<SearchItem> SearchItems = new List<SearchItem>();
    public DataModel_a12SupauCheckin obja12SupauCheckin;
    protected Search_Textfield st = null;
    private string TableName = "a12SupauCheckin";

    protected void Page_Init(object sender, EventArgs e)
    {
        #region 設定搜尋控制項目

        plSearch.Controls.Add(new LiteralControl("<br />"));
        
        
        Search_Date sd = new Search_Date(this.ViewState, plSearch, "日期範圍", "selectDate", "sCreatetime", "tbStartDate", "tbEndDate");
        SearchItems = SearchItems.Union(sd.SearchItems).ToList();
        Search_Checkbox sc = new Search_Checkbox(this.ViewState, plSearch, "僅列出有效資料", "chksFBUID", "sFBUID");
        SearchItems = SearchItems.Union(sc.SearchItems).ToList();
        st = new Search_Textfield(this.ViewState, plSearch, "sFBUID:", "tbsFBUID", "sFBUID", "sFBUID");
        SearchItems = SearchItems.Union(st.SearchItems).ToList();
        st = new Search_Textfield(this.ViewState, plSearch, "sFBDisplayName:", "tbsFBDisplayName", "sFBDisplayName", "sFBDisplayName");
        SearchItems = SearchItems.Union(st.SearchItems).ToList();
        st = new Search_Textfield(this.ViewState, plSearch, "sName:", "tbsName", "sName", "sName");
        SearchItems = SearchItems.Union(st.SearchItems).ToList();
        
        #endregion
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Query();
        }
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
    /// <param name="ExportMode">匯出模式。（預設為 false）若為真，則取消分頁。</param>
    private void Query(long page = 1, bool ExportMode = false)
    {
        sql.Append(String.Format("SELECT * FROM {0} WHERE 1=1", TableName));

        #region 將搜尋控制項的值轉換為 SQL Statement
        BackendSearchControl.ConvertControlToSQL(sql, SearchItems, plSearch, this.ViewState);
        #endregion

        sql.OrderBy("sID DESC");

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
        Query(1, true);
        patwGridView1.Export("ExcelOutput");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BackendSearchControl.Control_Binding(this.ViewState, plSearch);

        AspNetPager1.CurrentPageIndex = 1;
        Query();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Query(AspNetPager1.CurrentPageIndex);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void patwGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "exporttoexcel")
        {
            BackendSearchControl.Control_Binding(this.ViewState, plSearch);

            AspNetPager1.CurrentPageIndex = 1;
            Query();
        }

        
        	if (e.CommandName == "UpdatasValid")
        {
            db.Execute(String.Format("UPDATE {0} SET sValid=(sValid+1)%2 WHERE sID=@0", TableName), e.CommandArgument);
            Query(AspNetPager1.CurrentPageIndex);
        }
    }

    protected void patwGridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = patwGridView1.DataKeys[e.RowIndex].Value.ToString();
        db.Delete(TableName, "sID", null, id);

        Query(AspNetPager1.CurrentPageIndex);
    }
}
