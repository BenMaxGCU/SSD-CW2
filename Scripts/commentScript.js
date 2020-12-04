
function createComment(userId, ticketId, staffEmail) {
    console.log("Success");
    const commentText = document.getElementById("commentText").value;

    let ajaxCall;

    ajaxCall = $.ajax({
        url: 'Comment/Create',
        type: 'POST',
        data: { userId: userId, commentText: commentText, ticketId: ticketId }
    }).done(function (data) {

    });

}

function displayComment(commentId, userId, userEmail, commentText){
    if(commentId === null || commentId === undefined){
        displayError("Failed to submit");
    }
    
    var commentDiv = document.getElementById("comments");
    var commentDate = Date.now();

    const commentString = '<div class="d-flex flex-row comment-row" id="' + commentId +'">' +
        '<div class="comment-text w-100">' +
        '<h6 class="font-medium">' + userEmail + '</h6> <span class="m-b-15 d-block">' + commentText + '</span>' +
        '   <div class="comment-footer"> <span class="text-muted float-right">'+ commentDate +'</span></div> ' +
        ' </div></div> ';
    
    commentDiv.append(commentString);
}

function deleteComment(commentId) {
    $.ajax({
        url: 'Comment/Delete' + commentId,
        type: 'POST',
        data: { commentId: commentId },
        success: removeComment(commentId)
    });
}

function removeComment(commentId) {
    if (commentId) {
        document.getElementById(commentId).remove();
    }
}