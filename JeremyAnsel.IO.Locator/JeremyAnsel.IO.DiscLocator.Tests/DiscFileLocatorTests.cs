// <copyright file="DiscFileLocatorTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.DiscLocator.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed")]
    public class DiscFileLocatorTests
    {
        [Fact]
        public void Test1()
        {
            Action action = () =>
            {
                using (var locator = DiscFileLocatorFactory.Create(Stream.Null))
                {
                }
            };

            Assert.Throws<NotSupportedException>(action);
        }
    }
}
