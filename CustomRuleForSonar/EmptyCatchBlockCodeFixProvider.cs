using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace SonarRoslynTesting
{
    //[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EmptyCatchBlockCodeFixProvider)), Shared]
    public class EmptyCatchBlockCodeFixProvider 
    {
        //public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(EmptyCatchBlockAnalyzer.DiagnosticId);

        //public override Task RegisterCodeFixesAsync(CodeFixContext context)
        //{
        //    var diagnostic = context.Diagnostics[0];
        //    var catchClause = (CatchClauseSyntax)diagnostic.Location.SourceTree.GetRoot().FindNode(diagnostic.Location.SourceSpan);

        //    context.RegisterCodeFix(
        //        CodeAction.Create("Add a log statement", c => AddLogStatementAsync(context.Document, catchClause, c), nameof(EmptyCatchBlockCodeFixProvider)),
        //        diagnostic
        //    );

        //    return Task.CompletedTask;
        //}

        //private async Task<Document> AddLogStatementAsync(Document document, CatchClauseSyntax catchClause, CancellationToken cancellationToken)
        //{
        //    var logStatement = SyntaxFactory.ParseStatement("Console.WriteLine(e);");
        //    var newCatchClause = catchClause.WithBlock(catchClause.Block.AddStatements(logStatement));

        //    var root = await document.GetSyntaxRootAsync(cancellationToken);
        //    var newRoot = root.ReplaceNode(catchClause, newCatchClause);

        //    return document.WithSyntaxRoot(newRoot);
        //}
    }
}
