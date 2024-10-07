# Noko.WebApi Backend

## dotnet workload restore

```shell
dotnet workload restore
```

## dotnet ef core

```shell
dotnet tool install dotnet-ef
dotnet ef migrations add Init

# update database

dotnet ef database update

# remove last migration

dotnet ef migrations remove

# reset database

dotnet ef database drop
dotnet ef database update

# list all db contexts
dotnet ef dbcontext list
```

## dotnet upgrade assistant upgrade

```shell
dotnet tool install upgrade-assistant
upgrade-assistant upgrade
```

## dotnet outdated upgrade

```shell
dotnet tool install dotnet-outdated-tool
dotnet outdated --upgrade
```
