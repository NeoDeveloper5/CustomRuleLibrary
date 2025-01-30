using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CustomRuleForSonar
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CamelCaseTypeScriptAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "CamelCaseRule";
        private static readonly LocalizableString Title = "Variable names should be in camelCase";
        private static readonly LocalizableString MessageFormat = "Variable '{0}' should be in camelCase.";
        private static readonly LocalizableString Description = "Ensure that variable names follow the camelCase convention.";
        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, "Naming", DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            // Only analyze syntax nodes of type VariableDeclaratorSyntax
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.VariableDeclarator);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var variableDeclarator = (VariableDeclaratorSyntax)context.Node;

            // Check if the variable name follows camelCase
            var variableName = variableDeclarator.Identifier.Text;
            if (!IsCamelCase(variableName))
            {
                var diagnostic = Diagnostic.Create(Rule, variableDeclarator.GetLocation(), variableName);
                context.ReportDiagnostic(diagnostic);
            }
        }

        // Check if the variable name follows camelCase convention
        private bool IsCamelCase(string name)
        {
            // Camel case validation: first letter lowercase, no underscores or spaces
            return char.IsLower(name[0]) && !name.Contains("_");
        }
    }
}
