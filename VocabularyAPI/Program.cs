using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Models;
using VocabularyAPI.RepositoryPattern;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Use CORS
app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseSwagger(x => x.SerializeAsV2 = true);


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

app.UseHttpsRedirection();

//app.UseCors("Policy");

app.MapControllers();

app.Run();
