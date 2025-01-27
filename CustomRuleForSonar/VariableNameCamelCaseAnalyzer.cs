using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace SonarRoslynTesting
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class VariableNameCamelCaseAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "VariableNameCamelCase";
        private static readonly LocalizableString Title = "Variable name should be in camelCase";
        private static readonly LocalizableString MessageFormat = "Variable '{0}' should be in camelCase.";
        private static readonly LocalizableString Description = "Variable names should follow camelCase naming convention.";
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            // Register the analyzer to analyze variable declarations (local, parameters, etc.)
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.VariableDeclarator);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var variableDeclarator = (VariableDeclaratorSyntax)context.Node;

            // Get the variable name
            var variableName = variableDeclarator.Identifier.Text;

            // Check if the variable name is in camelCase (starts with lowercase letter)
            if (!IsCamelCase(variableName))
            {
                var diagnostic = Diagnostic.Create(Rule, variableDeclarator.GetLocation(), variableName);
                context.ReportDiagnostic(diagnostic);
            }
        }

        // Check if the variable name follows camelCase convention
        private bool IsCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            // Check that the first character is lowercase
            return char.IsLower(name[0]);
        }
    }
}
