using AutoMapper;
using FluentApi.Web.DTOs;
using FluentApi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web.Mapping
{
    public class CustomerProfile :Profile
    {//automapper.extensions.microsoft.dependencyinjection kütüphanesini ekleyince automapper kütüphanesini de otomatik ekler
        public CustomerProfile()
        {
            //CreateMap<Customer, CustomerDTO>();
            //CreateMap<CustomerDTO, Customer>();

            CreateMap<Customer, CustomerDTO>().ReverseMap();//yukardaki 2 tanesi yerine bu ikisinin de işini görüyor

            //Dikkat edilmesi gereken konu eşlemeler kolon isimleri aynı olunca algılanıyor. Farklı kolon ismi olursa dto'da o zaman aşağıdaki gibi tek tek tanıtmak gerekiyor.

            //CreateMap<Customer, CustomerDTO>().ReverseMap(); ardından aşağıdaki sorgu eğer bu olmazsa//CreateMap<Customer, CustomerDTO>();  //CreateMap<CustomerDTO, Customer>(); sonra aşağıdaki
            //CreateMap<Customer, CustomerDTO>()
            //    .ForMember(x => x.Isim, y => y.MapFrom(z => z.Name))
            //    .ForMember(x => x.Eposta, y => y.MapFrom(z => z.Email))
            //    .ForMember(x => x.Yas, y => y.MapFrom(z => z.Age));
            //Eğer DTO da Isim,Eposta,Yas olarak yeni kolon isimleri kullanılsaydı böyle tek tek tanımlardık metodu da aynı şekilde tanımlarız isim farklıysa

        }
    }
}
