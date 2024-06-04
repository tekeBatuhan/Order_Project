namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message)
            : base(true, message)
        {
        } 
        
        public SuccessResult(string message,int id)
            : base(true, message, id)
        {
        }

        public SuccessResult()
            : base(true)
        {
        }
    }
}