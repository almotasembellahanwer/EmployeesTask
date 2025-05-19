using EmployeeTask.Core.DTO.AddressDTO;
namespace EmployeeTask.Core.ServiceContracts;
public interface IAddressesService
{
    Task<IEnumerable<AddressResponse>?> GetAllAddresses();
    Task<AddressResponse?> GetAddressByID(int addressID);
    Task<AddressResponse?> AddAddress(AddressAddRequest? entity);
    Task<AddressResponse?> UpdateAddress(AddressUpdateRequest? entity);
    Task<bool> DeleteAddress(int addressID);
}
