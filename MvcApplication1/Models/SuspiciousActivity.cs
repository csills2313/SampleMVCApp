using CIC.CTRSAR.Model.EnumTypes;
using CIC.CTRSAR.Model.MetaModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace CIC.CTRSAR.Model
{
    [Table("SuspiciousActivity")]
    [Serializable(), DataContract()]
    [MetadataType(typeof(MetaModel.SuspiciousActivityMetadata))]
    public class SuspiciousActivity : IFinCENRecord
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember()]
        public int SuspiciousActivityID { get; set; }

        [NotMapped()]
        [FinCENField(1, 2, "{0,2}", "3A")]
        [JsonIgnore()]
        public String RecordType { get { return "3A"; } }

        [FinCENField(3, 5, "{0,5:D5}", null)]
        [DataMember()]
        public int TransactionSequenceNumber { get; set; }

        [DataMember()]
        public FilingType FilingType { get; set; }

        [NotMapped()]
        [FinCENField(9, 1, "{0,1}", null)]
        [MaxLength(1)]
        [JsonIgnore()]
        public String FilingTypeField { get { return FilingType.ToFinCENString(); } }

        [DataMember()]
        public Boolean IsContinuingActivity { get; set; }

        [NotMapped()]
        [FinCENField(10, 1, "{0,1}", null)]
        [JsonIgnore()]
        public Char IsContinuingActivityField { get { return IsContinuingActivity ? 'C' : ' '; } }

        [DataMember()]
        public Boolean IsJointReport { get; set; }

        [NotMapped()]
        [FinCENField(11, 1, "{0,1}", null)]
        [JsonIgnore()]
        public Char IsJointReportField { get { return IsJointReport ? 'D' : ' '; } }

        [MaxLength(14)]
        [DataMember()]
        public String BSAID { get; set; }

        [DataMember()]
        public int? PriorReportID { get; set; }

        [DataMember()]
        [ForeignKey("PriorReportID")]
        public virtual SuspiciousActivity PriorReport { get; set; }

        [NotMapped()]
        [FinCENField(12, 14, "{0,14}", null)]
        [JsonIgnore()]
        public String PriorReportDCN
        {
            get
            {
                if (FilingType == FilingType.Correction || IsContinuingActivity)
                {
                    if (PriorReport != null && !String.IsNullOrWhiteSpace(PriorReport.BSAID))
                    {
                        return PriorReport.BSAID;
                    }
                    else
                    {
                        return "00000000000000";
                    }
                }
                else
                {
                    return "              ";
                }
            }
        }

        [DataMember()]
        [FinCENField(26, 15, "{0,-15:F0}", null)]
        public Decimal? AmountInvolved { get; set; }

        [DataMember()]
        public Boolean IsAmountUnknown { get; set; }

        [NotMapped()]
        [FinCENField(41, 1, "{0,1}", null)]
        [JsonIgnore()]
        public Char NoOrUnknownAmountField { get { return IsAmountUnknown ? 'A' : (!AmountInvolved.HasValue || AmountInvolved == 0) ? 'B' : ' '; } }

        [DataMember()]
        [FinCENField(42, 8, "{0,8:MMddyyyy}", null)]
        public DateTime ActivityStartDate { get; set; }

        [DataMember()]
        [FinCENField(50, 8, "{0,8:MMddyyyy}", null)]
        public DateTime ActivityEndDate { get; set; }

        [DataMember()]
        public Decimal CumulativeAmount { get; set; }

        [NotMapped()]
        [FinCENField(58, 15, "{0,-15}", null)]
        [JsonIgnore()]
        public String CumulativeAmountField { get { return IsContinuingActivity ? CumulativeAmount.ToString("F0") : null; } }

        [DataMember()]
        public StructuringType StructuringTypes { get; set; }

        [NotMapped()]
        [FinCENField(73, 7, "{0,7}", null)]
        [JsonIgnore()]
        public String StructuringTypesField { get { return StructuringTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String StructuringOtherDescription { get; set; }

        [NotMapped()]
        [FinCENField(80, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String StructuringOtherDescriptionField { get { return (StructuringTypes & StructuringType.Other) == StructuringType.Other ? StructuringOtherDescription : null; } }

        [DataMember()]
        public TerroristFinancingType TerroristFinancingTypes { get; set; }

        [NotMapped()]
        [FinCENField(130, 2, "{0,2}", null)]
        [JsonIgnore()]
        public String TerroristFinancingTypesField { get { return TerroristFinancingTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String TerroristFinancingOtherDescription { get; set; }

        [NotMapped()]
        [FinCENField(132, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String TerroristFinancingOtherDescriptionField { get { return (TerroristFinancingTypes & TerroristFinancingType.Other) == TerroristFinancingType.Other ? TerroristFinancingOtherDescription : null; } }

        [DataMember()]
        public FraudType FraudTypes { get; set; }

        [NotMapped()]
        [FinCENField(182, 11, "{0,11}", null)]
        [JsonIgnore()]
        public String FraudTypesField { get { return FraudTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String FraudOtherDescription { get; set; }

        [NotMapped()]
        [FinCENField(193, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String FraudOtherDescriptionField { get { return (FraudTypes & FraudType.Other) == FraudType.Other ? FraudOtherDescription : null; } }

        [DataMember()]
        public CasinoActivityType CasinoActivityTypes { get; set; }

        [NotMapped()]
        [FinCENField(243, 5, "{0,5}", null)]
        [JsonIgnore()]
        public String CasinoActivityTypesField { get { return CasinoActivityTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String CasinoActivityOtherDescription { get; set; }

        [NotMapped()]
        [FinCENField(248, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String CasinoActivityOtherDescriptionField { get { return (CasinoActivityTypes & CasinoActivityType.Other) == CasinoActivityType.Other ? CasinoActivityOtherDescription : null; } }

        [DataMember()]
        public MoneyLaunderingType MoneyLaunderingTypes { get; set; }

        [NotMapped()]
        [FinCENField(298, 5, "{0,5}", null)]
        [JsonIgnore()]
        public String MoneyLaunderingTypesField { get { return MoneyLaunderingTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String MoneyLaunderingOtherDescription { get; set; }

        [NotMapped()]
        [FinCENField(311, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String MoneyLaunderingOtherDescriptionField { get { return (MoneyLaunderingTypes & MoneyLaunderingType.Other) == MoneyLaunderingType.Other ? MoneyLaunderingOtherDescription : null; } }

        [DataMember()]
        public SuspiciousIdentificationType SuspiciousIdentificationTypes { get; set; }

        [NotMapped()]
        [FinCENField(361, 6, "{0,6}", null)]
        [JsonIgnore()]
        public String SuspiciousIdentificationTypesField { get { return SuspiciousIdentificationTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String SuspiciousIdentificationOther { get; set; }

        [NotMapped()]
        [FinCENField(367, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String SuspiciousIdentificationOtherField { get { return (SuspiciousIdentificationTypes & SuspiciousIdentificationType.Other) == SuspiciousIdentificationType.Other ? SuspiciousIdentificationOther : null; } }

        [DataMember()]
        public UncategorizedSuspiciousActivityType UncategorizedSuspiciousActivityTypes { get; set; }

        [NotMapped()]
        [FinCENField(417, 19, "{0,19}", null)]
        [JsonIgnore()]
        public String UncategorizedSuspiciousActivityTypesField { get { return UncategorizedSuspiciousActivityTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String UncategorizedSuspiciousActivityOther { get; set; }

        [NotMapped()]
        [FinCENField(436, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String UncategorizedSuspiciousActivityOtherField { get { return (UncategorizedSuspiciousActivityTypes & UncategorizedSuspiciousActivityType.Other) == UncategorizedSuspiciousActivityType.Other ? UncategorizedSuspiciousActivityOther : null; } }

        [DataMember()]
        public InsuranceActivityType InsuranceActivityTypes { get; set; }

        [NotMapped()]
        [FinCENField(486, 7, "{0,7}", null)]
        [JsonIgnore()]
        public String InsuranceActivityTypesField { get { return InsuranceActivityTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String InsuranceActivityOther { get; set; }

        [NotMapped()]
        [FinCENField(493, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String InsuranceActivityOtherField { get { return (InsuranceActivityTypes & InsuranceActivityType.Other) == InsuranceActivityType.Other ? InsuranceActivityOther : null; } }

        [DataMember()]
        public SecuritiesActivityType SecuritiesActivityTypes { get; set; }

        [NotMapped()]
        [FinCENField(543, 7, "{0,7}", null)]
        [JsonIgnore()]
        public String SecuritiesActivityTypesField { get { return SecuritiesActivityTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String SecuritiesActivityOther { get; set; }

        [NotMapped()]
        [FinCENField(548, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String SecuritiesActivityOtherField { get { return (SecuritiesActivityTypes & SecuritiesActivityType.Other) == SecuritiesActivityType.Other ? SecuritiesActivityOther : null; } }

        [DataMember()]
        public MortageFraudType MortageFraudTypes { get; set; }

        [NotMapped()]
        [FinCENField(598, 7, "{0,7}", null)]
        [JsonIgnore()]
        public String MortageFraudTypesField { get { return MortageFraudTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String MortageFraudOther { get; set; }

        [NotMapped()]
        [FinCENField(603, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String MortageFraudOtherField { get { return (MortageFraudTypes & MortageFraudType.Other) == MortageFraudType.Other ? MortageFraudOther : null; } }

        [DataMember()]
        public ProductType ProductTypes { get; set; }

        [NotMapped()]
        [FinCENField(653, 20, "{0,20}", null)]
        [JsonIgnore()]
        public String ProductTypesField { get { return ProductTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String ProductTypeOther { get; set; }

        [NotMapped()]
        [FinCENField(673, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String ProductTypeOtherField { get { return (ProductTypes & ProductType.Other) == ProductType.Other ? ProductTypeOther : null; } }

        [DataMember()]
        public InstrumentType InstrumentTypes { get; set; }

        [NotMapped()]
        [FinCENField(723, 10, "{0,10}", null)]
        [JsonIgnore()]
        public String InstrumentTypesField { get { return InstrumentTypes.ToFinCENString(); } }

        [MaxLength(50)]
        [DataMember()]
        public String InstrumentTypeOther { get; set; }

        [NotMapped()]
        [FinCENField(733, 50, "{0,50}", null)]
        [JsonIgnore()]
        public String InstrumentTypeOtherField { get { return (InstrumentTypes & InstrumentType.Other) == InstrumentType.Other ? InstrumentTypeOther : null; } }

        [MaxLength(150)]
        [FinCENField(783, 150, "{0,150}", null)]
        [DataMember()]
        public String LEContactAgency { get; set; }

        [MaxLength(150)]
        [FinCENField(933, 150, "{0,150}", null)]
        [DataMember()]
        public String LEContactName { get; set; }

        [MaxLength(16)]
        [FinCENField(1083, 16, "{0,16}", null)]
        [DataMember()]
        public String LEContactTelephone { get; set; }

        [MaxLength(6)]
        [FinCENField(1099, 6, "{0,6}", null)]
        [DataMember()]
        public String LEContactExtension { get; set; }

        [FinCENField(1105, 8, "{0,8:MMddyyyy}", null)]
        [DataMember()]
        public DateTime? LEContactDate { get; set; }

        [MaxLength(10)]
        [FinCENField(1113, 10, "{0,10}", null)]
        [DataMember()]
        public String FilingInstitutionContactOffice { get; set; }

        [MaxLength(16)]
        [FinCENField(1123, 16, "{0,16}", null)]
        [DataMember()]
        public String FilingInstitutionPhoneNumber { get; set; }

        [MaxLength(6)]
        [FinCENField(1139, 6, "{0,6}", null)]
        [DataMember()]
        public String FilingInstitutionExtension { get; set; }

        [FinCENField(1145, 8, "{0,8:MMddyyyy}", null)]
        [DataMember()]
        public DateTime? DateFiled { get; set; }

        [NotMapped()]
        [FinCENField(1153, 20, "{0,20}", null)]
        [JsonIgnore()]
        public int InternalControlNumber { get { return SuspiciousActivityID; } }

        [NotMapped()]
        [FinCENField(1173, 18, "{0,18}", null)]
        [JsonIgnore()]
        public String Filler { get; set; }

        [NotMapped()]
        [FinCENField(1191, 10, "{0,10:D10}", null)]
        [JsonIgnore()]
        public int UserField { get { return SuspiciousActivityID; } }

        [DataMember()]
        public Boolean? Survey_WUTransactionApproved { get; set; }

        [DataMember()]
        public Boolean? Survey_WUHoldOnFunds { get; set; }

        [DataMember()]
        public Boolean? Survey_MultipleTransactionsInSameDay { get; set; }

        [DataMember()]
        [MaxLength(200)]
        public String Survey_MultipleTransactionsInSameDay_Amounts { get; set; }

        [DataMember()]
        public Boolean? Survey_MultipleTransactionConsecutiveDays { get; set; }

        [DataMember()]
        [MaxLength(200)]
        public String Survey_MultipleTransactionConsecutiveDays_Amounts { get; set; }

        [DataMember()]
        public Boolean? Survey_TransactionExplaination { get; set; }

        [DataMember()]
        [MaxLength(500)]
        public String Survey_TransactionExplainationDescription { get; set; }

        [DataMember()]
        public Boolean? Survey_CustomerDirectedByThirdParty { get; set; }

        [DataMember()]
        [MaxLength(500)]
        public String Survey_CustomerDirectedByThirdPartyDescription { get; set; }

        [DataMember()]
        public Boolean? Survey_SendsObtainedPhotoID { get; set; }

        [DataMember()]
        public Boolean? Survey_SendsObtainedIdentityInformation { get; set; }

        [DataMember()]
        public Boolean? Survey_ReceivesObtainedPhotoID { get; set; }

        [DataMember()]
        public Boolean? Survey_ReceivesObtainedIdentityInformation { get; set; }

        [DataMember()]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastChangeDateTime { get; set; }

        [DataMember()]
        public virtual List<FinancialInstitutionFiling> FinancialInstitutionFilings { get; set; }

        [DataMember()]
        [FinCENRecordCollection(OrderIndex = 0)]
        public virtual List<CommodityType> Commodities { get; set; }

        [DataMember()]
        [FinCENRecordCollection(OrderIndex = 1)]
        public virtual List<ProductDescription> Products { get; set; }

        [DataMember()]
        [FinCENRecordCollection(OrderIndex = 2)]
        public virtual List<TradingMarket> Markets { get; set; }

        [DataMember()]
        [FinCENRecordCollection(OrderIndex = 3)]
        public virtual List<IPAddress> IPAddresses { get; set; }

        [DataMember()]
        [FinCENRecordCollection(OrderIndex = 4)]
        public virtual List<CUSIPNumber> CUSIPNumbers { get; set; }

        [DataMember()]
        [FinCENRecordCollection(OrderIndex = 5)]
        public virtual List<SARSubject> Subjects { get; set; }

        [DataMember()]
        [FinCENRecordCollection(OrderIndex = 6)]
        public virtual List<NarrativeDescription> Narratives { get; set; }

        [JsonIgnore()]
        public SuspiciousActivitySummary SuspiciousActivitySummary
        {
            get
            {
                return new SuspiciousActivitySummary()
                {
                    UserField = SuspiciousActivityID,
                    SubjectCount = Subjects.Count,
                    NarrativeCount = Narratives.Count,
                    BranchOfficeCount = FinancialInstitutionFilings.Sum(x => x.BranchesInvolved.Count),
                };
            }
        }

        [NotMapped()]
        [JsonIgnore()]
        public String SuspiciousActivityString { get { return String.Format("{1:yyyy-MM-dd} - {2:yyyy-MM-dd} :: {0}", Subjects.First().FullName, ActivityStartDate, ActivityEndDate); } }

        public SuspiciousActivity()
        {
            CasinoActivityTypes = CasinoActivityType.Unselected;
            FraudTypes = FraudType.Unselected;
            InstrumentTypes = InstrumentType.Unselected;
            InsuranceActivityTypes = InsuranceActivityType.Unselected;
            MoneyLaunderingTypes = MoneyLaunderingType.Unselected;
            MortageFraudTypes = MortageFraudType.Unselected;
            ProductTypes = ProductType.Unselected;
            SecuritiesActivityTypes = SecuritiesActivityType.Unselected;
            StructuringTypes = StructuringType.Unselected;
            SuspiciousIdentificationTypes = SuspiciousIdentificationType.Unselected;
            TerroristFinancingTypes = TerroristFinancingType.Unselected;
            UncategorizedSuspiciousActivityTypes = UncategorizedSuspiciousActivityType.Unselected;

            Commodities = new List<CommodityType>();
            Products = new List<ProductDescription>();
            Markets = new List<TradingMarket>();
            IPAddresses = new List<IPAddress>();
            CUSIPNumbers = new List<CUSIPNumber>();
            Subjects = new List<SARSubject>();
            Narratives = new List<NarrativeDescription>();
            FinancialInstitutionFilings = new List<FinancialInstitutionFiling>();
            LastChangeDateTime = DateTime.Now;
        }
    }
}