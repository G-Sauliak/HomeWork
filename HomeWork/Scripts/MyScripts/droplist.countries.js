var urlGetCities = "";
var Cities = $('#Cities');
var Countries = $('#countries');

var selectedValue = false;
$("#countries option:selected").each(function () {
    if ($(this).text() === "Select Country") {
        Cities.prop("disabled", true);
        selectedValue = true;
    }
});
Countries.change(function () {
    Cities.prop("disabled", false);
    if (selectedValue) {
        Countries.children()[0].remove();
        selectedValue = false;
    }
    $("#countries option:selected").each(function () {
        var _id = $(this).val();
        $.getJSON(urlGetCities, { id: _id }, function (result) {
            Cities.empty();
            $.each(result, function (i) {
                Cities.append($("<option></option>")
                    .attr("value", result[i].Value)
                    .text(result[i].Text));
            });
        });
    });
});

