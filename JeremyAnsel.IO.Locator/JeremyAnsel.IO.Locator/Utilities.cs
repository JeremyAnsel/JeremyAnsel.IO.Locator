// <copyright file="Utilities.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.Locator
{
    using System;
    using System.IO;

    /// <summary>
    /// Utility methods.
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// Normalizes a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A normalized path.</returns>
        public static string PathNormalize(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            return path
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
                .TrimStart(Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Indicates whether two paths are equals.
        /// </summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <returns>A boolean.</returns>
        public static bool PathEquals(string path1, string path2)
        {
            path1 = Utilities.PathNormalize(path1);
            path2 = Utilities.PathNormalize(path2);

            return string.Equals(path1, path2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Indicates whether a path starts with a specified path.
        /// </summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <returns>A boolean.</returns>
        public static bool PathStartsWith(string path1, string path2)
        {
            path1 = Utilities.PathNormalize(path1);
            path2 = Utilities.PathNormalize(path2);

            return path1.StartsWith(path2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
