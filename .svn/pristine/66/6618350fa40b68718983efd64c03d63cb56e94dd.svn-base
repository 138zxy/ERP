<%@ Page Title="施工配比日报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"
    AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //this.ReportViewer1.LocalReport.Refresh();
        }
        else
        {

            tbDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbProductLineID.DataSource = this.SqlDataSource2;
            this.tbProductLineID.DataTextField = "ProductLineName";
            this.tbProductLineID.DataValueField = "ProductLineID";
            this.tbProductLineID.DataBind();
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=tbDate.ClientID %>').datepicker();

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    报表日期：<asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
    生产线：<asp:DropDownList ID="tbProductLineID" runat="server" Width="150px">
    </asp:DropDownList>

    <!--新加了两个查询条件-->
    任务编号：<asp:TextBox ID="txtTaskId" Width="100" runat="server"></asp:TextBox>
    客户名称：<asp:TextBox ID="txtCustName" Width="100" runat="server"></asp:TextBox>
    工程名称：<asp:TextBox ID="tbProjectName" Width="100" runat="server"></asp:TextBox>
    砼强度：<asp:TextBox ID="tbConStrength" Width="100" runat="server"></asp:TextBox>
    产品类型：<asp:DropDownList ID="ddlProduct" runat="server" Width="150px">
                <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="混凝土" Value="0"></asp:ListItem>
                <asp:ListItem Text="湿拌" Value="1"></asp:ListItem>
              </asp:DropDownList>
    

    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="700px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410711.rdlc" DisplayName="施工配比日报表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_sg_daily"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbDate" Name="Date" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbProductLineID" Name="ProductLineID" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtTaskId" Name="TaskId" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtCustName" Name="CustName" PropertyName="Text" />
               <asp:ControlParameter ControlID="tbProjectName" Name="projectname" PropertyName="Text" />
               <asp:ControlParameter ControlID="tbConStrength" Name="conStrength" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="ddlProduct" Name="Product" PropertyName="Text" />    


        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select ''  ProductLineID, '' ProductLineName  union  select ProductLineID,ProductLineName from ProductLine  where IsUsed=1"
        SelectCommandType="Text" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
</asp:Content>
