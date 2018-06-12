using System;
using System.Reflection;

namespace Facturasiweb.Factura.ApiConsultas.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}