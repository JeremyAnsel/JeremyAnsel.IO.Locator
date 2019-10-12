// <copyright file="FileLocatorTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.Locator.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using SharpCompress.Common;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed")]
    public class FileLocatorTests
    {
        [Fact]
        public void Test1()
        {
            var temp = Path.GetTempFileName();

            try
            {
                for (int pass = 0; pass < 2; pass++)
                {
                    using (var zip = WritableFileLocatorFactory.CreateArchive(temp, ArchiveType.Zip, CompressionType.LZMA))
                    {
                        zip.Create(Path.Combine("a", Path.GetRandomFileName()));
                    }
                }

                using (var reader = FileLocatorFactory.Create(temp))
                {
                    Assert.Equal(2, reader.EnumerateFiles().Count());
                }
            }
            finally
            {
                File.Delete(temp);
            }
        }
    }
}
