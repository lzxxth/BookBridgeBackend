using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITipoUserRepository,    TipoUserRepository>();
builder.Services.AddScoped<ITipoTransacaoRepository, TipoTransacaoRepository>();
builder.Services.AddScoped<ICategoriaRepository,   CategoriaRepository>();
builder.Services.AddScoped<IDisciplinaRepository,  DisciplinaRepository>();
builder.Services.AddScoped<IUserRepository,        UserRepository>();
builder.Services.AddScoped<IAutorRepository,       AutorRepository>();
builder.Services.AddScoped<ILivroRepository,       LivroRepository>();
builder.Services.AddScoped<ILivroAutorRepository,  LivroAutorRepository>();
builder.Services.AddScoped<IExemplarRepository,    ExemplarRepository>();
builder.Services.AddScoped<IHistoricoRepository,   HistoricoRepository>();
builder.Services.AddScoped<IPedidoRepository,      PedidoRepository>();
builder.Services.AddScoped<IMensagemRepository,    MensagemRepository>();
builder.Services.AddScoped<IAvaliacaoRepository,   AvaliacaoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
