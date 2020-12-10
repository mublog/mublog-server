namespace Mublog.Server.Domain.Data
{
    public class QueryParameters
    {
        protected virtual int MaxPageSize { get; set; } = 50;
        
        protected virtual int InitialSize { get; set; } = 10;
        
        public virtual int Page { get; set; } = 1;
        public virtual int Size
        {
            get => InitialSize;
            set => InitialSize = value < MaxPageSize  ? value : MaxPageSize;
        }
    }
}