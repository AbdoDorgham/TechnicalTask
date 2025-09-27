using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask.BusinessLogic.Utils
{
    public class Result
    {
        public Result() { }
        public Result(bool isFail) { IsFail = isFail; }
        public bool IsFail { get; set; }
        public string Message { get; set; }
        public object ReturnedObj { get; set; }

        public Result Fail(string message)
        {
            IsFail = true;
            Message = message;
            return this;
        }
        public Result Success(string message)
        {
            IsFail = false;
            Message = message;
            return this;
        }
    }
}
