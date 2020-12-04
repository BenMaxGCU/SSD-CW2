
function createComment(userId, ticketId, userEmail) {
    console.log("Success");
    const commentText = document.getElementById("commentText").value;
    
    try{
        $.ajax({
            url: '/Comments/Create/',
            type: 'POST',
            data: { userId: userId, commentText: commentText, ticketId: ticketId },
            success: function (data) {displayComment (data.commentId, data.userId, data.commentText, userEmail);},
            error: function (jqXHR, textStatus, errorThrown) { errorFunction(); }
        });
    }
    catch(err) {
        alert(err.message);
    }
}

function displayComment(commentId, userId, userEmail, commentText){
    if(commentId === null || commentId === undefined){
        alert("Failed to submit");
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
        url: '/Comments/Delete/' + commentId,
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