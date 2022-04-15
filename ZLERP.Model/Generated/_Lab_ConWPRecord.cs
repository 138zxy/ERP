
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 混凝土取样及拌合物工作性能检测记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_ConWPRecord : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Lab_ConWPRecordId);
            sb.Append(SamplingDate);
            sb.Append(ConStrength);
            sb.Append(ProjectName);
            sb.Append(ConsPos);
            sb.Append(CarNo);
            sb.Append(ProductLine);
            sb.Append(Slump);
            sb.Append(ExpansionDegree);
            sb.Append(WaterRetention);
            sb.Append(Cohesiveness);
            sb.Append(QualifiedJudgment);
            sb.Append(SamplingMan);
            sb.Append(Remark);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 试件编号
        /// </summary>
        [DisplayName("试件编号")]
        [StringLength(50)]
        public virtual string Lab_ConWPRecordId
        {
            get;
            set;
        }
        /// <summary>
        /// 取样日期
        /// </summary>
        [DisplayName("取样日期")]
        public virtual System.DateTime? SamplingDate
        {
            get;
            set;
        }
        /// <summary>
        /// 强度等级
        /// </summary>
        [DisplayName("强度等级")]
        [StringLength(50)]
        public virtual string ConStrength
        {
            get;
            set;
        }
        /// <summary>
        /// 工程名称
        /// </summary>
        [DisplayName("工程名称")]
        [StringLength(350)]
        public virtual string ProjectName
        {
            get;
            set;
        }
        /// <summary>
        /// 构件部位
        /// </summary>
        [DisplayName("构件部位")]
        [StringLength(50)]
        public virtual string ConsPos
        {
            get;
            set;
        }
        /// <summary>
        /// 取样车号
        /// </summary>
        [DisplayName("取样车号")]
        [StringLength(50)]
        public virtual string CarNo
        {
            get;
            set;
        }
        /// <summary>
        /// 生产线号
        /// </summary>
        [StringLength(50)]
        [DisplayName("生产线号")]
        public virtual string ProductLine
        {
            get;
            set;
        }
        /// <summary>
        /// 坍落度
        /// </summary>
        [DisplayName("坍落度")]
        [StringLength(50)]
        public virtual string Slump
        {
            get;
            set;
        }
        /// <summary>
        /// 扩展度
        /// </summary>
        [DisplayName("扩展度")]
        [StringLength(50)]
        public virtual string ExpansionDegree
        {
            get;
            set;
        }
        /// <summary>
        /// 保水性
        /// </summary>
        [DisplayName("保水性")]
        [StringLength(50)]
        public virtual string WaterRetention
        {
            get;
            set;
        }
        /// <summary>
        /// 粘聚性
        /// </summary>
        [DisplayName("粘聚性")]
        [StringLength(50)]
        public virtual string Cohesiveness
        {
            get;
            set;
        }
        /// <summary>
        /// 合格判定
        /// </summary>
        [DisplayName("合格判定")]
        [StringLength(50)]
        public virtual string QualifiedJudgment
        {
            get;
            set;
        }
        /// <summary>
        /// 取样人
        /// </summary>
        [DisplayName("取样人")]
        [StringLength(50)]
        public virtual string SamplingMan
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(350)]
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 运输单号
        /// </summary>
        [DisplayName("运输单号")]
        [StringLength(30)]
        public virtual string ShipDocID
        {
            get;
            set;
        }

        #endregion
    }
}
