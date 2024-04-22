﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using SqlSugar;
namespace WebApplication1.Config
{
    public static class HostBuilderExtend
    {
        public static void Register(this WebApplicationBuilder app)
        {
            app.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            app.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.Register<ISqlSugarClient>(context =>
                {
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        ConnectionString= "Data Source=RAINHE;Initial Catalog=MyBlog;User Id=sa;Password=yyk123147;Trusted_Connection=True; ",
                        DbType = DbType.SqlServer,
                        IsAutoCloseConnection = true,
                    },
                    db =>
                    {
                        db.Aop.OnLogExecuting = (sql, pars) =>
                        {
                            Console.WriteLine(sql);
                        };
                    });
                    return db;
                });

                builder.RegisterModule(new AutofacModuleRegister());

            });
            //app.Services.AddAutoMapper(typeof(AutoMapperConfigs));

            //app.Services.Configure<JWTtoken>(app.Configuration.GetSection("JWTtoken"));

            //JWTtoken to = new JWTtoken();
            //app.Configuration.Bind("JwtToken",to);
            //app.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidAudience = to.Audience,
            //            ValidIssuer = to.Issuer,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(to.SecurityKey))

            //        };
            //    });

            //后端跨域
            //app.Services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
            //});
        }
    }
}
