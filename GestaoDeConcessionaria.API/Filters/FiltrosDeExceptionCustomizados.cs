using GestaoDeConcessionaria.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestaoDeConcessionaria.API.Filters
{
    public class FiltrosDeExceptionCustomizados : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FluentValidation.ValidationException validationException )
            {
                var errors = validationException.Errors
                    .Select(e => $"{e.PropertyName} : {e.ErrorMessage}")
                    .ToList();
                var result = new ObjectResult(new { Errors = errors })
                {
                    StatusCode = 400
                };

                context.Result = result;
                context.ExceptionHandled = true;
            }
            else if (context.Exception is DomainValidationException dvEx)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Errors = new[] { dvEx.Message }
                });
                context.ExceptionHandled = true;
            }
            else if(context.Exception is UnauthorizedAccessException)
            {
                context.Result = new ObjectResult(new
                {
                    mensagem = "Acesso não autorizado.",
                    detalhes = context.Exception.Message
                })
                {
                    StatusCode = 403
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is ArgumentNullException 
                || context.Exception is KeyNotFoundException)
            {
                context.Result = new ObjectResult(new
                {
                    mensagem = "Um ou mais parâmetros obrigatórios estão ausentes.",
                    detalhes = context.Exception.Message
                })
                {
                    StatusCode = 404
                };
                context.ExceptionHandled = true;
            }
            else if(context.Exception is HttpRequestException 
                || context.Exception is InvalidOperationException || context.Exception is not null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                context.ExceptionHandled = true;
            }
        }
    }
}
