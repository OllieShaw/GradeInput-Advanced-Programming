using GradeInput_Advanced_Programming.Helper;
using GradeInput_Advanced_Programming.Models;
using GradeInput_Advanced_Programming.Trading;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.UappGrade
{
    public class UappGradeVm
    {
        [Display(Name = "Grade ID")]
        [ReadOnly(true)]
        public int Id { get; set; }

        [Display(Name = "Grade Name")]
        public string? GradeName { get; set; }

        [Display(Name = "Table Name")]
        public string? TableName { get; set; }

        [Display(Name = "Spot Table")]
        public string? SpotTable { get; set; }

        [Display(Name = "Spot Name")]
        public string? SpotName { get; set; }

        [Display(Name = "Forward Grade")]
        public string? ForwardGrade { get; set; }

        [Display(Name = "Litres Per Tonne")]
        public double? LitresPerTonne { get; set; }

        [Display(Name = "Costs Add-On")]
        public double? CostsAddOn { get; set; }

        [Display(Name = "Multiple To MT")]
        public double? MultipleToMt { get; set; }

        [Display(Name = "Rounding")]
        public double? Rounding { get; set; }

        [Display(Name = "Diff Add-On")]
        public double DiffAddOn { get; set; }

        [Display(Name = "Default Costs Add-On")]
        public double? DefaultCostsAddOn { get; set; }

        [Display(Name = "Volume Type")]
        public int? Volumetype { get; set; }

        [Display(Name = "Is Indic Available")]
        public bool IsIndicAvailable { get; set; }

        [Display(Name = "Reported Currency")]
        public int? ReportedCurrency { get; set; }

        [Display(Name = "Is Contract Month Specific?")]
        public bool IsContractMonthSpecific { get; set; }

        [Display(Name = "Winter Grade Start Day")]
        public int? WinterGradeStartDay { get; set; }

        [Display(Name = "Winter Grade Start Month")]
        public int? WinterGradeStartMonth { get; set; }

        [Display(Name = "Winter Grade End Day")]
        public int? WinterGradeEndDay { get; set; }

        [Display(Name = "Winter Grade End Month")]
        public int? WinterGradeEndMonth { get; set; }
        [Display(Name = "Country?")]
        public GeographicJurisdiction? Country { get; set; }
        public int CountryID { get; set; }
        public bool IsEdit { get; set; } = false;

        public bool IsSplitGrade
        {
            get
            {
                return SplitData != null && SplitData.Count > 0;
            }
        }

        public List<SplitData>? SplitData { get; set; }



        public SelectList? VolumeType { get; set; }
        public SelectList? GeographicJurisdiction { get; set; }
        public SelectList? GradesAsSelectList { get; set; }
        public SelectList? CurrencysAsSelectList { get; set; }

        public Exception? Exception { get; set; }



        public void LoadUi(TradingContext context)
        {
            GradesAsSelectList = UtilityHelper.ConvertToSelectList(context.UAppGrades.AsEnumerable(), nameof(UAppGrade.Id), nameof(UAppGrade.GradeName));
            VolumeType = UtilityHelper.ConvertToSelectList(context.Volumetypes.AsEnumerable(), nameof(GradeInput_Advanced_Programming.Trading.Volumetype.Id), nameof(GradeInput_Advanced_Programming.Trading.Volumetype.Type));
            CurrencysAsSelectList = UtilityHelper.ConvertToSelectList(context.Currencies.AsEnumerable(), nameof(Currency.Id), nameof(Currency.CurrencyName));
            var G = UtilityHelper.AddEmptyOptionToSelectList(EnumExtensions.ToSelectList<GeographicJurisdiction>());
            GeographicJurisdiction = G;
        }

        internal void FromDb(UAppGrade uAppGrade)
        {
            Id = uAppGrade.Id;
            GradeName = uAppGrade.GradeName;
            TableName = uAppGrade.TableName;
            SpotTable = uAppGrade.SpotTable;
            SpotName = uAppGrade.SpotName;
            ForwardGrade = uAppGrade.ForwardGrade;
            LitresPerTonne = uAppGrade.LitresPerTonne;
            CostsAddOn = uAppGrade.CostsAddOn;
            MultipleToMt = uAppGrade.MultipleToMt;
            Rounding = uAppGrade.Rounding;
            DiffAddOn = uAppGrade.DiffAddOn;
            DefaultCostsAddOn = uAppGrade.DefaultCostsAddOn;
            Volumetype = uAppGrade.Volumetype;
            IsIndicAvailable = uAppGrade.IsIndicAvailable;
            ReportedCurrency = uAppGrade.ReportedCurrency;
            IsContractMonthSpecific = uAppGrade.IsContractMonthSpecific;
            WinterGradeStartDay = uAppGrade.WinterGradeStartDay;
            WinterGradeStartMonth = uAppGrade.WinterGradeStartMonth;
            WinterGradeEndDay = uAppGrade.WinterGradeEndDay;
            WinterGradeEndMonth = uAppGrade.WinterGradeEndMonth;
            if (uAppGrade.CountryId.HasValue)
                Country = EnumExtensions.GetEnumFromId<GeographicJurisdiction>(uAppGrade.CountryId.Value);

            if (Country.HasValue)
            {
                CountryID = (int)Country.Value;

            }
            SplitData = UAppGradeHelper.GetSplitDataFromDb(uAppGrade.SplitData);

        }
        internal UAppGrade ToDbObject()
        {
            var uAppGrade = new UAppGrade
            {
                Id = Id,
                GradeName = GradeName,
                TableName = TableName,
                SpotTable = SpotTable,
                SpotName = SpotName,
                ForwardGrade = ForwardGrade,
                LitresPerTonne = LitresPerTonne,
                CostsAddOn = CostsAddOn,
                MultipleToMt = MultipleToMt,
                Rounding = Rounding,
                DiffAddOn = DiffAddOn,
                DefaultCostsAddOn = DefaultCostsAddOn,
                Volumetype = Volumetype,
                IsIndicAvailable = IsIndicAvailable,
                ReportedCurrency = ReportedCurrency,
                IsContractMonthSpecific = IsContractMonthSpecific,
                WinterGradeStartDay = WinterGradeStartDay,
                WinterGradeStartMonth = WinterGradeStartMonth,
                WinterGradeEndDay = WinterGradeEndDay,
                WinterGradeEndMonth = WinterGradeEndMonth
            };
            if (Country.HasValue)
                uAppGrade.CountryId = (int)Country.Value;
            uAppGrade.SplitData = UAppGradeHelper.GetSplitDataFromViewModelStatic(SplitData);
            return uAppGrade;
        }
    }

    public class SplitData
    {
        public int GradeId { get; set; }
        public decimal Ratio { get; set; }
    }
}
