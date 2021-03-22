using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string FullName { get; set; }
        public string CreditCardNumber { get; set; }//Modelden gelen veriyi sınıf+propertyname ile direkt yakalarız
        public DateTime CreditCardValidDate { get; set; }//Modelden gelen veriyi sınıf+propertyname ile direkt yakalarız eğer bu şekilde olmazise CustomerProfile da .ForMember ile eşleriz

        //Yada bu şekilde kullanım yerine Modeldeki(CreditCard Modeli) isimlerle aynı yazarız ve ardından CustomerProfile da .IncludeMembers(x=>x.CreditCard) yazarız .ForMember lardan önce
    }
}
