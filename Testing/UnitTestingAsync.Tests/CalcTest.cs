using Xunit;
using UnitTestingAsync;

namespace UnitTestingAsync.Tests
{
    public class CalcTest
    {
        [Fact]
        public void Plus2plus3Equals5()
        {
            Assert.Equal(5, Calc.Plus(2, 3));
        }
    }
}