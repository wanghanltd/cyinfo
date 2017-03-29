using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CYInfo.CMKUnitTest
{
    [TestClass]
    public class DataProcess
    {
        [TestMethod]
        public void GetBrands()
        {
            CYInfo.CMK.Helper_Code.Common.DataProcess.GetBrands();

        }


        [TestMethod]
        public void GetBrandSizeChart()
        {
            Dictionary<string, string> brandDic = new Dictionary<string, string>();
            brandDic.Add("BrandName","Adidas");
            CYInfo.CMK.Helper_Code.Common.DataProcess.GetBrandSizeChart(JObject.Parse(JsonConvert.SerializeObject(brandDic)));

        }

    }
}
