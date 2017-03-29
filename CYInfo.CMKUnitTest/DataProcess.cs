using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
