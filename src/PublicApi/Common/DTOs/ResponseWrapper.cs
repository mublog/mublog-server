using System.Collections.Generic;

namespace Mublog.Server.PublicApi.Common.DTOs
{
    public class ResponseWrapper<T>
    {
        public T Data { get; init; }
        public IEnumerable<string> Messages { get; init; }
        public bool IsError { get; init; }
    }

    public static class ResponseWrapper
    {
        public static ResponseWrapper<object> Success(string message)
        {
            var messages = new string[]
            {
                message
            };

            return Success<object>(null, messages);
        }

        public static ResponseWrapper<object> Success(IEnumerable<string> messages = null)
        {
            return Success<object>(null, messages);
        }

        public static ResponseWrapper<T> Success<T>(T data, string message)
        {
            var messages = new string[]
            {
                message
            };

            return Success<T>(data, messages);
        }

        public static ResponseWrapper<T> Success<T>(T data, IEnumerable<string> messages = null)
        {
            if (messages == null)
            {
                messages = new string[]
                {
                    "Success"
                };
            }

            return new ResponseWrapper<T>
            {
                Data = data,
                Messages = messages,
                IsError = false
            };
        }

        public static ResponseWrapper<object> Error(string error)
        {
            var errors = new string[]
            {
                error
            };
            return Error(errors);
        }

        public static ResponseWrapper<object> Error(IEnumerable<string> errors)
        {
            return new ResponseWrapper<object>
            {
                Data = null,
                Messages = errors,
                IsError = true
            };
        }
    }
}