using AutoMapper;
using ElectroShop.Data;
using ElectroShop.Jwt;
using ElectroShop.Models;
using ElectroShop.Services;
using ElectroShop.Services.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO.Compression;
using System.Text;

namespace ElectroShop
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public IConfiguration config { get; }


        public Startup(IConfiguration configuration)
        {
            
            config = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            var ctr = config["sqlstr"];
            services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(ctr));

            services.AddIdentity<User, Microsoft.AspNetCore.Identity.IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ShopDbContext>()
            //.AddErrorDescriber<TranslatedIdentityErrorDescriber>()
            .AddDefaultTokenProviders();

            services.AddSingleton<IJwtFactory, JwtFactory>();

            var jwtAppSettingOptions = config.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("ApiUser", policy => policy.RequireClaim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess));
                option.AddPolicy("ApiAdmin", policy => policy.RequireClaim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.AdminAccess));
                option.AddPolicy("ApiSeller", policy =>
                              policy.RequireClaim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol,
                              Helpers.Constants.Strings.JwtClaims.SellerAccess,
                              Helpers.Constants.Strings.JwtClaims.AdminAccess
                              ));
                option.AddPolicy("ApiBuyer", policy =>
                              policy.RequireClaim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol,
                              Helpers.Constants.Strings.JwtClaims.BuyerAccess,
                              Helpers.Constants.Strings.JwtClaims.AdminAccess,
                              Helpers.Constants.Strings.JwtClaims.SellerAccess
                              ));
            });

            services.Configure<Microsoft.AspNetCore.Identity.IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 7;
            });

            services.AddSingleton<ElectroShop.Factory.IHttpClientFactory, ElectroShop.Factory.HttpClientFactory>();
            services.AddTransient<IEmailService, EmailService>();
            //services.AddSingleton<IEmailConfiguration>(config.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.AddCors(options =>
            {
                options.AddPolicy("bpShparak", builder => builder.AllowAnyOrigin().WithMethods("GET", "POST", "HEAD").WithHeaders("accept", "content-type", "origin", "x-custom-header"));
                options.AddPolicy("ZarinPal", builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://www.zarinpal.com"));
            });

            //services.AddProgressiveWebApp();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                //options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // register repository 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IImageGalleryRepository, ImageGalleryRepository>();
            services.AddScoped<IProductPropertyRepository, ProductPropertyRepository>();

           // services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddAutoMapper();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();
            //app.UseAuthentication();

            app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://leeoeadminpanel.leeoe.com").AllowCredentials());

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                          !System.IO.Path.HasExtension(context.Request.Path.Value) &&
                          !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "index.html";
                    await next();
                }
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //provide a way to apply migrations and should therefore only be used in development
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
