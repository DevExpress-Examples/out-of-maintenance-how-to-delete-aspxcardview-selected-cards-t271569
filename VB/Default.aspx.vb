Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private table As DataTable = Nothing
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        If (Not IsPostBack) AndAlso (Not IsCallback) Then
            table = New DataTable()
            table.Columns.Add("ID", GetType(Integer))
            table.Columns.Add("Data", GetType(String))
            table.PrimaryKey = New DataColumn() { table.Columns("ID") }
            For i As Integer = 0 To 9
                table.Rows.Add(New Object() { i, "row " & i.ToString() })
            Next i
            Session("table") = table
        Else
            table = DirectCast(Session("table"), DataTable)
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If (Not IsCallback) AndAlso (Not IsPostBack) Then
            ASPxCardView1.DataBind()
        End If
    End Sub
    Protected Sub ASPxCardView1_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxCardViewEditorEventArgs)
        Dim grid2 As ASPxCardView = DirectCast(sender, ASPxCardView)
        If e.Column.FieldName = "ID" Then
            Dim textBox As ASPxTextBox = CType(e.Editor, ASPxTextBox)
            textBox.ClientEnabled = False
            If grid2.IsNewCardEditing Then
                table = DirectCast(Session("table"), DataTable)
                textBox.Text = GetNewId().ToString()
            End If
        End If
    End Sub
    Protected Sub ASPxCardView1_CardInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        table = DirectCast(Session("table"), DataTable)
        Dim cv As ASPxCardView = DirectCast(sender, ASPxCardView)
        Dim row As DataRow = table.NewRow()
        row("ID") = e.NewValues("ID")
        row("Data") = e.NewValues("Data")
        cv.CancelEdit()
        e.Cancel = True
        table.Rows.Add(row)
    End Sub
    Protected Sub ASPxCardView1_CardUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        table = DirectCast(Session("table"), DataTable)
        Dim row As DataRow = table.Rows.Find(e.Keys(0))
        row("Data") = e.NewValues("Data")
        ASPxCardView1.CancelEdit()
        e.Cancel = True
    End Sub

    Protected Sub ASPxCardView1_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        ASPxCardView1.DataSource = table
    End Sub
    #Region "#CustomCallbackMethod"
    Protected Sub ASPxCardView1_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxCardViewCustomCallbackEventArgs)
        If e.Parameters = "Delete" Then
            table = DirectCast(Session("table"), DataTable)
            Dim selectItems As List(Of Object) = ASPxCardView1.GetSelectedFieldValues("ID")
            For Each selectItemId As Object In selectItems
                table.Rows.Remove(table.Rows.Find(selectItemId))
            Next selectItemId
            ASPxCardView1.DataBind()
            ASPxCardView1.Selection.UnselectAll()
        End If
    End Sub
    #End Region ' #CustomCallbackMethod
    Private Function GetNewId() As Integer
        table = DirectCast(Session("table"), DataTable)
        If table.Rows.Count = 0 Then
            Return 0
        End If
        Dim max As Integer = Convert.ToInt32(table.Rows(0)("ID"))
        For i As Integer = 1 To table.Rows.Count - 1
            If Convert.ToInt32(table.Rows(i)("ID")) > max Then
                max = Convert.ToInt32(table.Rows(i)("ID"))
            End If
        Next i
        Return max + 1
    End Function
End Class