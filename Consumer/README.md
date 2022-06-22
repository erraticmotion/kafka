# Consumer

Build `Consumer` inside the VM

```console
cd ./getting-started/Consumer

dotnet build Consumer.csproj
```

Consme event

```console
dotnet run --project Consumer.csproj $(pwd)/getting-started.properties
```

CTRL+C to quit