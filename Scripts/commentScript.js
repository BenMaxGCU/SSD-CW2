
function createComment(userId, ticketId, userEmail) {


    console.log("Success");
    const commentText = document.getElementById("commentText").value;


    try {
        $.ajax({
            url: '/Comments/Create/',
            type: 'POST',
            data: { userId: userId, commentText: commentText, ticketId: ticketId },
            success: function (data) { displayComment(data, userId, commentText, userEmail); }, //You're returning a string and not returning an object from the controller so you can't access it like x.whatever
            error: function (jqXHR, textStatus, errorThrown) { }
        });
    }
    catch (err) {
        console.log(err.message);
    }
}

//made this, I can't figure out why createComment isn't getting called, received errors about wrong number of parameters
//there may be a function with the same name in one of the jquery/bootstrap files causing a conflict but this works
function test(userId, ticketId, userEmail) {
    createComment(userId, ticketId, userEmail)
}
function displayComment(commentId, userId, userEmail, commentText) {
    if (commentId === null || commentId === undefined) {
        alert("Failed to submit");
    }

    var commentDiv = document.getElementById("comments");
    var commentDate = Date.now();

    const commentString = '<div class="d-flex flex-row comment-row" id="' + commentId + '">' +
        '<div class="comment-text w-100">' +
        '<h6 class="font-medium">' + userEmail + '</h6> <span class="m-b-15 d-block">' + commentText + '</span>' +
        '   <div class="comment-footer"> <span class="text-muted float-right">' + commentDate + '</span></div> ' +
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