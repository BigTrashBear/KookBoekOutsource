using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kookboek.Models;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;

namespace Kookboek.Web.Controllers
{
    public class CookbookController : Controller
    {
        // new a list of sample recipe model.
        RecipeListModel recipeListModel = new RecipeListModel();


        // Placeholder data, REMOVE WHEN ATTACHED TO DATABASE!!  
        private void FillSampleRecipeListModel()
        {
            // Sample recipes.
            recipeListModel.RecipeList.Add(new RecipeModel() {Id = 1, Name = "Gras", PrepTime = 120, Calories = 210, Ingredients = new List<string>() { "Egg", "Tomato" } , Instructions = "-Warm up your oven, -Mix the flour and egg", Rating = 0, TotalRatings = 0});
            recipeListModel.RecipeList.Add(new RecipeModel() {Id = 2, Name = "Pizza", Instructions = "Lekker als ontbijt", Calories = 1232, Rating = 3.5, TotalRatings = 7});
        }

        // Placeholder data, Add new model here
        private void AddRecipeToRecipeListModel(RecipeModel newRecipe)
        {          
            recipeListModel.RecipeList.Add(newRecipe);
        }
        // Returns the recipe list view.
        public IActionResult RecipeList()
        {
            // Code to enter logic / DAL here.

            // Fills the list of sample recipes.
            FillSampleRecipeListModel(); // Replace this with Database data.

            return View(recipeListModel);
        }
        // Selection between which list of recipes to view.
        public RecipeListModel ChooseRecipeListPartial(int id)
        {
            // Code to enter logic / DAL here.

            // Fills the list of sample recipes.
            FillSampleRecipeListModel(); // Replace this with Database data.

            // Based on selection, a different id is returned to determined which model to show.
            // Currently, they all return the sanme model with the same recipes.
            switch (id)
            {
                case 1:
                    return recipeListModel;
                case 2:
                    return recipeListModel;
                case 3:
                    return recipeListModel;
                case 4:
                    return recipeListModel;
                default:
                    return recipeListModel;
            }
        }

        // Returns a view of the details of selected recipe based on id
        public IActionResult ShowRecipe(int id)
        {
            // Code to enter logic / DAL here.

            // Fills the list of sample recipes.
            FillSampleRecipeListModel(); // Replace this with Database data.

            // Get the model for the recipe to show by id.
            RecipeModel recipeToShow = recipeListModel.RecipeList.Find(x => x.Id == id); // Replace this with Database data.

            return View("RecipeDetails", recipeToShow);
        }

        // Return view for adding new recipe. 
        public IActionResult CreateNewRecipe()
        {
            return View("NewRecipe");
        }

        // Posts the new recipe with given values through model paramater, then returns view of recipe list.
        public IActionResult PostNewRecipe(RecipeModel model)
        {
            if (ModelState.IsValid)
            {
                // Fills the list of sample recipes.
                FillSampleRecipeListModel(); // Replace this with Database data.

                // The id of the new recipe is the total recipes in the list + 1, probably needs to be handled through database. 
                model.Id = recipeListModel.RecipeList.Count + 1;

                 RecipeModel newModel = model;
                 // Call the AddRecipeToListModel method to add the new recipe to the list.
                 AddRecipeToRecipeListModel(newModel);
                 // Give the recipeListModel with new recipe added when returning to the list view.
                 return View("RecipeList", recipeListModel);
            }
            else
            {
                 return View("NewRecipe");
            }
        }


        public string RateRecipe()
        {
            // The Id of rated recipe from client
            int id = Convert.ToInt16(Request.Form["recipeId"]);

            // The given rating from client
            double rating = Convert.ToDouble(Request.Form["ratingValue"]);

            // Fills the list of sample recipes.
            FillSampleRecipeListModel(); // Replace this with Database data.

            // The recipe model that we want to rate based on given id
            RecipeModel recipeToRate = recipeListModel.RecipeList.Find(x => x.Id == id);  // Replace this with Database data.

            // The following can be applied in Logic. This code works but will not be in the sample as the Recipe data isnt actually stored, when it is, it will be visible. 
            // (star rating updates when recipe is rated).

            // A check for when the user has already rated the recipe has to be added here.
            bool userHasRatedRecipe = false;       

            // Check if an Id and rating has been given.
            if (Request.Form.Count > 0 && userHasRatedRecipe == false)
            {              
             
                // The maximum rating
                int maxRating = 5;
                // The percentage of given rating
                int ratingPercentage = Convert.ToInt16((rating / maxRating) * 100);
                // If there is no existing rating...
                if (recipeToRate.TotalRatings == 0)
                {
                    // ... The rating of the recipe is the given rating
                    recipeToRate.Rating = rating;
                }
                else // If there is an existing rating...
                {
                    // ... Caculate the weight of the existing rating
                    double ratingWeight = recipeToRate.Rating * recipeToRate.TotalRatings;
                    // Add one rating to the total amount of ratings to account for the new rating
                    recipeToRate.TotalRatings++;
                    // Calculate the new rating
                    recipeToRate.Rating = (ratingWeight + rating) / recipeToRate.TotalRatings;
                }
                // After succesfully rating the recipe, user isnt allowed to rate anymore.
                userHasRatedRecipe = true;
                return "Successfully rated!";
            }
            // If the user has already rated the recipe...
            else if (Request.Form.Count > 0 && userHasRatedRecipe == true)
            {
                // ... Update the rating instead.
                double ratingWeight = recipeToRate.Rating * recipeToRate.TotalRatings;
                recipeToRate.Rating = (ratingWeight + rating) / recipeToRate.TotalRatings;
                return "Rating updated!";
            }
            else // If there is nothing in the given form.. (Recipe got removed while user on was on the page?)
            {
                return "Something went wrong.";
            }
        }
    }
}