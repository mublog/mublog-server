namespace Mublog.Server.Domain.Data
{
    public class QueryParameters
    {
        private const int MaxPageSize = 50;
        
        private int _size = 10;
        
        public int Page { get; set; } = 1;
        public int Size
        {
            get => _size;
            set => _size = value < MaxPageSize  ? value : MaxPageSize;
        }
    }
}