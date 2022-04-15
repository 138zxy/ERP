using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeightingSystem.Models;
using WeightingSystem.UserControls;
using WeightingSystem.Helpers;


namespace WeightingSystem
{
    public partial class SiloStatus : Form
    {

        private ERPDataProvider edp = new ERPDataProvider();

        public SiloStatus()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            //获取ERP中所有筒仓信息
            IList<Silo> list = edp.GetAllSiloList();
            string currentProductLine = string.Empty;
            System.Drawing.Font ProductLineNameFont = new Font("微软雅黑", 15);

            for (int i = 0; i < list.Count; i++)
            {
                //显示每行中的"生产线名称"
                if (currentProductLine != list[i].ProductLineName)
                {
                    currentProductLine = list[i].ProductLineName;
                    Label lblProductLine = new Label()
                    {
                        Text = currentProductLine,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = ProductLineNameFont
                    };

                    this.flowLayoutPanel1.Controls.Add(lblProductLine);
                    toolTipSilo.SetToolTip(lblProductLine, "ProduceLine Label");
                }
                //构建每个筒仓的控件显示
                SiloStatusRectangle rect = new SiloStatusRectangle()
                {
                    SiloName = list[i].SiloName,
                    StuffName = list[i].StuffName,
                    Content =  ConvertKg2T(list[i].Content) ?? 0,
                    MaxContent = ConvertKg2T(list[i].MaxContent),
                    MinContent = ConvertKg2T(list[i].MinWarm),
                    MaxWarn = ConvertKg2T(list[i].MaxWarm),
                    MinWarn = ConvertKg2T(list[i].MinWarm)
                };

                rect.InitializeBeforeDisplay();
                this.flowLayoutPanel1.Controls.Add(rect);
                //提示
                string siloToolTipText = string.Format("当前库存:{0}吨\r\n筒仓容量:{1}吨\r\n最大报警容量:{2}吨\r\n最小报警容量:{3}",
                                                        ConvertKg2T(list[i].Content) ?? 0,
                                                        ConvertKg2T(list[i].MaxContent),
                                                        ConvertKg2T(list[i].MinWarm),
                                                        ConvertKg2T(list[i].MaxWarm),
                                                        ConvertKg2T(list[i].MinWarm));
                toolTipSilo.SetToolTip(rect.PictureBox, siloToolTipText);

                if (i  <= list.Count -2 && list[i].ProductLineName != list[i + 1].ProductLineName)
                {
                    this.flowLayoutPanel1.SetFlowBreak(rect, true);
                }
            }

        }

        public List<Silo> GetSiloList()
        {
            List<Silo> list = new List<Silo>();
            Random r = new Random();
            for (int productLineIndex = 0; productLineIndex < 10; productLineIndex++)
            {
                for (int siloIndex = 0; siloIndex < 20; siloIndex++)
                {
                    Silo s = new Silo()
                    {
                        SiloName = "SiloName" + productLineIndex + "_" + siloIndex,
                        StuffName = "StuffName" + productLineIndex + "_" + siloIndex,
                        ProductLineID = productLineIndex.ToString().PadLeft(3, '0'),
                        ProductLineName = productLineIndex + "#线",
                        Content = r.Next(0, 200),
                        MaxContent = r.Next(0, 1000),
                        MinContent = 0,
                        MaxWarm = r.Next(0, 1000),
                        MinWarm = r.Next(0, 200)
                    };
                    list.Add(s);
                }
            }
            return list;
        }

        private decimal? ConvertKg2T(decimal? kgValue)
        {
            if (!kgValue.HasValue) return null;
            else return Math.Round(kgValue.Value / 1000, 2);
        }
    }
}
