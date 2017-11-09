var userID = "";

$('#UserListBox').on('click', 'a[href = "#"]', function () {

    var val = $(this).attr('UserID');
    $("#user_content").load(urlDetails, { id: val });

    $('#user_content').removeClass().addClass('fadeIn animated').
        one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass();
        });
});

$('#UserListBox').on('click', '.list-group-item', function () {
    userID = $(this).val();
    $('.nav li').removeClass('active');
    $(this).addClass('active');
});

function EditUser() {
    if (!userID == "") {
        window.location.href = urlEditUser.replace('userID', userID);
    };
};

function DeleteUser() {
    $.get(urlDeleteUser, { id: userID }, function () {
        location.reload();
    });
}








