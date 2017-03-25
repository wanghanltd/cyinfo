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
    public class GetBrandSizeChartController : ApiController
    {
        public IHttpActionResult Post([FromBody]JObject value)
        {

            //

            string result = string.Empty; ;
            try
            {
                //调用计算脚长
                result = DataProcess.BrandSize(value);

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
