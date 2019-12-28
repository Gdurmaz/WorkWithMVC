using System.Collections.Generic;

namespace Project.Core.Helper.MessageMethod
{
    public class ResultMethod<T> where T:class
    {
        public List<ErrorMessage> Errors { get; set; }
        public T Result { get; set; }
        public ResultMethod()
        {
            Errors = new List<ErrorMessage>();
        }
        public void AddErrorCode(ErrorCode code,string message)
        {
            Errors.Add(new ErrorMessage() {Code=code,Message=message });
        }
    }
}
