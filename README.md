# NuPack
Visual Studio extension for building and publishing NuGet packages.

## Features of NuPack 2022
* Support both SDK-based projects and classic .NET framework projects.Use the built-in dotnet/nuget CLI instead of manually selecting nuget.exe.
* Migrate .nuspec to Package Properties.
* DONOT keep source URLs or API keys any more but symbols servers, use the sources in NuGet.Config as default.

## Screenshots
![Deploy context menu](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/deploy_context_menu.png)
![Package metadata](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/nuget_manifest.png)
![Pack options](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/pack_push.png)

## Migration for Classic projects
* Use `Upgrade` command in project context menu to migrate your classic projects to SDK-based projects.

![Upgrade project](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/upgrade_classic_projects.png)

* Use `Migrate package.nuspec to Package Properties` command of this extend to migrate your package.nuspec to Package Properties.

![Migrate .nuspec](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/migrate_nuspec.png)

## Release notes

[Release notes](https://raw.githubusercontent.com/cnsharp/nupack/master/release_notes.txt)

## Download
* [VS 2022](https://marketplace.visualstudio.com/items?itemName=CnSharpStudio.NuPack)
* [VS 2017/2019](https://github.com/cnsharp/NuPack/releases/tag/2017)
