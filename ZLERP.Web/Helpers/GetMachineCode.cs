using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;
using System.Text;
using System.Runtime.InteropServices;

namespace ZLERP.Web.Helpers
{
    public class GetMachineCode
    {
        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <returns></returns>
        public static string getMachineCode()
        {
            StringBuilder builder = new StringBuilder(GetCpuID() + GetHardDiskID1());
            for (int i = 0; i < builder.Length; i++)
            {
                builder[i] = Convert.ToChar((int)((Convert.ToInt32(builder[i]) % 7) + Convert.ToInt32(builder[i])));
            }
            return builder.ToString(); 
        }
        //取CPU编号   
        public static String GetCpuID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                String strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }

        }//end method   

        //取第一块硬盘编号   
        public String GetHardDiskID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                String strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }//end    

        static string GetHardDiskID1()
        {
            try
            {
                ManagementClass cObject = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = cObject.GetInstances();
                String strHardDiskID = null;
                foreach (ManagementObject mo in moc)
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }//end  

        [DllImport("DiskID32.dll")]
        public static extern long DiskID32(ref byte DiskModel, ref byte DiskID);
        public static string GetDiskIDXP()
        {
            byte[] DiskModel = new byte[31];
            byte[] DiskID = new byte[31];
            int i;
            string ID = "";
            if (DiskID32(ref DiskModel[0], ref DiskID[0]) != 1)
            {
                for (i = 0; i < 31; i++)
                {
                    if (Convert.ToChar(DiskID[i]) != Convert.ToChar(0))
                    {
                        ID = ID + Convert.ToChar(DiskID[i]);
                    }
                }
                ID = ID.Trim();
            }
            else
            {
                return "-1";
            }
            return ID;
        }
    }
}