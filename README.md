# NokoWebApi (Nokotan) ASP.Net 9.0

## created by <ahmadasysyafiq@proton.me>

## dotnet run

```shell
dotnet run --project NokoWebApi
```

## dotnet developer certificates https trusted

```shell
dotnet dev-certs https --trust
```

## dotnet workload restore

```shell
dotnet workload restore
```

## dotnet tool update all packages

```shell
dotnet tool list -g
dotnet tool install <package-id> -g
dotnet tool update --all -g
```

## dotnet tool install dotnet-ef

```shell
dotnet tool install dotnet-ef -g

# dotnet list all migrations
dotnet ef migrations list

dotnet ef migrations list --context NokoWebApi.Repository.<repository-name>

# dotnet added new migration
dotnet ef migrations add <migration-name>

dotnet ef migrations add <migration-name> --context NokoWebApi.Repository.<repository-name>

# dotnet remove last migration
dotnet ef migrations remove

# dotnet update database
dotnet ef database update

dotnet ef database update --context NokoWebApi.Repository.<repository-name>

# dotnet reset database
dotnet ef database drop
dotnet ef database update

dotnet ef database drop --context NokoWebApi.Repository.<repository-name>
dotnet ef database update --context NokoWebApi.Repository.<repository-name>

# dotnet database context list
dotnet ef dbcontext list

dotnet ef dbcontext info --context NokoWebApi.Repository.<repository-name>
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

## dotnet build release with self-contained

```shell
dotnet build -c:Release --self-contained true
```
