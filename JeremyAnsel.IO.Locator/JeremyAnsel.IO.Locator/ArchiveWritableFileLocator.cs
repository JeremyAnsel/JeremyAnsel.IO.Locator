// <copyright file="ArchiveWritableFileLocator.cs" company="Jérémy Ansel">
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
    using SharpCompress.Common;
    using SharpCompress.Writers;

    /// <summary>
    /// A writable archive file locator.
    /// </summary>
    internal sealed class ArchiveWritableFileLocator : IWritableFileLocator
    {
        /// <summary>
        /// A file stream.
        /// </summary>
        private Stream fileStream;

        /// <summary>
        /// An archive.
        /// </summary>
        private IWriter archive;

        /// <summary>
        /// The file keys.
        /// </summary>
        private SortedSet<string> keys;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveWritableFileLocator"/> class.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <param name="archiveType">The archive type.</param>
        /// <param name="compressionType">The compression type.</param>
        public ArchiveWritableFileLocator(string root, ArchiveType archiveType, CompressionType compressionType)
        {
            this.keys = new SortedSet<string>();

            using (var memory = new MemoryFileLocator())
            {
                if (File.Exists(root))
                {
                    long length = 0;

                    using (var file = File.OpenRead(root))
                    {
                        length = file.Length;
                    }

                    if (length != 0)
                    {
                        using (var reader = FileLocatorFactory.Create(root))
                        {
                            memory.WriteAll(reader);
                        }
                    }
                }

                this.fileStream = File.OpenWrite(root);
                this.archive = WriterFactory.Open(this.fileStream, archiveType, new WriterOptions(compressionType));

                if (!memory.IsEmpty)
                {
                    this.WriteAll(memory);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveWritableFileLocator"/> class.
        /// </summary>
        /// <param name="root">A stream.</param>
        /// <param name="archiveType">The archive type.</param>
        /// <param name="compressionType">The compression type.</param>
        public ArchiveWritableFileLocator(Stream root, ArchiveType archiveType, CompressionType compressionType)
        {
            this.keys = new SortedSet<string>();

            this.archive = WriterFactory.Open(root, archiveType, new WriterOptions(compressionType));
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by the <see cref="ArchiveWritableFileLocator"/> object.
        /// </summary>
        public void Dispose()
        {
            if (this.archive != null)
            {
                this.archive.Dispose();
                this.archive = null;
            }

            if (this.fileStream != null)
            {
                this.fileStream.Dispose();
                this.fileStream = null;
            }

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
                throw new ArgumentNullException("path");
            }

            path = Utilities.PathNormalize(path);

            if (this.keys.Contains(path))
            {
                throw new ArgumentOutOfRangeException("path");
            }

            using (var buffer = new MemoryStream())
            {
                buffer.WriteByte(0);
                buffer.Seek(0, SeekOrigin.Begin);

                this.archive.Write(path, buffer);
            }

            this.keys.Add(path);
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
                throw new ArgumentNullException("path");
            }

            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            path = Utilities.PathNormalize(path);

            if (this.keys.Contains(path))
            {
                throw new ArgumentOutOfRangeException("path");
            }

            this.archive.Write(path, data);
            this.keys.Add(path);
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
