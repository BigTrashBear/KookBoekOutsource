// This javascript is a mess.

// When a button in the selection of recipe list to view is clicked, based on which button (id)...
function ButtonClick(id) {
    // Update the model
    UpdateModel(id);
    // Hide the partial views
    HidePartials();
    // Show the partial view that we want to show based on id and change button color
    switch (id) {
        case 1:
            $('#allrecipesPartial').show();
            $('#allrecipesbtn').css("background-color", "#629c44");
            break;
        case 2:
            $('#myrecipesPartial').show();
            $('#myrecipesbtn').css("background-color", "#629c44");
            break;
        case 3:
            $('#favoriterecipesPartial').show();
            $('#favrecipesbtn').css("background-color", "#629c44");  
            break;
        case 4:
            $('#searchrecipesPartial').show();
            $('#searchrecipesbtn').css("background-color", "#629c44");
            break;
        default:

    }
    // Prevent submit
    return false;
}
// Get model data to show on the table of list based on input (i.e. when My Recipes is selected, return a list of the user's recipes.) Currently only returns the default list.
function UpdateModel(id) {

    var formData = new FormData();

    formData.set('id', id);
    // Pass id to server method ChooseRecipeListPartial
    $.ajax({
        url: '/Cookbook/ChooseRecipeListPartial',
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (output) {
            // Return correct model.
        }
    });
}
// Called on ButtonClick to hide the partial views.
function HidePartials() {
    $('#allrecipesPartial').hide();
    $('#allrecipesbtn').css("background-color", "#81c760");

    $('#myrecipesPartial').hide();
    $('#myrecipesbtn').css("background-color", "#81c760");

    $('#favoriterecipesPartial').hide();
    $('#favrecipesbtn').css("background-color", "#81c760");

    $('#searchrecipesPartial').hide();
    $('#searchrecipesbtn').css("background-color", "#81c760");
}

// Gets the rating of the recipes to show and changes the stars based on the % of the rating.
function CalculateStars(modelId, modelRating) {

    var rating = modelRating;

    console.log(rating);

    const starTotal = 5;

    const starPercentage = (rating / starTotal) * 100;

    const starPercentageRounded = `${(Math.round(starPercentage / 10) * 10)}%`;

    document.getElementById(modelId).style.width = starPercentageRounded;
}