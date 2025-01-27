using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarRoslynTesting
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EmptyCatchBlockAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "EmptyCatchBlock";
        private static readonly LocalizableString Title = "Empty catch block";
        private static readonly LocalizableString MessageFormat = "Catch block should not be empty.";
        private static readonly LocalizableString Description = "Catch blocks should not be left empty.";
        private const string Category = "Style";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            // We need to register for syntax node analysis
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.TryStatement);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var tryStatement = (TryStatementSyntax)context.Node;
            var catchClause = tryStatement.Catches.FirstOrDefault();

            // Check if the catch block is empty
            if (catchClause != null && catchClause.Block?.Statements.Count == 0)
            {
                var diagnostic = Diagnostic.Create(Rule, catchClause.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
