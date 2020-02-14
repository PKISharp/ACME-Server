using ACME.Protocol.HttpModel.Requests;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Protocol.Server
{
    public class AcmeRequestBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(!typeof(AcmeHttpRequest).IsAssignableFrom(bindingContext.ModelType))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            
        }
    }
}
