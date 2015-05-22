// <copyright file="UdfFileLocator.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.DiscLocator
{
    using System.IO;
    using DiscUtils.Udf;
    using IO.Locator;

    /// <summary>
    /// An udf based disc file locator.
    /// </summary>
    internal sealed class UdfFileLocator : DiscFsFileLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UdfFileLocator"/> class.
        /// </summary>
        /// <param name="root">The root path.</param>
        public UdfFileLocator(string root)
            : base(root, stream => new UdfReader(stream))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdfFileLocator"/> class.
        /// </summary>
        /// <param name="locator">A file locator</param>
        /// <param name="root">The root path.</param>
        public UdfFileLocator(IFileLocator locator, string root)
            : base(locator, root, stream => new UdfReader(stream))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UdfFileLocator"/> class.
        /// </summary>
        /// <param name="root">A stream.</param>
        public UdfFileLocator(Stream root)
            : base(root, stream => new UdfReader(stream))
        {
        }

        /// <summary>
        /// Detects whether a stream is an udf disc.
        /// </summary>
        /// <param name="root">A stream.</param>
        /// <returns>A boolean.</returns>
        public static bool IsUdfFile(Stream root)
        {
            return UdfReader.Detect(root);
        }
    }
}
