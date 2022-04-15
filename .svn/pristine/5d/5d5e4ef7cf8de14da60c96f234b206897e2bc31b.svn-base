<%@ Page Title="原材料货款结算单" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        { 
        }
        else
        {
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            this.tb_InMonth.Text = DateTime.Now.ToString("yyyy-MM");
            
 
            this.tb_Supply.DataSource = this.SqlDataSource2;
            this.tb_Supply.DataTextField = "ShortName";
            this.tb_Supply.DataValueField = "SupplyID";
            this.tb_Supply.DataBind();


            this.tb_StuffInfo.DataSource = this.SqlDataSource3;
            this.tb_StuffInfo.DataTextField = "StuffName";
            this.tb_StuffInfo.DataValueField = "StuffID";
            this.tb_StuffInfo.DataBind();
            
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        LocalReport rpt = this.ReportViewer1.LocalReport;

 
    }

    protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
    {
        LocalReport rpt = (LocalReport)e.Report;
        //if (subDataSource.SelectParameters.Count == 6)
        //{
        //    subDataSource.SelectParameters.Remove(subDataSource.SelectParameters["InMonth"]);
        //    subDataSource.SelectParameters.Remove(subDataSource.SelectParameters["Supply"]);
        //    subDataSource.SelectParameters.Remove(subDataSource.SelectParameters["StuffInfo"]);
        //}
        subDataSource.SelectParameters.Add("ID", rpt.GetParameters()["ID"].Values.FirstOrDefault()); 
        rpt.DataSources.Add(new ReportDataSource("DataSet1", "subDataSource"));
    }

</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
      
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
 纳入月份：<asp:TextBox ID="tb_InMonth"  runat="server"></asp:TextBox>  
 供货厂家： <asp:DropDownList ID="tb_Supply" runat="server" Width="150px" >
    </asp:DropDownList>
 材料名称：<asp:DropDownList ID="tb_StuffInfo" runat="server" Width="150px" >
    </asp:DropDownList> 
 <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"  OnDrillthrough="ReportViewer1_Drillthrough">
        <LocalReport ReportPath="Reports\Bale\R220803.rdlc"  DisplayName="原材料货款结算单">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_stuffin_stats"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
          <SelectParameters>
            <asp:ControlParameter ControlID="tb_InMonth" Name="InMonth" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tb_Supply" Name="Supply" PropertyName="Text" />
            <asp:ControlParameter ControlID="tb_StuffInfo" Name="StuffInfo" PropertyName="Text" />    
          </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="subDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_StuffIn_Detail"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    
    </asp:SqlDataSource>

      <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="select ''  SupplyID, '' ShortName  union  SELECT SupplyID,ShortName FROM dbo.SupplyInfo WHERE IsUsed=1 AND SupplyKind IN('Su3','Su5') ORDER BY ShortName "  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="select ''  StuffID, '' StuffName  union  SELECT StuffID,StuffName FROM dbo.StuffInfo WHERE IsUsed=1 ORDER BY StuffName "  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
       </asp:SqlDataSource>
</asp:Content>
