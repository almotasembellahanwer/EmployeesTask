using AutoMapper;
using EmployeeTask.Core.Domain.Entities;
using EmployeeTask.Core.Domain.RepositoryContracts;
using EmployeeTask.Core.DTO.AddressDTO;
using EmployeeTask.Core.Exceptions;
using EmployeeTask.Core.ServiceContracts;
namespace EmployeeTask.Core.Services;
public class AddressesService : IAddressesService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressesService(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AddressResponse>?> GetAllAddresses()
    {
        IEnumerable<Address>? addresses = await _addressRepository.GetAllAddresses();
        if (addresses is null)
            return new List<AddressResponse>();
        IEnumerable<AddressResponse> response = _mapper.Map<IEnumerable<AddressResponse>>(addresses);
        return response;
    }

    public async Task<AddressResponse?> GetAddressByID(int addressID)
    {
        if (addressID == 0)
            throw new BadRequestException("Invalid ID");
        Address? address = await _addressRepository.GetAddressByID(addressID);
        if (address is null)
            throw new NotFoundException("address not found");
        AddressResponse response = _mapper.Map<AddressResponse>(address);
        return response;
    }
    public async Task<AddressResponse?> AddAddress(AddressAddRequest? entity)
    {
        if (entity is null)
            throw new BadRequestException("Invalid address to add");
        Address address = _mapper.Map<Address>(entity);
        Address? addressAdded = await _addressRepository.AddAddress(address);
        if (addressAdded is null)
            throw new BadRequestException("error while adding address");
        AddressResponse response = _mapper.Map<AddressResponse>(addressAdded);
        return response;
    }
    public async Task<AddressResponse?> UpdateAddress(AddressUpdateRequest? entity)
    {
        if (entity is null)
            throw new BadRequestException("Invalid address to add");
        Address address = _mapper.Map<Address>(entity);
        Address? addressUpdated = await _addressRepository.UpdateAddress(address);
        if (addressUpdated is null)
            throw new BadRequestException("error while updating address");
        AddressResponse response = _mapper.Map<AddressResponse>(addressUpdated);
        return response;
    }
    public async Task<bool> DeleteAddress(int addressID)
    {
        if (addressID == 0)
            throw new BadRequestException("Invalid ID");
        bool isDeleted = await _addressRepository.DeleteAddress(addressID);
        return isDeleted;
    }
}
