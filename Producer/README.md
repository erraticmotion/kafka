# Producer

Build `Producer` inside the VM.

```console
cd ./getting-started/Producer

dotnet build Producer.csproj
```

Produce Events

```console
dotnet run --project Producer.csproj $(pwd)/getting-started.properties
```