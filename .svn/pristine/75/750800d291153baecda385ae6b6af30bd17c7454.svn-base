<%@ Page Title="混凝土开盘鉴定记录" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
        }
        else
        {
            if (Request["uid"] != null)
            {
                LocalReport rpt = (LocalReport)this.ReportViewer1.LocalReport;
                this.tbID.Text = Request["uid"].ToString();
                rpt.SetParameters(new ReportParameter("OP_ID", this.tbID.Text));
                //设置EnterpriseName参数为全局配置中的EnterpriseName
                var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
                if (entCfg != null)
                {
                    rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
                }
                this.ReportViewer1.LocalReport.Refresh();
            }
        }
       
    }



    ZLERP.Business.PublicService s = null;
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tbID.Text.Length > 0)
        {
            this.ReportViewer1.LocalReport.Refresh();
            LocalReport rpt = this.ReportViewer1.LocalReport;
            rpt.SetParameters(new ReportParameter("OP_ID", this.tbID.Text));
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (entCfg != null)
            {
                rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
            }


            //获得生产线
            if (s == null)
                s = new ZLERP.Business.PublicService();
            ZLERP.Model.ShippingDocument shipdoc = s.ShippingDocument.Get(this.tbID.Text);
            if (shipdoc != null)
            {
                string productlineID = shipdoc.ProductLineID;
                //获得生产线筒仓
                ZLERP.Model.ProductLine p = s.GetGenericService<ZLERP.Model.ProductLine>().Get(productlineID);
                IList<ZLERP.Model.SiloProductLine> siloList = p.SiloProductLines.Where(pp => pp.Rate > 0).ToList();
                int num = siloList.Count;//也是用料数
                if (num > 0)
                {
                    if (13 - num > 0)
                    {
                        System.Data.DataTable t = ((System.Data.DataView)SqlDataSource1.Select(new DataSourceSelectArguments())).Table;
                        if (t.Rows.Count == 0)
                            return;
                        //缺少
                        int q = 13 - num;
                        //盘数
                        //int time = t.Rows.Count / num;

                        System.Data.DataRow r;
                        //缺少列条数
                        //生成每盘数
                        object[] objs = t.Rows[0].ItemArray;
                        for (int j = 0; j < q; j++)
                        {
                            //循环数据
                            r = t.NewRow();
                            for (int k = 0; k < objs.Length; k++)
                            {
                                r[k] = objs[k];
                            }
                            r["pottimes"] = j + 1;
                            r["siloname"] = DBNull.Value; ;
                            r["stuffname"] = string.Format("<NULL{0}>", j);
                            r["ActualAmount"] = DBNull.Value;
                            r["TheoreticalAmount"] = DBNull.Value;
                            r["Amount"] = DBNull.Value;
                            r["PerAmount"] = DBNull.Value;
                            r["SupplyName"] = DBNull.Value;
                            r["Sizex"] = DBNull.Value;
                            t.Rows.Add(r);
                        }


                        ReportDataSource reportDataSource = new ReportDataSource();
                        reportDataSource.Value = t;
                        reportDataSource.Name = "DataSet1";
                        ReportViewer1.LocalReport.DataSources[0] = reportDataSource;
                        this.ReportViewer1.LocalReport.Refresh();
                    }
                }
            }
        }

      
        UpdateCardReport();
    }

    public void UpdateCardReport()
    {
        System.Xml.XmlDocument XMLDoc = new System.Xml.XmlDocument();
        XMLDoc.Load(Server.MapPath(@"R710843.rdlc"));
        System.Xml.XmlNamespaceManager xmn = new System.Xml.XmlNamespaceManager(XMLDoc.NameTable);
        xmn.AddNamespace("X", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");
        string strXPath;
        strXPath = "/X:Report/X:Body/X:ReportItems/X:Tablix/X:TablixBody";
        System.Xml.XmlNodeList NDList = XMLDoc.SelectSingleNode(strXPath, xmn).ChildNodes;

        string strXPath2 = "//X:Report//X:Body//X:ReportItems//X:Tablix//X:TablixBody//X:TablixColumns";
        System.Xml.XmlNodeList NDList2 = XMLDoc.SelectSingleNode(strXPath2, xmn).ChildNodes;
        
        System.Xml.XmlNode subNode = NDList.Item(0);
        /*
        subNode.ChildNodes.Count即为报表列数
        subNode.ChildNodes.Item(X).innerText即为第X列的列宽,X下标从0开始
        */
        string clength = 23.97112 / (subNode.ChildNodes.Count) + "cm";
        for (int i = 0; i < subNode.ChildNodes.Count; i++)
        {
            subNode.ChildNodes.Item(i).InnerText = clength;     // 修改第一列列宽为20mm
        }
        XMLDoc.Save(Server.MapPath(@"R710843.rdlc"));  // 写XML
        
        
        //System.Xml.XmlDocument XMLDoc = new System.Xml.XmlDocument();

        //XMLDoc.Load(Server.MapPath(@"R710843.rdlc"));//获取报表文件

        //System.Xml.XmlNamespaceManager xmn = new System.Xml.XmlNamespaceManager(XMLDoc.NameTable);
        //xmn.AddNamespace("X", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");
        //string strXPath;
        //strXPath = "//X:Report//X:Body//X:ReportItems//X:Tablix9//X:TablixBody//X:TablixColumns";
        //System.Xml.XmlNodeList NDList = XMLDoc.SelectSingleNode(strXPath, xmn).ChildNodes;
        //System.Xml.XmlNode subNode = NDList.Item(0); //获取默认宽度
        ////int listwith = Int32.Parse(OpreateDB.Query(MyConstant.ShouldFirstGetGoods).Tables[0].Rows[0][1].ToString());
        ////subNode.ChildNodes.Item(0).InnerText = (float)196 / (float)listwith + "mm";

        //// 修改列宽为20mm 因矩阵中的列式动态得到。所以只需要改本列宽度，后面的宽度一致
        //string clength = (float)23.97112*100 / (float)(subNode.ChildNodes.Count) + "mm";
        //for (int i = 0; i < subNode.ChildNodes.Count; i++)
        //{
        //    subNode.ChildNodes.Item(i).InnerText = clength;     // 修改第一列列宽为20mm
        //}
        //XMLDoc.Save(Server.MapPath(@"R710843.rdlc"));
        //this.ReportViewer1.LocalReport.Refresh();
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

    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        发货单号
        <asp:TextBox ID="tbID" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="查询" 
            Width="57px" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\QCC\R710843.rdlc"  DisplayName="混凝土开盘鉴定记录">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_ProducerecordDetail"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="True"  >
           <SelectParameters> 
                 <asp:ControlParameter ControlID="tbID" Name="ShipDocID" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>       
</asp:Content>
