using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page
{
    DataTable table = null;
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Data", typeof(string));
            table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };
            for (int i = 0; i < 10; i++)
            {
                table.Rows.Add(new object[] { i, "row " + i.ToString() });
            }
            Session["table"] = table;
        }
        else
            table = (DataTable)Session["table"];
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsCallback && !IsPostBack) ASPxCardView1.DataBind();
    }
    protected void ASPxCardView1_CellEditorInitialize(object sender, DevExpress.Web.ASPxCardViewEditorEventArgs e)
    {
        ASPxCardView grid2 = (ASPxCardView)sender;
        if (e.Column.FieldName == "ID")
        {
            ASPxTextBox textBox = (ASPxTextBox)e.Editor;
            textBox.ClientEnabled = false;
            if (grid2.IsNewCardEditing)
            {
                table = (DataTable)Session["table"];
                textBox.Text = GetNewId().ToString();
            }
        }
    }
    protected void ASPxCardView1_CardInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        table = (DataTable)Session["table"];
        ASPxCardView cv = (ASPxCardView)sender;
        DataRow row = table.NewRow();
        row["ID"] = e.NewValues["ID"];
        row["Data"] = e.NewValues["Data"];
        cv.CancelEdit();
        e.Cancel = true;
        table.Rows.Add(row);
    }
    protected void ASPxCardView1_CardUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        table = (DataTable)Session["table"];
        DataRow row = table.Rows.Find(e.Keys[0]);
        row["Data"] = e.NewValues["Data"];
        ASPxCardView1.CancelEdit();
        e.Cancel = true;
    }

    protected void ASPxCardView1_DataBinding(object sender, EventArgs e)
    {
        ASPxCardView1.DataSource = table;
    }
    #region #CustomCallbackMethod
    protected void ASPxCardView1_CustomCallback(object sender, DevExpress.Web.ASPxCardViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Delete")
        {
            table = (DataTable)Session["table"];
            List<Object> selectItems = ASPxCardView1.GetSelectedFieldValues("ID");
            foreach (object selectItemId in selectItems)
            {
                table.Rows.Remove(table.Rows.Find(selectItemId));
            }
            ASPxCardView1.DataBind();
            ASPxCardView1.Selection.UnselectAll();
        }
    }
    #endregion #CustomCallbackMethod
    private int GetNewId()
    {
        table = (DataTable)Session["table"];
        if (table.Rows.Count == 0) return 0;
        int max = Convert.ToInt32(table.Rows[0]["ID"]);
        for (int i = 1; i < table.Rows.Count; i++)
        {
            if (Convert.ToInt32(table.Rows[i]["ID"]) > max)
                max = Convert.ToInt32(table.Rows[i]["ID"]);
        }
        return max + 1;
    }
}