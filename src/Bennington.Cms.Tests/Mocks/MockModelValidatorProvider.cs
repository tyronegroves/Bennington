using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bennington.Cms.Tests.Mocks
{
    public class MockModelValidatorProvider : ModelValidatorProvider
    {
        private readonly List<ModelValidationResult> modelValidationResults = new List<ModelValidationResult>();
        private readonly List<Type> validatedTypes = new List<Type>();

        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            validatedTypes.Add(metadata.ModelType);
            return new[] {new MockModelValidator(modelValidationResults, metadata, context)};
        }

        public bool TypeWasValidated(Type type)
        {
            return validatedTypes.Contains(type);
        }
    }
}