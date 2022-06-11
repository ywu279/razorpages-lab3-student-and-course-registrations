# Razorpage - lab3 - student and course registrations
The purpose of the project is to implement a web application that registers courses and enters/changes grades for a selected student.

<*CST8256 Web Programming Languages I - lab 3*>

## Built with
- ASP.NET [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio) Web Application(.NET framework 6.0)
- Default built-in [Bootstrap v5.1.0](https://getbootstrap.com/)
- Default built-in [jQuery](https://jquery.com/) JavaScript Library v3.5.1
- Third party class libraries packed as NuGet package - `AcademicManagement.2.0.0.nupkg`

## Get started
1. Download Visual Studio 2022
2. You have to install the `AcademicManagement` package to the Lab3 project. 

    Download NuGet package `AcademicManagement.2.0.0.nupkg` from the **ClassLib** folder on GitHub. Create a folder called  **LocalClassLib** on your computer and move the package into the folder you created. From Visual Studio's menu, select **Tools > NuGet Package Manager > Manage NuGet Packages for Solution...**
    ![image](https://user-images.githubusercontent.com/58931129/173199329-189e8635-4c41-4d89-a968-40be14fd3c5c.png)
    After installation, you should be able to see it under the project's **Dependencies > Packages > AcademicManagement (2.0.0)**
    ![image](https://user-images.githubusercontent.com/58931129/173199530-3c822a63-d318-4d61-b728-3d52bf9ba53b.png)

## Objectives
1. Add third party class libraries
2. Understand and use ASP.NET **Tag Helpers**
3. Add JavaScript to Razor Pages
4. Use Sessions in Razor page web application
5. Sort list of C# objects
