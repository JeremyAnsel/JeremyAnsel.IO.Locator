// <copyright file="Utilities.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.IO.DiscLocator
{
    using System.Globalization;
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
                .TrimStart(Path.DirectorySeparatorChar)
                .Insert(0, Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture));
        }
    }
}
