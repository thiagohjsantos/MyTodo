<h1 align="center">MyTodo</h1>

<br/>

>## 📖 About

A web application that allows you to create and manage your to-do list, built using ASP.NET Core 5 MVC, EF Core 5, ASP.NET Identity and SQL Server.

<br/>

>## 🛠 Technologies 

- [ASP.NET Core 5 MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-5.0)
- [EF Core 5](https://docs.microsoft.com/en-us/ef/)
- [ASP.NET Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)

<br/>

>## 📋 Prerequisites

To run the application on your local machine you will need to have installed the following tools:

- [.NET SDK 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)

<br/>

>## 📦 Cloning the Repository 

To clone the project follow the instructions bellow:

```
$ git clone https://github.com/thiagohjsantos/MyTodo.git 
```
<br/>

>## 📝 Code Modifications

In some circumstances you may have to change your SQL server name in the connection string of your database, to do that just go in the file "appsettings.json" and do as follows:

```bash
# Replace ".\\SQLEXPRESS" with your respective SQL server name
"DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=Todo;Integrated Security=True;"
```

<br/>

>## 🎬 Run Application 

To run the application you can use an IDE such as Visual Studio or a code editor such as Visual Studio Code. You can also run the application through the .NET CLI, for this open a command shell and enter the following command:

```bash
# Go to the directory where the MyTodo.csproj file is located
$ cd MyTodo

# Run the application 
$ dotnet watch run 
```
