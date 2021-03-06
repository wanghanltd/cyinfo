﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CYInfo.CMK.Controllers
{
    [Authorize]
    public class WebApiController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello REST API",  "I am Authorized" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "Hello Authorized API with ID = " + id;
        }

        // POST api/values
        public void Post([FromBody]JObject value)
        {
            string tttt = value.ToString();

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
