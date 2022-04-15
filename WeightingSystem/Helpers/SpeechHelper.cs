using System;
using System.Collections.Generic;
using System.Text;
using SpeechLib;
using System.Diagnostics;

namespace WeightingSystem.Helpers
{

    public class ThreadSpeak
    {
        public System.Threading.Thread t { get; set; }
        public string SpeakTxt { get; set; }
    }

    public class SpeechHelper
    {

        public void Speak(object msg)
        {
            SpVoice voice = new SpVoice();
            ThreadSpeak ts = (ThreadSpeak)msg;
            voice.Speak(ts.SpeakTxt.ToString(), SpeechVoiceSpeakFlags.SVSFDefault);
            ts.t.Abort();

        }

        #region TTS语言库播报
        /// <summary>
        /// TTS语言库播报
        /// </summary>
        /// <param name="reportmsg"></param>
        public void TTSSpeak(object reportmsg)
        {
            ThreadSpeak ts = (ThreadSpeak)reportmsg;
            new Process { StartInfo = { FileName = "cmd.exe", Arguments = "/c report \"" + ts.SpeakTxt + "\"", WindowStyle = ProcessWindowStyle.Hidden, CreateNoWindow = true } }.Start();

        }
        public static void TTsSpeakText(string reportmsg)
        {
            if (!string.IsNullOrEmpty(reportmsg))
            {
                SpeechHelper sh = new SpeechHelper();
                ThreadSpeak ts = new ThreadSpeak();
                ts.SpeakTxt = reportmsg;
                sh.TTSSpeak(ts);
            }

        }
        #endregion
    }
}
