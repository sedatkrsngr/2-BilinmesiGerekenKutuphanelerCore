using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDay { get; set; }
        public IList<Adress> Adresses { get; set; }
        public Gender Gender { get; set; }

        public CreditCard CreditCard { get; set; }
        public string GetFullName()//Automapper da metotlar eşleştirmeden gelmesi için başına Get yazmamız gerekiyor ana modelde sonrası aynı isim olmalı
        {//Metot yazmak veritabanını etkilemez ve veritabanına yansımaz
            return $"{Name}-{Email}-{Age}";
        }
    }
}
