using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api.Configurations;

public class CqrsConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var item in application.Controllers.Where((ControllerModel a) => a.ControllerType.Name.Contains("Query", StringComparison.OrdinalIgnoreCase)))
        {
            foreach (var item2 in item.Selectors.Where((SelectorModel b) => b.AttributeRouteModel != null))
            {
                string controllerName = GetControllerName(item.ControllerType.Name, "Query");
                item2.AttributeRouteModel = new AttributeRouteModel
                {
                    Template = "api/" + controllerName
                };
            }
        }
    }

    private static string GetControllerName(string source, string term)
    {
        int startIndex = source.IndexOf(term, StringComparison.OrdinalIgnoreCase);
        string text = RemoveControllerFromControllerTypeName(source);
        return text.Remove(startIndex, term.Length);
    }

    private static string RemoveControllerFromControllerTypeName(string source)
    {
        int startIndex = source.IndexOf("Controller", StringComparison.OrdinalIgnoreCase);
        return source.Remove(startIndex, 10);
    }
}
