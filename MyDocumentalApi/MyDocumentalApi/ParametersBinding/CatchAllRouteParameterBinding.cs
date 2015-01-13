﻿using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace MyDocumentalApi.ParametersBinding
{
    public class CatchAllRouteParameterBinding : HttpParameterBinding {

    private readonly string _parameterName;
    private readonly char _delimiter;

    public CatchAllRouteParameterBinding(
        HttpParameterDescriptor descriptor, char delimiter) : base(descriptor) {

        _parameterName = descriptor.ParameterName;
        _delimiter = delimiter;
    }

    public override Task ExecuteBindingAsync(
        System.Web.Http.Metadata.ModelMetadataProvider metadataProvider,
        HttpActionContext actionContext,
        CancellationToken cancellationToken) {

        var routeValues = actionContext.ControllerContext.RouteData.Values;
            
        if (routeValues[_parameterName] != null) {

            string[] catchAllValues = 
                routeValues[_parameterName].ToString().Split(_delimiter);

            actionContext.ActionArguments.Add(_parameterName, catchAllValues);
        }
        else {

            actionContext.ActionArguments.Add(_parameterName, new string[0]);
        }

        return Task.FromResult(0);
    }
}
}