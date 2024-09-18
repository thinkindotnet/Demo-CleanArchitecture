// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    category: "Style",
    checkId:"IDE0300:Simplify collection initialization", 
    Justification = "Personal preference: I prefer elaborate collection initialization", 
    Scope = "module")]

[assembly: SuppressMessage(
    category: "Style",
    checkId: "IDE0290:Use primary constructor", 
    Justification = "Personal preference: Do not want to use Primary Constructors", 
    Scope = "module")]
