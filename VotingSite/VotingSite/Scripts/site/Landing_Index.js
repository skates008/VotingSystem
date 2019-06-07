
// just examples for later use.. for now.
//$("#reviewBallot").on('click',
//    function (event) {
//        alert("Review your ballot?!");
//    });

//$("#startVoting").on('click',
//    function(event) {
//        alert("Start Voting!");
//    });

$(document).ready(function() {

    $("a[id^='ContestItem_'][id$='_Id']").on("click",
        function() {

            var contestIdNum = $(this).attr("id").match(/[\d]+/g, '');
            var data = { "contestId": Number(contestIdNum) };

            //console.log(
            //    "a[id^='ContestItem_'][id$='_Id']).on('click', ...); " + contestIdNum);
            //alert("a[id^='ContestItem_'][id$='_Id']).on('click', ...); contestIdNum: " + contestIdNum);

            // Call: [/Landing/] SelectContest(int contestId)
            //var existingGetUrl = "Landing/SelectContest/" + contestId + "/";
            var existingGetUrl = "/Landing/SelectContest/";

            // make the ajax call
            $.ajax({
                url: existingGetUrl,
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                //cache: false,
                data: JSON.stringify(data),
                success:
                    function(results) {
                        alert(results);
                    },
                error:
                    function( jqXhr, textStatus, errorThrown) {
                        alert("Error occured: jqXHR: /" + jqXhr + 
                            "/, textStatus: /" + textStatus + "/, errorThrown: /" + errorThrown + "/");
                    }
            });
        });

    // 
    // SUBMIT handler for #submitDetails button 
    // 
    $("#submitDetails").on("keypress click", function (event) {

        if (event.which === 13 || event.type === "click") {
            // Validate the form first, if it's ok, THEN submit.
            if (validateForm()) {
                $("#detailsForm").submit();
                return true;
            }
        }

        return false;
    });

});
