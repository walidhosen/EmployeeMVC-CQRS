using Employee.Ioc.Configaration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option=>
{
    option.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Employee",
            Version = "v1",
            Description = "This is a Eployee Project to see how doucoment can easy be generate"+"for Asp.net Core.",

            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "Alif",
                Email = "walidhosen23@gmail.com"
            }
        });
});



builder.Services.MapCore(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //add it
    app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json",
    "Demo Documentation v1"));

    app.UseReDoc(options =>
    {
        options.DocumentTitle = "Demo Documentation";
        options.SpecUrl = "/swagger/v1/swagger.json";
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
