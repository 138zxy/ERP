<%@ Page Title="混凝土综合结算单" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (Cfg_EntName != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
            }

            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            this.tb_InMonth.Text = DateTime.Now.ToString("yyyy-MM");
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
        var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
        if (Cfg_EntName != null)
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
        }
        LocalReport rpt = (LocalReport)e.Report;
        rpt.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));

        var ContractID = rpt.GetParameters()["ContractID"] == null ? "" : rpt.GetParameters()["ContractID"].Values[0].ToString();
        if (ContractID!="")
        {
            var Month = rpt.GetParameters()["ID"].Values.FirstOrDefault().Split('/')[0];
            rpt.SetParameters(new ReportParameter("InMonth", Month));
            //加载的第一个报表（各种抬头数据）
            SqlDataSource4.SelectParameters.Add("ID", rpt.GetParameters()["ID"].Values.FirstOrDefault());
            SqlDataSource4.SelectParameters.Add("type", "0");
            rpt.DataSources.Add(new ReportDataSource("DataSet1", "SqlDataSource4"));

            //加载的第二个报表
            SqlDataSource5.SelectParameters.Add("ID", rpt.GetParameters()["ID"].Values.FirstOrDefault());
            SqlDataSource5.SelectParameters.Add("type", "1");
            rpt.DataSources.Add(new ReportDataSource("DataSet2", "SqlDataSource5"));

            SqlDataSource6.SelectParameters.Add("ID", rpt.GetParameters()["ID"].Values.FirstOrDefault());
            SqlDataSource6.SelectParameters.Add("type", "2");
            rpt.DataSources.Add(new ReportDataSource("DataSet3", "SqlDataSource6"));
        }
        var InMonth = rpt.GetParameters()["ID"].Values.FirstOrDefault().Split('/')[0];
        rpt.SetParameters(new ReportParameter("InMonth", InMonth));
        //加载的第一个报表（各种抬头数据）
        subDataSource.SelectParameters.Add("ID", rpt.GetParameters()["ID"].Values.FirstOrDefault());
        subDataSource.SelectParameters.Add("type","0"); 
        rpt.DataSources.Add(new ReportDataSource("DataSet1", "subDataSource"));

        //加载的第二个报表
        SqlDataSource2.SelectParameters.Add("ID", rpt.GetParameters()["ID"].Values.FirstOrDefault());
        SqlDataSource2.SelectParameters.Add("type", "1");
        rpt.DataSources.Add(new ReportDataSource("DataSet2", "SqlDataSource2"));

        SqlDataSource3.SelectParameters.Add("ID", rpt.GetParameters()["ID"].Values.FirstOrDefault());
        SqlDataSource3.SelectParameters.Add("type", "2");
        rpt.DataSources.Add(new ReportDataSource("DataSet3", "SqlDataSource3"));
    }

</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var Date_Length = 10; //2013-01-01
        $.datepicker.setDefaults({ dateFormat: 'yy-mm' });
        if ($('#<%=tb_InMonth.ClientID %>').val().length <= Date_Length) {
            $('#<%=tb_InMonth.ClientID %>').datepicker();
        }
        else {
            $('#<%=tb_InMonth.ClientID %>').datetimepicker();
        }
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
 纳入月份：<asp:TextBox ID="tb_InMonth"  runat="server"></asp:TextBox>  
 工程名称：<asp:TextBox ID="tb_Project"  runat="server"></asp:TextBox>  
 合同名称：<asp:TextBox ID="tb_Contract"  runat="server"></asp:TextBox>  
 
 <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="700px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"  OnDrillthrough="ReportViewer1_Drillthrough">
        <LocalReport ReportPath="Reports\Finance\R020110.rdlc"  DisplayName="混凝土综合结算单">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_BenTon_All"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
          <SelectParameters>
            <asp:ControlParameter ControlID="tb_InMonth" Name="InMonth" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tb_Project" Name="Project" PropertyName="Text" />   
            <asp:ControlParameter ControlID="tb_Contract" Name="ContractName" PropertyName="Text" />   
          </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="subDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_BenTonAll_Detail"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource> 
     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_BenTonAll_Detail"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource> 
     <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_BenTonAll_Detail"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource> 
    
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_BenTonAll_Detail2"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_BenTonAll_Detail2"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_BenTonAll_Detail2"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource>  
</asp:Content>
