// <copyright file="DiscFileLocatorFactory.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.DiscLocator
{
    using System;
    using System.IO;
    using IO.Locator;

    /// <summary>
    /// A factory to create a disc file locator.
    /// </summary>
    /// <remarks>
    /// The supported formats are: Iso, Udf.
    /// </remarks>
    public static class DiscFileLocatorFactory
    {
        /// <summary>
        /// Creates a disc file locator from a file.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <returns>A file locator.</returns>
        public static IFileLocator Create(string root)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (DiscFileLocatorFactory.IsDisc(root, IsoFileLocator.IsIsoFile))
            {
                return new IsoFileLocator(root);
            }

            if (DiscFileLocatorFactory.IsDisc(root, UdfFileLocator.IsUdfFile))
            {
                return new UdfFileLocator(root);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Creates a disc file locator from a locator.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="root">The root path.</param>
        /// <returns>A file locator.</returns>
        public static IFileLocator Create(IFileLocator locator, string root)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }

            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (DiscFileLocatorFactory.IsDisc(locator, root, IsoFileLocator.IsIsoFile))
            {
                return new IsoFileLocator(locator, root);
            }

            if (DiscFileLocatorFactory.IsDisc(locator, root, UdfFileLocator.IsUdfFile))
            {
                return new UdfFileLocator(locator, root);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Creates a disc file locator from a stream.
        /// </summary>
        /// <param name="root">The stream.</param>
        /// <returns>A file locator.</returns>
        public static IFileLocator Create(Stream root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (IsoFileLocator.IsIsoFile(root))
            {
                root.Seek(0, SeekOrigin.Begin);
                return new IsoFileLocator(root);
            }

            if (UdfFileLocator.IsUdfFile(root))
            {
                root.Seek(0, SeekOrigin.Begin);
                return new UdfFileLocator(root);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Detects if the specified root is a disc.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="detect">The detection function.</param>
        /// <returns>A boolean.</returns>
        private static bool IsDisc(string root, Func<Stream, bool> detect)
        {
            using (var file = File.OpenRead(root))
            {
                return detect(file);
            }
        }

        /// <summary>
        /// Detects if the specified root from a locator is a disc.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <param name="root">The root.</param>
        /// <param name="detect">The detection function.</param>
        /// <returns>A boolean.</returns>
        private static bool IsDisc(IFileLocator locator, string root, Func<Stream, bool> detect)
        {
            using (var file = locator.Open(root))
            {
                return detect(file);
            }
        }
    }
}
