using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CustomRuleForSonar
{
    public class CamelCaseIdHTMLAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "CamelCaseIdRule";
        private static readonly LocalizableString Title = "ID attribute value should be in camel case";
        private static readonly LocalizableString MessageFormat = "ID attribute '{0}' is not in camel case";
        private static readonly LocalizableString Description = "Ensure that HTML ID attributes are in camel case.";
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.Attribute);
        }

        private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var attributeSyntax = (AttributeSyntax)context.Node;
            if (attributeSyntax.Name.ToString() == "id")
            {
                var idValue = attributeSyntax.ArgumentList.Arguments.First().ToString().Trim('"');
                if (!IsCamelCase(idValue))
                {
                    var diagnostic = Diagnostic.Create(Rule, attributeSyntax.GetLocation(), idValue);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private static bool IsCamelCase(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, "^[a-z]+([A-Z][a-z]*)*$");
        }
    }
}
