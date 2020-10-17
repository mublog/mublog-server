using AutoMapper;

namespace Mublog.Server.PublicApi
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<object, object>().ReverseMap();
        }
    }
}