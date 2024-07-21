namespace Library_Management_System.Dto
{
    public class BaseResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
