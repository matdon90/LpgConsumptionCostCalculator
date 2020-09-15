[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/matdon90/LpgConsumptionCostCalculator">
    <img src="https://user-images.githubusercontent.com/49766006/87756082-87b06780-c808-11ea-9162-90881ab55e58.JPG" title="LPG CCC" alt="LPG CCC">
  </a>

  <h3 align="center">LpgConsumptionCostCalculator</h3>

  <p align="center">
    The LPG consumption cost calculator is a web application for storing, analyzing and managing the costs of fuel and gas consumption for cars with an LPG installation.
    <br />
    <br />
    <a href="https://github.com/matdon90/LpgConsumptionCostCalculator/issues">Report Bug</a>
    ·
    <a href="https://github.com/matdon90/LpgConsumptionCostCalculator/issues">Request Feature</a>
  </p>
</p>

<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
* [Used Technologies](#used-technologies)
* [Getting Started](#getting-started)
* [Usage](#usage)
* [Contributing](#contributing)
* [Contact](#contact)



<!-- ABOUT THE PROJECT -->
## About The Project

LpgConsumptionCostCalculator is a ASP.NET MVC5 web app designed for storing, analyzing and managing the costs of fuel and gas consumption for cars with an LPG installation.
Project created for my own needs to support me and my wife with cooperatively analyzing and managing costs of travel with petrol car using LPG installation, but it can either be use for diesel cars with LPG installation. More details in Usage section.

<!-- USED TECHNOLOGIES -->
### Used Technologies

* [ASP.NET MVC v5.2.7.0](https://dotnet.microsoft.com/apps/aspnet)
* [Firebase Database](https://firebase.google.com/)
* [Autofac](https://autofac.org/)
* [AutoMapper](https://automapper.org/)
* [Okta](https://www.okta.com/)
* [Bootstrap v4.4.1](https://getbootstrap.com/)
* [SB Admin 2 v4.0.7](https://startbootstrap.com/template-overviews/sb-admin-2)
* [jQuery v3.3.1](https://jquery.com/)
* [Chart.js v2.9.3](https://www.chartjs.org)
* [Font Awesome 4.7.0](http://fontawesome.io)

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps:

* Download the latest stable version from the download tab and unzip it to your folder
* Open the solution in Visual Studio 2019. 
* Clean solution.
* Build the solution to install Nuget packages.
* In case of RoslynCompilerBug run following in PM Console:

```shell
Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
```

* Change in web project in `Web.config` the data needed for Okta Identity:

```xml
    <add key="okta:ClientId" value="yourClientId" />
    <add key="okta:ClientSecret" value="yourClientSecret" />
    <add key="okta:OktaDomain" value="yourOktaDomain" />
    <add key="okta:Token" value="yourOktaToken" />
```

* Change in data project in `FirebaseConn.cs` file the data for your Firebase Database connection:

```csharp
namespace LpgConsumptionCostCalculator.Data.Services
{
    public class FirebaseConn
    {
        private const String databaseUrl = "yourDatabaseUrl";
        private const String databaseSecret = "yourDatabaseSecret";
```

* Run application
* Fire up your browser and open url `https://localhost:44396/`
* Enjoy ;-)

Please note that I have tested the app in Chrome browser where no issues where discovered.


<!-- USAGE EXAMPLES -->
## Usage

This section contains screens of application's parts with all main functionalities.

#### List of cars

After clicking `List` in `CARS` section from navigation bar the list of cars is shown. 
Guest can view the list of entered cars and check selected car details on seperate modal.
In addition, it is possible to search the list, order columns and set the number of entries on one page.

<img src="https://user-images.githubusercontent.com/49766006/87756090-8bdc8500-c808-11ea-8412-09ce5dc2567a.JPG" title="List of cars" alt="List of cars">

Logged user can additionally add new car, edit or delete existing one.

<img src="https://user-images.githubusercontent.com/49766006/87756099-91d26600-c808-11ea-8486-b1b4a9f1a38e.JPG" title="New car form" alt="New car form">

#### List of receipts

After clicking name of the chosen car and then selecting `Fuel receipts` in `FUEL RECEIPTS` section from navigation bar the list of receipts is shown. 
Guest can view the list of receipts for selected car and check details of fuel recipt.
In addition, it is possible to search the list, order columns and set the number of entries on one page.

<img src="https://user-images.githubusercontent.com/49766006/87756110-9565ed00-c808-11ea-88b6-fa41a6aa6bd2.JPG" title="List of receipts" alt="List of receipts">

Logged user can additionally add fuel receipt, edit or delete existing one.

<img src="https://user-images.githubusercontent.com/49766006/87756114-97c84700-c808-11ea-9cb4-38e2631e5b6d.JPG" title="New fuel receipt form" alt="New fuel receipt form">

#### Important data, trends and reports generating

After clicking name of the chosen car and then selecting `Graphs` in `FUEL RECEIPTS` section from navigation bar the graphs and reports section is shown. 
Guest can view the important fuel consumption and fuel price data from selected time range.
In addition the graphs with fuel consumption and fuel price for 100 km is shown.

<img src="https://user-images.githubusercontent.com/49766006/87756121-9b5bce00-c808-11ea-8dc3-85bbce2b572c.JPG" title="Graphs" alt="Graphs">

Logged user can additionally export report to PDF or CSV file from selected time range.

<img src="https://user-images.githubusercontent.com/49766006/87756130-9dbe2800-c808-11ea-8d43-617dc0f98fcd.JPG" title="Report configuration" alt="Report configuration">

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. 🍴 Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. 🔃 Open a Pull Request


<!-- CONTACT -->
## Contact

Mateusz Donhefner

Project Link: [https://github.com/matdon90/LpgConsumptionCostCalculator](https://github.com/matdon90/LpgConsumptionCostCalculator)

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/matdon90/LpgConsumptionCostCalculator.svg?style=flat-square
[contributors-url]: https://github.com/matdon90/LpgConsumptionCostCalculator/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/matdon90/LpgConsumptionCostCalculator.svg?style=flat-square
[forks-url]: https://github.com/matdon90/LpgConsumptionCostCalculator/network/members
[stars-shield]: https://img.shields.io/github/stars/matdon90/LpgConsumptionCostCalculator.svg?style=flat-square
[stars-url]: https://github.com/matdon90/LpgConsumptionCostCalculator/stargazers
[issues-shield]: https://img.shields.io/github/issues/matdon90/LpgConsumptionCostCalculator.svg?style=flat-square
[issues-url]: https://github.com/matdon90/LpgConsumptionCostCalculator/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/mateusz-donhefner/