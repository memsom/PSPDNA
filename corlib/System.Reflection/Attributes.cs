// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyCompanyAttribute : Attribute
    {
        public AssemblyCompanyAttribute(string company)
        {
            Company = company;
        }

        public string Company { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyConfigurationAttribute : Attribute
    {
        public AssemblyConfigurationAttribute(string configuration)
        {
            Configuration = configuration;
        }

        public string Configuration { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyFileVersionAttribute : Attribute
    {
        public AssemblyFileVersionAttribute(string version)
        {
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }

        public string Version { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyInformationalVersionAttribute : Attribute
    {
        public AssemblyInformationalVersionAttribute(string informationalVersion)
        {
            InformationalVersion = informationalVersion;
        }

        public string InformationalVersion { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyTitleAttribute : Attribute
    {
        public AssemblyTitleAttribute(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyProductAttribute : Attribute
    {
        public AssemblyProductAttribute(string product)
        {
            Product = product;
        }

        public string Product { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyVersionAttribute : Attribute
    {
        public AssemblyVersionAttribute(string version)
        {
            Version = version;
        }

        public string Version { get; }
    }
}
