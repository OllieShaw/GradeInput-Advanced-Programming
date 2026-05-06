using Xunit;
using Microsoft.EntityFrameworkCore;
using GradeInput_Advanced_Programming.Controllers;
using GradeInput_Advanced_Programming.Trading;
using Models.UappGrade;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

public class UAppGradeControllerTests
{
    private TradingContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<TradingContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new TradingContext(options);
    }

    [Fact]
    public void AddSplitRow_FirstRow_ShouldBe100Percent()
    {
        var context = GetDbContext();
        var controller = new UAppGradeController(context);

        var vm = new UappGradeVm
        {
            SplitData = new List<SplitData>()
        };

        var result = controller.AddSplitRow(vm) as ViewResult;
        var model = result.Model as UappGradeVm;

        Assert.Single(model.SplitData);
        Assert.Equal(100m, model.SplitData.First().Ratio);
    }

    [Fact]
    public void AddSplitRow_SecondRow_ShouldSplitCorrectly()
    {
        var context = GetDbContext();
        var controller = new UAppGradeController(context);

        var vm = new UappGradeVm()
        {
            SplitData = new List<SplitData>()
        };

        var result = controller.AddSplitRow(vm) as ViewResult;
        var model = result.Model as UappGradeVm;

        Assert.Single(model.SplitData);
        Assert.Equal(100m, model.SplitData[0].Ratio);
    }

    [Fact]
    public void Create_ValidModel_RedirectsToIndex()
    {
        var context = GetDbContext();
        var controller = new UAppGradeController(context);

        var vm = new UappGradeVm
        {
            GradeName = "Test Grade"
        };

        var result = controller.Create(vm);

        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public void Edit_InvalidModel_ReturnsView()
    {
        var context = GetDbContext();
        var controller = new UAppGradeController(context);

        controller.ModelState.AddModelError("GradeName", "Required");

        var vm = new UappGradeVm();

        var result = controller.Edit(vm);

        Assert.IsType<ViewResult>(result);
    }
}