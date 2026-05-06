using System;
using System.Collections.Generic;

namespace GradeInput_Advanced_Programming.Trading;

public partial class Currency
{
    public int Id { get; set; }

    public string? CurrencyName { get; set; }

    public char? Symbol { get; set; }


    public virtual ICollection<UAppGrade> UAppGrades { get; set; } = new List<UAppGrade>();
}
