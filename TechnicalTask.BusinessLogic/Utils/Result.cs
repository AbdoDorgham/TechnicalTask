using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask.BusinessLogic.Utils
{
    public class Result<T> where T : class
    {
        public Result() { }
        public Result(bool isFail) { IsFail = isFail; }
        public bool IsFail { get; set; }
        public string Message { get; set; }
        public T ReturnedObj { get; set; }

        public Result<T> Fail(string message)
        {
            IsFail = true;
            Message = message;
            return this;
        }
        public Result<T> Success(string message = "", T returnedObj = null)
        {
            IsFail = false;
            Message = message;
            ReturnedObj = returnedObj;
            return this;
        }
    }
}
