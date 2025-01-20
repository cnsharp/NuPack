# NuPack
Visual Studio extension for building and publishing NuGet packages.

### Features of NuPack 2022
* It's a clean version just support SDK-based projects.Use dotnet CLI as default build tool instead of NuGet.exe.
* Migrate package.nuspec to Package Properties.
* DONOT keep source URLs or API keys any more, use the sources in NuGet.Config.

### Screen shots
![Deploy context menu](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/deploy_context_menu.png)
![Package metadata](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/nuget_manifest.png)
![Pack options](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/pack_push.png)

### Migration for Classic projects
* Use `Upgrade` command in project context menu to migrate your classic projects to SDK-based projects.
![Upgrade project](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/upgrade_classic_projects.png)
* Use `Migrate package.nuspec to Package Properties` command of this extend to migrate your package.nuspec to Package Properties.
![Migrate .nuspec](https://raw.githubusercontent.com/cnsharp/nupack/master/screenshots/17.x/migrate_nuspec.png)

### Release notes

[Release notes](https://raw.githubusercontent.com/cnsharp/nupack/master/release_notes.txt)

### Download
* [VS 2022](https://marketplace.visualstudio.com/items?itemName=CnSharpStudio.NuPack2022)
* [VS 2019 and Previous](https://marketplace.visualstudio.com/items?itemName=CnSharpStudio.NuPack)

### Fork on Github
[https://github.com/cnsharp/nupack](https://github.com/cnsharp/nupack)
 