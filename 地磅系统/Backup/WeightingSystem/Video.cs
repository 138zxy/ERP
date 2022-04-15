using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WeightingSystem
{
    /// <summary>
    /// 视频类库
    /// </summary>
    public class Video
    {
        #region 枚举与结构体
        /// <summary>
        /// 位置结构体
        /// </summary>
        public struct CSRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        /// <summary>
        /// 视频子类型
        /// </summary>
        public enum VideoSubType
        {
            VideoSubType_None = -1,
            VideoSubType_RGB555 = 0,
            VideoSubType_RGB24,
            VideoSubType_YUY2,
            VideoSubType_YVU9,
            VideoSubType_YV12,
        }

        /// <summary>
        /// 视频流的各种信息
        /// </summary>
        public struct VIDEOSTREAMINFO
        {
            public VideoSubType subtype;
            public CSRect rcSource;
            public CSRect rcTarget;
            public UInt32 dwBitRate;
            public UInt32 dwBitErrorRate;
            public long AvgTimePerFrame;
            public BITMAPINFOHEADER bmiHeader;
        }

        /// <summary>
        /// 宽、高、颜色位率等
        /// </summary>
        public struct BITMAPINFOHEADER
        {
            public UInt32 biSize;
            public Int32 biWidth;
            public Int32 biHeight;
            public Int16 biPlanes;
            public Int16 biBitCount;
            public UInt32 biCompression;
            public UInt32 biSizeImage;
            public Int32 biXPelsPerMeter;
            public Int32 biYPelsPerMeter;
            public UInt32 biClrUsed;
            public UInt32 biClrImportant;
        }

        /// <summary>
        /// 视频压缩Filter的信息
        /// </summary>
        public struct VIDEOCOMPRESSORINFO
        {
            public string szName;
            public Int32 dwHandle;
        }

        /// <summary>
        /// 当前制式
        /// </summary>
        public enum VideoStandard
        {
            VideoStandard_None = 0x00000000,
            VideoStandard_NTSC_M = 0x00000001,
            VideoStandard_NTSC_M_J = 0x00000002,
            VideoStandard_NTSC_433 = 0x00000004,
            VideoStandard_PAL_B = 0x00000010,
            VideoStandard_PAL_D = 0x00000020,
            VideoStandard_PAL_H = 0x00000080,
            VideoStandard_PAL_I = 0x00000100,
            VideoStandard_PAL_M = 0x00000200,
            VideoStandard_PAL_N = 0x00000400,
            VideoStandard_PAL_60 = 0x00000800,
            VideoStandard_SECAM_B = 0x00001000,
            VideoStandard_SECAM_D = 0x00002000,
            VideoStandard_SECAM_G = 0x00004000,
            VideoStandard_SECAM_H = 0x00008000,
            VideoStandard_SECAM_K = 0x00010000,
            VideoStandard_SECAM_K1 = 0x00020000,
            VideoStandard_SECAM_L = 0x00040000,
            VideoStandard_SECAM_L1 = 0x00080000,
        }

        /// <summary>
        /// 调用系统对话框设置各种音、视频属性
        /// </summary>
        public enum PropertyDialog
        {
            PropertyDlg_VideoCaptureFilter = 0,
            PropertyDlg_VideoCapturePin, // *
            PropertyDlg_VideoPreviewPin, // *
            PropertyDlg_VideoCrossbar,
            PropertyDlg_AudioCaptureFilter,
            PropertyDlg_AudioCapturePin,
            PropertyDlg_AudioCrossbar,
            PropertyDlg_TVAudioFilter,
            PropertyDlg_TVTuner,
            PropertyDlg_VfwCaptureFormat, // *
            PropertyDlg_VfwCaptureSource,
            PropertyDlg_VfwCaptureDisplay,
        }
        #endregion 

        #region API函数
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        [DllImport("DSStream.dll", EntryPoint = "DSStream_Initialize")]
        public static extern int DSStream_Initialize();

        /// <summary>
        /// 关闭连接
        /// </summary>
        [DllImport("DSStream.dll", EntryPoint = "DSStream_Uninitialize")]
        public static extern void DSStream_Uninitialize();

        /// <summary>
        /// 开始显示
        /// </summary>
        /// <param name="iCardID">捕捉卡编号</param>
        /// <param name="bOverlay">是否Overlay模式</param>
        /// <param name="hParentWnd">父窗体</param>
        /// <returns></returns>
        [DllImport("DSStream.dll", EntryPoint = "DSStream_ConnectDevice")]
        public static extern int DSStream_ConnectDevice(int iCardID, bool bOverlay, IntPtr hParentWnd);

        [DllImport("DSStream.dll", EntryPoint = "DSStream_DisconnectDevice")]
        public static extern int DSStream_DisconnectDevice(int iCardID);

        /// <summary>
        /// 设置视频停靠父窗体
        /// </summary>
        /// <param name="iCardID">捕捉卡号</param>
        /// <param name="hParentWnd">父窗体</param>
        /// <returns></returns>
        [DllImport("DSStream.dll", EntryPoint = "DSStream_SetOwnerWnd")]
        public static extern int DSStream_SetOwnerWnd(int iCardID, IntPtr hParentWnd);

        /// <summary>
        /// 设置视频在父窗体的位置
        /// </summary>
        /// <param name="iCardID">捕捉卡号</param>
        /// <param name="rc">位置</param>
        /// <returns></returns>
        [DllImport("DSStream.dll", EntryPoint = "DSStream_SetWindowPos")]
        public static extern int DSStream_SetWindowPos(int iCardID, CSRect rc);

        /// <summary>
        /// 得到视频卡的数量
        /// </summary>
        /// <param name="pCardNum">视频卡数</param>
        /// <returns></returns>
        [DllImport("DSStream.dll")]
        public static extern int DSStream_GetCardNumber(out int pCardNum);

        /// <summary>
        /// 调用系统的参数设置窗体
        /// </summary>
        /// <param name="iCardID">捕捉卡号</param>
        /// <param name="id">结构</param>
        /// <param name="hParentWnd">父窗体句柄</param>
        /// <param name="szCaption">显示的抬头栏字符串</param>
        /// <returns></returns>
        [DllImport("DSStream.dll")]
        public static extern int DSStream_DisplayPropertyDialogs(int iCardID, PropertyDialog id, IntPtr hParentWnd, string szCaption);


        /// <summary>
        /// 设置录像存储位置
        /// </summary>
        /// <param name="iCardID">捕捉卡号</param>
        /// <param name="szFileName">存储位置及名称</param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_SetCaptureFile(int iCardID, string szFileName);

        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="iCardID"></param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_StartCapture(int iCardID);

        /// <summary>
        /// 结束录像
        /// </summary>
        /// <param name="iCardID"></param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_StopCapture(int iCardID);

        /// <summary>
        /// 设置视频源
        /// </summary>
        /// <param name="iCardID"></param>
        /// <param name="idInPin">视频源。对于 SDK-2000 卡，范围是 0-2；对于四路卡，范围是 0-4</param>
        /// <param name="idOutPin">必须为0</param>
        /// <returns></returns>
        [DllImport("DSStream.dll")]
        public static extern int DSStream_RouteInPinToOutPin(int iCardID, int idInPin, int idOutPin);

        /// <summary>
        /// 抓拍图像并存储
        /// </summary>
        /// <param name="iCardID"></param>
        /// <param name="szFileName"></param>
        /// <param name="iQuality"></param>
        [DllImport("DSStream.dll")]
        public static extern int DSStream_SaveToJpgFile(int iCardID, string szFileName, int iQuality);

        /// <summary>
        /// 设置视频制式
        /// </summary>
        /// <param name="iCardID"></param>
        /// <param name="vs">制式</param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_SetVideoStandard(int iCardID, VideoStandard vs);

        /// <summary>
        /// 是否正在录像
        /// </summary>
        /// <param name="iCardID">卡号</param>
        /// <param name="bIsCapturing">是否正在录像</param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_IsCapturing(int iCardID, ref bool bIsCapturing);

        /// <summary>
        /// 选择视频压缩算法
        /// </summary>
        /// <param name="szCompName">算法名称</param>
        /// <returns></returns>
        [DllImport("DSStream.dll")]
        public static extern int DSStream_ChooseVideoCompressor(string szCompName);

        /// <summary>
        /// 设置视频压缩质量
        /// </summary>
        /// <param name="iQuality">0-100</param>
        /// <returns></returns>
        [DllImport("DSStream.dll")]
        public static extern int DSStream_SetVideoCompressorQuality(int iQuality);

        /// <summary>
        /// 设置视频流的信息
        /// </summary>
        /// <param name="iCardID">卡号</param>
        /// <param name="vsi">视频流的各种信息</param>
        /// <param name="pin"></param>
        /// <returns></returns>
        [DllImport("DSStream.dll")]
        public static extern int DSStream_SetVideoInfo(int iCardID, VIDEOSTREAMINFO vsi, int pin);

        /// <summary>
        /// 断开某个Pin上连接的所有Filter
        /// </summary>
        /// <param name="iCardID"></param>
        /// <param name="pin"></param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_DisconnectPin(int iCardID, int pin);

        /// <summary>
        /// 连接某个Pin上连接的所有Filter
        /// </summary>
        /// <param name="iCardID"></param>
        /// <param name="pin"></param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_ConnectPin(int iCardID, int pin);

        /// <summary>
        /// 显示或隐藏Date
        /// </summary>
        /// <param name="iCardID"></param>
        /// <param name="bShow"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_ShowDate(int iCardID, bool bShow, int x, int y);

        /// <summary>
        /// 显示或隐藏Time
        /// </summary>
        /// <param name="iCardID"></param>
        /// <param name="bShow"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport("DSStream.dll")]
        public static extern void DSStream_ShowTime(int iCardID, bool bShow, int x, int y);

        #endregion
    }
}
