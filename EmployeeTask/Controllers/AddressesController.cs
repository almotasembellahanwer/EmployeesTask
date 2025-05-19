using EmployeeTask.Core.DTO.AddressDTO;
using EmployeeTask.Core.DTO;
using EmployeeTask.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly APIResponse _response;
    private readonly IAddressesService _addressesService;

    public AddressesController(IAddressesService addressesService)
    {
        _response = new();
        _addressesService = addressesService;
    }
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetAllAddresses()
    {
        IEnumerable<AddressResponse>? addresses = await _addressesService.GetAllAddresses();
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = addresses;
        return Ok(_response);
    }
    [HttpGet("Get/{addressID:int}",Name = "GetAddress")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetAddress(int addressID)
    {
        if (addressID == 0)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        AddressResponse? address = await _addressesService.GetAddressByID(addressID);
        if (address is null)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = false;
            return NotFound(_response);
        }
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = address;
        return Ok(_response);
    }
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> AddAddress(AddressAddRequest? addressRequest)
    {
        if (addressRequest is null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        AddressResponse? address = await _addressesService.AddAddress(addressRequest);
        if (address is null)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = false;
            return NotFound(_response);
        }
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = address;
        return CreatedAtRoute("GetAddress", new { addressID = address.AddressID }, _response);
    }
    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> UpdateAddress(AddressUpdateRequest? addressRequest)
    {
        if (addressRequest is null)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        AddressResponse? address = await _addressesService.UpdateAddress(addressRequest);
        if (address is null)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = false;
            return NotFound(_response);
        }
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = address;
        return Ok(_response);
    }
    [HttpDelete("Delete/{addressID:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> DeleteAddress(int addressID)
    {
        if (addressID == 0)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        bool isDeleted = await _addressesService.DeleteAddress(addressID);
        if (!isDeleted)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = false;
            return NotFound(_response);
        }

        return NoContent();
    }
}
