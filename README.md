# Razorpage - lab3 - student and course registrations
The purpose of the project is to implement a web application that registers courses and enters/changes grades for a selected student.

<*CST8256 Web Programming Languages I - lab 3*>

## Built with
- ASP.NET [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio) Web Application(.NET framework 6.0)
- Default built-in [Bootstrap v5.1.0](https://getbootstrap.com/)
- Default built-in [jQuery](https://jquery.com/) JavaScript Library v3.5.1
- Third party class libraries packed as NuGet package - AcademicManagement.2.0.0.nupkg

## Get started
1. Download Visual Studio 2022
2. You have to install the **AcademicManagement.2.0.0.nupkg** package to the Lab3 project. You'll find it under the **LocalClassLib** folder.

    From Visual Studio's menu, select **Tools > NuGet Package Manager > Manage NuGet Packages for Solution...**
    ![image](https://user-images.githubusercontent.com/58931129/173199329-189e8635-4c41-4d89-a968-40be14fd3c5c.png)
    After installation, you should be able to see it under the project's **Dependencies > Packages > AcademicManagement (2.0.0)**
    ![image](https://user-images.githubusercontent.com/58931129/173199530-3c822a63-d318-4d61-b728-3d52bf9ba53b.png)

## Objectives
1. Add third party class libraries
2. Understand and use ASP.NET **Tag Helpers**
3. Add JavaScript to Razor Pages
4. Use Sessions in Razor page web application
5. Sort list of C# objects

## Features
### 1. Select a student from dropdownlist
    ![image](https://user-images.githubusercontent.com/58931129/173492468-340181e0-6a8f-4948-aa96-f6bc49fcb0f6.png)
    
### 2. Register courses
    ![image](https://user-images.githubusercontent.com/58931129/173492592-c2ee50e7-e6d7-4698-bca0-035b4d70a1a4.png)
    
### 3. Enter Grades
    ![image](https://user-images.githubusercontent.com/58931129/173492692-c28d6bb1-af4f-48b4-a1e6-6f97b39cdf69.png)
    
### 4. Sort Courses and Academic Records
    When a user selects a different student from the dropdown list, the page must maintain the previous selected sort order
    ![image](https://user-images.githubusercontent.com/58931129/173493208-04d7410d-f98b-4476-ad86-f39197fe970c.png)
    ![image](https://user-images.githubusercontent.com/58931129/173493251-9cee518f-41b5-4b8c-aa32-cd862daaec4c.png)

## Acknowledgements
A list of recourses I found helpful to learn Razor Pages:
- [Razor Pages with Entity Framework Core in ASP.NET Core - Tutorial 1 of 8](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-6.0&tabs=visual-studio)
- [Razor Pages for ASP.NET Core(.NET 6) - freeCodeCamp.org](https://youtu.be/eru2emiqow0)

## Disclaimer
This project is to be used for educational purposes only.
