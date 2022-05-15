# BasicRabbitMqConsumer

A basic .NET Core RabbitMQ consumer console application use for simple deployments in Kubernetes.

## dotnet CLI

dotnet CLI used to create this project:

```ps1: In C:\src\github.com\ongzhixian\BasicRabbitMqConsumer
dotnet new sln -n BasicRabbitMqConsumer
dotnet new console -n BasicRabbitMqConsumer.ConsoleApp
dotnet sln .\BasicRabbitMqConsumer.sln add .\BasicRabbitMqConsumer.ConsoleApp\

dotnet add .\BasicRabbitMqConsumer.ConsoleApp\ package Microsoft.Extensions.Configuration
dotnet add .\BasicRabbitMqConsumer.ConsoleApp\ package Microsoft.Extensions.Configuration.UserSecrets

dotnet add .\BasicRabbitMqConsumer.ConsoleApp\ package RabbitMQ.Client

dotnet user-secrets --project .\BasicRabbitMqConsumer.ConsoleApp\ init
dotnet user-secrets --project .\BasicRabbitMqConsumer.ConsoleApp\ set "rabbitmq:Url" "amqps://<username>:<password>@<server>/<instance>"

```

Other ways to extend configuration

```
Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Configuration.CommandLine 
Microsoft.Extensions.Configuration.Binder 
Microsoft.Extensions.Configuration.EnvironmentVariables
```