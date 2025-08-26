using System;
using System.Collections.Generic;

namespace PdfApi.Dtos
{
    public class LoaReportDto
    {
        public string? FacilityName { get; set; }
        public string? FacilityAddress { get; set; }
        public string? FacilityProvince { get; set; }
        public string? PatientName { get; set; }
        public string? CardNumber { get; set; }
        public string? FormattedDateOfBirth { get; set; }
        public string? PolicyNumber { get; set; }
        public string? PolicyPeriod { get; set; }
        public string? PolicyHolder { get; set; }
        public string? ProductScheme { get; set; }
        public string? PlanName { get; set; }
        public string FormattedLetterDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public string? LogoImageSrc { get; set; }
        public string? ReferenceNumber { get; set; }

        public List<LoaBenefitDetailDto> Benefits { get; set; } = new List<LoaBenefitDetailDto>();
        public List<TermAndConditionDto> TermsAndConditions { get; set; } = new List<TermAndConditionDto>();
    }
    
    public class LoaBenefitDetailDto
    {
        public string? NAMAPROVIDER { get; set; }
        public string? ALAMATPROVIDER { get; set; }
        public string? KOTAPROVIDER { get; set; }
        public string? NAMAPESERTA { get; set; }
        public string? NOKA { get; set; }
        public string? TGLLAHIR { get; set; }
        public string? NOPOLIS { get; set; }
        public string? MASAPOLIS { get; set; }
        public string? NAMABU { get; set; }
        public string? BENEFITNAME { get; set; }
        public string? TAGIHAN { get; set; }
        public string? schema_product { get; set; }
        public string? plan_code { get; set; }
    }

    public class TermAndConditionDto
    {
        public string? deskripsi_loa { get; set; }
    }
}
