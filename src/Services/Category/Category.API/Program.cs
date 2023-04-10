using Category.API;
using Category.Application;
using Category.Infrastructure;
using Core.Service;
using Core.Service.Exceptions;
using Hellang.Middleware.ProblemDetails;
using MassTransit;
using Search.Service;
using System.Data;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoreServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSearchServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiservices(builder.Configuration);

// Configure problem details
builder.Services.AddProblemDetails(options =>
{
    // Only include exception details in a development environment. There's really no need
    // to set this as it's the default behavior. It's just included here for completeness :)
    options.IncludeExceptionDetails = (ctx, ex) => !builder.Environment.IsProduction();



    options.OnBeforeWriteDetails = (ctx, details) =>
    {
        // keep consistent with asp.net core 2.2 conventions that adds a tracing value
        var traceId = Activity.Current?.Id ?? ctx.TraceIdentifier;
        details.Extensions["traceId"] = traceId;
    };

    // Map known exceptions to 400 instead of default 500.
    options.MapToStatusCode<DomainException>(StatusCodes.Status400BadRequest);
    options.MapToStatusCode<ServiceException>(StatusCodes.Status400BadRequest);

    // This will map HttpRequestException to the 503 Service Unavailable status code.
    options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

    // This will map the MassTransit RequestFaultException to 400
    options.MapToStatusCode<RequestFaultException>(StatusCodes.Status400BadRequest);

    // This will map DBConcurrencyException to the 409 Conflict status code.
    options.MapToStatusCode<DBConcurrencyException>(StatusCodes.Status409Conflict);

    // This will map NotImplementedException to the 501 Not Implemented status code.
    options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

    // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
    // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);

    // You can configure the middleware to re-throw certain types of exceptions, all exceptions or based on a predicate.
    // This is useful if you have upstream middleware that needs to do additional handling of exceptions.
    options.Rethrow<Exception>();
});


var app = builder.Build();

// Add ProblemDetailsMiddleware to the application pipeline
app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

