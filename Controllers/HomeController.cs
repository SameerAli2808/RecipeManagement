using RecipeManagement.Models;
using RecipeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManagement.Controllers
{
    [Route("[controller]/[action]")]
    //[Route("[controller]")] // option 1
    public class HomeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IRecipeRepository recipeRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _recipeRepository = recipeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        //[Route("")] // Whatever the URL this action will be routed
        //[Route("[action]")] // option 1
        //[Route("~/Home")] // To avaoid 404 if no attribute had been entered 
        public ViewResult Index()
        {
            var model = _recipeRepository.GetAllRecipes();
            return View(model);
        }

        [Route("{id}")]
        //[Route("[action]/{id}")] // option 1
        public ViewResult Details(int id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Recipe = _recipeRepository.GetRecipe(id),
                PageTitle = "Recipe Details",
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            Recipe recipe = _recipeRepository.GetRecipe(Id);
            RecipeEditViewModel recipeEditViewModel = new RecipeEditViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Source = recipe.Source,
                Ingredient = recipe.Ingredient,
                Quantity = recipe.Quantity,
                Time = recipe.Time,
                Preperation = recipe.Preperation,
                ExistingPhotoPath = recipe.PhotoPath
            };
            return View(recipeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(RecipeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Recipe recipe = _recipeRepository.GetRecipe(model.Id);
                recipe.Name = model.Name;
                recipe.Source = model.Source;
                recipe.Ingredient = model.Ingredient;
                recipe.Quantity = model.Quantity;
                recipe.Time = model.Time;
                recipe.Preperation = model.Preperation;

                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    recipe.PhotoPath = ProcessUploadedFile(model);
                }

                Recipe updatedRecipe = _recipeRepository.Update(recipe);

                return RedirectToAction("index");
            }

            return View(model);
        }

        private string ProcessUploadedFile(RecipeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (IFormFile photo in model.Photos)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(RecipeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                Recipe newRecipe = new Recipe
                {
                    Name = model.Name,
                    Source = model.Source,
                    Ingredient = model.Ingredient,
                    Quantity = model.Quantity,
                    Time = model.Time,
                    Preperation = model.Preperation,
                    PhotoPath = uniqueFileName
                };

                _recipeRepository.Add(newRecipe);
                return RedirectToAction("details", new { id = newRecipe.Id });
            }

            return View();
        }

    }
}
