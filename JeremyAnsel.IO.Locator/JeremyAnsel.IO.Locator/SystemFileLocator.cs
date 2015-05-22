// <copyright file="SystemFileLocator.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.Locator
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// A file system file locator.
    /// </summary>
    internal sealed class SystemFileLocator : IFileLocator
    {
        /// <summary>
        /// A file locator.
        /// </summary>
        private IFileLocator locator;

        /// <summary>
        /// The root path.
        /// </summary>
        private string rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemFileLocator"/> class.
        /// </summary>
        /// <param name="root">The root path.</param>
        public SystemFileLocator(string root)
        {
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException();
            }

            this.rootPath = root;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemFileLocator"/> class.
        /// </summary>
        /// <param name="locator">A file locator.</param>
        /// <param name="root">The root path.</param>
        public SystemFileLocator(IFileLocator locator, string root)
        {
            if (locator == null && !Directory.Exists(root))
            {
                throw new DirectoryNotFoundException();
            }

            this.locator = locator;
            this.rootPath = root;
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by the <see cref="ArchiveFileLocator"/> object.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Indicates whether the specified path exists.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A boolean.</returns>
        public bool Exists(string path)
        {
            string fullPath = Path.Combine(this.rootPath, path);

            if (this.locator != null)
            {
                return this.locator.Exists(fullPath);
            }

            return File.Exists(fullPath);
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A stream.</returns>
        public Stream Open(string path)
        {
            string fullPath = Path.Combine(this.rootPath, path);

            if (this.locator != null)
            {
                return this.locator.Open(fullPath);
            }

            return File.OpenRead(fullPath);
        }

        /// <summary>
        /// Enumerate the files.
        /// </summary>
        /// <returns>An enumeration.</returns>
        public IEnumerable<string> EnumerateFiles()
        {
            return this.EnumerateFiles(string.Empty);
        }

        /// <summary>
        /// Enumerate the files.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <returns>An enumeration.</returns>
        public IEnumerable<string> EnumerateFiles(string root)
        {
            string fullPath = Utilities.PathNormalize(Path.Combine(this.rootPath, root));

            IEnumerable<string> results;

            if (this.locator != null)
            {
                results = this.locator.EnumerateFiles(fullPath);
            }
            else
            {
                results = Directory.EnumerateFiles(fullPath, "*.*", SearchOption.AllDirectories);
            }

            foreach (var file in results)
            {
                yield return Utilities.PathNormalize(Utilities.PathNormalize(file).Substring(fullPath.Length));
            }
        }
    }
}
