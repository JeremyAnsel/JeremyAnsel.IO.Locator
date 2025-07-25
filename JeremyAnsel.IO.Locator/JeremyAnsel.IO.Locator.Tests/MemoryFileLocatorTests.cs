using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace JeremyAnsel.IO.Locator.Tests;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed")]
public class MemoryFileLocatorTests
{
    [Fact]
    public void New_IsEmpty()
    {
        using var locator = new MemoryFileLocator();

        Assert.True(locator.IsEmpty);
    }

    [Fact]
    public void Clear_IsEmpty()
    {
        using var locator = new MemoryFileLocator();
        locator.Create("test1");

        locator.Clear();

        Assert.True(locator.IsEmpty);
    }

    [Fact]
    public void Create_Valid()
    {
        using var locator = new MemoryFileLocator();
        locator.Create("test1");
        locator.Create("test2");

        var files = locator.EnumerateFiles().ToList();

        Assert.Equal(2, files.Count);
        Assert.Equal("test1", files[0]);
        Assert.Equal("test2", files[1]);
    }

    [Fact]
    public void Create_Null()
    {
        using var locator = new MemoryFileLocator();
        Assert.Throws<ArgumentNullException>(() => locator.Create(string.Empty));
    }

    [Fact]
    public void Create_Duplicate()
    {
        using var locator = new MemoryFileLocator();
        locator.Create("test1");
        Assert.Throws<ArgumentOutOfRangeException>(() => locator.Create("test1"));
    }

    [Fact]
    public void Exists()
    {
        using var locator = new MemoryFileLocator();
        locator.Create("test1");
        locator.Create("test2");

        Assert.True(locator.Exists("test1"));
        Assert.True(locator.Exists("test2"));
    }
}
