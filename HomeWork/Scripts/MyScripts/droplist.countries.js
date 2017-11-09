var urlGetCities = "";
var CitiesDropDown = $('#Cities');
var CountriesDropDown = $('#countries');

var selectedValue = false;
$("#Cities option:selected").each(function () {
    if ($(this).text() === "Select City") {
        CitiesDropDown.prop("disabled", true);
        selectedValue = true;
    }
});
CountriesDropDown.change(function () {
    CitiesDropDown.prop("disabled", false);
    if (selectedValue) {
        CountriesDropDown.children()[0].remove();
        selectedValue = false;
    }
    $("#countries option:selected").each(function () {
        var _id = $(this).val();
        $.getJSON(urlGetCities, { id: _id }, function (result) {
            CitiesDropDown.empty();
            $.each(result, function (i) {
                CitiesDropDown.append($("<option></option>")
                    .attr("value", result[i].Value)
                    .text(result[i].Text));
            });
        });
    });
});