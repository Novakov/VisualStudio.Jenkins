using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace JenkinsBuilds.Validation
{
    public static class ValidationFactory
    {
        private static Dictionary<RuntimeTypeHandle, IValidator> validators = new Dictionary<RuntimeTypeHandle, IValidator>();

        public static IValidator GetValidator(Type modelType)
        {
            IValidator validator;
            if (!validators.TryGetValue(modelType.TypeHandle, out validator))
            {
                var typeName = modelType.Name + "Validator";
                var type = Type.GetType(modelType.Namespace + "." + typeName, true);
                validator = (IValidator)Activator.CreateInstance(type);
            }

            return validator;
        }
    }
}
