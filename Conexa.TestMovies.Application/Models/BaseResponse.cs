

namespace Conexa.TestMovies.Application.Models
{
    public class BaseResponse
    {
        private bool v;

        public bool IsSuccesful { get; set; }
        public object? Result { get; set; }
        public int Code { get; set; }
        public List<string> Errors { get; set; }

        public BaseResponse(bool isSuccesful, object? result, int code, List<string> errors)
        {
            IsSuccesful = isSuccesful;
            Result = result;
            Code = code;
            Errors = errors;
        }

        public BaseResponse(bool isSuccesful, object? result, int code)
        {
            IsSuccesful = isSuccesful;
            Result = result;
            Code = code;
        }

        public static BaseResponse Success(object? result, int code = 200) => 
            new BaseResponse(true, result, code);

        public static BaseResponse Failure(object? result, List<string> errors, int code = 400) => 
            new BaseResponse(false, result, code, errors);

    }
}
