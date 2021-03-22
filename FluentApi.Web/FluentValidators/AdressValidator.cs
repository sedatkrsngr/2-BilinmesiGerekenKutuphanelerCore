using FluentApi.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web.FluentValidators
{
    public class AdressValidator : AbstractValidator<Adress>
    {
        //FluentValidation.ASpNetCore indirdikten sonra AbstractValidator<T> ile kalıtım alırız
        //Eklediğimiz model validationlar çift taraflıdır. Clint ve Server. Eğer kullanıcı javascripti kapatırsa otomatik serverdan kontrol eder.
        public string notEmptyMessage { get; } = "{PropertyName} alanı boş olamaz!";
        public AdressValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage(notEmptyMessage);
            RuleFor(x => x.Province).NotEmpty().WithMessage(notEmptyMessage);
            RuleFor(x => x.PostCode).NotEmpty().WithMessage(notEmptyMessage).MaximumLength(5).WithMessage("{PropertyName} Alanı {MaxLenght} den büyük olamaz!");
        }
    }
}
