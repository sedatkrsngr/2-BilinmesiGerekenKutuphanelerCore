using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SwaggerApp.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SwaggerApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SwaggerDBContext>(opt =>
            {
                opt.UseSqlServer(Configuration["ConnectionStrings"]);
            });
            services.AddControllers();

            //.net core 5 ile Swashbuckle.AspNetCore kütüphanesi otomatik gelmekte ve otomatik startupa eklenmekte. Biz sadece Description ve Contact kýsýmlarýný ekledik. Eskiden Info ve Contact iken þimdi OpenApi eklenmiþ.
            //Yorum satýrlarýnýn api documentinde xml görünmesi için proje sað-click->Edit Project File-> PropertyGroup tag içerisine  <GenerateDocumentationFile>true</GenerateDocumentationFile> ekleriz
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("ProductV1", new OpenApiInfo { 
                    Title = "SwaggerApp.Web",
                    Version = "v1",
                    Description="Ürün Ekleme/Silme/Güncelleme iþlemleri",
                    Contact= new OpenApiContact
                    {                        
                        Name="Sedat Karasungur",
                        Email="sedatkarasungur4@gmail.com",
                        Url= new Uri("https://www.sedatkrsngr.com")
                    }
                });
                //aþaðýdakiler sonradan eklendi
                var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";//xml dosya ismi//
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);//xml dosyasýnýn oluþacaðý yol
                c.IncludeXmlComments(xmlPath);//Gösterilecek yorum satýrý
            });///////////////
     

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();//normalde eklememiz lazým ama .net core 5 ile otomatik ekli gelmekte.
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/ProductV1/swagger.json", "Product API"));//Otomatik gelmekte adý 'SwaggerApp.Web v1' yerine Product API yaptýk swagger/ProductV1/ yazan yerin adý ise c.SwaggerDoc("ProductV1",.. ile baþladýðýmýz yer ile birebir ayný olmalý varsayýlan olarak ikisi de v1 geliyor
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
