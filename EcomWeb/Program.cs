using AutoMapper;
using EcomWeb.Dtos.User;
using EcomWeb.Models;
using EcomWeb.Repository;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MyDbConnection");
//builder.Services.AddDbContext<MyDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<UserAddDto, User>();
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
