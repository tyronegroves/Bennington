using System.Collections.Generic;
using System.Web.Mvc;

namespace Bennington.Cms.Tests.Mocks
{
    public class MockModelValidator : ModelValidator
    {
        private readonly IEnumerable<ModelValidationResult> modelValidationResults;

        public MockModelValidator(IEnumerable<ModelValidationResult> modelValidationResults, ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext)
        {
            this.modelValidationResults = modelValidationResults;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            return modelValidationResults;
        }
    }
}