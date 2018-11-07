<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function OnClickButtonDel(s, e) {
            card.PerformCallback('Delete');
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxCardView ID="ASPxCardView1" KeyFieldName="ID" AutoGenerateColumns="False" runat="server" OnCellEditorInitialize="ASPxCardView1_CellEditorInitialize" OnCardInserting="ASPxCardView1_CardInserting" OnCardUpdating="ASPxCardView1_CardUpdating" ClientInstanceName="card" OnCustomCallback="ASPxCardView1_CustomCallback" OnDataBinding="ASPxCardView1_DataBinding">
            <Columns>
                <dx:CardViewTextColumn FieldName="ID" VisibleIndex="0">
                </dx:CardViewTextColumn>
                <dx:CardViewTextColumn FieldName="Data" VisibleIndex="1">
                </dx:CardViewTextColumn>
            </Columns>
            <CardLayoutProperties>
                <Items>
                    <dx:CardViewCommandLayoutItem HorizontalAlign="Right" ShowSelectCheckbox="True">
                    </dx:CardViewCommandLayoutItem>
                    <dx:CardViewColumnLayoutItem ColumnName ="ID" >
                    </dx:CardViewColumnLayoutItem>
                    <dx:CardViewColumnLayoutItem ColumnName="Data" >
                    </dx:CardViewColumnLayoutItem>
                    <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                    </dx:EditModeCommandLayoutItem>
                </Items>
            </CardLayoutProperties>
        </dx:ASPxCardView>
        <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btn1" AutoPostBack="false" Text="Delete">
            <ClientSideEvents Click="OnClickButtonDel"/>
        </dx:ASPxButton>
    

    </div>
    </form>
</body>
</html>
