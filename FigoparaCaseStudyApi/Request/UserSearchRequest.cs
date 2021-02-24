using System.Collections.Generic;

namespace FigoparaCaseStudyApi.Request
{
    public class UserSearchRequest
    {
        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 10;

        //Key: 
        public IList<IDictionary<string, object>> Query { get; set; } = new List<IDictionary<string, object>>();
        public IList<IDictionary<string, string>> Orders { get; set; } = new List<IDictionary<string, string>>();
    }
}