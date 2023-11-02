var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/* The call to UseCloudEvents adds CloudEvents middleware into to the 
 * ASP.NET Core middleware pipeline. 
 * This middleware will unwrap requests that use the CloudEvents structured 
 * format, so the receiving method can read the event payload directly. */
app.UseCloudEvents();

/* The call to MapSubscribeHandler in the endpoint routing configuration 
 * will add a Dapr subscribe endpoint to the application.
 * 
 * This endpoint will respond to requests on /dapr/subscribe.
 * When this endpoint is called, it will automatically find all WebAPI
 * action methods decorated with the Topic attribute and instruct
 * Dapr to create subscriptions for them. */
app.MapSubscribeHandler();

app.MapControllers();


app.Run();
