﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using REST_API_LEARN.Models;

namespace REST_API_LEARN.Controllers
{
    public class CustomModelValidatorProvider : IModelValidatorProvider
    {
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            if (context.ModelMetadata.ContainerType == typeof(TodoItem))
            {
                context.Results.Add(new ValidatorItem
                {
                    Validator = new UserModelValidator(),
                    IsReusable = true
                });
            }
        }
    }

    public class UserModelValidator : IModelValidator
    {
        private static readonly object _emptyValidationContextInstance = new object();
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext validationContext)
        {
            var validationResults = new List<ModelValidationResult>();


            if (validationContext.ModelMetadata.Name == "FirstName" && validationContext.Model == null)
            {
                var validationResult = new ModelValidationResult("", "FirstName is required");

                validationResults.Add(validationResult);

            }
            return validationResults;
        }
    }
}
