// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MessagePack.SourceGenerator
{
    [Generator]
    public class FormatterGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // System.Diagnostics.Debugger.Launch();
            }
#endif

            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var receiver = context.SyntaxReceiver as SyntaxReceiver;
            if (receiver == null)
            {
                return;
            }

            foreach (var (type, attr) in receiver.Targets)
            {
                var model = context.Compilation.GetSemanticModel(type.SyntaxTree);

                var symbol = model.GetSymbolInfo(type);
                var namedTypeSymbol = symbol.Symbol as INamedTypeSymbol;

                // TODO: run TypeCollector
            }
        }

        private class SyntaxReceiver : ISyntaxReceiver
        {
            public List<(StructDeclarationSyntax Type, AttributeSyntax Attr)> Targets { get; } = new();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is StructDeclarationSyntax s && s.AttributeLists.Count > 0)
                {
                    var attr = s.AttributeLists
                        .SelectMany(x => x.Attributes)
                        .FirstOrDefault(x => x.Name.ToString() is "MessagePackObject" or "MessagePackObjectAttribute");

                    if (attr != null)
                    {
                        Targets.Add((s, attr));
                    }
                }
            }
        }
    }
}
