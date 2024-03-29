[![Release badge](https://img.shields.io/badge/release-latest-brightgreen.svg)](https://github.com/schgab/RadioTerm/releases/latest)
# What is RadioTerm?

RadioTerm is a simple to use and lightweight console online radio player for Windows and Linux.

## Prerequisite

You need libvlc installed on Linux. It should come with a standard installation of VLC Player.

## Adding a station

Press `a` to add a station. You will be asked to provide a name and a URL. Make sure to provide the URL of the stream itself not the URL of the website, otherwise you'll see an error.
An easy way of retrieving the stream url is by starting the audiostream on the website and using `[].slice.call(document.getElementsByTagName('audio')).map(el => el.src)` in the browser console to get a list of urls.

## Deleting a station

Stations can be deleted by pressing the `d` key, which brings up a small menu. Use the arrow down and up keys to select a station. On `Enter` the selected station will be deleted

## Files being created

RadioTerm saves your stations in a .json file under
- `C:\Users\[YourUsername]\AppData\Roaming\RadioTerm` on Windows
- `XDG_CONFIG_HOME/RadioTerm` on Linux 

# Building the project

Clone the repository <b>clean</b> solution then build it using VS2019.

RadioTerm uses following NuGet packages:
- [LibVLCSharp](https://github.com/videolan/libvlcsharp)
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)

# Release

Get the latest release [here](https://github.com/schgab/RadioTerm/releases/latest)

Windows: Releases are currently on hold as I need to cherrypick the parts from libvlc that are required.
