# Layanan Pelaporan PDF dengan Handlebars (PdfReportingHandlebars)

Ini adalah sebuah layanan backend (API) yang dibangun menggunakan ASP.NET Core untuk menghasilkan dokumen PDF secara dinamis. Layanan ini menggunakan template HTML yang didukung oleh **Handlebars.js** untuk memasukkan data dari request JSON, kemudian mengonversi hasilnya menjadi file PDF.

Proyek ini sangat cocok untuk kebutuhan pembuatan dokumen berulang seperti surat perjanjian (Letter of Agreement), faktur (invoice), sertifikat, atau laporan kustom lainnya.

## Fitur Utama

-   **Generasi PDF Dinamis**: Membuat file PDF dari template HTML dan data JSON.
-   **Template Engine Handlebars**: Memudahkan pembuatan template yang logis dan mudah dibaca.
-   **Endpoint RESTful API**: Mudah diintegrasikan dengan aplikasi frontend atau layanan lainnya.
-   **Siap untuk Kontainerisasi**: Dilengkapi dengan `Dockerfile` untuk build dan deployment yang mudah menggunakan Docker.
-   **Ringan dan Cepat**: Dibangun di atas platform .NET yang modern dan berkinerja tinggi.

## Teknologi yang Digunakan

-   **Backend**: ASP.NET Core 8 (atau versi .NET yang Anda gunakan)
-   **Bahasa**: C#
-   **Template Engine**: [Handlebars.Net](https://github.com/Handlebars-Net/Handlebars.Net) (library .NET untuk Handlebars)
-   **PDF Engine**: Menggunakan library konversi HTML ke PDF (contoh: *IronPDF*, *PuppeteerSharp*, *WkHtmlToPdf*, dll.)
-   **Deployment**: Docker
