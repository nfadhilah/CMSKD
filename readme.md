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

2.  MSSQL SERVER 2012

3.  Visual Studio 2019

**HOW TO RUN**

1.  Clone Project

2.  Klik file CMSKD.sln

3.  Start Without Debugging (Ctrl + F5)

4.  Buka <http://localhost:5000/swagger> di browser

**FORMAT RESPONSE**

<https://github.com/proudmonkey/AutoWrapper>

**LOGS**

Log disimpan di path "/API/Logs/logs.db " menggunakan serverless No SQL
Database (LiteDB). File logs.db dapat dibuka dengan menggunakan LiteDB
Studio.

[https://github.com/mbdavid/LiteDB.Studio/releases![](.//media/image1.png){width="4.780302930883639in"
height="2.5543318022747155in"}](https://github.com/mbdavid/LiteDB.Studio/releases#)

**DB Configuration**

Project ini juga mendukung Database selain MSSQL Server.

Berikut contoh konfigurasi untuk Postgre SQL.

![](.//media/image2.png){width="5.598485345581802in"
height="3.002688101487314in"}

**Daftar Library**

1.  Net Core 3.1

<https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1>

2.  AutoWrapper

<https://github.com/proudmonkey/AutoWrapper>

3.  Dapper Repository

<https://dapper.phnx47.net/>

4.  MeditR

<https://github.com/jbogard/MediatR/wiki>

5.  AutoMapper

<https://automapper.org/>

6.  Fluent Validation

<https://fluentvalidation.net/>

7.  Serilog

<https://serilog.net/>

8.  LiteDB

<https://www.litedb.org/>
