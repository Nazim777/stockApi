1. create new project
# dotnet new webapi
2. run project
# dotnet watch run

3. install entity framework
# dotnet tool install --global dotnet-ef --version 7.0.0
4. add package entity framework design
# dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0
or we can install package from nuget package manager
click -> f1
5. verify instalation
# dotnet ef
6. install sql server entity framework
# dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.0
<!-- 6. Enter scaffold command
# dotnet ef dbcontext scaffold "Server=DESKTOP-QUDMRGS\SQLEXPRESS;Database=testdb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models

retore the package
dotnet restore -->
6. or after adding database context and models manually
# dotnet ef migrations add init
# dotnet ef database update

7. database relatoinship
# newtonSoft.json 13.0.3 version
# Microsoft.AspNetCore.Mvc.NewtonsoftJson 6.0.1 version

8. jwt
# Microsoft.Extensions.Identity.Core 6.0.0 version
# Microsoft.AspNetCore.Identity.EntityFrameworkCore 6.0.0 version
# Microsoft.AspNetCore.Authentication.JwtBearer 6.0.0 version

9. after finishig step 8
# dotnet ef migrations add identity
# dotnet ef database update

10. after finishing step 9 and configuring the role
# dotnet ef migrations add SeedRole
# dotnet ef database update