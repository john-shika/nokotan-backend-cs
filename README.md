# NokoWebApi (Nokotan) ASP.Net 9.0

## dotnet workload restore

```shell
dotnet workload restore
```

## dotnet tool install dotnet-ef

```shell
dotnet tool install dotnet-ef -g

# dotnet list all migrations
dotnet ef migrations list

# dotnet added new migration
dotnet ef migrations add <migration-name>

# dotnet remove last migration
dotnet ef migrations remove

# dotnet update database
dotnet ef database update

# dotnet reset database
dotnet ef database drop
dotnet ef database update
```

## step by step using dotnet-ef
- new migration and update database

```shell
dotnet ef migrations add <migration-name> --context NokoWebApi.Repositories.UserRepository
dotnet ef database update --context NokoWebApi.Repositories.UserRepository
```

- drop database and remove last migration

```shell
dotnet ef database drop --context NokoWebApi.Repositories.UserRepository
dotnet ef migrations remove --context NokoWebApi.Repositories.UserRepository
```

## dotnet tool install dotnet-outdated-tool

```shell
dotnet tool install dotnet-outdated-tool -g

# dotnet upgrade all outdated packages
dotnet outdated --upgrade
```

## dotnet tool install upgrade-assistant

```shell
dotnet tool install upgrade-assistant -g

# dotnet upgrade with dotnet assistant
upgrade-assistant upgrade

# dotnet analyze with dotnet assistant
upgrade-assistant analyze
```

# dotnet add package Authentication JwtBearer

```shell
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```
