using UniqueWord.Api.Models.Interfaces;

namespace UniqueWord.Api.Models
{
    public class UniqueWordEntry : IHasId
    {
        private TextEntry _textEntry;

        protected UniqueWordEntry()
        {

        }

        internal UniqueWordEntry(TextEntry textEntry)
        {
            TextEntry = textEntry;
        }

        public virtual long? Id { get; set; }
        public virtual string Word { get; set; }
        public virtual TextEntry TextEntry
        {
            get => _textEntry;
            protected set => _textEntry = value;
        }
    }
}
