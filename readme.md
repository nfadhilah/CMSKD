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

3.  Visual Studio 2019 / VS Code

4.  Support MSSQL Server, PostgreSQL, MySQL

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

**HOW TO ADD NEW FEATURE**

1. Tambahkan entity class di project domain. Class entity ini digunakan untuk mapping C# object ke Database Object.

   `Domain/DaftPhk3.cs`
   
   ```c#
   [Table("DAFTPHK3")]
   public class DaftPhk3
   {
       [Key]
       public string KdP3 { get; set; }
       public string NmP3 { get; set; }
       public string NmInst { get; set; }
       public string NoRcP3 { get; set; }
       public string NmBank { get; set; }
       public string JnsUsaha { get; set; }
       public string Alamat { get; set; }
       public string Telepon { get; set; }
       public string NPWP { get; set; }
       public string UnitKey { get; set; }
   }
   ```
   
2. Tambahkan class repository dari class entity. Class repository  merupakan turunan dari kelas DapperRepository<TEntity>. Dimana TEntity adalah class entity yang sebelumnya kita tambahkan.

   `Persistence/Repository/DaftPhk3Repository.cs`

   ```c#
   namespace Persistence.Repository
   {
     public class DaftPhk3Repository : DapperRepository<DaftPhk3>
     {
       public DaftPhk3Repository(IDbConnection connection) : base(connection)
       {
       }
   
       public DaftPhk3Repository(
         IDbConnection connection, ISqlGenerator<DaftPhk3> sqlGenerator) : base(
         connection, sqlGenerator)
       {
       }
     }
   }
   ```

3. Tambahkan class repository sebagai member dari interface IDbContext.

   `Persistence/DbContext.cs`

   ```c#
   public interface IDbContext
   {
       ...
       DaftPhk3Repository DaftPhk3 { get; }
   }
   ```

4. Tambahkan implementasi interface IDBContext di class DbContext.

   `Persistence/DbContext.cs`

   ```c#
   public class DbContext : IDbContext
   {
       public DbContext(string connectionString)
       {
           MicroOrmConfig.SqlProvider = SqlProvider.MSSQL;
           Connection = new SqlConnection(connectionString);
           Connection.Open();
       }
   
       public IDbConnection Connection { get; }
   
       ...
   
       public DaftPhk3Repository DaftPhk3 => new DaftPhk3Repository(Connection);
   }
   ```

   

5. Buat folder baru sesuai dengan nama fitur di Project Application. Folder ini merupakan container untuk semua operasi CRUD terhadap sebuah fitur. Pada project CMSKD ini kita menggunakan CQRS (Command Query Responsibility Segregation) Pattern dengan bantuan library MediatR.  Untuk operasi read kita gunakan turunan dari class IRequest<TResult> dan untuk operasi write kita menggunakan turunan dari class  IRequest.

   - Contoh berikut kita akan membuat query list dari table DAFTPHK3. Untuk kebutuhan performance, query dengan return result yang sangat besar disarankan untuk dibungkus dengan class PaginationWrapper.

   - Class Query

     `Application/Rekanan/List.cs`

     ```c#
     public class List
       {
         public class Query : PaginationQuery, IRequest<PaginationWrapper>
         {
           public string NmP3 { get; set; }
           public string NmInst { get; set; }
           public string NmBank { get; set; }
           public string JnsUsaha { get; set; }
         }   
       }
     ```

   - Class Query Handler

     `Application/Rekanan/List.cs`

     ```c#
     public class Handler : IRequestHandler<Query, PaginationWrapper>
     {
         private readonly IDbContext _context;
     
         public Handler(IDbContext context)
         {
             _context = context;
         }
     
         public async Task<PaginationWrapper> Handle(
             Query request, CancellationToken cancellationToken)
         {
             var parameters = new List<Expression<Func<DaftPhk3, bool>>>();
     
             if (!string.IsNullOrWhiteSpace(request.NmP3))
                 parameters.Add(d => d.NmP3.Contains(request.NmP3));
     
             if (!string.IsNullOrWhiteSpace(request.NmInst))
                 parameters.Add(d => d.NmInst.Contains(request.NmInst));
     
             if (!string.IsNullOrWhiteSpace(request.NmBank))
                 parameters.Add(d => d.NmBank.Contains(request.NmBank));
     
             if (!string.IsNullOrWhiteSpace(request.JnsUsaha))
                 parameters.Add(d => d.JnsUsaha == request.JnsUsaha);
     
             var predicate = PredicateBuilder.ComposeWithAnd(parameters);
     
             var totalItemsCount = _context.DaftPhk3.FindAll(predicate).Count();
     
             var result = await _context.DaftPhk3
                 .SetLimit(request.Limit, request.Offset)
                 .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KdP3)
                 .FindAllAsync(predicate);
     
             return new PaginationWrapper(result, new Pagination
                                          {
                                              CurrentPage = request.CurrentPage,
                                              PageSize = request.PageSize,
                                              TotalItemsCount = totalItemsCount
                                          });
         }
     }
     
     ```

6. Tambahkan Controller di project API.  

   `Api/Controllers/DaftPhk3Controller.cs`

   ```c#
   public class DaftUnitController : BaseController
   {
       [HttpGet]
       public async Task<IActionResult> Get() =>
           Ok(await Mediator.Send(new List.Query()));
   }
   ```

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
