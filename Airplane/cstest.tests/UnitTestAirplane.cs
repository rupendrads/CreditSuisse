using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cstest.tests
{
    [TestClass]
    public class UnitTestAirplane
    {
        private IAirplane airplane;
        private int simulationsCount = 100;

        public UnitTestAirplane()
        {
            airplane = new Airplane();
        }

        [TestMethod]
        public void CheckProbability_Returns_Probability()
        {
            var result = airplane.CheckProbability(simulationsCount);

            Assert.IsTrue(result.Probability > 0);
        }

        [TestMethod]
        public void CheckProbability_Returns_Not_Null_Object()
        {
            var result = airplane.CheckProbability(simulationsCount);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CheckProbability_Runs_For_Given_Number_of_Simulations()
        {
            var result = airplane.CheckProbability(simulationsCount);

            Assert.AreEqual(result.PassengerMatchList.Count, simulationsCount);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void CheckProbability_Throws_Error_When_Simulation_Count_Zero()
        {
            airplane.CheckProbability(0);
        }
    }
}
