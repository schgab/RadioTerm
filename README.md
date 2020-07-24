# What is RadioTerm?

RadioTerm is a simple to use and lightweight console online radio player for Windows.

## Adding a station

Press a to add a station. You will be asked to provide a name and a URL. Make sure to provide the URL of the stream itself not the URL of the website, otherwise you'll see an error.

## Files being created

RadioTerm saves your station in a .json file under `C:\Users\[YourUsername]\AppData\Roaming\RadioTerm`. There will be no other files being created.

# Building the project

Clone the repository and compile it using VS2019.

RadioTerm uses following NuGet packages:
- NAudio
- Newtonsoft.Json
- Costura.Fody
