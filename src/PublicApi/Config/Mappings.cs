using AutoMapper;

namespace Mublog.Server.PublicApi.Config
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<object, object>().ReverseMap();
        }
    }
}