using AutoMapper;
using EcomWeb.Dtos.Category;
using EcomWeb.Dtos.Product;
using EcomWeb.Dtos.Role;
using EcomWeb.Dtos.User;
using EcomWeb.Models;
using EcomWeb.Repository;
using EcomWeb.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MyDbConnection");
builder.Services.AddDbContext<MyDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorziation",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { new OpenApiSecurityScheme {
           Reference = new OpenApiReference
           {
               Type =  ReferenceType.SecurityScheme,
               Id = "Bearer"
           }
       },
       new string[]{}
        }
    });
});

builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:SecretKey"])),

        ClockSkew = TimeSpan.Zero
    };
});

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<User, UserAddDto>().ReverseMap();
    mc.CreateMap<User, UserResultDto>().ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName)).ReverseMap();
    mc.CreateMap<User, UserUpdateDto>().ReverseMap();

    mc.CreateMap<Role, RoleAddDto>().ReverseMap();
    mc.CreateMap<Role, RoleResultDto>().ReverseMap();

    mc.CreateMap<Product, ProductResultDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName)).ReverseMap();
    mc.CreateMap<Product, ProductAddDto>().ReverseMap();
    mc.CreateMap<Product, ProductUpdateDto>().ReverseMap();
    mc.CreateMap<Category, CategoryResultDto>()
  .ForMember(dest => dest.ParentCategory, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.CategoryName : null)).ReverseMap();

    mc.CreateMap<Category, CategoryAddDto>().ReverseMap();

    //mc.CreateMap<Product, Product>()
    //    .ForMember(dest => dest.ProductId, opt => opt.Ignore()); // Ignore the ProductId property when mapping



});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//builder.Services.AddControllers().AddJsonOptions(x =>
//                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Scoped
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
