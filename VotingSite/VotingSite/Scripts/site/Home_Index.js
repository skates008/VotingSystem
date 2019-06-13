
function Show() {
    //debugger;
    //var data = @Html.Raw(JsonConvert.SerializeObject(Model));  
    var data = "@Html.Raw(Json.Encode(Model))";
    $.ajax({
        type: "POST",
        // dataType: 'html', // this can be omitted - the ajax() function will work it out  
        cache: false,
        url: "/CN/Show",
        data: data,
        success: function(data, textStatus, jqXHR) {
            console.log("Success! [in Home_Index.js]");
        },
        error: function(jqXHR, textStatus, errorThrown) {
            console.log("Epic Fail! [in Home_Index.js]");
        }
    });
}

function validateForm() {
    var form = document.forms[0];

    $(form).validate({
        showErrors: function (errorMap, errorList) {
            console.log("Your form contains " + this.numberOfInvalids() + " errors, see details below.");
            //this.defaultShowErrors();
        }
    });

    if (!$(form).valid()) {
        console.log("There are validation error's in the model.");

        var errors = $(form).err;

        return false;
    }
    else {
        return true;
    }
}

$(document).ready(function() {
    // 
    // click handler for the #loginSubmit button 
    // 
    $("#loginSubmit").on("click",
        function(event) {

            //event.preventDefault();

            // Validate the form first, if it's ok, THEN submit.
            if (validateForm()) {
                $("#loginForm").submit();
                return true;
            }

            return false;
        });

    // 
    // see if the user hit Enter within either field. If so, act like they
    // clicked on the LOG IN button.
    // 
    $("#LoginId, #LoginPin").on("keypress",
        function(event) {

            var keycode = (event.keyCode ? event.keyCode : event.which);

            if (keycode === 13) {
                // Validate the form first, if it's ok, THEN submit.
                if (validateForm()) {
                    $("#loginForm").submit();
                }
            }
        });

});
