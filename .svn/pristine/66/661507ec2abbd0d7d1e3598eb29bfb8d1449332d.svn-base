using System;
using System.Collections.Generic;
using System.Text;
using SpeechLib;
using WeightingSystem.Models;
using WeightingSystem.Helpers;
using FastReport;
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
            int LastValue = agh.getLastValue(agh.GetPrefix(c.prefixPat), c.idLen);
            string MetageID = agh.GenerateID(LastValue);
            int incrementCount = 0;
            while (edp.checkExistStuffInID(MetageID) > 0)           //校正磅单号，使得传送到ERP内的磅单号绝对不重复
            {
                LastValue += 1;
                incrementCount++;
                MetageID = agh.GenerateID(LastValue);
            }
            si.LastValue = incrementCount;
            return MetageID;
        }


    

        /// <summary>
        /// 设置视频参数
        /// </summary>
        public void SetVidQuality(int width,int height)
        {
            Video.DSStream_ChooseVideoCompressor("\0");
            Video.DSStream_SetVideoCompressorQuality(Config.Ini.GetInt16(Config.Section.BaseSetting, Config.ConfigKey.VideoQuality, 50));
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
            using (Report report = new Report())
            {

                decimal PrintTotalNum = _si.TotalNum;
                decimal PrintCarWeight = _si.CarWeight;
                decimal PrintInNum = _si.InNum;
                decimal PrintWRate = _si.WRate;

                if (Config.Ini.GetString(Config.Section.BaseSetting, Config.ConfigKey.PrintUnit, string.Empty) == "T")
                {
                    PrintTotalNum = PrintTotalNum / 1000;
                    PrintCarWeight = PrintCarWeight / 1000;
                    PrintInNum = PrintInNum/1000;
                    if (c.RateMode == 0)                //若显示T 而且用固定扣杂
                    {
                        PrintWRate = PrintWRate / 1000;
                    }
                }

                report.Load(MainForm.REPORT_PATH);
                report.SetParameterValue("过磅编号", _si.StuffInID);
                report.SetParameterValue("车辆编号", _si.CarNo);
                report.SetParameterValue("司机姓名", _si.Driver);
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
                report.SetParameterValue("净重", PrintInNum);
                report.SetParameterValue("材料规格", _si.Spec);
                report.SetParameterValue("扣重t", (PrintTotalNum - PrintCarWeight) * PrintWRate / 100);
                
                if (c.RateMode == 0)
                {
                    report.SetParameterValue("扣重", PrintWRate);
                }
                else
                {
                    report.SetParameterValue("扣重", PrintWRate.ToString("0") + "  %");
                }
                report.SetParameterValue("备注", _si.Remark);
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
                report.Report.PrintSettings.ShowDialog = false;
                report.Print();
            }
        }

        /// <summary>
        /// 混凝土过磅单打印（出厂）
        /// </summary>
        /// <param name="tz"></param>
        public void printShipDocConcreteReport(ShippingDocument ShipDoc)
        {
            using (Report report = new Report())
            {

                report.Load(MainForm.SHIPPINGDOCUMENT_REPORT_PATH);
                report.SetParameterValue("工程名称", ShipDoc.ProjectName);
                report.SetParameterValue("工程地址", ShipDoc.ProjectAddr);
                report.SetParameterValue("施工单位", ShipDoc.ConstructUnit);
                report.SetParameterValue("客户名称", ShipDoc.CustName);
                report.SetParameterValue("发货单号", ShipDoc.ShipDocID);
                report.SetParameterValue("车号", ShipDoc.CarID);
                report.SetParameterValue("砼强度", ShipDoc.ConStrength);
                report.SetParameterValue("塌落度", ShipDoc.RealSlump);
                report.SetParameterValue("浇筑方式", ShipDoc.CastMode);
                report.SetParameterValue("浇筑部位", ShipDoc.ConsPos);
                report.SetParameterValue("泵名称", ShipDoc.PumpName);
                report.SetParameterValue("毛重", ShipDoc.TotalWeight);
                report.SetParameterValue("皮重", ShipDoc.CarWeight);
                report.SetParameterValue("净重", ShipDoc.Weight);
                report.SetParameterValue("已供方量",ShipDoc.ProvidedCube);
                report.SetParameterValue("已供车次",ShipDoc.ProvidedTimes);
                report.SetParameterValue("换算率", ShipDoc.Exchange);
                report.SetParameterValue("过磅方量", ShipDoc.Cube);
                report.SetParameterValue("生产时间", ShipDoc.ProduceDate);
                report.SetParameterValue("生产线", ShipDoc.ProductLineName);
                report.SetParameterValue("称重人", MainForm.CurrentUserName);
                report.Report.PrintSettings.ShowDialog = false;
                report.Print();
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
    }
}
