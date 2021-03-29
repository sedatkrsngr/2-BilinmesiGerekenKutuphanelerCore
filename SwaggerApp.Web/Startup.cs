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

            //.net core 5 ile Swashbuckle.AspNetCore k�t�phanesi otomatik gelmekte ve otomatik startupa eklenmekte. Biz sadece Description ve Contact k�s�mlar�n� ekledik. Eskiden Info ve Contact iken �imdi OpenApi eklenmi�.
            //Yorum sat�rlar�n�n api documentinde xml g�r�nmesi i�in proje sa�-click->Edit Project File-> PropertyGroup tag i�erisine  <GenerateDocumentationFile>true</GenerateDocumentationFile> ekleriz
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("ProductV1", new OpenApiInfo { 
                    Title = "SwaggerApp.Web",
                    Version = "v1",
                    Description="�r�n Ekleme/Silme/G�ncelleme i�lemleri",
                    Contact= new OpenApiContact
                    {                        
                        Name="Sedat Karasungur",
                        Email="sedatkarasungur4@gmail.com",
                        Url= new Uri("https://www.sedatkrsngr.com")
                    }
                });
                //a�a��dakiler sonradan eklendi
                var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";//xml dosya ismi//
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);//xml dosyas�n�n olu�aca�� yol
                c.IncludeXmlComments(xmlPath);//G�sterilecek yorum sat�r�
            });///////////////
     

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();//normalde eklememiz laz�m ama .net core 5 ile otomatik ekli gelmekte.
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/ProductV1/swagger.json", "Product API"));//Otomatik gelmekte ad� 'SwaggerApp.Web v1' yerine Product API yapt�k swagger/ProductV1/ yazan yerin ad� ise c.SwaggerDoc("ProductV1",.. ile ba�lad���m�z yer ile birebir ayn� olmal� varsay�lan olarak ikisi de v1 geliyor
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
