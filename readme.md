**STRUKTUR PROJECT**

1.  API : API Project

2.  Application: Semua logic bisnis disimpan di project ini. Setiap
    folder menggambarkan fitur aplikasi. Menggunakan CQRS Pattern yang
    memisahkan logic antara operasi reads dan writes.

3.  Domain: Business object. Berisikan simple POCO (Plain Old Class
    Object). Digunakan untuk mapping object database ke C\# class.

4.  Persistence: Akses Database. Berisi implementasi repository dan
    database context.

5.  Infrastructure: Berisi implementasi dari Application.Interfaces.

**MINIMUM REQUIREMENT**

1.  ASP.NET CORE SDK VERSI 3.1

2.  Windows Server 2012 R2+ ([klik untuk melihat daftar supported OS](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md "klik untuk melihat daftar supported OS"))

3.  Visual Studio 2019

**HOW TO RUN**

1.  Clone Project

2.  Klik file CMSKD.sln

3.  Buka Nuget Package Manager Console kemudian ketik command berikut:
    -   dotnet user-secrets init
    -   dotnet user-secrets set "TokenKey" "super secret key"

4.  Start Without Debugging (Ctrl + F5)

5.  Buka <http://localhost:5000/swagger> di browser 

**FORMAT RESPONSE**

<https://github.com/proudmonkey/AutoWrapper>

**LOGS**

Log disimpan di path "/API/Logs/logs.db " menggunakan serverless No SQL
Database (LiteDB). File logs.db dapat dibuka dengan menggunakan LiteDB
Studio.

<https://github.com/mbdavid/LiteDB.Studio/releases>
![](.//media/image1.png)

**DB Configuration**

Project ini juga mendukung Database selain MSSQL Server.

Berikut contoh konfigurasi untuk PostgreSQL.

![](.//media/image2.png)

**Daftar Library**

1.  Net Core 3.1

<https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1>

2.  AutoWrapper

<https://github.com/proudmonkey/AutoWrapper>

3.  Dapper Repository

<https://dapper.phnx47.net/>

4.  MediatR

<https://github.com/jbogard/MediatR/wiki>

5.  AutoMapper

<https://automapper.org/>

6.  Fluent Validation

<https://fluentvalidation.net/>

7.  Serilog

<https://serilog.net/>

8.  LiteDB

<https://www.litedb.org/>
