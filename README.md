# Game.Gambling
I tried to develop using latest tech and design which really interesting and learning.

# Tools:
- .Net 7
- Visual Studio 2022
- SqLite
- RabitMq
- Masstransit
- XUnit
- Moq
- Microsoft Identity for auth

# Architecture/Style:
- Microservices
- Clean Architecture

The project is consist of below three solution

# 1- Game.Gambling.Packages.sln (Contains Common Package)
- It contains RabitMq/Masstransit publisher and context and related functionality. It is published as a package and is used in below projects

# 2- Game.Security.sln (Microservice 1)
This solution contains player management and authentication related functionality. and is consist of below projects
- Game.Security.API
- Game.Security.Infrastructure
- Game.Security.Application
- Game.Security.Domain
- Game.Security.Tests
            
# 3- Game.Gambling.sln (Microservice 2)
This is main microservice this solution is responsible for Gambling Bet API and related functionality, consist of three projects
- Game.Gambling.API
- Game.Gambling.Infrastructure
- Game.Gambling.Application
- Game.Gambling.Domain
- Game.Gambling.Tests

Both Microservices are independent and has its own database. Secruity Microservice publishes a Message when a new user is created. A consumer in Gmabling Microservice handles the published message and adds the new player reward points of 10,000 to get started.
