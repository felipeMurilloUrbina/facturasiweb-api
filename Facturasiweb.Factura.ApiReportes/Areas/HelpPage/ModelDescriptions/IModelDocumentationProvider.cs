using System;
using System.Reflection;

namespace Facturasiweb.Factura.ApiReportes.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}