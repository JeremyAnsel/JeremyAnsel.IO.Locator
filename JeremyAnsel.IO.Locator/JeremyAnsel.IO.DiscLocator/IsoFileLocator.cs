// <copyright file="IsoFileLocator.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.DiscLocator
{
    using System.IO;
    using DiscUtils.Iso9660;
    using IO.Locator;

    /// <summary>
    /// An iso based disc file locator.
    /// </summary>
    internal sealed class IsoFileLocator : DiscFsFileLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsoFileLocator"/> class.
        /// </summary>
        /// <param name="root">The root path.</param>
        public IsoFileLocator(string root)
            : base(root, stream => new CDReader(stream, true, true))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsoFileLocator"/> class.
        /// </summary>
        /// <param name="locator">A file locator.</param>
        /// <param name="root">The root path.</param>
        public IsoFileLocator(IFileLocator locator, string root)
            : base(locator, root, stream => new CDReader(stream, true, true))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsoFileLocator"/> class.
        /// </summary>
        /// <param name="root">A stream.</param>
        public IsoFileLocator(Stream root)
            : base(root, stream => new CDReader(stream, true, true))
        {
        }

        /// <summary>
        /// Detects whether a stream is an iso disc.
        /// </summary>
        /// <param name="root">A stream.</param>
        /// <returns>A boolean.</returns>
        public static bool IsIsoFile(Stream root)
        {
            return CDReader.Detect(root);
        }
    }
}
