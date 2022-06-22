# .NET Kafka Getting Started

```console
docker compose up -d
```

## Create Topic

```console
docker compose exec broker \
  kafka-topics --create \
    --topic purchases \
    --bootstrap-server localhost:9092 \
    --replication-factor 1 \
    --partitions 1
```