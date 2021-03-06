using System;
using System.Collections.Generic;
using System.Text;
using SpeechLib;
using WeightingSystem.Models;
using WeightingSystem.Helpers;
using FastReport;
using log4net;
using System.Windows.Forms;
using System.IO;
namespace WeightingSystem.Helpers
{

    public class PublicHelper
    {
        public IList<string> GetCarList()
        {
            LocalDataProvider ldp = new LocalDataProvider();
            IList<Car> CarList = ldp.GetCarList("1=1");
            IList<string> CarNoList = new List<string>();
            foreach (Car car in CarList)
            {
                CarNoList.Add(car.CarNo);
            }
            CarNoList.Insert(0, string.Empty);
            return CarNoList;
        }

        /// <summary>
        /// 获得磅单号
        /// </summary>
        /// <returns></returns>
        public string getMetageNo(StuffIn si)
        {
            AutoGenerateHelper agh = new AutoGenerateHelper();
            ERPDataProvider edp = new ERPDataProvider();
            Config c = new Config();
            int NextValue = agh.getLastValue(agh.GetPrefix(c.prefixPat), c.idLen);
            si.IDPrefix = agh.GetPrefix(c.prefixPat);
            string MetageID = agh.GenerateID(NextValue);
            int incrementCount = 0;
            while (edp.checkExistStuffInID(MetageID) > 0)           //校正磅单号，使得传送到ERP内的磅单号绝对不重复
            {
                NextValue += 1;
                incrementCount++;
                MetageID = agh.GenerateID(NextValue);
            }
            si.NextValue = NextValue+1;
            return MetageID;
        }

        /// <summary>
        /// 获得原材料销售单号
        /// </summary>
        /// <returns></returns>
        public string getMetageNo(StuffSale sale)
        {
            AutoGenerateHelper agh = new AutoGenerateHelper();
            ERPDataProvider edp = new ERPDataProvider();
            Config c = new Config();
            int NextValue = agh.getLastValue_Sale("StuffSale", agh.GetPrefix(c.prefixPat_Sale));
            sale.IDPrefix = agh.GetPrefix(c.prefixPat_Sale);
            string MetageID = agh.GenerateID_Sale(NextValue);
            int incrementCount = 0;
            while (edp.checkExistStuffInID(MetageID) > 0)           //校正磅单号，使得传送到ERP内的磅单号绝对不重复
            {
                NextValue += 1;
                incrementCount++;
                MetageID = agh.GenerateID_Sale(NextValue);
            }
            sale.NextValue = NextValue + 1;
            return MetageID;
        }

        /// <summary>
        /// 设置视频参数
        /// </summary>
        public void SetVidQuality(int width,int height)
        {
            Video.DSStream_ChooseVideoCompressor("\0");
            Video.DSStream_SetVideoCompressorQuality(Config.Ini.GetInt32(Config.Section.BaseSetting, Config.ConfigKey.VideoQuality, 50));
            Video.BITMAPINFOHEADER bm = new Video.BITMAPINFOHEADER();

            bm.biWidth = width;
            bm.biHeight = height;
            Video.VIDEOSTREAMINFO vs = new Video.VIDEOSTREAMINFO();
            vs.subtype = Video.VideoSubType.VideoSubType_YUY2;
            vs.bmiHeader = bm;

            Video.DSStream_SetVideoInfo(0, vs, 1);
           
            Video.DSStream_ShowDate(0, true, 10, 5);
            Video.DSStream_ShowTime(0, true, 150, 5);
        }

        public void Print(StuffIn _si)
        {
            Config c = new Config();          
            if (c.PrintStuffinReportCheck)//可选择报表模板
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Environment.CurrentDirectory + "\\report\\diy\\";
                ofd.Filter = "报表文件(*.frx)|*.frx";
                if (!Directory.Exists(ofd.InitialDirectory))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(ofd.InitialDirectory);
                }
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    printT(_si,ofd.FileName);
                }
            }
            else
            {
                printT(_si, MainForm.REPORT_PATH);
            }
        }
        private void printT(StuffIn _si,string filepath)
        {
            using (Report report = new Report())
            {
                ERPDataProvider edp = new ERPDataProvider();
                string Price = edp.getPriceByStuffInID(_si.StuffInID);
                decimal money = Convert.ToDecimal(Price) * (_si.InNum / 1000);
                int Allmoney = int.Parse((money / 10).ToString().Substring(0, (money / 10).ToString().IndexOf('.'))) * 10;
                //Int32 Allmoney = Convert.ToInt32(Math.Round((decimal)money, 1, MidpointRounding.AwayFromZero) * 100);
                decimal PrintTotalNum = _si.TotalNum;
                decimal PrintCarWeight = _si.CarWeight;
                decimal PrintInNum = _si.InNum;
                decimal PrintWRate = _si.WRate;
                string weightnum = NumToChinese(PrintInNum/1000)+"吨";
                //if (Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.PrintUnit, string.Empty) == "T")
                //{
                //    PrintTotalNum = PrintTotalNum / 1000;
                //    PrintCarWeight = PrintCarWeight / 1000;
                //    PrintInNum = PrintInNum / 1000;
                //    if (c.RateMode == 0)                //若显示T 而且用固定扣杂
                //    {
                //        PrintWRate = PrintWRate / 1000;
                //    }
                //}
                
                //report.Load(MainForm.REPORT_PATH);
                report.Load(filepath);
                report.SetParameterValue("过磅编号", _si.StuffInID);
                report.SetParameterValue("车辆编号", _si.CarNo);
                report.SetParameterValue("司机", _si.Driver);
                report.SetParameterValue("材料名称", _si.StuffName);
                report.SetParameterValue("筒仓编号", _si.SiloID);
                report.SetParameterValue("筒仓名称", _si.SiloName);
                report.SetParameterValue("供货厂家", _si.SupplyName);
                report.SetParameterValue("运输厂家", _si.TransportName);
                report.SetParameterValue("过磅员", MainForm.CurrentUserName);
                report.SetParameterValue("毛重", PrintTotalNum);
                report.SetParameterValue("毛重称量时间", _si.InDate);
                report.SetParameterValue("皮重", PrintCarWeight);
                report.SetParameterValue("皮重称量时间", _si.OutDate);
                report.SetParameterValue("净重中文", weightnum);
                report.SetParameterValue("材料规格", _si.Spec);
                report.SetParameterValue("扣重t", PrintWRate);
                report.SetParameterValue("扣重", _si.MingWeight);

                //if (c.RateMode == 0)
                //{
                //    report.SetParameterValue("扣重", _si.MingWeight);
                //}
                //else
                //{
                //    report.SetParameterValue("扣重", PrintWRate.ToString("0") + "  %");
                //}
                report.SetParameterValue("备注", _si.Remark);
                report.SetParameterValue("厂商数量", _si.SupplyNum);
                report.SetParameterValue("来源单据号", _si.SourceNumber);
                report.SetParameterValue("运输单位", _si.TransportName);
                report.SetParameterValue("方数", _si.FootNum);
                report.SetParameterValue("收货单位", _si.CompanyName);
                report.SetParameterValue("材料来源地", _si.SourceAddr);
                report.SetParameterValue("方量", _si.Volume);
                report.SetParameterValue("比重", _si.Proportion);
                report.SetParameterValue("单价", Price);
                report.SetParameterValue("总金额", Allmoney.ToString());
                decimal jiesuanNum = _si.SupplyNum < _si.InNum ? _si.SupplyNum : _si.InNum;
                report.SetParameterValue("结算数量", PrintInNum.ToString());
                report.SetParameterValue("净重日成", jiesuanNum.ToString());
                report.SetParameterValue("净重", _si.InNum.ToString());
                report.Report.PrintSettings.ShowDialog = false;
                report.Print();
            }
        }

        /// <summary>
        /// 混凝土过磅单打印（转退料）
        /// </summary>
        /// <param name="tz"></param>
        public void printTzConcreteReport(TZrelation tz)
        {
            using (Report report = new Report())
            {             
                report.Load(MainForm.TZCONCRETEREPORT_PATH);
                report.SetParameterValue("标题", tz.Title);
                report.SetParameterValue("工程名称", tz.ProjectName);
                report.SetParameterValue("客户名称", tz.CustName);
                report.SetParameterValue("发货单号", tz.SourceShipDocID);
                report.SetParameterValue("车号", tz.CarID);
                report.SetParameterValue("砼强度", tz.ConStrength);
                report.SetParameterValue("浇筑方式", tz.CastMode);
                report.SetParameterValue("浇筑部位", tz.ConsPos);
                report.SetParameterValue("毛重", tz.TotalWeight);
                report.SetParameterValue("皮重", tz.CarWeight);
                report.SetParameterValue("净重", tz.Weight);
                report.SetParameterValue("换算率", tz.Exchange);
                report.SetParameterValue("方量", tz.Cube);
                report.SetParameterValue("过磅时间", tz.BuildTime);
                report.SetParameterValue("称重人", MainForm.CurrentUserName);
                report.SetParameterValue("剩退类型", tz.Type);
                report.Report.PrintSettings.ShowDialog = false;
                report.Print();
            }
        }

        /// <summary>
        /// 混凝土过磅单打印（出厂）
        /// </summary>
        /// <param name="tz"></param>
        static ILog log = LogManager.GetLogger(typeof(PublicHelper));
        public void printShipDocConcreteReport(ShippingDocument ShipDoc)
        {
            using (Report report = new Report())
            {
                ERPDataProvider edp = new ERPDataProvider();
                report.Load(MainForm.SHIPPINGDOCUMENT_REPORT_PATH);
                string CarNo = edp.getCarNoByCarID(ShipDoc.CarID);
                string BetonTag = edp.getBetonTagByShipDocID(ShipDoc.ShipDocID);
                report.SetParameterValue("工程名称", ShipDoc.ProjectName);
                report.SetParameterValue("工程地址", ShipDoc.ProjectAddr);
                report.SetParameterValue("施工单位", ShipDoc.ConstructUnit);
                report.SetParameterValue("客户名称", ShipDoc.CustName);
                report.SetParameterValue("抗渗等级", ShipDoc.ImpGrade);
                report.SetParameterValue("工地电话", ShipDoc.Tel);
                report.SetParameterValue("标题", ShipDoc.Title);
                report.SetParameterValue("发货单号", ShipDoc.ShipDocID);
                report.SetParameterValue("车号", ShipDoc.CarID);
                report.SetParameterValue("车辆编号", CarNo);
                
                report.SetParameterValue("出厂时间", ShipDoc.DeliveryTime);
                report.SetParameterValue("司机", ShipDoc.Driver);
                report.SetParameterValue("砼强度", ShipDoc.ConStrength);
                report.SetParameterValue("坍落度", ShipDoc.RealSlump);
                report.SetParameterValue("浇筑方式", ShipDoc.CastMode);
                report.SetParameterValue("浇筑部位", ShipDoc.ConsPos);
                report.SetParameterValue("泵名称", ShipDoc.PumpName);
                report.SetParameterValue("毛重", ShipDoc.TotalWeight);
                report.SetParameterValue("皮重", ShipDoc.CarWeight);
                report.SetParameterValue("净重", ShipDoc.Weight);
                report.SetParameterValue("已供方量", ShipDoc.ProvidedCube);
                report.SetParameterValue("已供车次", ShipDoc.ProvidedTimes);
                report.SetParameterValue("调度已供方量", ShipDoc.ProvidedCube_Dis);
                report.SetParameterValue("调度已供车次", ShipDoc.ProvidedTimes_Dis);
                report.SetParameterValue("换算率", ShipDoc.Exchange);
                report.SetParameterValue("过磅方量", ShipDoc.Cube);
                report.SetParameterValue("生产时间", ShipDoc.ProduceDate);
                report.SetParameterValue("生产线", ShipDoc.ProductLineName);
                report.SetParameterValue("称重人", ShipDoc.WeightMan);
                report.SetParameterValue("备注", ShipDoc.Remark);
                report.SetParameterValue("发货单类型", ShipDoc.ShipDocType);

                report.SetParameterValue("任务单号", ShipDoc.TaskID);
                report.SetParameterValue("计划方量", ShipDoc.PlanCube);
                report.SetParameterValue("出票方量", ShipDoc.ParCube);
                report.SetParameterValue("砂浆方量", ShipDoc.SlurryCount);
                report.SetParameterValue("运输方量", ShipDoc.ShippingCube);
                report.SetParameterValue("前场工长", ShipDoc.LinkMan);
                report.SetParameterValue("业务员", ShipDoc.Salesman);

                report.SetParameterValue("操作员", ShipDoc.Operator);
                report.SetParameterValue("司机", ShipDoc.Driver);
                report.SetParameterValue("理论配比名称", ShipDoc.FormulaName);
                report.SetParameterValue("施工配比号", ShipDoc.ConsMixpropID);
                report.SetParameterValue("其他要求", ShipDoc.OtherDemand);
                report.SetParameterValue("质检员", ShipDoc.Surveyor);
                report.SetParameterValue("运距", ShipDoc.Distance);
                report.SetParameterValue("砼标记", BetonTag);


                report.Report.PrintSettings.ShowDialog = false;
                string str = "\r\n工程名称:" + ShipDoc.ProjectName
                           + "\r\n工程地址:" + ShipDoc.ProjectAddr
                           + "\r\n施工单位:" + ShipDoc.ConstructUnit
                           + "\r\n客户名称:" + ShipDoc.CustName
                           + "\r\n抗渗等级:" + ShipDoc.ImpGrade
                           + "\r\n工地电话:" + ShipDoc.Tel
                           + "\r\n标题:" + ShipDoc.Title
                           + "\r\n发货单号:" + ShipDoc.ShipDocID
                           + "\r\n车号:" + ShipDoc.CarID
                           + "\r\n出厂时间:" + ShipDoc.DeliveryTime
                           + "\r\n司机:" + ShipDoc.Driver
                           + "\r\n砼强度:" + ShipDoc.ConStrength
                           + "\r\n坍落度:" + ShipDoc.RealSlump
                           + "\r\n浇筑方式:" + ShipDoc.CastMode
                           + "\r\n浇筑部位:" + ShipDoc.ConsPos
                           + "\r\n泵名称:" + ShipDoc.PumpName
                           + "\r\n毛重:" + ShipDoc.TotalWeight
                           + "\r\n皮重:" + ShipDoc.CarWeight
                           + "\r\n净重:" + ShipDoc.Weight
                           + "\r\n已供方量:" + ShipDoc.ProvidedCube
                           + "\r\n已供车次:" + ShipDoc.ProvidedTimes
                           + "\r\n换算率:" + ShipDoc.Exchange
                           + "\r\n过磅方量:" + ShipDoc.Cube
                           + "\r\n生产时间:" + ShipDoc.ProduceDate
                           + "\r\n生产线:" + ShipDoc.ProductLineName
                           + "\r\n称重人:" + ShipDoc.WeightMan
                           + "\r\n备注:" + ShipDoc.Remark;
                //log.Debug(str);
                log.Debug("\r\n---------------------------过磅单打印打印中......------------------------");
                report.Print();
                log.Debug("\r\n---------------------------过磅单打印打印完成------------------------");
            }

        }
        public void printShipDocConcreteReportGH(ShippingDocumentGH ShipDoc)
        {
            using (Report report = new Report())
            {
                ERPDataProvider edp = new ERPDataProvider();
                report.Load(MainForm.SHIPPINGDOCUMENTGH_REPORT_PATH);
                ShipDoc.Tel = edp.getTelByShippingDocumentGH(ShipDoc.ShipDocID);
                string CarNo = edp.getCarNoByCarID(ShipDoc.CarID);
                report.SetParameterValue("工程名称", ShipDoc.ProjectName);
                report.SetParameterValue("工程地址", ShipDoc.ProjectAddr);
                report.SetParameterValue("施工单位", ShipDoc.ConstructUnit);
                report.SetParameterValue("客户名称", ShipDoc.CustName);
                report.SetParameterValue("抗渗等级", ShipDoc.ImpGrade);
                report.SetParameterValue("工地电话", ShipDoc.Tel);
                report.SetParameterValue("标题", ShipDoc.Title);
                report.SetParameterValue("发货单号", ShipDoc.ShipDocID);
                report.SetParameterValue("车号", ShipDoc.CarID);

                report.SetParameterValue("出厂时间", ShipDoc.DeliveryTime);
                report.SetParameterValue("司机", ShipDoc.Driver);
                report.SetParameterValue("砼强度", ShipDoc.ConStrength);
                report.SetParameterValue("坍落度", ShipDoc.RealSlump);
                report.SetParameterValue("浇筑方式", ShipDoc.CastMode);
                report.SetParameterValue("浇筑部位", ShipDoc.ConsPos);
                report.SetParameterValue("泵名称", ShipDoc.PumpName);
                report.SetParameterValue("毛重", ShipDoc.TotalWeight);
                report.SetParameterValue("皮重", ShipDoc.CarWeight);
                report.SetParameterValue("净重", ShipDoc.Weight);
                report.SetParameterValue("已供方量", ShipDoc.ProvidedCube);
                report.SetParameterValue("已供车次", ShipDoc.ProvidedTimes);
                report.SetParameterValue("换算率", ShipDoc.Exchange);
                report.SetParameterValue("过磅方量", ShipDoc.Cube);
                report.SetParameterValue("生产时间", ShipDoc.ProduceDate);
                report.SetParameterValue("生产线", ShipDoc.ProductLineName);
                report.SetParameterValue("称重人", ShipDoc.WeightMan);
                report.SetParameterValue("备注", ShipDoc.Remark);
                report.SetParameterValue("发货单类型", ShipDoc.ShipDocType);

                report.SetParameterValue("任务单号", ShipDoc.TaskID);
                report.SetParameterValue("计划方量", ShipDoc.PlanCube);
                report.SetParameterValue("砂浆方量", ShipDoc.SlurryCount);
                report.SetParameterValue("运输方量", ShipDoc.ShippingCube);
                report.SetParameterValue("前场工长", ShipDoc.LinkMan);
                report.SetParameterValue("业务员", ShipDoc.Salesman);

                report.SetParameterValue("操作员", ShipDoc.Operator);
                report.SetParameterValue("客户筒仓号", ShipDoc.CustSiloNo);
                report.SetParameterValue("司机", ShipDoc.Driver);
                report.SetParameterValue("配比名称", ShipDoc.FormulaName);
                report.SetParameterValue("车辆编号", CarNo);


                report.Report.PrintSettings.ShowDialog = false;
                string str = "\r\n工程名称:" + ShipDoc.ProjectName
                           + "\r\n工程地址:" + ShipDoc.ProjectAddr
                           + "\r\n施工单位:" + ShipDoc.ConstructUnit
                           + "\r\n客户名称:" + ShipDoc.CustName
                           + "\r\n抗渗等级:" + ShipDoc.ImpGrade
                           + "\r\n工地电话:" + ShipDoc.Tel
                           + "\r\n标题:" + ShipDoc.Title
                           + "\r\n发货单号:" + ShipDoc.ShipDocID
                           + "\r\n车号:" + ShipDoc.CarID
                           + "\r\n出厂时间:" + ShipDoc.DeliveryTime
                           + "\r\n司机:" + ShipDoc.Driver
                           + "\r\n砼强度:" + ShipDoc.ConStrength
                           + "\r\n坍落度:" + ShipDoc.RealSlump
                           + "\r\n浇筑方式:" + ShipDoc.CastMode
                           + "\r\n浇筑部位:" + ShipDoc.ConsPos
                           + "\r\n泵名称:" + ShipDoc.PumpName
                           + "\r\n毛重:" + ShipDoc.TotalWeight
                           + "\r\n皮重:" + ShipDoc.CarWeight
                           + "\r\n净重:" + ShipDoc.Weight
                           + "\r\n已供方量:" + ShipDoc.ProvidedCube
                           + "\r\n已供车次:" + ShipDoc.ProvidedTimes
                           + "\r\n换算率:" + ShipDoc.Exchange
                           + "\r\n过磅方量:" + ShipDoc.Cube
                           + "\r\n生产时间:" + ShipDoc.ProduceDate
                           + "\r\n生产线:" + ShipDoc.ProductLineName
                           + "\r\n称重人:" + ShipDoc.WeightMan
                           + "\r\n备注:" + ShipDoc.Remark;
                //log.Debug(str);
                log.Debug("\r\n---------------------------过磅单打印打印中......------------------------");
                report.Print();
                log.Debug("\r\n---------------------------过磅单打印打印完成------------------------");
            }

        }

        /// <summary>
        /// 干混过磅单打印（出厂）
        /// </summary>
        /// <param name="tz"></param>
        public void printShipDocGHReport(ShippingDocumentGH ShipDoc)
        {
            using (Report report = new Report())
            {
                report.Load(MainForm.SHIPPINGDOCUMENTGH_REPORT_PATH);
                report.SetParameterValue("运输单号", ShipDoc.ShipDocID);
                report.SetParameterValue("过磅时间", ShipDoc.ProduceDate);
                report.SetParameterValue("收货单位", ShipDoc.CustName);
                report.SetParameterValue("工程名称", ShipDoc.ProjectName);
                report.SetParameterValue("货物名称", ShipDoc.ConStrength);
                report.SetParameterValue("车号", ShipDoc.CarID);
                report.SetParameterValue("供货单位", ShipDoc.SupplyUnit);
                report.SetParameterValue("毛重", ShipDoc.TotalWeight);
                report.SetParameterValue("皮重", ShipDoc.CarWeight);
                report.SetParameterValue("净重", ShipDoc.Weight);
                report.SetParameterValue("过磅员", ShipDoc.Builder);

                report.Report.PrintSettings.ShowDialog = false;
                log.Debug("\r\n---------------------------干混过磅单打印打印中......------------------------");
                report.Print();
                log.Debug("\r\n---------------------------干混过磅单打印打印完成------------------------");
            }

        }
        /// <summary>
        /// 材料出厂过磅单
        /// </summary>
        /// <param name="entity"></param>
        public void printStuffSaleReport(StuffSale entity)
        {
            using (Report report = new Report())
            {
                report.Load(MainForm.StuffSale_REPORT_PATH);
                report.SetParameterValue("磅单号", entity.StuffSaleID);
                report.SetParameterValue("进厂时间", entity.ArriveTime);
                report.SetParameterValue("出厂时间", entity.DeliveryTime);
                report.SetParameterValue("供货单位", entity.CompName);
                report.SetParameterValue("收货单位", entity.SupplyID);
                report.SetParameterValue("材料名称", entity.StuffName);
                report.SetParameterValue("车号", entity.CarNo);
                report.SetParameterValue("毛重",Math.Round(((decimal)entity.TotalWeight / 1000),2));
                report.SetParameterValue("皮重",Math.Round(((decimal)entity.CarWeight / 1000),2));
                report.SetParameterValue("净重",Math.Round(((decimal)entity.Weight / 1000),2));
                report.SetParameterValue("过磅员", entity.Builder);
                report.SetParameterValue("备注", entity.Remark);

                report.Report.PrintSettings.ShowDialog = false;
                log.Debug("\r\n---------------------------材料出厂过磅单打印打印中......------------------------");
                report.Print();
                log.Debug("\r\n---------------------------材料出厂过磅单打印打印完成------------------------");
            }

        }

        public List<ListItem> bindStuffType()
        {
            List<ListItem> itemList = new List<ListItem>();
            ListItem li = new ListItem();
            li.Value = "FA";
            li.Text = "砂子";
            itemList.Add(li);
            li = new ListItem();
            li.Value = "CA";
            li.Text = "石子";
            itemList.Add(li);
            li = new ListItem();
            li.Value = "CE";
            li.Text = "水泥";
            itemList.Add(li);
            li = new ListItem();
            li.Value = "WA";
            li.Text = "水";
            itemList.Add(li);
            li = new ListItem();
            li.Value = "AIR";
            li.Text = "掺合料";
            itemList.Add(li);
            li = new ListItem();
            li.Value = "ADM";
            li.Text = "外加剂";
            itemList.Add(li);
            li = new ListItem();
            li.Value = "OTHER";
            li.Text = "其他";
            itemList.Add(li);

            return itemList;
        }

        /// <summary>
        /// 数字转大写
        /// </summary>
        /// <param name="Num">数字</param>/// <returns></returns>
        public  string NumToChinese(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾点角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    //str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零";
                //str5 = "零点整";
            }
            str5 = str5.Replace("角", "");
            str5 = str5.Replace("分", "");
            return str5;
        }

    }
}
