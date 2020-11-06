---
title: EFCoreDemo 練習範例
tags: Github, EFCore/Asp.net Core 3.1
description: EFCore 練習範例-DBFirst
---

# EFCoreDemo
使用 Microsoft Visual Studio Community 2019。


### Step 1. 在指定目錄下建立一個空白方案，使用Dotnet Core CLI
```shell=
dotnet new sln
```
關於指令可參考 [dotnet new](https://docs.microsoft.com/zh-tw/dotnet/core/tools/dotnet-new)

### Step2. 開啟空白方案使用Visual Studio Community 2019

### Step3. 在空白方案內建立一個類別庫(.Net Core)
此範例類別庫命名為`EFModule`，並在此類別庫內新增`DAL`與`Models`目錄。

類別庫Nuget套件新增以下項目
* `Microsoft.EntityFrameworkCore`
* `Microsoft.EntityFrameworkCore.Design`
* `Microsoft.EntityFrameworkCore.Tools`
* `Microsoft.EntityFrameworkCore.SqlServer`

使用範例資料庫來源`Northwind` --> `instnwnd.sql`

https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs

使用DB Scaffold 逆向工程建立`DBContexts`與`Model`
* Visual Studio
```shell=
Scaffold-DbContext "Server=10.0.0.13;Database=Northwind;Integrated Security=false;User ID=Northwind_User;Password=password"  Microsoft.EntityFrameworkCore.SqlServer -ContextDir DAL -OutputDir Models
```
`工具`-->`Nuget套件管理員`-->`套件管理器主控台`

* .Net Core CLI
```shell=
dotnet ef dbcontext scaffold "Server=10.0.0.13;Database=Northwind;Integrated Security=false;User ID=Northwind_User;Password=password"  Microsoft.EntityFrameworkCore.SqlServer --context-dir DAL --output-dir Models
```


完成後`DBContexts`會在`DAL/`，`Model`會在`Model/`

MS SQL Connect Syntax：
```
Server=127.0.0.1;Database=MySchema;Integrated Security=false;User ID=user;Password=password
```

可參考[反向工程-指定資料表](https://docs.microsoft.com/zh-tw/ef/core/managing-schemas/scaffolding?tabs=vs#specifying-tables)


### Step4. 在空白方案內建立一個 `Asp.net Core Web應用程式`
將上個步驟建立的類別庫`EFModule`加入專案參考。

Nuget套件新增以下項目
* `Microsoft.EntityFrameworkCore.Design`

在`Startup.cs`配置 - **ConfigureServices** 注入需要服務
```csharp=
public void ConfigureServices(IServiceCollection services)
{
    // 注入 Controller，未注入的話會找不到controller，跟路由有關係
    services.AddControllers();

    // 加入EF SQL SERVER
    services.AddEntityFrameworkSqlServer();

    // 加入 DB Context - NothwindContext
    services.AddDbContext<NorthwindContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );
}
```




