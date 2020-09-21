using AutoMapper;

namespace Mublog.Server.Api
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<object, object>().ReverseMap();
        }
    }
}