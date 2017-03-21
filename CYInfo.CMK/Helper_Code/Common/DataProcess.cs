using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CYInfo.CMK.Helper_Code.Common
{
    [Authorize]
    public class DataProcess
    {
        public static string MatchShoes(JObject value)
        {
            string return_str = string.Empty;
            List<Dictionary<string, object>> shoesLst = new List<Dictionary<string, object>>();
            Dictionary<string, object> shoeDic = new Dictionary<string, object>();
            try
            {

                //算法处理过程
                shoeDic.Add("BRAND_ID", "New Balance");
                shoeDic.Add("SIZE_NUM_ID", "43");
                shoeDic.Add("ITEM_NAME", "New Balance 574");
                shoeDic.Add("Others", "");
                shoesLst.Add(shoeDic);
                return_str =  JsonConvert.SerializeObject(shoesLst);
            }
            catch(Exception ex)
            {

            }
            return return_str;
        }
    }
}