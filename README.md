# About my project 
This is my Hotel Booking and Management project. I decided to make this project to challenge my skills and I also made this project as close to the real world as possible.

This project consists of two application:   
The first application is a Web page where people can book a room for certain dates.  
The second application is a desktop application for reception workers to book-in the guest.    

I choose ASP.NET Core as my Web user interface and WPF Core as my desktop interface.  
For the database I choose Microsoft SQL Server and SQLite with the ability of switching between them.  

</br>
This project utilises N-Tier Data Architecture with two libraries to support this architecture. 
</br>
Example of dependency structure: 


[ HotelApp.Web, HotelApp.Desktop ] => [ HotelAppLibrary ] => [ HotelAppDataBaseAccessLibrary  ]

# Technologies that I used:

WPF Core  
ASP.NET  
XML  
SSDT  
Dependency Injection  
Interfaces and Inheritance   
Git  
Design Patterns  
WOULD Framework  
Data Validation  
SQL Server  
SQLite  
SSMS  
  

 
# Setting Up

The only and only thing you need to do is change appsettings.json files for each project.
You need to choose the database you want to use and either insert the connection string for SQL or specify the file location for SQLite.    

# Last Words  
This was my first real world application and currently I am making a new application that is generally much more advanced.   
It's still in development mode but you can keep track of my progress by looking at my commits.   
You can find this project here - https://github.com/PufusIE/RetailApp
