using CYInfo.CMK.Helper_Code.Common;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CYInfo.CMK.Controllers
{
    public class GetBrandsController : ApiController
    {
        public IHttpActionResult Get()
        {
            BsonArray result = new BsonArray();
            try
            {
               
               result = (BsonArray)DataProcess.GetBrands();

            }
            catch (Exception ex)
            {
                return Ok(new { Code = "-1", Message = ex.Message });
            }
            //返回处理结果
            return Ok(new { Code = "0", Message = result.ToJson() });
        }
    }
}
