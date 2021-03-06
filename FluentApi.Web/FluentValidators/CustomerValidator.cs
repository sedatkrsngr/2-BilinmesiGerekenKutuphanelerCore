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
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş olamaz!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş olamaz!").EmailAddress().WithMessage("Email doğru formatta olmalıdır");
            RuleFor(x => x.Age).NotEmpty().WithMessage("Yaş boş olamaz!").ExclusiveBetween(18,60).WithMessage("18 ile 60 arasında olmalıdır.");
        }
    }
}
