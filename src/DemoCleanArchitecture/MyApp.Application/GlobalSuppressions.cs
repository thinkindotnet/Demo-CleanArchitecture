// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.


using System.Diagnostics.CodeAnalysis;


// Instead of:
//          int[] arr = new int[] { 10, 20, 30 };
//          OR
//          var arr = new int[] { 10, 20, 30 };
// in C# 12, we can use the simplified collection initialization:
//          int[] arr = [ 10, 20, 30 ];
[assembly: SuppressMessage(
    category: "Style",
    checkId:"IDE0300:Simplify collection initialization", 
    Justification = "Personal preference: I prefer elaborate collection initialization", 
    Scope = "module")]


// Instead of:
//          List<Employee> arr = new List<Employee>();
// in C# 12, we can use the simplified collection initialization:
//          List<Employee> arr = [];
[assembly: SuppressMessage(
    category: "Style",
    checkId: "IDE0028:Simplify collection initialization",
    Justification = "Personal preference: I prefer elaborate collection initialization.",
    Scope = "module")]


[assembly: SuppressMessage(
    category: "Style",
    checkId: "IDE0290:Use primary constructor", 
    Justification = "Personal preference: I do not want to use Primary Constructors.", 
    Scope = "module")]
