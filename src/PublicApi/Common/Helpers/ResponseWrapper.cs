using System;
using System.Collections.Generic;

namespace Mublog.Server.PublicApi.Common.Helpers
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
            var messages = new[]
            {
                message
            };

            return Success<object>(Array.Empty<string>(), messages);
        }

        public static ResponseWrapper<object> Success(IEnumerable<string> messages = null)
        {
            return Success<object>(Array.Empty<string>(), messages);
        }

        public static ResponseWrapper<T> Success<T>(T data, string message)
        {
            var messages = new[]
            {
                message
            };

            return Success(data, messages);
        }

        public static ResponseWrapper<T> Success<T>(T data, IEnumerable<string> messages = null)
        {
            if (messages == null)
            {
                messages = new[]
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
            var errors = new[]
            {
                error
            };
            return Error(errors);
        }

        public static ResponseWrapper<object> Error(IEnumerable<string> errors)
        {
            return new ResponseWrapper<object>
            {
                Data = Array.Empty<string>(),
                Messages = errors,
                IsError = true
            };
        }
    }
}