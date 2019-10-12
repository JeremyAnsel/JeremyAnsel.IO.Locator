// <copyright file="IFileLocator.cs" company="Jérémy Ansel">
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

    /// <summary>
    /// The file locator interface.
    /// </summary>
    public interface IFileLocator : IDisposable
    {
        /// <summary>
        /// Indicates whether the specified path exists.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A boolean.</returns>
        bool Exists(string path);

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <returns>A stream.</returns>
        Stream Open(string path);

        /// <summary>
        /// Enumerate the files.
        /// </summary>
        /// <returns>An enumeration.</returns>
        IEnumerable<string> EnumerateFiles();

        /// <summary>
        /// Enumerate the files.
        /// </summary>
        /// <param name="root">The root path.</param>
        /// <returns>An enumeration.</returns>
        IEnumerable<string> EnumerateFiles(string root);
    }
}
