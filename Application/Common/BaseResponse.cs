using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class BaseResponse<T> {
        public bool Success { get; set; } = true;
        public string Error { get; set; }
        public T Data { get; set; }

        public static BaseResponse<T> Ok(T data) => new BaseResponse<T> { Success = true, Data = data };
        public static BaseResponse<T> Fail(string error) => new BaseResponse<T> { Success = false, Error = error };
    }
}
