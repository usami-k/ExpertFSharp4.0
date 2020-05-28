# Paket

## Install Paket

```
dotnet new tool-manifest
dotnet tool install Paket
```

## Install packages using Paket

Create `paket.dependencies` file.

```
dotnet paket init
```

Edit `paket.dependencies` file.

- Remove `storage: none` line.
- Add `nuget` line.

Run `paket install`.

```
dotnet paket install
```
