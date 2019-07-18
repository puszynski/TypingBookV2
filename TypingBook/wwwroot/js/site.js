﻿

// create ajax links from, e.g.:  <a class="nav-link text-dark" asp-action="Index" asp-controller="Home" asp-route-id="123" data - target="body-container" id = "ajax_link" >Type</a >
function ajaxLink() {
    $("#ajax_link").click(function (e) {

        e.preventDefault(); 
        
        var myLink = $(this);

        $.get(myLink.attr("hreaf"), function (res) {
            $('#' + myLink.data("target")).html(res);
        });
    });
}


function typingBook(currentBookPage, bookPagesJson, bookId) {
    document.onkeypress = function (e) {
        e = e || window.event;

        var book_content = document.getElementById('book_content').textContent;
        pageLength = bookPagesJson[currentBookPage].length;

        //only for dev porpuse:
        $('.typedCode').html(e.keyCode);
        $('.codeToType').html(book_content.charCodeAt(0));

        if (isSameChar(e.which, book_content.charCodeAt(0))) {
            var decreasedValue = parseInt($(".correctTyped").text(), 10) + 1;
            $('.correctTyped').html(decreasedValue);

            updateBookPageStatusBar(pageLength);

            //document.body.style.backgroundColor = "dimgray";
            document.getElementById('typed_content').innerHTML += book_content.charAt(0);
            document.getElementById('book_content').innerHTML = book_content.substr(1);


            if (document.getElementById('book_content').innerHTML === '') {
                //when book pages end
                var bookPages = bookPagesJson;
                var nextPage = ++currentBookPage;

                saveTypingResult(bookId, nextPage);

                $('.progress-bar-correct').css({ 'width': '0%' });
                $('.progress-bar-wrong').css({ 'width': '0%' });
                $('.correctTyped').html('0');
                $('.wrongTyped').html('0');

                if (bookPages.length <= nextPage) {                    
                        window.location.href = '@Url.Action("Index", "Book")'; 
                }
                else {
                    document.getElementById('typed_content').innerHTML = '';
                    document.getElementById('book_content').innerHTML = bookPages[nextPage];
                }
            }
        }
        else {
            var increasedValue = parseInt($(".wrongTyped").text(), 10) + 1;
            $('.wrongTyped').html(increasedValue);

            //document.body.style.backgroundColor = "white";
            updateBookPageStatusBar(pageLength);
        }
    };
}

function isSameChar(typedCharCode, charToType) {
    if (typedCharCode == charToType) {
        return true;
    }
    else if (charToType > 8200 /*charToType == 8217 || charToType == 8211 || charToType == 8220 || charToType = 8221*/) {
        return true;
    }
    else
        return false;
}


function updateBookPageStatusBar(pageLength) {
    var correctTyped = parseInt($(".correctTyped").text(), 10);
    var wrongTyped = parseInt($(".wrongTyped").text(), 10);

    var correctPercent = correctTyped / (pageLength + wrongTyped) * 100;
    var wrongPercent = wrongTyped / (pageLength + wrongTyped) * 100;

    $('.progress-bar-correct').css({ 'width': correctPercent + '%' });
    $('.progress-bar-wrong').css({ 'width': wrongPercent + '%' });
}




function saveTypingResult(bookId, nextBookPage) {
    var url = '/Typing/SaveTypingResult';

    var correctTyped = parseInt($(".correctTyped").text(), 10);
    var wrongTyped = parseInt($(".wrongTyped").text(), 10);

    $.ajax({
        url: url,
        data: {
            bookId: bookId,
            nextBookPage: nextBookPage,
            correctTyped: correctTyped,
            wrongTyped: wrongTyped
        },                              // Send as object => https://stackoverflow.com/questions/1068189/post-an-object-as-data-using-jquery-ajax
        type: 'POST',
        datatype: 'json',
        //success: function () {
        //    console.log("Data has been sended successfully.");  
        //},
        error: function () {
           console.log("Error while calling the /Typing/SaveTypingResult from site.js, function: saveTypingResult()");
        }
    });
}
