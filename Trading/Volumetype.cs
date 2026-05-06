using System;
using System.Collections.Generic;

namespace GradeInput_Advanced_Programming.Trading;

public partial class Volumetype
{
    public int Id { get; set; }

    public string? Type { get; set; }


    public virtual ICollection<UAppGrade> UAppGrades { get; set; } = new List<UAppGrade>();

}
