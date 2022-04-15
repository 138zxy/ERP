using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WeightingSystem.UserControls
{
    public partial class SiloStatusRectangle : UserControl
    {
        public SiloStatusRectangle()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 筒仓名称
        /// </summary>
        public string SiloName { get; set; }

        /// <summary>
        /// 材料名称
        /// </summary>
        public string StuffName { get; set; }

        /// <summary>
        /// 当前库存(吨）
        /// </summary>
        public decimal Content { get ;set; }

        /// <summary>
        /// 最小容量(吨）
        /// </summary>
        public decimal? MinContent { get; set; }

        /// <summary>
        /// 最大容量(吨）
        /// </summary>
        public decimal? MaxContent { get; set; }

        /// <summary>
        /// 最小报警容量(吨）
        /// </summary>
        public decimal? MinWarn { get; set; }

        /// <summary>
        /// 最大报警容量(吨）
        /// </summary>
        public decimal? MaxWarn { get; set; }

        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="bmp"></param>
        public void SetSiloImage(Bitmap bmp)
        {
            this.pbSilo.Image = Image.FromHbitmap(bmp.GetHbitmap());
        }

        public PictureBox PictureBox
        {
            get { return this.pbSilo; }
        }


        /// <summary>
        /// 计算当前容量占最大容量的百分比
        /// </summary>
        /// <param name="content"></param>
        /// <param name="maxContent"></param>
        /// <returns></returns>
        private string CalculateContentPercentText()
        {
            if (!this.MaxContent.HasValue || this.MaxContent.Value == 0) return string.Empty;

            decimal rate = Math.Round(this.Content / this.MaxContent.Value, 4);

            if (rate > 1)
            {
                rate = 1;
            }
            else if (rate < 0)
            {
                rate = 0;
            }

            return string.Format("{0}%", rate * 100);
        }

        /// <summary>
        /// 生成库存图片
        /// </summary>
        /// <returns></returns>
        private Bitmap GenerateSiloImage()
        {
            int imgWidth = this.pbSilo.Width;
            int imgHeight = this.pbSilo.Height;

            Bitmap bmp = new Bitmap(imgWidth, imgHeight);
            Graphics grp = Graphics.FromImage(bmp);
            //grp.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, imgWidth, imgHeight));

            //填充色默认为绿色
            Color contentColor = ColorTranslator.FromHtml("#008000");
            if (MaxWarn.HasValue && Content >= MaxWarn) contentColor = ColorTranslator.FromHtml("#eb4949");
            if (MinWarn.HasValue && Content <= MinWarn) contentColor = ColorTranslator.FromHtml("#eb4949");

            int contentHeight = 0;
            if (MaxContent.HasValue && MaxContent != 0)
            {
                contentHeight = Convert.ToInt32(imgHeight * (Content / MaxContent.Value));
            }

            grp.FillRectangle(new SolidBrush(contentColor), 0, imgHeight - contentHeight, imgWidth, contentHeight);

            return bmp;
        }
        public void InitializeBeforeDisplay()
        {
            this.lblSiloName.Text = this.SiloName;
            this.lblStuffName.Text = "(" +  this.StuffName + ")";
            this.lblContent.Text = this.Content + " 吨";
            this.pbSilo.Image = Image.FromHbitmap(GenerateSiloImage().GetHbitmap());
        }     
    }
}
