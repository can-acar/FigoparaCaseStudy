namespace FigoparaCaseStudyApi.Response
{
    public class ServiceResponse
    {
        public bool Status { get; set; } = false;
        public object Data { get; set; } = null;
        public string Message { get; set; } = null;
    }
}