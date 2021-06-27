using Microsoft.AspNetCore.Mvc;

namespace cstest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirplaneController : ControllerBase
    {
        private IAirplane airplane;
        public AirplaneController()
        {
            airplane = new Airplane();
        }

        [HttpGet]
        public Result CheckProbability(int simulationsCount)
        {
            return airplane.CheckProbability(simulationsCount);
        }
    }
}
