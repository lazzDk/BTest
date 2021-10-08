using System.Collections.Generic;

namespace UniqueWord.Api.ReturnTypes
{
    public class UniqueWordResult
    {
        public int DistinctUniqueWords { get; set; }
        public IEnumerable<string> WatchListWords { get; set; }
        public string Error { get; set; } 

        public static UniqueWordResult ErrorUniqueWordResult(string error)
        {
            return new UniqueWordResult
            {
                DistinctUniqueWords = 0, 
                WatchListWords = new List<string>(),
                Error = error
            };
        }
    }

    
}
