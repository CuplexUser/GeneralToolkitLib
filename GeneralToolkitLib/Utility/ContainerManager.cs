using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace ImageViewer.Utility
{
    public static class ContainerManager 
    {
        public static T[] ResolveAllWithParameters<T>(this IContainer Container, IDictionary<string, object> parameters)
        {
            var _parameters = new List<Parameter>();
            foreach (var parameter in parameters)
            {
                _parameters.Add(new NamedParameter(parameter.Key, parameter.Value));
            }
            return Container.Resolve<IEnumerable<T>>(_parameters).ToArray();
        }
    }
}