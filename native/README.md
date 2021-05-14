# DNA
The DNA project is originally at https://github.com/chrisdunelm/DotNetAnywhere, though
has not recently been maintained (~ 5 years). See the 'src' directory for license info.

This code is a merge of the original code changes from other DNA repos, plus the extensions made by Steve Sanderson's Blazor project. https://github.com/SteveSanderson/Blazor

In this copy of the DNA code, various changes have been made, e.g.:
 - To fix some bugs
 - To support loading .NET Core-style assemblies

It differs from Blazor because there is no support for JavaScript (sorry, not interested in that), and so we have excluded all of the changes related to JavaScript integration that Blazor added. The main issue with this version is that the P/Invoke interface is a bit broken, due to the changes in Blazor. I'm going to look  at reverting to the original mechanism, but I didn't have time yet to look at why Steve hard coded the interface for .Net Core.

I took the Blazor changes to allow Core support, as this makes compiling code on any platform a lot easier.

Likewise, the corlib.csproj project has been extended to support extra APIs.
