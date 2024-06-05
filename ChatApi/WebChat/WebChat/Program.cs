using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebChat.Infrastructure;
using WebChat.Application.StartServices.ServiceCollection;
using Serilog;
using WebChat.Domain.Shared;
using WebChat.Persistence.StartServices.ServiceCollection;
using WebChat.Presentation.MiddleWare;
using WebChat.Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddInfrastructure()
                .AddApplication()
                .AddPresistance(builder.Configuration);

builder.Host.UseSerilog((context, configuration) => {
    configuration.ReadFrom.Configuration(context.Configuration);
});
/*
 the new
 */
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;//true
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,//false
        ValidateAudience = true,//false
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddMvcCore();
builder.Services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 102400000;
});
builder.Services.AddAuthorization();
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => 
    true));
});

builder
    .Services
    .AddControllers()
    .AddApplicationPart(WebChat.Presentation.AssemblyReference.Assembly);

/*builder.Services.Configure<ApiBehaviorOptions>(options
        => options.SuppressModelStateInvalidFilter = true);*/
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
//app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseCors("CORSPolicy");
app.UseSerilogRequestLogging();
app.AddGlobalExceptionGlobalHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoint =>
{
    endpoint.MapHub<ChatHub>("/chatHub");
});
app.Run();
