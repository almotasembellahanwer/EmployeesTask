using EmployeeTask.Core.Domain.Entities;

namespace EmployeeTask.Core.Domain.RepositoryContracts;
public interface IAddressRepository
{
    Task<IEnumerable<Address>?> GetAllAddresses();
    Task<Address?> GetAddressByID(int addressID);
    Task<Address?> AddAddress(Address? entity);
    Task<Address?> UpdateAddress(Address? entity);
    Task<bool> DeleteAddress(int addressID);


}
