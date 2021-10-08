using UniqueWord.Api.Models.Interfaces;

namespace UniqueWord.Api.Models
{
    public class TextEntry: IHasId
    {
        public virtual long? Id { get; set; }
        public virtual int NoOfWords { get; set; }
    }
}
