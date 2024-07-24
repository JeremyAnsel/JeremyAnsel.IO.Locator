// <copyright file="ArchiveFileLocator.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.Locator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using SharpCompress.Archives;

    /// <summary>
    /// An archive file locator.
    /// </summary>
    internal sealed class ArchiveFileLocator : IFileLocator
    {
        /// <summary>
        /// A file stream.
        /// </summary>
        private Stream? fileStream;

        /// <summary>
        /// an archive.
        /// </summary>
        private IArchive? archive;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveFileLocator"/> class.
        /// </summary>
        /// <param name="root">The root path.</param>
        public ArchiveFileLocator(string root)
        {
            this.archive = ArchiveFactory.Open(root);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveFileLocator"/> class.
        /// </summary>
        /// <param name="locator">A file locator.</param>
        /// <param name="root">The root path.</param>
        public ArchiveFileLocator(IFileLocator? locator, string root)
        {
            if (locator != null)
            {
                this.fileStream = locator.Open(root);
                this.archive = ArchiveFactory.Open(this.fileStream);
            }
            else
            {
                this.archive = ArchiveFactory.Open(root);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveFileLocator"/> class.
        /// </summary>
        /// <param name="root">A stream.</param>
        public ArchiveFileLocator(Stream root)
        {
            this.archive = ArchiveFactory.Open(root);
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by the <see cref="ArchiveFileLocator"/> object.
        /// </summary>
        public void Dispose()
        {
            if (this.fileStream != null)
            {
                this.fileStream.Dispose();
                this.fileStream = null;
            }

            if (this.archive != null)
            {
                this.archive.Dispose();
                this.archive = null;
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Indicates whether the specified path exists.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A boolean.</returns>
        public bool Exists(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            foreach (var entry in this.archive!.Entries)
            {
                if (entry.IsDirectory)
                {
                    continue;
                }

                if (Utilities.PathEquals(entry.Key, path))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A stream.</returns>
        public Stream Open(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            foreach (var entry in this.archive!.Entries)
            {
                if (entry.IsDirectory)
                {
                    continue;
                }

                if (Utilities.PathEquals(entry.Key, path))
                {
                    return entry.OpenEntryStream();
                }
            }

            throw new FileNotFoundException();
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
            foreach (var entry in this.archive!.Entries)
            {
                if (entry.IsDirectory)
                {
                    continue;
                }

                if (Utilities.PathStartsWith(entry.Key, root))
                {
                    yield return Utilities.PathNormalize(entry.Key);
                }
            }
        }
    }
}
