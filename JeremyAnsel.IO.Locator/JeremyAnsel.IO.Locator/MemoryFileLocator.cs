// <copyright file="MemoryFileLocator.cs" company="Jérémy Ansel">
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
    /// An in memory file locator.
    /// </summary>
    public sealed class MemoryFileLocator : IFileLocator, IWritableFileLocator
    {
        /// <summary>
        /// The database.
        /// </summary>
        private Dictionary<string, byte[]> database;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryFileLocator"/> class.
        /// </summary>
        public MemoryFileLocator()
        {
            this.database = new Dictionary<string, byte[]>();
        }

        /// <summary>
        /// Gets a value indicating whether the file locator is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.database.Count == 0;
            }
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by the <see cref="MemoryFileLocator"/> object.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clears the file locator.
        /// </summary>
        public void Clear()
        {
            this.database.Clear();
        }

        /// <summary>
        /// Indicates whether the specified path exists.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A boolean.</returns>
        public bool Exists(string path)
        {
            return this.database.ContainsKey(Utilities.PathNormalize(path));
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A stream.</returns>
        public Stream Open(string path)
        {
            path = Utilities.PathNormalize(path);

            var buffer = this.database[path];
            return new MemoryStream(buffer, false);
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
            foreach (var file in this.database.Keys)
            {
                if (Utilities.PathStartsWith(file, root))
                {
                    yield return file;
                }
            }
        }

        /// <summary>
        /// Create a file.
        /// </summary>
        /// <param name="path">A path.</param>
        public void Create(string path)
        {
            if (string.IsNullOrEmpty("path"))
            {
                throw new ArgumentNullException("path");
            }

            path = Utilities.PathNormalize(path);

            if (this.database.ContainsKey(path))
            {
                throw new ArgumentOutOfRangeException("path");
            }

            this.database.Add(path, new byte[0]);
        }

        /// <summary>
        /// Write a file.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <param name="data">The data.</param>
        public void Write(string path, Stream data)
        {
            if (string.IsNullOrEmpty("path"))
            {
                throw new ArgumentNullException("path");
            }

            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            path = Utilities.PathNormalize(path);

            if (this.database.ContainsKey(path))
            {
                throw new ArgumentOutOfRangeException("path");
            }

            using (var buffer = new MemoryStream())
            {
                data.CopyTo(buffer);

                this.database.Add(path, buffer.ToArray());
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
                throw new ArgumentNullException("locator");
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
