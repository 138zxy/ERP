using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model
{
    public class WorkFlow
    {
        //guid（可不填）
        public string id { get; set; }
        //流程id
        public string FlowID { get; set; }
        //第一步id
        public string StepID { get; set; }
        //真实姓名
        public string StepName { get; set; }
        //对应程序（ERP）实例的主键
        public string InstanceID { get; set; }
        //任务组id（可不填）
        public string GroupID { get; set; }
        //流程标题
        public string Title { get; set; }
        //发送人（User-flowid）
        public string SenderID { get; set; }
        public string SenderName { get; set; }
        //接收人（User-flowid）
        public string ReceiveID { get; set; }
        public string ReceiveName { get; set; }
    }
}
