using AutoMapper;

namespace mublog_server.Api
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<object, object>().ReverseMap();
        }
    }
}