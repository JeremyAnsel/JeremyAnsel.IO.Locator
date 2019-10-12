// <copyright file="FileLocatorFactory.cs" company="Jérémy Ansel">
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
    /// A factory to create a file locator.
    /// </summary>
    /// <remarks>
    /// The supported formats are: file system, Zip, Rar, Tar, 7Zip, GZip.
    /// </remarks>
    public static class FileLocatorFactory
    {
        /// <summary>
        /// Creates a file locator.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <returns>A file locator.</returns>
        public static IFileLocator Create(string root)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (Path.HasExtension(root))
            {
                return new ArchiveFileLocator(root);
            }

            return new SystemFileLocator(root);
        }

        /// <summary>
        /// Creates a file locator from a file locator.
        /// </summary>
        /// <param name="locator">The source file locator.</param>
        /// <param name="root">The root path.</param>
        /// <returns>A file locator.</returns>
        public static IFileLocator Create(IFileLocator locator, string root)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }

            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (Path.HasExtension(root))
            {
                return new ArchiveFileLocator(locator, root);
            }

            return new SystemFileLocator(locator, root);
        }

        /// <summary>
        /// Creates a file locator from a stream.
        /// </summary>
        /// <param name="root">A stream.</param>
        /// <returns>A file locator.</returns>
        public static IFileLocator Create(Stream root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            return new ArchiveFileLocator(root);
        }
    }
}
