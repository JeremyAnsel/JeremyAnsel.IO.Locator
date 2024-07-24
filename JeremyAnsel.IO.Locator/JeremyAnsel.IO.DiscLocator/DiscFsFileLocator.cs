// <copyright file="DiscFsFileLocator.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.DiscLocator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DiscUtils;
    using IO.Locator;

    /// <summary>
    /// Base class for the disc file locators.
    /// </summary>
    internal abstract class DiscFsFileLocator : IFileLocator
    {
        /// <summary>
        /// A file stream.
        /// </summary>
        private Stream? fileStream;

        /// <summary>
        /// A disc.
        /// </summary>
        private DiscFileSystem? disc;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscFsFileLocator"/> class.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <param name="create">The create function.</param>
        protected DiscFsFileLocator(string root, Func<Stream, DiscFileSystem> create)
        {
            this.fileStream = File.OpenRead(root);
            this.disc = create(this.fileStream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscFsFileLocator"/> class.
        /// </summary>
        /// <param name="locator">A file locator.</param>
        /// <param name="root">The root path.</param>
        /// <param name="create">The create function.</param>
        protected DiscFsFileLocator(IFileLocator? locator, string root, Func<Stream, DiscFileSystem> create)
        {
            if (locator != null)
            {
                this.fileStream = locator.Open(root);
            }
            else
            {
                this.fileStream = File.OpenRead(root);
            }

            this.disc = create(this.fileStream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscFsFileLocator"/> class.
        /// </summary>
        /// <param name="root">A stream.</param>
        /// <param name="create">The create function.</param>
        protected DiscFsFileLocator(Stream root, Func<Stream, DiscFileSystem> create)
        {
            this.disc = create(root);
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by the <see cref="DiscFsFileLocator"/> object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Indicates whether the specified path exists.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A boolean.</returns>
        public bool Exists(string path)
        {
            return this.disc!.FileExists(Utilities.PathNormalize(path));
        }

        /// <summary>
        /// Open the specified file.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A stream.</returns>
        public Stream Open(string path)
        {
            return this.disc!.OpenFile(Utilities.PathNormalize(path), FileMode.Open, FileAccess.Read);
        }

        /// <summary>
        /// Enumerates the files.
        /// </summary>
        /// <returns>The files.</returns>
        public IEnumerable<string> EnumerateFiles()
        {
            return this.EnumerateFiles(string.Empty);
        }

        /// <summary>
        /// Enumerates the files.
        /// </summary>
        /// <param name="root">A root path.</param>
        /// <returns>The files.</returns>
        public IEnumerable<string> EnumerateFiles(string root)
        {
            root = Utilities.PathNormalize(root);

            if (!this.disc!.DirectoryExists(root))
            {
                yield break;
            }

            foreach (var file in this.disc.GetFiles(root, "*.*", SearchOption.AllDirectories))
            {
                yield return Utilities.PathNormalize(file);
            }
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by the <see cref="DiscFsFileLocator"/> object.
        /// </summary>
        /// <param name="disposing">A boolean.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.disc != null)
                {
                    this.disc.Dispose();
                    this.disc = null;
                }

                if (this.fileStream != null)
                {
                    this.fileStream.Dispose();
                    this.fileStream = null;
                }
            }
        }
    }
}
