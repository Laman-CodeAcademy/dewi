using dewi.Contexts;
using dewi.Helpers;
using dewi.Models;
using dewi.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dewi.Areas.Admin.Controllers;
[Area("Admin")]
public class EmployeeController(AppDbContext _context, IWebHostEnvironment _environment) : Controller
{

    private readonly string folderPath = Path.Combine(_environment.WebRootPath, "assets", "img");
    private async Task SendPositionsWithViewBag()
    {
        var positions = await _context.Positions.Select(p => new SelectListItem()
        {
            Text = p.Name,
            Value = p.Id.ToString(),
        }).ToListAsync();
        ViewBag.Positions = positions;
    }
    public async Task<IActionResult> Index()
    {
        var employees = await _context.Employee.Select(employee => new EmployeeGetVM()
        {
            Id = employee.Id,
            Name = employee.Name,
            Description = employee.Description,
            Image = employee.Image,
        }).ToListAsync();

        return View(employees);
    }

    public async Task<IActionResult> Create()
    {
        await SendPositionsWithViewBag();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateVM vm)
    {
        await SendPositionsWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        var existPosition = await _context.Positions.AnyAsync();
        if (!existPosition)
        {
            ModelState.AddModelError("Position", "Position not found");
            return View(vm);
        }
        if (!vm.Image.CheckSize(2))
        {

            ModelState.AddModelError("IMage", "IMage size must be less than 2mb");
            return View(vm);
        }
        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("IMage", "file must be image type");
            return View(vm);
        }

        string uniqueFileName = await vm.Image.FileUploadAsync(folderPath);

        Employee newEmployee = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            Image = uniqueFileName,
            PositionId = vm.PositionId,
        };
        await _context.Employee.AddAsync(newEmployee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.Employee.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        _context.Employee.Remove(employee);
        await _context.SaveChangesAsync();

        string deletedFilePath = Path.Combine(folderPath, employee.Image);
        FileHelper.FileDelete(deletedFilePath);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var employee = await _context.Employee.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        EmployeeUpdateVM vm = new EmployeeUpdateVM()
        {
            Name= employee.Name,
            Description= employee.Description,
            PositionId = employee.PositionId,
            //Image=employee.Image,
        };
        await SendPositionsWithViewBag();
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Update(EmployeeUpdateVM vm)
    {
       await SendPositionsWithViewBag();
        if (!ModelState.IsValid) { 
        return View(vm);
        }
        var existEmployee = await _context.Employee.FindAsync(vm.Id);
        if (existEmployee == null)
        {
            return NotFound();
        }
        var existPosition = await _context.Positions.AnyAsync(x=>x.Id==vm.PositionId);
        if (!existPosition) {
            ModelState.AddModelError("position id", "psotion not found");
            return View(vm);
        }

        if (!vm.Image.CheckSize(2))
        {

            ModelState.AddModelError("IMage", "IMage size must be less than 2mb");
            return View(vm);
        }
        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("IMage", "file must be image type");
            return View(vm);
        }

        existEmployee.Name =  vm.Name;
        existEmployee.Description  = vm.Description;
        existEmployee.PositionId = vm.PositionId;

        if(vm.Image is { })
        {
            string newFolderPath = await vm.Image.FileUploadAsync(folderPath);
            string deletedImagePath = Path.Combine(folderPath, existEmployee.Image);
            FileHelper.FileDelete(deletedImagePath);
        }

        _context.Employee.Update(existEmployee);
        await _context.SaveChangesAsync();


        return RedirectToAction(nameof(Index));

    }
}