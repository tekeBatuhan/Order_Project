namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message)
            : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, string message,int id)
        {
            Success = success;
            Message = message;
            Id   = id;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
    }
}