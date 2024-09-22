# JeremyAnsel.IO.Locator

[![Build status](https://ci.appveyor.com/api/projects/status/k287tlr4vk3bs5c5/branch/master?svg=true)](https://ci.appveyor.com/project/JeremyAnsel/jeremyansel-io-locator/branch/master)
[![Code coverage](https://raw.githubusercontent.com/JeremyAnsel/JeremyAnsel.IO.Locator/gh-pages/coverage/badge_combined.svg)](https://jeremyansel.github.io/JeremyAnsel.IO.Locator/coverage/)
![License](https://img.shields.io/github/license/JeremyAnsel/JeremyAnsel.IO.Locator)

JeremyAnsel.IO.Locator:
[![NuGet Version](https://img.shields.io/nuget/v/JeremyAnsel.IO.Locator)](https://www.nuget.org/packages/JeremyAnsel.IO.Locator)

JeremyAnsel.IO.DiscLocator:
[![NuGet Version](https://img.shields.io/nuget/v/JeremyAnsel.IO.DiscLocator)](https://www.nuget.org/packages/JeremyAnsel.IO.DiscLocator)

JeremyAnsel.IO.Locator is a .Net library to abstract file access to file system, compressed archives and disc images.

Description     | Value
----------------|----------------
License         | [The MIT License (MIT)](https://github.com/JeremyAnsel/JeremyAnsel.IO.Locator/blob/master/LICENSE.txt)
Documentation   | http://jeremyansel.github.io/JeremyAnsel.IO.Locator
Code coverage   | https://jeremyansel.github.io/JeremyAnsel.IO.Locator/coverage/
Source code     | https://github.com/JeremyAnsel/JeremyAnsel.IO.Locator
Nuget           | https://www.nuget.org/packages/JeremyAnsel.IO.Locator
Nuget           | https://www.nuget.org/packages/JeremyAnsel.IO.DiscLocator
Build           | https://ci.appveyor.com/project/JeremyAnsel/jeremyansel-io-locator/branch/master

The supported formats by `FileLocatorFactory` are: file system, Zip, Rar, Tar, 7Zip, GZip.
[SharpCompress](https://github.com/adamhathcock/sharpcompress) is used to provide support for compressed archives.

The supported formats by `DiscLocatorFactory` are: Iso, Udf.
[DiscUtils](https://github.com/LTRData/DiscUtils) is used to provide support for disc images.

# Usage

## File locator
```csharp
using IFileLocator factory = FileLocatorFactory.Create(filename);
```

## Disc locator
```csharp
using IFileLocator factory = DiscFileLocatorFactory.Create(filename);
```
