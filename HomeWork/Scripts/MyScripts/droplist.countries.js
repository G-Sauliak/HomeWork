var urlGetCities = "";
var Cities = $('#Cities');
var Countries = $('#countries');
$("#countries option:selected").each(function () {
    if ($(this).text() === "Select Country") {
        Cities.prop("disabled", true);
    }
});
Countries.change(function () {
    Cities.prop("disabled", false);
    if (Countries.children()[0].textContent === "Select Country") {
        Countries.children()[0].remove();
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

