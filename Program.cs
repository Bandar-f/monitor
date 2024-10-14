using api.Data;
using api.Helper;
using api.Interfaces;
using api.Repos;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//initalize
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddNewtonsoftJson(option=>{
 option.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var hangfireConnection=builder.Configuration.GetConnectionString("Hangfire");

var dbConnection=builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<ApplicationDBContext>(options=>{
    options.UseSqlServer(hangfireConnection);
}

);

//hangfire client specification
builder.Services.AddHangfire(config=>config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180).UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(hangfireConnection,new SqlServerStorageOptions { SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5) }));


//manage internal objects
builder.Services.AddTransient<IEmailService, EmailServiceRepo>();

builder.Services.AddScoped<IMutasilRepo,MutasilRepo>();
builder.Services.AddScoped<ICSTRepo,CSTRepo>();

builder.Services.AddScoped<HttpCall>();






//hangfire server specification
builder.Services.AddHangfireServer();


//allow cors
builder.Services.AddCors(options =>
     {
         options.AddPolicy("AllowAll",
             builder =>
             {
                 builder
                 .AllowAnyOrigin() 
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 ;
             });
     });





var app = builder.Build();

app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHangfireDashboard();
app.MapHangfireDashboard("/hangfire");
// RecurringJob.AddOrUpdate("test",()=>HttpCall.GetMutasil(),"* * * * *");
// RecurringJob.RemoveIfExists("test");






app.MapControllers();


app.Run();


