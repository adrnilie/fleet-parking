using FleetParking.Business.Services;
using FleetParking.Business.Storage;
using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.ParkingRights;
using FleetParking.Business.Storage.Entities.Shared;
using FleetParking.WebApi.Model.Request;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FleetParking.WebApi.Controllers
{
    [ApiController]
    [Route("parking-rights")]
    public class ParkingRightsController : ControllerBase
    {
        private readonly IFleetParkingDbContext _context;
        private readonly IFleetParkingService _fleetParkingService;

        public ParkingRightsController(IFleetParkingDbContext context, IFleetParkingService fleetParkingService)
        {
            _context = context;
            _fleetParkingService = fleetParkingService;
        }

        [HttpPost("assign-parking-right/{ownerId:guid}")]
        public async Task<IActionResult> AssignParkingRight([Required][FromRoute] Guid ownerId, [FromBody] AssignParkingRightRequest request)
        {
            var result = await _fleetParkingService.AssignParkingRight(
                new OwnerId(ownerId),
                new EmailAddress(request.EmailAddress),
                request.Name);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("accept/{ownerId:guid}/{parkerId:guid}")]
        public async Task<IActionResult> AcceptInvitation([Required][FromRoute] Guid ownerId, [Required][FromRoute] Guid parkerId)
        {
            var result = await _fleetParkingService.AcceptInvitation(
                new OwnerId(ownerId),
                new ParkerId(parkerId));

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("revoke/{ownerId:guid}/{parkerId:guid}/{parkingRightId:guid}")]
        public async Task<IActionResult> RevokeParkingRight([Required][FromRoute] Guid ownerId, [Required][FromRoute] Guid parkerId, [Required][FromRoute] Guid parkingRightId)
        {
            var result = await _fleetParkingService.RevokeParkingRights(
                new OwnerId(ownerId),
                new ParkerId(parkerId),
                new ParkingRightId(parkingRightId)
            );

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("delete/{ownerId:guid}/{parkerId:guid}")]
        public async Task<IActionResult> DeleteParker([Required][FromRoute] Guid ownerId, [Required][FromRoute] Guid parkerId)
        {
            var result = await _fleetParkingService.DeleteParker(
                new OwnerId(ownerId),
                new ParkerId(parkerId)
            );

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("generate/{ownerId:guid}")]
        public async Task<IActionResult> GenerateParkingRights([Required][FromRoute] Guid ownerId)
        {
            var parkingRights = Enumerable.Range(1, 3)
                .Select(_ => ParkingRight.NewParkingRight(new OwnerId(ownerId)));

            _context.AddRange(parkingRights);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok();
        }
    }
}