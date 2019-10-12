// <copyright file="IWritableFileLocator.cs" company="Jérémy Ansel">
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
    /// The writable file locator interface.
    /// </summary>
    public interface IWritableFileLocator : IDisposable
    {
        /// <summary>
        /// Create a file.
        /// </summary>
        /// <param name="path">A path.</param>
        void Create(string path);

        /// <summary>
        /// Write a file.
        /// </summary>
        /// <param name="path">A path.</param>
        /// <param name="data">The data.</param>
        void Write(string path, Stream data);

        /// <summary>
        /// Write the files from a file locator.
        /// </summary>
        /// <param name="locator">A file locator.</param>
        void WriteAll(IFileLocator locator);

        /// <summary>
        /// Write the files from a file locator.
        /// </summary>
        /// <param name="locator">A file locator.</param>
        /// <param name="root">The root path.</param>
        void WriteAll(IFileLocator locator, string root);
    }
}
