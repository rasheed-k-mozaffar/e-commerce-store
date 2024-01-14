# e-commerce-store
The store provides a comprehensive dashboard for managing products,
product categories, and orders. It also allows users to register, browse
products, search for products, add products to their cart, and checkout.
The backend is built with ASP.NET Core and Frontend by HTML,CSS,JS
through a friend
Features:
- Full dashboard
- Responsive frontend
- Account and cart for sell
- The store is designed to be easy to use for both customers and
administrators.

## Setting Up the E-Commerce Store

It's important to note that this project requires the following versions and packages to be installed:

- .NET SDK version 7.0 or higher
- Microsoft.AspNetCore.Identity.EntityFrameworkCore version 7.0.5
- Microsoft.EntityFrameworkCore.Design version 7.0.5
- Microsoft.EntityFrameworkCore.SQLite version 7.0.5
- Microsoft.EntityFrameworkCore.SqlServer version 7.0.5
- Microsoft.EntityFrameworkCore.Tools version 7.0.5
- Microsoft.VisualStudio.Web.CodeGeneration.Design version 7.0.6

If you're using Visual Studio, you may need a `.sln` file, we work on vs code and use dotnet command so we do not need one ,so if you're using VS, create a new empty MVC project and move the project files to it.

**Note:** that in VS Code, you may see errors in all files on the first open. However, these problems will disappear completely after you click on "build". It appears that before starting the build process, it is difficult for VS Code to recognize some things. The important thing is that these problems will disappear and not return. Also, don't forget to add the C# extension if you're using VS Code.



In default project use SQL Lite and project have Included database with some products to test.

if you want to create new database : 

1. delete images on Image folder and Image/Description sub-folder , do not delete background image or folder.
2. Delete ApplicationDvContext.db ApplicationDbContext.db-shm ApplicationDbContext.db-wal
3. Open the project folder and use the `dotnet ef` command.
4. Run the following command to generate a migration: `dotnet ef migrations add {migrationName}`
6. Run the following command to update the database: `dotnet ef database update`
6. remove comment from //await SeedData.SeedUsersAndRolesAsync(app); on Program.cs
6. run project
6. recomment //await SeedData.SeedUsersAndRolesAsync(app); on Program.cs

the included sql light ApplicationDbContext.db , the  Admin user is : `Admin`

and Passsword is : `Coding@1234?`

and non admin user is : `app-user` and with same password

if you create new database , the admin user also will be : Admin and Password : Coding@1234?

you can change this from `/data/SeedData.cs`.

If you prefer to use SQL Server, simply change the source to connect to your database then also :

1. Open the project folder and use the `dotnet ef` command.
2. Run the following command to generate a migration: `dotnet ef migrations add {migrationName}`
3. Run the following command to update the database: `dotnet ef database update`
4. You can now run the project.


## view:
**Home:**
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/6908aab3-1a76-406f-90d6-5be62072bd83)
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/651c8e1a-5736-483a-ab46-5065dbafb83e)
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/5f4dbc07-ab50-4ba2-9be2-c9cafb93a28a)
**Login:**
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/18d06bc1-27c5-4e02-8e15-73bd3f177d3a)
**Register:**
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/38fb0486-6a95-4490-a54d-4eff8c91945e)
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/49a826b6-329f-4bbe-a426-8da8715b5891)
**Dashbaord:**
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/0cc2d53b-210c-47c6-84d3-127bfd5192a4)
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/9268ff6b-61a9-4071-988f-fc8dd9ff68e4)
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/cc1127ae-d099-45bb-a42b-b2af62d1e0eb)
**Cart:**
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/fd515dd5-41a3-4713-b6d2-efb264477524)
**Checkout:**
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/6ae76212-adfe-4e32-80fc-dbe93111b255)
![image](https://github.com/IsmaelE77/e-commerce-store/assets/93754014/739129b6-d5d2-484a-b77a-eeb24ed1e6fb)
