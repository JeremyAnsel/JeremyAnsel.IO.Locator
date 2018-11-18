# JeremyAnsel.IO.Locator

[![Build status](https://ci.appveyor.com/api/projects/status/k287tlr4vk3bs5c5/branch/master?svg=true)](https://ci.appveyor.com/project/JeremyAnsel/jeremyansel-io-locator/branch/master)
[![Code coverage](https://jeremyansel.github.io/JeremyAnsel.IO.Locator/coverage/badge_combined.svg)](https://jeremyansel.github.io/JeremyAnsel.IO.Locator/coverage/)

JeremyAnsel.IO.Locator:
[![NuGet Version](https://buildstats.info/nuget/JeremyAnsel.IO.Locator)](https://www.nuget.org/packages/JeremyAnsel.IO.Locator)
[![NuGet Status](http://nugetstatus.com/JeremyAnsel.IO.Locator.png)](http://nugetstatus.com/packages/JeremyAnsel.IO.Locator)

JeremyAnsel.IO.DiscLocator:
[![NuGet Version](https://buildstats.info/nuget/JeremyAnsel.IO.DiscLocator)](https://www.nuget.org/packages/JeremyAnsel.IO.DiscLocator)
[![NuGet Status](http://nugetstatus.com/JeremyAnsel.IO.DiscLocator.png)](http://nugetstatus.com/packages/JeremyAnsel.IO.DiscLocator)

JeremyAnsel.IO.Locator is a library to abstract file access to file system, compressed archives and disc images.

Description     | Value
----------------|----------------
License         | [The MIT License (MIT)](https://github.com/JeremyAnsel/JeremyAnsel.IO.Locator/blob/master/LICENSE.txt)
Web site        | http://jeremyansel.github.io/JeremyAnsel.IO.Locator
Documentation   | http://jeremyansel.github.io/JeremyAnsel.IO.Locator/doc/
Code coverage   | https://jeremyansel.github.io/JeremyAnsel.IO.Locator/coverage/
Source code     | https://github.com/JeremyAnsel/JeremyAnsel.IO.Locator
Nuget           | https://www.nuget.org/packages/JeremyAnsel.IO.Locator
Nuget           | https://www.nuget.org/packages/JeremyAnsel.IO.DiscLocator
Build           | https://ci.appveyor.com/project/JeremyAnsel/jeremyansel-io-locator/branch/master

The supported formats by `FileLocatorFactory` are: file system, Zip, Rar, Tar, 7Zip, GZip.
[SharpCompress](https://github.com/adamhathcock/sharpcompress) is used to provide support for compressed archives.

The supported formats by `DiscLocatorFactory` are: Iso, Udf.
[DiscUtils](https://github.com/discutils/discutils) is used to provide support for disc images.
