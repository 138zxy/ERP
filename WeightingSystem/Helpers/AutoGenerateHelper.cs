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
             ERPDataProvider edp = new ERPDataProvider();
             int NextValue = 0;
             IList<AutoGenerate> autoGenerateist = edp.GetAutoGenerateList("[Table]='StuffIn' and IDPrefix='" + prefixPat.ToString()+"';");
             if (autoGenerateist.Count > 0)
             {
                 NextValue = autoGenerateist[0].NextValue;
             }

             return NextValue;
         }

         public int getLastValue_Sale(string TableName, string IDPrefix)
         {
             ERPDataProvider edp = new ERPDataProvider();
             int NextValue = 0;
             IList<AutoGenerate> autoGenerateist = edp.GetAutoGenerateList("[Table]='" + TableName.ToString() + "' and IDPrefix='" + IDPrefix.ToString() + "';");
             if (autoGenerateist.Count > 0)
             {
                 NextValue = autoGenerateist[0].NextValue;
             }
             return NextValue;
         }
         public string GenerateID(int NextValue)
         {
             Config c = new Config();
             string Prefix = GetPrefix(c.prefixPat);
             string len = (NextValue ).ToString().PadLeft(c.idLen, '0');
             return Prefix + len;
         }

         public string GenerateID_Sale(int NextValue)
         {
             Config c = new Config();
             string Prefix = GetPrefix(c.prefixPat_Sale);
             string len = (NextValue).ToString().PadLeft(c.idLen_Sale, '0');
             return Prefix + len;
         }

         public string GetPrefix(string prefixPat)
         {
             //TODO: do other pattern resolve
             StringBuilder prefix = new StringBuilder(prefixPat);
             DateTime now = DateTime.Now;
             prefix.Replace("[yyyy]", now.ToString("yyyy"));
             prefix.Replace("[yy]", now.ToString("yy"));
             prefix.Replace("[MM]", now.ToString("MM"));
             prefix.Replace("[dd]", now.ToString("dd"));
             prefix.Replace("[HH]", now.ToString("HH"));
             prefix.Replace("[mm]", now.ToString("mm"));
             prefix.Replace("[ss]", now.ToString("ss"));
             return prefix.ToString();
         }               
    }
}
