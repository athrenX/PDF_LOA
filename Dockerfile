# Stage 1: Build & Publish
# Menggunakan .NET 8 SDK sebagai dasar untuk membangun aplikasi
# Ganti '8.0' dengan versi .NET yang Anda gunakan jika berbeda
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy file project (.csproj) dan solution (.sln) terlebih dahulu untuk optimasi cache
COPY *.sln .
COPY PdfReportingHandlebars/*.csproj ./PdfReportingHandlebars/

# Restore dependencies
RUN dotnet restore

# Copy sisa source code
COPY . .

# Publish aplikasi dengan konfigurasi Release
WORKDIR /source/PdfReportingHandlebars
RUN dotnet publish -c Release -o /app/publish --no-restore

# Stage 2: Final Image
# Menggunakan ASP.NET runtime yang lebih kecil sebagai dasar image akhir
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy hasil publish dari stage 'build'
COPY --from=build /app/publish .

# Tentukan port yang akan diekspos oleh container (sesuaikan jika perlu, 80 adalah default)
EXPOSE 8080
EXPOSE 8081

# Entry point untuk menjalankan aplikasi saat container dimulai
ENTRYPOINT ["dotnet", "PdfReportingHandlebars.dll"]