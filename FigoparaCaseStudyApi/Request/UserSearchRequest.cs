using System.Collections.Generic;

namespace FigoparaCaseStudyApi.Request
{
    public class UserSearchRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}