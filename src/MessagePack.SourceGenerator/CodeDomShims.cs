// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable SA1649 // File name should match first type name

namespace System.CodeDom.Compiler
{
    public class CompilerError
    {
        public string? ErrorText { get; set; }

        public bool IsWarning { get; set; }
    }

    public class CompilerErrorCollection
    {
        public void Add(CompilerError error)
        {
        }
    }
}

#pragma warning restore SA1649 // File name should match first type name
