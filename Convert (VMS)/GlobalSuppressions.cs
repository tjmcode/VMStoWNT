// <copyright file="GlobalSuppressions.cs" company="MicroCODE Incorporated">Copyright © 2018-2020 MicroCODE Incorporated Troy, MI</copyright><author>Timothy J. McGuire</author>
//
//
// MicroCODE Incorporated -- Visual Studio 2019
//
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to all MicroCODE projects.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

// FxCop Suppression to keep certain naming from raising warnings and errors
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "namespace", Target = "MicroCODE", MessageId = "CODE", Justification = "Company Name: MicroCODE")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1034:Do not nest type", Justification = "Entire design is based on Microsoft 2010 Rules", MessageId = "MicroCODE Nested Types")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1062:Externally visible method uses nullable field", Justification = "The nullable fields are always loaded in the Constructor", MessageId = "MicroCODE Constructor Contract")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1303:Use Resource Table for static strings", Justification = "Operator messages are translated by 'Language' class, all internals are static English", MessageId = "MicroCODE Language Support")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1031:Do not catch General Exceptions", Justification = "No exception in a manufacturing/automation should be allowed to crash an application", MessageId = "MicroCODE Manufacturing")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "MicroCODE Language is used for all localization", MessageId = "MicroCODE Language Support")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Code Analysis conflcits, need to dispose of other objects", MessageId = "Microsoft Inconsistences")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0057:Use range operator", Justification = "StyleCop does not recognize C# 8.0 Range Operator", Scope = "member", Target = "~M:MicroCODE.App.CopyFiles(System.String,System.String,System.String,System.Int32)~System.Boolean")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0056:Use index operator", Justification = "StyleCop does not recognize C# 8.0 Range Operator", Scope = "member", Target = "~M:MicroCODE.ASCII.ReduceWhiteSpace(System.String)~System.String")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0057:Use range operator", Justification = "StyleCop does not recognize C# 8.0 Range Operator", Scope = "member", Target = "~M:MicroCODE.ASCII.TrimEnd(System.String)~System.String")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0057:Use range operator", Justification = "StyleCop does not recognize C# 8.0 Range Operator", Scope = "member", Target = "~M:MicroCODE.ASCII.TrimEnds(System.String)~System.String")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0057:Use range operator", Justification = "StyleCop does not recognize C# 8.0 Range Operator", Scope = "member", Target = "~M:MicroCODE.App.RestoreFiles(System.String,System.String,System.String)~System.Boolean")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0057:Use range operator", Justification = "StyleCop does not recognize C# 8.0 Range Operator", Scope = "member", Target = "~M:MicroCODE.App.DebugMessage(System.String,System.String)")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "StyleCop does not recognize C# 8.0 Switch", Scope = "member", Target = "~M:MicroCODE.App.DotNetName(System.String,System.Boolean)~System.String")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0057:Use range operator", Justification = "StyleCop does not recognize C# 8.0 Range Operator", Scope = "member", Target = "~M:MicroCODE.ASCII.TrimEnd(System.String)~System.String")]