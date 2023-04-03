using FleetParking.Business.Storage;
using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.ParkingRights;
using FleetParking.Business.Storage.Entities.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FleetParking.WebApi.Controllers
{
    [ApiController]
    [Route("parking-rights")]
    public class ParkingRightsController : ControllerBase
    {
        private readonly FleetParkingDbContext _context;

        public ParkingRightsController(FleetParkingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> CreateAndDelegate()
        {

            var parker = Parker.NewParker(
                "John Doe",
                new EmailAddress("john.doe@mail.com"),
                new OwnerId(Guid.NewGuid()));

            //parker.AssignParkingRight(ParkingRight.Default());

            _context.Add(parker);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok();
        }
    }
}