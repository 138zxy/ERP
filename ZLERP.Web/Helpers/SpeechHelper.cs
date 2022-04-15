using System;
using System.Collections.Generic;
using System.Text;
//using SpeechLib;

namespace ZLERP.Web.Helpers
{

    public class ThreadSpeak
    {
        public System.Threading.Thread t { get; set; }
        public string SpeakTxt { get; set; }
    }

    public class SpeechHelper
    {
        public void Speak(object msg) {
            //SpVoice voice = new SpVoice();
            ThreadSpeak ts = (ThreadSpeak)msg;
            //voice.Speak(ts.SpeakTxt.ToString(), SpeechVoiceSpeakFlags.SVSFDefault);
            ts.t.Abort();
           
        }

    }
}
