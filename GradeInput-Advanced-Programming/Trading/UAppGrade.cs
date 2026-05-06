using System;
using System.Collections.Generic;

namespace GradeInput_Advanced_Programming.Trading;

public partial class UAppGrade
{
    public int Id { get; set; }

    public string? GradeName { get; set; }

    public string? TableName { get; set; }

    public string? SpotTable { get; set; }

    public string? SpotName { get; set; }

    public string? ForwardGrade { get; set; }

    public double? LitresPerTonne { get; set; }

    public double? CostsAddOn { get; set; }

    public double? MultipleToMt { get; set; }

    public double? Rounding { get; set; }

    public double DiffAddOn { get; set; }

    public double? DefaultCostsAddOn { get; set; }

    public int? Volumetype { get; set; }

    public int? ReportedCurrency { get; set; }

    public bool IsSplitGrade { get; set; }

    public string? SplitData { get; set; }

    public bool IsIndicAvailable { get; set; }

    public bool IsContractMonthSpecific { get; set; }

    public int? WinterGradeStartDay { get; set; }

    public int? WinterGradeStartMonth { get; set; }

    public int? WinterGradeEndDay { get; set; }

    public int? WinterGradeEndMonth { get; set; }

    public int? CountryId { get; set; }

    public virtual Currency? ReportedCurrencyNavigation { get; set; }

    public virtual Volumetype? VolumetypeNavigation { get; set; }
}
