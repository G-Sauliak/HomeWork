var userID = "";

$('#UserListBox').on('click', 'a[href = "#"]', function () {
    var val = $(this).attr('UserID');
    $("#user_content").load(urlDetails, { id: val });
});

$('#UserListBox').on('click', '.list-group-item', function () {
    userID = $(this).val();
    $('.nav li').removeClass('active');
    $(this).addClass('active');
});
function EditUser() {
    window.location.href = urlEditUser.replace('userID', userID);
}
function DeleteUser() {
    $.get(urlDeleteUser, { id: userID }, function () {
        location.reload();
    });
}








