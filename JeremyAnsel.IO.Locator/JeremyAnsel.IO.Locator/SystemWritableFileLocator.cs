// <copyright file="SystemWritableFileLocator.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.Locator
{
    using System;
    using System.IO;

    /// <summary>
    /// A writable file system file locator.
    /// </summary>
    internal sealed class SystemWritableFileLocator : IWritableFileLocator
    {
        /// <summary>
        /// The root path.
        /// </summary>
        private string rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemWritableFileLocator"/> class.
        /// </summary>
        /// <param name="root">The root path.</param>
        public SystemWritableFileLocator(string root)
        {
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException();
            }

            this.rootPath = root;
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by the <see cref="ArchiveWritableFileLocator"/> object.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Create a file.
        /// </summary>
        /// <param name="path">A path.</param>
        public void Create(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            string fullPath = Utilities.PathNormalize(Path.Combine(this.rootPath, path));

            if (Directory.Exists(fullPath) || File.Exists(fullPath))
            {
                throw new ArgumentOutOfRangeException(nameof(path));
            }

            string dirPath = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            using (File.Create(fullPath))
            {
            }
        }

        /// <summary>
        /// Write a file.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <param name="data">The data.</param>
        public void Write(string path, Stream data)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            this.Create(path);

            using (var file = File.OpenWrite(path))
            {
                data.CopyTo(file);
            }
        }

        /// <summary>
        /// Write the files from a file locator.
        /// </summary>
        /// <param name="locator">A file locator.</param>
        public void WriteAll(IFileLocator locator)
        {
            this.WriteAll(locator, string.Empty);
        }

        /// <summary>
        /// Write the files from a file locator.
        /// </summary>
        /// <param name="locator">A file locator.</param>
        /// <param name="root">The root path.</param>
        public void WriteAll(IFileLocator locator, string root)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }

            foreach (var file in locator.EnumerateFiles(root))
            {
                using (var stream = locator.Open(file))
                {
                    this.Write(file, stream);
                }
            }
        }
    }
}
