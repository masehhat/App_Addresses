using AsanPardakht.Core.Entities;
using AsanPardakht.Core.Models.Views;
using AutoMapper;

namespace AsanPardakht.Core.Mappings;

public class LocationMappingProfile : Profile
{
    public LocationMappingProfile()
    {
        CreateMap<Location, LocationView>()
            .ForMember(view => view.CombinedAddress, src => src.MapFrom(x => x.Province + ", " + x.City + ", " +  x.Address));
    }
}