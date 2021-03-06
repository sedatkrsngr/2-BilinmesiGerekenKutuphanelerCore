using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web.Models
{
    public class AppDbContext : DbContext
    {   //Aşağıdaki 3 kütüphane indirilmeli.
        //EntityFrameworkCore
        //EntityFrameworkCore.SqlServer
        //EntityFrameworkCore.Tools 
        //Ctor tab tab ile constructor oluşturabiliriz kısaca
        //Startupta ilgili context ayarını yaptıktan sonra  package-manage-consele ekranında  add-migration Initial-1 diyerek başladık  Inıtial-1 bizim verdiğimiz bir isim sırasıyla -2,-3
        //remove-migration ise son migrationu geri alır
        //Update-Database dedikten sonra Controller->entity framwork-Customer-Appdbcontext diyip hazır şablonu ekledik.
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

    }
}
