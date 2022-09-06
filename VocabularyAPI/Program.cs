using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VocabularyAPI.Models;
using VocabularyAPI.RepositoryPattern;

var builder = WebApplication.CreateBuilder(args);


//Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//Dependency Injection
builder.Services.AddDbContext<DictionaryDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("dictionarydb")));
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<ISynonymService, SynonymService>();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Enable CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin", x =>
    {
        x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var app = builder.Build();
app.UseAuthorization();
app.UseAuthentication();

//Use CORS
app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger(x => x.SerializeAsV2 = true);

//LogIn Endpoint
app.MapPost("/login", (UserLogin user, [FromServices] IUserService service) => LogIn(user, service));

//Mapping Controllers
app.MapGet("/words", ([FromServices] IWordService service) =>
{
    return service.GetAllWords();
});


//Get word by id
app.MapGet("/words/{id}", (int id, [FromServices] IWordService service) =>
{
    return service.GetWordById(id);
});

//Post Word
app.MapPost("/words", (Word word, [FromServices] IWordService service) =>
{
    service.AddWord(word);
});

//Put/Edit word
app.MapPut("/words/{id}", (Word word, [FromServices] IWordService service) =>
{
    service.UpdateWord(word);
});

//Mapp Delete
app.MapDelete("/words/{id}", (int id, [FromServices] IWordService service) =>
{
    service.DeleteWord(id);
});

//Mapp LogIn Controller
IResult LogIn(UserLogin user, [FromServices] IUserService service)
{
    if (!String.IsNullOrEmpty(user.Username) && !String.IsNullOrEmpty(user.Password))
    {
        var loggedUser = service.GetUser(user);
        if (loggedUser is null) return Results.NotFound("User does not exist!");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedUser.Username),
            new Claim(ClaimTypes.Email, loggedUser.EmailAddress),
            new Claim(ClaimTypes.GivenName, loggedUser.FirstName),
            new Claim(ClaimTypes.Surname, loggedUser.LastName),
            new Claim(ClaimTypes.Role, loggedUser.Role),
        };

        var token = new JwtSecurityToken
        (
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60),
            notBefore:DateTime.UtcNow,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder
                        .Configuration["Jwt:Key"])),
                        SecurityAlgorithms.HmacSha256) //New symmetric key from appsettingjson and encrypt using hashing
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token); //retrun defined properties defined in a string
        return Results.Ok(tokenString);
    }
    
}

app.UseHttpsRedirection();

//app.UseCors("Policy");

app.MapControllers();

app.Run();
