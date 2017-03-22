using CYInfo.CMK.Helper_Code.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CYInfo.CMK.Controllers
{
     [Authorize]
    public class GetShoesController : ApiController
    {
        // POST api/values

         /// <summary>
        /// http://www.myprecisionfit.com/#!/page/0
         /// </summary>
         /// <param name="value">
         /// value={
                    //    "Gender": "1",//0 代表女；1代表男
                    //    "Weight": "23.5",
                    //    "FootType": "Medium-arch",//High-arch/Medium-arch/Low-arch/Flat
                    //    "UsuallyWear":"Wide" //Narrow/Standard/Wide Extra/Wide
                    //"Brands": [
                    //                {
                    //                    "BrandName": "Nike",
                    //                    "ModelNumber": "Revolution 3",
                    //                    "Size": 43,
                    //                    "UnitType": "Europe",
                    //                },
                    //                {
                    //                    "BrandName": "New Balance",
                    //                    "ModelNumber": "MX608V4",
                    //                    "Size": 43,
                    //                    "UnitType": "Europe",
                    //                }
                    //            ] 
                    //}
         /// </param>
         /// <returns></returns>
        public IHttpActionResult Post([FromBody]JObject value)
        {
            string result = string.Empty; ;
            try
            {
                //调用处理算法
                result = DataProcess.MatchShoes(value);

            }
            catch (Exception ex)
            {
                return Ok(new { Code = "-1", Message = ex.Message });
            }
            //返回处理结果
            return Ok(new { Code = "0", Message = result });
        }


    }
}
