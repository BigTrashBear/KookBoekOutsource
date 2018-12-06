function addIngredient() {
    try {
        // Gets the value from the ingredient input field by id ingredientInputField
        var input = document.getElementById("ingredientInputField").value;
        // Gets element by id ingredientListbox
        var x = document.getElementById("ingredientListbox");
        // Creates element 
        var option = document.createElement("option");
        option.text = input;
        x.add(option);
        document.getElementById('ingredientInputField').value = ''
    } catch (e) {
        message.innerHTML = "Something went wrong!";
    }
    
}

function removeSelectedIngredient() {
    try {
        // Gets element by id ingredientListbox
        var x = document.getElementById("ingredientListbox");
        // Removes selected index
        x.remove(x.selectedIndex);
    } catch (e) {
        message.innerHTML = "Something went wrong!";
    }
    
}

function readURL(input) {
    try {
        // Checks input
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                // Sets values to image
                $('#image')
                    .attr('src', e.target.result)
                    .width(256)
                    .height(144);
            };

            reader.readAsDataURL(input.files[0]);
        }
    } catch (e) {
        message.innerHTML = "Something went wrong!";
    }
    
}

$(document).ready(function () {

    $("#newrecipeform").submit(function (e) {

        $("#ingredientListbox option").prop("selected", true);

    });

});
//function validation() {
//    var message = "";
//    Validation.RequireField("Instructions", "Ingredient name is required!");
//    Validation.Add("Instructions", Validator.StringLength(5));

//    if () {

//    }
//}