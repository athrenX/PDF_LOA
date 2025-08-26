using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PdfApi.Dtos;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

public class LoaService
{
    private readonly IConfiguration _configuration;

    public LoaService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<LoaReportDto?> GetLoaReportAsync(string claimNo)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");      
        await using var connection = new SqlConnection(connectionString);

        var benefitDetails = (await connection.QueryAsync<LoaBenefitDetailDto>(
            "provider.SP_GET_REPORT_LOA",
            new { claim_no = claimNo },
            commandType: CommandType.StoredProcedure
        )).ToList();
        
        if (benefitDetails == null || !benefitDetails.Any())
        {
            return null;
        }
      
        var terms = (await connection.QueryAsync<TermAndConditionDto>(
            "provider.SP_TC_LOA_LOC",
            new { claim_no = claimNo },
            commandType: CommandType.StoredProcedure
        )).ToList();
       
        var headerData = benefitDetails.First(); 
        
        var report = new LoaReportDto
        {
            FacilityName = headerData.NAMAPROVIDER,
            FacilityAddress = headerData.ALAMATPROVIDER,
            FacilityProvince = headerData.KOTAPROVIDER,
            PatientName = headerData.NAMAPESERTA,
            CardNumber = headerData.NOKA,
            FormattedDateOfBirth = headerData.TGLLAHIR,
            PolicyNumber = headerData.NOPOLIS,
            PolicyPeriod = headerData.MASAPOLIS,
            PolicyHolder = headerData.NAMABU,
            ProductScheme = headerData.schema_product,
            PlanName = headerData.plan_code, 
            ReferenceNumber = claimNo,
            Benefits = benefitDetails, 
            TermsAndConditions = terms 
        };

        return report;
    }
}
