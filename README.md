# Ships And Ports
## Architecture
The solution contains:
1) ShipsAndPorts.Core - abstractions (interfaces for repositories, services), models, api models
2) ShipsAndPorts.Repositories - database context including db models, repositories, base repository - includes CRUD operations
3) ShipsAndPorts.Services - services which get data from repositories, do some business logic and pass data to the controller layer
4) ShipsAndPorts.Web - contains API controllers and Angular basic app (can be ignored for this project)

The solution supposed to have:
1) ShipsAndPorts.Repositories.Tests - should include unit tests for repositories with mocking the dbContext
2) ShipsAndPorts.Services.Tests - should include unit tests for services with mocking the repositories which are used in the service (PortRepository, ShipRepository)
3) ShipsAndPorts.Web.Tests - should include unit tests for the API controllers

## Dependency Injection
DI is used on all levels. Interfaces are stored in the ShipsAndPorts.Core project. Also Repositories and Services contain ModuleInitializer extension. That extension contains AddDependenciesToContainer method and that method includes all dependencies.

Depndencies are injected in the constructors via intrefaces like this:
```
public ShipService(IShipRepository shipRepository, 
            IPortRepository portRepository,
            IMapper mapper)...
```

AddDependenciesToContainer looks like this:

```
private static void AddDependenciesToContainer(IServiceCollection services)
{
  services.AddTransient<IShipRepository, ShipRepository>();
  services.AddTransient<IPortRepository, PortRepository>();
}
```

## BaseRepository and IBaseRepository

```
 public interface IBaseRepository<TEntity>
    {
        TEntity Get(int id);
        bool Update(TEntity record);
        TEntity Add(TEntity record);
        bool Delete(int id);
        IQueryable<TEntity> GetAll();
    }
```
Every entity in the database supposed to have it's own repository. All repositories have CRUD operations, so that's why we place the code into base classes and interfaces, just not to repeat the code 
every time. 

Also BaseRepository returns IQuerable instead of IList or IEnumerable. We need it to be able to add some ordering or filtering or taking a few records instead of all. And in that case 
we will not get all data from the database, but just records we need. IQuerable is not executed immediately.


## BaseService and IBaseService
```
public interface IBaseService<TViewModel>
{
    TViewModel Get(int id);
    bool Update(TViewModel record);
    TViewModel Add(TViewModel record);
    bool Delete(int id);
    IEnumerable<TViewModel> GetAll();
}
```

```
public class BaseService<TViewModel, TEntity> : IBaseService<TViewModel>
{
    protected IBaseRepository<TEntity> repository;
    protected readonly IMapper mapper;
```

BaseService also contains CRUD operations. So that's why it makes sense to have that code in base class as well. Because in that case we can write code just once and it will be applied to all services.

Every Service inherited from IBaseService and BaseServices like this:
```
public class ShipService : BaseService<ShipApiModel, Ship>, IShipService
{
```

## Mapping
We have database models we don't want to pass to the controller layer, because in that case we will get dependency on the database implementation. To exclude that we have api model classes.
The idea of those classes is to have classes for the controller layer. Every service supposed to return only api models and not db models. That why we need mappeing here.

I use IMapper to map api models to the db models and back. In our case we have pretty simple mapping, but it can be more complicated. In most cases we can just use:
```
var rec = mapper.Map<TEntity>(record);
```
That will conver record to the TEntity type and map fields.

## ShipService
This service is one of the most interesting ones, because it contains this method:
```
public ClosestPortModel GetClosestPort(string shipId)
```
We need both ships data and ports data to calculate the distance to the closest port. First of all we need to get ship by it and GeoLocation of that ship. We use ShipRepository for that.

Then we need to get closeset port and data about it. We need PortRepository there.

The code looks pretty simple, because we use methods of repositories:
```
public ClosestPortModel GetClosestPort(string shipId)
{
    var result = new ClosestPortModel();
    var ship = shipRepository.GetByShipId(shipId);
    if (ship != null)
    {
        var port = portRepository.GetClosestPort(ship.Geolocation);
        if(port!=null)
        {
            result.Distance = port.Geolocation.Distance(ship.Geolocation).Value;
            // Time in hours = distance / speed
            result.ArrivalTime = DateTime.UtcNow.AddHours(result.Distance / ship.Velocity);
            result.PortDetails = mapper.Map<PortApiModel>(port);
            result.ShipDetails = mapper.Map<ShipApiModel>(ship);
        }
    }

    return result;
}
```

Good architecture is when the code is simple) And even Junior/Middle developer can read it)

## Branches
Git has multiple branches:
`) master - it can be used for production
2) develop - current version under development
3) feat-x-... - branch for a particluar feature. When finished developer can create a pull request to merge his feature or bug branch to the develop.

Also we can add one more pre-release branch for QA team.

## CI/CD
It can be setup this way. When new merge gets to the develop branch, CI/CD should run unit tests and if they passed publish to dev environment (staging).

When staging is stable, we merge develop into master and master runs unit tests and published to the production if tests passed. 

## Docker
The solution has basic support of the docker. This part is not done, because we need scripts for the datasbe as well. The script looks like this:
```
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ShipsAndPort.Web/ShipsAndPorts.Web.csproj", "ShipsAndPort.Web/"]
COPY ["ShipsAndPorts.Core/ShipsAndPorts.Core.csproj", "ShipsAndPorts.Core/"]
COPY ["ShipsAndPorts.Services/ShipsAndPorts.Services.csproj", "ShipsAndPorts.Services/"]
COPY ["ShipsAndPorts.Repositories/ShipsAndPorts.Repositories.csproj", "ShipsAndPorts.Repositories/"]
RUN dotnet restore "ShipsAndPort.Web/ShipsAndPorts.Web.csproj"
COPY . .
WORKDIR "/src/ShipsAndPort.Web"
RUN dotnet build "ShipsAndPorts.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShipsAndPorts.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipsAndPorts.Web.dll"]
```

## Things to improve
Noting is perfect in this world) So here is the short list:
1) add unit tests 
2) complete docker file to setup db as well
3) make all calls asynchronous (repositoey, service, controller)
4) polish the Angular part
5) remove unused controllers
6) add migrations with prepopulated data



