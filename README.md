# What is RadioTerm?

RadioTerm is a simple to use and lightweight console online radio player for Windows.

## Adding a station

Press `a` to add a station. You will be asked to provide a name and a URL. Make sure to provide the URL of the stream itself not the URL of the website, otherwise you'll see an error.

## Deleting a station

Stations can be deleted by pressing the `d` key, which brings up a small menu. Use the arrow down and up keys to select a station. On `Enter` the selected station will be deleted

## Files being created

RadioTerm saves your station in a .json file under `C:\Users\[YourUsername]\AppData\Roaming\RadioTerm`. There will be no other files being created.

# Building the project

Clone the repository <b>clean</b> solution then build it using VS2019.

RadioTerm uses following NuGet packages:
- NAudio
- Newtonsoft.Json

# Release

RadioTerm provides two releases:
- A self contained .exe written in .NET Framework 4.6. [.NET Framework release](https://github.com/schgab/RadioTerm/releases/tag/v2.0)
- A .NET Core 3.1 release. [.NET Core release](https://github.com/schgab/RadioTerm/releases/tag/v2.1)

<b>Note</b>: The .NET Framework release (tag v2.0) will not be updated. Future releases will be in net core only.
