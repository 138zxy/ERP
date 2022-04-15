using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  生产调度
    /// </summary>
    public class DispatchListGH : _DispatchListGH
    {

        [Required]
        public override string ProductLineID
        {
            get
            {
                return base.ProductLineID;
            }
            set
            {
                base.ProductLineID = value;
            }
        }

        [Required]
        public override decimal? PCRate
        {
            get
            {
                return base.PCRate;
            }
            set
            {
                base.PCRate = value;
            }
        }

        /// <summary>
        /// 工程名称
        /// </summary>
        public virtual string ProjectName
        {
            get{
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.ProjectName; 
            }
        }

        /// <summary>
        /// 砼强度
        /// </summary>
        public virtual string ConStrength 
        {
            get{
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.ConStrength; 
            }
        }

        /// <summary>
        /// 浇筑方式
        /// </summary>
        public virtual string CastMode 
        {
            get{
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.CastMode; 
            }
        }


        /// <summary>
        /// 施工部位
        /// </summary>
        public virtual string ConsPos 
        {
            get{
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.ConsPos; 
            }
        }

        /// <summary>
        /// 车号
        /// </summary>
        public virtual string CarID 
        {
            get{
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.CarID; 
            }
        } 
 
        /// <summary>
        /// 累计车数
        /// </summary>
        public virtual int? ProvidedTimes 
        {
            get{
                return ShippingDocumentGH == null ? 0 : ShippingDocumentGH.ProvidedTimes; 
            }
        } 
 
        /// <summary>
        /// 已供方量
        /// </summary>
        public virtual decimal ProvidedCube 
        {
            get{
                return ShippingDocumentGH == null ? 0 : ShippingDocumentGH.ProvidedCube; 
            }
        }

        /// <summary>
        /// 计划方量
        /// </summary>
        public virtual decimal PlanCube
        {
            get
            {
                return ShippingDocumentGH == null ? 0 : ShippingDocumentGH.PlanCube;
            }
        } 

        /// <summary>
        /// 出票方量
        /// </summary>
        public virtual decimal ParCube 
        {
            get{
                return ShippingDocumentGH == null ? 0 : ShippingDocumentGH.ParCube; 
            }
        } 
 
        /// <summary>
        /// 司机姓名
        /// </summary>
        public virtual string Driver 
        {
            get{
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.Driver; 
            }
        } 
        /// <summary>
        /// 发货员
        /// </summary>
        public virtual string Signer 
        {
            get{
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.Signer; 
            }
        } 
         

        /// <summary>
        /// 发货单备注
        /// </summary>
        public override string Remark 
        {
            get{
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.Remark; 
            }
        }

        /// <summary>
        /// 打印次数
        /// </summary>
        public virtual int? PrintCount
        {
            get
            {
                return ShippingDocumentGH == null ? 0 : ShippingDocumentGH.PrintCount;
            }
        }

        /// <summary>
        /// 其他方量
        /// </summary>
        public virtual decimal OtherCube
        {
            get
            {
                return ShippingDocumentGH == null ? 0 : ShippingDocumentGH.OtherCube;
            }
        }

        /// <summary>
        /// 虚方量
        /// </summary>
        public virtual decimal? XuCube
        {
            get
            {
                return ShippingDocumentGH == null ? 0 : ShippingDocumentGH.XuCube;
            }
        }

        /// <summary>
        /// 剩余方量
        /// </summary>
        public virtual decimal? RemainCube
        {
            get
            {
                return ShippingDocumentGH == null ? 0 : ShippingDocumentGH.RemainCube;
            }
        }

        /// <summary>
        /// 质检员
        /// </summary>
        public virtual string Surveyor
        {
            get
            {
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.Surveyor;
            }
        }

        /// <summary>
        /// 合同编号
        /// </summary>
        public virtual string ContractID
        {
            get
            {
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.ContractID;
            }
        }

        /// <summary>
        /// 生产线
        /// </summary>
        public virtual string ProductLineName
        {
            get
            {
                return ShippingDocumentGH == null ? string.Empty : ShippingDocumentGH.ProductLineName;
            }
        }

        /// <summary>
        /// 发货单号
        /// </summary>
        public virtual string ShipDocID
        {
            get;
            set;
        }
        /// <summary>
        /// 任务单号
        /// </summary>
        public virtual string TaskID
        {
            get;
            set;
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName
        {
            get { return this.ProductLine.Company == null ? "" : this.ProductLine.Company.CompName; }
        }
	}
}