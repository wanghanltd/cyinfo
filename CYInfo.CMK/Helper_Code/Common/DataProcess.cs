using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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
        private static DefaultMongoDb DB = new DefaultMongoDb("BasicData");
        /// <summary>
        /// 根据参数中提供的脚尺寸，获取各品牌鞋子对应的尺码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetShoeSizesByFoot(JObject value)
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

        /// <summary>
        /// 根据参数中提供的脚尺寸，计算出 International标准对应的尺码尺寸
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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



        //根据品牌 名字 获取该品牌的 |SizeCharts
        public static string GetShoeSizesByUsedShoes(JObject value)
        {
            string return_str = string.Empty;

            try
            {
                //获取 品牌参数
                //获取性别参数


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


        public static string GetBrandSizeChart(JObject value)
        {
            string return_str = string.Empty;

            try
            {

                string brandName = value["BrandName"].ToString();
                Dictionary<string, object> returnDic = new Dictionary<string, object>(); 

                try
                {
                    var targetCollection = DB.database.GetCollection("Sizes4Brand");
                    var entities = targetCollection.FindAll();
                    List<IMongoQuery> qryList = new List<IMongoQuery>();
                    qryList.Add(Query.EQ("BrandName", brandName));

                    IMongoQuery query = Query.And(qryList);

                    var entity = targetCollection.FindOne(query);

                    if (entity != null)
                    {

                        returnDic.Add("BrandName",brandName);
                        string[] genders = { "Women", "Men", "Kids", "Baby" };
                        foreach (string gender in genders)
                        {
                            if (entity.IndexOfName(gender) >= 0)
                            {
                                returnDic.Add(gender, entity[gender]);
                            }
                        }
                    }

                    //return_str = returnDic.ToJson();
                    return_str = JsonConvert.SerializeObject(returnDic);
                }
                catch (Exception ex)
                {
                   
                }

            }
            catch(Exception ex)
            {

            }


            return return_str;
        }

        public static object GetBrands()
        {
            string return_str = string.Empty;
            BsonArray brandsList = new BsonArray();
            BsonDocument brandsDic = new BsonDocument();
            try
            {
                var targetCollection = DB.database.GetCollection("Prefix4Brand");
                var entities = targetCollection.FindAll();

                foreach (var entity in entities)
                {
                    brandsDic = new BsonDocument();
                    brandsDic.Add("BrandPrefix", entity["BrandPrefix"]);
                    brandsDic.Add("Brands", entity["Brands"]);
                    brandsList.Add(brandsDic);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return brandsList;
        }
        
        
    }
}