// <copyright file="WritableFileLocatorFactory.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.Locator
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using SharpCompress.Common;

    /// <summary>
    /// A factory to create a writable file locator.
    /// <para>
    /// The supported formats are: file system, Zip, Tar, GZip.
    /// </para>
    /// </summary>
    public static class WritableFileLocatorFactory
    {
        /// <summary>
        /// Creates a writable file locator.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <returns>A writable file locator.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Reviewed")]
        public static IWritableFileLocator Create(string? root)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            string ext = Path.GetExtension(root);

            if (!string.IsNullOrEmpty(ext))
            {
                return ext.ToLowerInvariant() switch
                {
                    ".zip" => new ArchiveWritableFileLocator(root!, ArchiveType.Zip, CompressionType.Deflate),
                    ".gz" => new ArchiveWritableFileLocator(root!, ArchiveType.GZip, CompressionType.GZip),
                    _ => throw new NotSupportedException(),
                };
            }

            return new SystemWritableFileLocator(root!);
        }

        /// <summary>
        /// Creates a writable file locator for an archive.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <param name="archiveType">The archive type.</param>
        /// <param name="compressionType">The compression type.</param>
        /// <returns>A writable file locator.</returns>
        public static IWritableFileLocator CreateArchive(string? root, ArchiveType archiveType, CompressionType compressionType)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            return new ArchiveWritableFileLocator(root!, archiveType, compressionType);
        }

        /// <summary>
        /// Creates a writable file locator for an archive.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <param name="archiveType">The archive type.</param>
        /// <returns>A writable file locator.</returns>
        public static IWritableFileLocator CreateArchive(string? root, ArchiveType archiveType)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            return new ArchiveWritableFileLocator(root!, archiveType, CompressionType.Deflate);
        }

        /// <summary>
        /// Creates a writable file locator for an archive from a stream.
        /// </summary>
        /// <param name="root">A stream</param>
        /// <param name="archiveType">The archive type.</param>
        /// <param name="compressionType">The compression type.</param>
        /// <returns>A writable file locator.</returns>
        public static IWritableFileLocator CreateArchive(Stream? root, ArchiveType archiveType, CompressionType compressionType)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            return new ArchiveWritableFileLocator(root, archiveType, compressionType);
        }

        /// <summary>
        /// Creates a writable file locator for an archive from a stream.
        /// </summary>
        /// <param name="root">A stream.</param>
        /// <param name="archiveType">The archive type.</param>
        /// <returns>A writable file locator.</returns>
        public static IWritableFileLocator CreateArchive(Stream? root, ArchiveType archiveType)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            return new ArchiveWritableFileLocator(root, archiveType, CompressionType.Deflate);
        }
    }
}
