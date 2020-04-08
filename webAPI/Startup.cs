using Core.Utilities.Security.Encyription;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace webAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //istek yapýlan yer domain neyse o yazýlacak port tanýmlýyorsun
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));

            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //Aþaðýda nugetten ekledik JwtBearerDefaults
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // gerigelecekveriler
                    ValidateAudience = true,
                    ValidateLifetime = true,// bunlarý kontrol edeyim mi
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };            


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.WithOrigins("http://localhost:61270").AllowAnyHeader());// buradan gelen her türlü post put falan kabul et
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();//Jwt Ýçin kendimiz ekledik.Bir anahtar giriþ saðlar.//authentication üstte olacak.
            app.UseAuthorization();// giriþ saðladým ama neler yapabilicem yetkilerimi belirtir.token vasýtasýyla yapýcaz.
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
