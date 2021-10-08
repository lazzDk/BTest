using UniqueWord.Api.Models.Interfaces;

namespace UniqueWord.Api.Models
{
    public class WatchWord : IHasId
    {
        public virtual long? Id { get; set; }
        public virtual string Word { get; set; }
    }
}
