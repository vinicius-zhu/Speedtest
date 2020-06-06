using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpeedtestCore;

namespace SpeedtestTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BBLTest bbltest = new SpeedtestCore.BBLTest();
            bbltest.Close();
        }
    }
}
