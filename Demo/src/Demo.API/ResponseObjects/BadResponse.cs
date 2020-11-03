using System.Collections.Generic;

namespace Demo.API.ResponseObjects
{
    public class BadResponse
    {
        public bool success { get; set; }
        public IEnumerable<object> errors { get; set; }
    }
}
