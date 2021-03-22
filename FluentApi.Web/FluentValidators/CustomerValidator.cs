using FluentApi.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web.FluentValidators
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        //FluentValidation.ASpNetCore indirdikten sonra AbstractValidator<T> ile kalıtım alırız
        //Eklediğimiz model validationlar çift taraflıdır. Clint ve Server. Eğer kullanıcı javascripti kapatırsa otomatik serverdan kontrol eder.
        public string notEmptyMessage { get; } = "{PropertyName} alanı boş olamaz!";
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(notEmptyMessage);
            RuleFor(x => x.Email).NotEmpty().WithMessage(notEmptyMessage).EmailAddress().WithMessage("Email doğru formatta olmalıdır");
            RuleFor(x => x.Age).NotEmpty().WithMessage(notEmptyMessage).ExclusiveBetween(18,60).WithMessage("18 ile 60 arasında olmalıdır.");
            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(notEmptyMessage).Must(x =>
            {
                return DateTime.Now.AddYears(-18)>x;
            }).WithMessage("Yaşınız 18 yaşından büyük olmalıdır!");

            RuleForEach(x => x.Adresses).SetValidator(new AdressValidator());//İlişkili olduğu tablonun hatalarını ilişkili Customer üzerinden adress kontrolünü yaparız IValidator ile
        }
    }
}
