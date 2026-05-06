using GradeInput_Advanced_Programming.Trading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Models;
using Models.UappGrade;

namespace GradeInput_Advanced_Programming.Controllers
{
    public class UAppGradeController : Controller
    {
        private readonly TradingContext _context;
        public UAppGradeController(TradingContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var existing = _context.UAppGrades.Find(id);
            if (existing == null)
                return NotFound();

            _context.UAppGrades.Remove(existing);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(UappGradeVm uappGradeVm)
        {
            if (!ModelState.IsValid)
            {
                uappGradeVm.LoadUi(_context);
                return View("/Views/UappGrade/Create.cshtml", uappGradeVm);
            }

            try
            {
                var existing = _context.UAppGrades
                    .FirstOrDefault(e => e.Id == uappGradeVm.Id);

                if (existing == null)
                    return NotFound();

                var updated = uappGradeVm.ToDbObject();

                existing.GradeName = updated.GradeName;
                existing.TableName = updated.TableName;
                existing.SpotTable = updated.SpotTable;
                existing.SpotName = updated.SpotName;
                existing.ForwardGrade = updated.ForwardGrade;
                existing.LitresPerTonne = updated.LitresPerTonne;
                existing.CostsAddOn = updated.CostsAddOn;
                existing.MultipleToMt = updated.MultipleToMt;
                existing.Rounding = updated.Rounding;
                existing.DiffAddOn = updated.DiffAddOn;
                existing.DefaultCostsAddOn = updated.DefaultCostsAddOn;
                existing.Volumetype = updated.Volumetype;
                existing.IsIndicAvailable = updated.IsIndicAvailable;
                existing.ReportedCurrency = updated.ReportedCurrency;
                existing.IsContractMonthSpecific = updated.IsContractMonthSpecific;
                existing.WinterGradeStartDay = updated.WinterGradeStartDay;
                existing.WinterGradeStartMonth = updated.WinterGradeStartMonth;
                existing.WinterGradeEndDay = updated.WinterGradeEndDay;
                existing.WinterGradeEndMonth = updated.WinterGradeEndMonth;
                existing.CountryId = updated.CountryId;
                existing.SplitData = updated.SplitData;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                uappGradeVm.LoadUi(_context);
                uappGradeVm.Exception = e;
                return View("/Views/UappGrade/Create.cshtml", uappGradeVm);
            }
        }



        public IActionResult Edit(int Id)
        {
            var UappGrade = _context.UAppGrades.FirstOrDefault(e => e.Id == Id);
            if (UappGrade == null) return NotFound();

            var mapped = MapDbUappGradeToVm(UappGrade);

            mapped.LoadUi(_context);
            mapped.IsEdit = true;
            return View("/Views/UappGrade/Create.cshtml", mapped);
        }
        public IActionResult Index()
        {
            UappGradeIndex uappGradeIndex = new UappGradeIndex();
            uappGradeIndex.uappGradeVms = _context.UAppGrades.AsEnumerable().Select(uAppGrade => MapDbUappGradeToVm(uAppGrade))
               .OrderByDescending(e => e.Id).ToList();
            return View(uappGradeIndex);
        }
        private UappGradeVm MapDbUappGradeToVm(UAppGrade uAppGrade)
        {

            var uappGradeVm = new UappGradeVm();
            uappGradeVm.FromDb(uAppGrade);

            return uappGradeVm;
        }

        [HttpGet]
        public IActionResult Create()
        {
            UappGradeVm uappGradeVm = new UappGradeVm();
            uappGradeVm.LoadUi(_context);
            return View(uappGradeVm);
        }

        [HttpPost]
        public IActionResult AddSplitRow(UappGradeVm uappGradeVm)
        {
            if (uappGradeVm.SplitData == null)
                uappGradeVm.SplitData = new List<SplitData>();

            int newGradeId = uappGradeVm.SplitData.Any()
                ? uappGradeVm.SplitData.Max(s => s.GradeId) + 1
                : 1;

            decimal totalExistingRatio = uappGradeVm.SplitData.Sum(s => s.Ratio);
            decimal remainingRatio = 100m - totalExistingRatio;

            if (!uappGradeVm.SplitData.Any())
            {
                remainingRatio = 100m;
            }
            else if (remainingRatio > 0)
            {
                remainingRatio = remainingRatio / 2m;

                var lastRow = uappGradeVm.SplitData.Last();
                lastRow.Ratio -= remainingRatio;
            }
            else
            {
                remainingRatio = 0m;
            }

            uappGradeVm.SplitData.Add(new SplitData
            {
                GradeId = newGradeId,
                Ratio = remainingRatio
            });

            uappGradeVm.LoadUi(_context);

            return View("/Views/UappGrade/Create.cshtml", uappGradeVm);
        }
        [HttpPost]
        public IActionResult Create(UappGradeVm uappGradeVm)
        {
            try
            {
                uappGradeVm.LoadUi(_context);
                var MappedDbObject = uappGradeVm.ToDbObject();
                _context.UAppGrades.Add(MappedDbObject);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                uappGradeVm.Exception = e;
                return View(uappGradeVm);
            }

        }
    }
}
