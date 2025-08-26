using Microsoft.AspNetCore.Mvc;
using HandlebarsDotNet;
using PuppeteerSharp;
using System;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/loa")]
public class LoaController : ControllerBase
{
    private readonly LoaService _loaService;

    public LoaController(LoaService loaService)
    {
        _loaService = loaService;
    }

    [HttpGet("export/{claimNo}")]
    public async Task<IActionResult> ExportLoaByClaimNo(string claimNo, [FromQuery] string format = "pdf")
    {
        try
        {
            var loaReportData = await _loaService.GetLoaReportAsync(claimNo);
            if (loaReportData == null)
            {
                return NotFound(new { message = $"Tidak ada data untuk nomor klaim: {claimNo}" });
            }
            
            var imagePath = "Assets/LOGO_MANDIRI.jpg";
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound(new { message = "File logo tidak ditemukan di " + imagePath });
            }
            var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);
            var base64Image = Convert.ToBase64String(imageBytes);
            loaReportData.LogoImageSrc = $"data:image/jpeg;base64,{base64Image}";

            var templateHtml = await System.IO.File.ReadAllTextAsync("Templates/LoaTemplate.html");
            var compiledTemplate = Handlebars.Compile(templateHtml);
            var html = compiledTemplate(loaReportData);

            await new BrowserFetcher().DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);

            var fileName = $"LOA_Report_{claimNo}_{DateTime.Now:yyyyMMdd}";

            if (format.ToLower() == "image")
            {
                var imageStream = await page.ScreenshotStreamAsync(new ScreenshotOptions { Type = ScreenshotType.Png, FullPage = true });
                imageStream.Position = 0;
                return File(imageStream, "image/png", $"{fileName}.png");
            }
            else
            {
                var pdfStream = await page.PdfStreamAsync(new PdfOptions 
                { 
                    Width = "215mm", 
                    Height = "330mm",
                    PrintBackground = true 
                });
                pdfStream.Position = 0;
                return File(pdfStream, "application/pdf", $"{fileName}.pdf");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Terjadi kesalahan internal.", error = ex.Message });
        }
    }
}