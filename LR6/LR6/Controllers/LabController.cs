using LabLibrary;
using LR6.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LR6.Controllers
{
    public class LabController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public LabController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Labs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessLab(int labName, IFormFile inputFile)
        {
            if (inputFile == null || inputFile.Length == 0)
                return BadRequest("Please upload a file");

            string[] input;
            using (var reader = new StreamReader(inputFile.OpenReadStream()))
            {
                var fileContent = await reader.ReadToEndAsync();
                input = fileContent.Split(Environment.NewLine);
            }
            string result;

            try
            {
                result = LabRunner.RunLab(labName, input);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

            var output = new { result = result };

            return Json(output);
        }
    }
}