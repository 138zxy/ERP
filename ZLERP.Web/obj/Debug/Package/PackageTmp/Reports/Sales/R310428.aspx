<%@ Page Title="原材料销售报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!Page.IsPostBack)
        {
            //获取全局配置中的ChangeTime
            var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
        }
        this.ReportViewer1.LocalReport.DisplayName = string.Format("{0}({1}-{2})", this.Title, tbBeginDate.Text,tbEndDate.Text);
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        string CarNo = this.tbCarNo.Text == "" ? " " : this.tbCarNo.Text;
        string StuffName = this.tbStuffName.Text == "" ? " " : this.tbStuffName.Text;
        string SupplyID = this.tbSupplyID.Text == "" ? " " : this.tbSupplyID.Text;
        string CompName = this.tbCompName.Text == "" ? " " : this.tbCompName.Text;
        
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", this.tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", this.tbEndDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("CarNo", CarNo));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("StuffName", StuffName));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("SupplyID", SupplyID));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("CompName", CompName)); 
        this.ReportViewer1.LocalReport.Refresh();
    }

    public void refresh()
    {
        string CurrentYear = Session["YearAccount"] == null ? "ZLERP" : Session["YearAccount"].ToString();

        this.SqlDataSource1.ConnectionString = this.SqlDataSource1.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.ProviderName = this.SqlDataSource1.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.DataBind();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=tbBeginDate.ClientID %>').datetimepicker();
            $('#<%=tbEndDate.ClientID %>').datetimepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
           
        </asp:ScriptManager>
    出站起始时间：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    出站截止时间：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>　
    车牌号码：<asp:TextBox ID="tbCarNo" runat="server"></asp:TextBox>　
    材料名称：<asp:TextBox ID="tbStuffName" runat="server"></asp:TextBox>　
    供货单位：<asp:TextBox ID="tbSupplyID" runat="server"></asp:TextBox>　
    收货单位：<asp:TextBox ID="tbCompName" runat="server"></asp:TextBox>　
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
     
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Sales\R310428.rdlc"  DisplayName="原材料销售报表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_StuffSale"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                  <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />  
             　<asp:ControlParameter ControlID="tbCarNo" Name="CarNo" PropertyName="Text" />  
             　<asp:ControlParameter ControlID="tbStuffName" Name="StuffName" PropertyName="Text" />  
             <asp:ControlParameter ControlID="tbSupplyID" Name="SupplyID" PropertyName="Text" />  
             <asp:ControlParameter ControlID="tbCompName" Name="CompName" PropertyName="Text" />  
           </SelectParameters>
       </asp:SqlDataSource> 

</asp:Content>
