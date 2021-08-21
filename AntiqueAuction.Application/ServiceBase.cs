using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AntiqueAuction.Application
{
    public abstract class ServiceBase
    {
        /// <summary>
        /// Validate DTOs using <see cref=" System.ComponentModel.DataAnnotations"/> attributes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Validate(object model)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results,true);
            if (!isValid)
                throw new Shared.Exceptions.ValidationException(results.Select(s => s.ErrorMessage));
            return true;
        }
    }
}
