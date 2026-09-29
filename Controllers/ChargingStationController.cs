using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemoteEVEnterpriseModels.Request;
using RemoteEVEnterpriseService.Interfaces;

namespace Remote_EV_Enterprise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargingStationController : ControllerBase
    {
        private readonly IChargingStationService _chargingStationService;
        public ChargingStationController(IChargingStationService chargingStationService)
        {
            _chargingStationService = chargingStationService;
        }
        [HttpGet("getAllChargingStations")]
        public async Task<IActionResult> GetAllChargingStations()
        {
            var stations = await _chargingStationService.GetAllChargingStations();
            return Ok(stations);
        }
        [HttpGet("getChargingStationById")]
        public async Task<IActionResult> GetChargingStationById(int stationId)
        {
            var station = await _chargingStationService.GetChargingStationById(stationId);
            if (station == null)
            {
                return NotFound("Charging station not found.");
            }
            return Ok(station);
        }

        [HttpPost("addChargingStation")]
        public async Task AddChargingStation(ChargingStationRegisterRequestModel station)
        {
            long userId = GetUserIdFromToken();
            await _chargingStationService.AddChargingStation(station,userId);
        }

        private long GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            var userId = userIdClaim?.Value;
            return userId != null ? long.Parse(userId) : 0;
        }
    }
}
