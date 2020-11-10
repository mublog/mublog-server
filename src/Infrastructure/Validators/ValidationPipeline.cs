using System.Collections.Generic;
using System.Linq;
using Mublog.Server.Domain.Common;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mublog.Server.Infrastructure.Validators
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, BaseResponse>
        where TRequest : IRequest<BaseResponse>
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipeline(ILogger logger, IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger;
            _validators = validators;
        }

        public async Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<BaseResponse> next)
        {
            var validationFailures = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (validationFailures.Any())
            {
                if (next.Target is BaseResponse)
                {
                    var response = new BaseResponse();
                    var errors = "";
                    foreach (var failure in validationFailures)
                    {
                        errors += $"{failure.ErrorCode}: {failure.ErrorMessage}\n";
                    }

                    return await Task.FromResult(new BaseResponse());
                }
            }

            return await next.Invoke();
        }
    }
}