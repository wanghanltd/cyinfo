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
            List<Dictionary<string, object>> shoesList = new List<Dictionary<string, object>>();
            Dictionary<string, object> shoeDic = new Dictionary<string, object>();
            try
            {

                //算法处理过程
                shoeDic.Add("BRAND_ID", "New Balance");
                shoeDic.Add("SIZE_NUM_ID", "43");
                shoeDic.Add("ITEM_NAME", "New Balance 574");
                shoeDic.Add("Others", "");
                shoesList.Add(shoeDic);
                return_str =  JsonConvert.SerializeObject(shoesList);
            }
            catch(Exception ex)
            {

            }
            return return_str;
        }


        public static string CalculateFootSize(JObject value)
        {
            string return_str = string.Empty;
            try
            {
                List<Dictionary<string, object>> footSizeList = new List<Dictionary<string, object>>();
                Dictionary<string, object> footSizeDic = new Dictionary<string, object>();

                //InMillimetres
                footSizeDic.Add("UnitType", "InMillimetres");
                footSizeDic.Add("Size ", "43");
                footSizeList.Add(footSizeDic);


                //Inches
                footSizeDic = new Dictionary<string, object>();
                footSizeDic.Add("UnitType", "Inches");
                footSizeDic.Add("Size ", "23");
                footSizeList.Add(footSizeDic);
                return_str = JsonConvert.SerializeObject(footSizeList);
            }
            catch(Exception ex)
            {

            }
            return return_str;

        }

        public static string BrandSize(JObject value)
        {
            string return_str = string.Empty;

            try
            {

                //女性

                //男性

                //儿童

                //婴儿

            }
            catch(Exception ex)
            {

            }


            return return_str;
        }
        
    }
}