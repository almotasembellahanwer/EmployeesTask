using AutoMapper;
using EmployeeTask.Core.Domain.Entities;
using EmployeeTask.Core.DTO.AddressDTO;
using EmployeeTask.Core.DTO.EmployeeDTO;

namespace EmployeeTask.Core.Mappers;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee,EmployeeResponse>().ReverseMap();
        CreateMap<Employee, EmployeeAddRequest>().ReverseMap();
        CreateMap<Employee, EmployeeUpdateRequest>().ReverseMap();

        CreateMap<Address, AddressResponse>().ReverseMap();
        CreateMap<Address, AddressAddRequest>().ReverseMap();
        CreateMap<Address, AddressUpdateRequest>().ReverseMap();

    }
}
