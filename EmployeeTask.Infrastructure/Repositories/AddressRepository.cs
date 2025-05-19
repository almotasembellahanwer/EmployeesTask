using EmployeeTask.Core.Domain.Entities;
using EmployeeTask.Core.Domain.RepositoryContracts;
using EmployeeTask.Core.Exceptions;
using EmployeeTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.Infrastructure.Repositories;
public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Address>?> GetAllAddresses()
    {
        IEnumerable<Address> addresses = await _context.Addresses
            .ToListAsync();
        if (addresses is null)
            return new List<Address>();
        return addresses;
    }

    public async Task<Address?> GetAddressByID(int addressID)
    {
        if (addressID == 0)
            throw new BadRequestException($"Invalid ID");
        Address? address = await _context.Addresses
    .FirstOrDefaultAsync(temp=>temp.AddressID == addressID);
        if (address is null)
            throw new NotFoundException("address not found");
        return address;
    }
    public async Task<Address?> AddAddress(Address? entity)
    {
        if (entity is null)
            throw new ArgumentNullException("Invalid Address");
         _context.Addresses.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Address?> UpdateAddress(Address? entity)
    {
        if (entity is null)
            throw new ArgumentNullException("Invalid Address");
        _context.Addresses.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAddress(int addressID)
    {
        Address? address = await GetAddressByID(addressID);
        if (address is null)
            throw new NotFoundException("Address not found");
        _context.Addresses.Remove(address);
        int rowsCountAffected = await _context.SaveChangesAsync();
        return rowsCountAffected > 0;
    }
}
