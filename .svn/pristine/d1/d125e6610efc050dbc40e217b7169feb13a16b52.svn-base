using System;
using WeightingSystem.Models;
using WeightingSystem.Helpers;
using System.Text;
using System.Collections.Generic;
namespace WeightingSystem.Helpers
{


    public class AutoGenerateHelper
    { 
         public int getLastValue(string prefixPat,int idLen)
         {
             LocalDataProvider ldp = new LocalDataProvider();
             int LastValue = 0;
             IList<AutoGenerate> autoGenerateist = ldp.getAutoGenerateList("prefixPat='" + prefixPat + "' and idLen=" + idLen.ToString());
             if (autoGenerateist.Count > 0)
             {
                 LastValue = autoGenerateist[0].LastValue;
             }
             
             return  LastValue;
         }



         public string GenerateID(int lastValue)
         {
             Config c = new Config();
             string Prefix = GetPrefix(c.prefixPat);
             string len = (lastValue + 1).ToString().PadLeft(c.idLen, '0');
             return Prefix + len;
         }

         public string GetPrefix(string prefixPat)
         {
             //TODO: do other pattern resolve
             StringBuilder prefix = new StringBuilder(prefixPat);
             DateTime now = DateTime.Now;
             prefix.Replace("[yyyy]", now.ToString("yyyy"));
             prefix.Replace("[MM]", now.ToString("MM"));
             prefix.Replace("[dd]", now.ToString("dd"));
             prefix.Replace("[HH]", now.ToString("HH"));
             prefix.Replace("[mm]", now.ToString("mm"));
             prefix.Replace("[ss]", now.ToString("ss"));
             return prefix.ToString();
         }

         
        
    }
}
