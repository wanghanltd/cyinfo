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

    /// <summary>
    /// 根据用户输入的脚的长度和脚围，给出 
    /// </summary>
    [Authorize]
    public class CalculateFootSizesController :  
    {
            // POST api/values
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// value={
        //    "Gender": "1",//0 代表女；1代表男
        //    "Weight": "23.5",
        //    "FootType": "Medium-arch",//High-arch/Medium-arch/Low-arch/Flat
        //    "UsuallyWear":"Wide" //Narrow/Standard/Wide Extra/Wide
        //"Feet": [
        //                {
        //                    "FootSide": "Left",
        //                    "Length": 267,
        //                    "Girth": 241,
        //                    "UnitType": "InMillimetres",
        //                },
        //                {
        //                    "FootSide": "Right",
        //                    "Length": 267,
        //                    "Girth": 241,
        //                    "UnitType": "InMillimetres",
        //                }
        //            ] 
        //}
        /// <returns></returns>
            public IHttpActionResult Post([FromBody]JObject value)
            {

                //

                string result = string.Empty; ;
                try
                {
                    //调用计算脚长
                    result = DataProcess.CalculateFootSize(value);

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
