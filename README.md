# LukePad

LukePad is an open-source "encrypted" notepad. All files are saved and opened as compressed, encrypted 256-bit AES binary files to help stop prying eyes from discovering sensitive text data.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.
1. Install Prerequisites

2. Clone the repo

```PS C:\Code> git clone https://github.com/lukasdragon/LukePad.git```
3. Restore Nuget Packages

```nuget restore```
4. Build the Solution

```devenv EncryptedNotePad.sln /Build "Release|x86"

### Prerequisites

1. Visual Studio 2017

## Deployment

For deployment, you may either build the project yourself from the latest changes or you may download executables from [releases](https://github.com/lukasdragon/LukePad/releases) 

## Built With

* [Microsoft Winforms](https://docs.microsoft.com/en-us/dotnet/framework/winforms/) - UI Platform
* [Krypton Toolkit](https://github.com/ComponentFactory/Krypton) - Controls used for theming
* [Modern Encryption of a String C#](https://gist.github.com/jbtule/4336842) - Used to encrypt binary files

## Contributing

Please read [CONTRIBUTING.md](https://github.com/lukasdragon/LukePad/blob/master/CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/lukasdragon/LukePad/tags). 

## Authors

* **Lukas Olson** - *Initial work* - [lukasdragon](https://github.com/lukasdragon)

See also the list of [contributors](https://github.com/lukasdragon/LukePad/graphs/contributors) who participated in this project.

## License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* ComponentFactory for their WinForms skin used in this project.
* James Tuley for their "Modern Encryption of a String C#". 
* Zaxophone for their input and for humouring me on this project.
