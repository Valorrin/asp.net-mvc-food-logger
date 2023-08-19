$(function () {
    $("#datepicker").datepicker({
        dateFormat: "yy-mm-dd",
        showOtherMonths: true,
        selectOtherMonths: true,
        changeMonth: true,
        changeYear: true,
        maxDate: 0, // Allow only past dates
        onSelect: function (selectedDate) {
            // Update the "Add Food" and "Add Recipe" links with selected date
            var addFoodLink = $("#addFoodLink");
            var addRecipeLink = $("#addRecipeLink");

            var addFoodUrl = addFoodLink.data("url");
            var addRecipeUrl = addRecipeLink.data("url");


            addFoodLink.attr("href", addFoodUrl + "?selectedDate=" + selectedDate);
            addRecipeLink.attr("href", addRecipeUrl + "?selectedDate=" + selectedDate);

            // Fetch and update diary entries using AJAX
            $.get("/Diary/LoadDiary", { selectedDate: selectedDate }, function (data) {
                $(".entry-list-container").html(data);
            });
        }
    });

    // Get the selected date from the URL parameter
    var urlParams = new URLSearchParams(window.location.search);
    var selectedDateFromUrl = urlParams.get("selectedDate");


    // If selectedDateFromUrl is not null, set the datepicker's selected date
    if (selectedDateFromUrl) {

        var decodedDate = decodeURIComponent(selectedDateFromUrl);
        $("#datepicker").datepicker("setDate", new Date(decodedDate));

        // Update the links to use the selected date from the url
        $("#addFoodLink").attr("href", $("#addFoodLink").data("url") + "?selectedDate=" + selectedDateFromUrl);
        $("#addRecipeLink").attr("href", $("#addRecipeLink").data("url") + "?selectedDate=" + selectedDateFromUrl);
    } else {
        // Get the current date
        var today = new Date();
        var formattedToday = $.datepicker.formatDate("yy-mm-dd", today);

        // Automatically select today's date in the datepicker
        $("#datepicker").datepicker("setDate", formattedToday);

        // Update the links to use today's date
        $("#addFoodLink").attr("href", $("#addFoodLink").data("url") + "?selectedDate=" + formattedToday);
        $("#addRecipeLink").attr("href", $("#addRecipeLink").data("url") + "?selectedDate=" + formattedToday);
    }
});