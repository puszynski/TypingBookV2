// allow to use bootsrap tooltips - throw console error on some pages..
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

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
            var decreasedValue = parseInt($("#correctTyped").text(), 10) + 1;
            $('#correctTyped').html(decreasedValue);

            updateTotalMilliseconds();

            updateBookPageStatusBar(pageLength);

            //document.body.style.backgroundColor = "dimgray";
            document.getElementById('typed_content').innerHTML += book_content.charAt(0);
            document.getElementById('book_content').innerHTML = book_content.substr(1);


            if (document.getElementById('book_content').innerHTML === '') {
                //when book pages end
                var bookPages = bookPagesJson;
                var nextPage = ++currentBookPage;

                displaySummaryAlert();
                saveTypingResult(bookId, nextPage);

                $('.progress-bar-correct').css({ 'width': '0%' });
                $('.progress-bar-wrong').css({ 'width': '0%' });
                $('#correctTyped').html('0');
                $('#wrongTyped').html('0');

                if (bookPages.length <= nextPage) {                    
                        window.location.href = 'book'; //redirect when book ends
                }
                else {
                    document.getElementById('typed_content').innerHTML = '';
                    document.getElementById('book_content').innerHTML = bookPages[nextPage];
                }
            }
        }
        else {
            var increasedValue = parseInt($("#wrongTyped").text(), 10) + 1;
            $('#wrongTyped').html(increasedValue);

            //document.body.style.backgroundColor = "white";
            updateBookPageStatusBar(pageLength);
        }
    };
}


function updateTotalMilliseconds() {
    var startTimeMilliseconds = document.getElementById('startTime').innerHTML;
    var endTimeMilliseconds = new Date().getTime();
    var milliseconds = parseInt(endTimeMilliseconds) - parseInt(startTimeMilliseconds);
    var total = parseInt(document.getElementById('totalMilliseconds').innerHTML);

    var limitMilliseconds = 2000;

    if (milliseconds > limitMilliseconds ) {
        milliseconds = limitMilliseconds;
    }

    document.getElementById('totalMilliseconds').innerHTML = total + milliseconds;
    document.getElementById('startTime').innerHTML = endTimeMilliseconds;
}

function setStartTime() {
    var start = new Date().getTime();
    document.getElementById("startTime").innerHTML = start;
}


function isSameChar(typedCharCode, charToType) {
    if (typedCharCode == charToType) {
        return true;
    }
    else if (charToType > 8200) { ignore 
        return true;
    }
    else
        return false;
}

function updateBookPageStatusBar(pageLength) {
    var correctTyped = parseInt($("#correctTyped").text(), 10);
    var wrongTyped = parseInt($("#wrongTyped").text(), 10);

    var correctPercent = correctTyped / (pageLength + wrongTyped) * 100;
    var wrongPercent = wrongTyped / (pageLength + wrongTyped) * 100;

    $('#progress-bar-correct').css({ 'width': correctPercent + '%' });
    $('#progress-bar-wrong').css({ 'width': wrongPercent + '%' });
}

function saveTypingResult(bookId, nextBookPage) {
    var url = '/Typing/SaveTypingResult';

    var correctTyped = parseInt($("#correctTyped").text(), 10);
    var wrongTyped = parseInt($("#wrongTyped").text(), 10);

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
        //    console.log("Data has been sended successfully to '/Typing/SaveTypingResult'.");
        //},
        error: function () {
           console.log("Error while calling the /Typing/SaveTypingResult from site.js, js function: saveTypingResult()");
        }
    });

    saveStatistics();
}

function displaySummaryAlert() {
    //TODO create new alert and add info about correct typed/wrong + time in seconds
    var timeIcon = "<svg width=\"1em\" height=\"1em\" viewBox=\"0 0 16 1\6\" class=\"bi bi - stopwatch\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\"> <path fill-rule=\"evenodd\" d=\"M6 .5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1H9v1.07A7.001 7.001 0 0 1 8 16 7 7 0 0 1 7 2.07V1h-.5A.5.5 0 0 1 6 .5zM8 3a6 6 0 1 0 .001 12A6 6 0 0 0 8 3zm0 2.1a.5.5 0 0 1 .5.5V9a.5.5 0 0 1-.5.5H4.5a.5.5 0 0 1 0-1h3V5.6a.5.5 0 0 1 .5-.5z\" /></svg >";
    var correctIcon = "<svg width=\"1em\" height=\"1em\" viewBox=\"0 0 16 16\" class=\"bi bi - check\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\"> <path fill-rule=\"evenodd\" d=\"M10.97 4.97a.75.75 0 0 1 1.071 1.05l-3.992 4.99a.75.75 0 0 1-1.08.02L4.324 8.384a.75.75 0 1 1 1.06-1.06l2.094 2.093 3.473-4.425a.236.236 0 0 1 .02-.022z\" /></svg >";
    var wrongIcon = "<svg width=\"1em\" height=\"1em\" viewBox=\"0 0 16 16\" class=\"bi bi - x\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\"> <path fill-rule=\"evenodd\" d=\"M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z\" /></svg >";

    var correctTyped = parseInt($("#correctTyped").text(), 10);
    var wrongTyped = parseInt($("#wrongTyped").text(), 10);

    var typedTimeInMiliseconds = parseInt($("#totalMilliseconds").text(), 10);
    var typedTimeInSeconds = typedTimeInMiliseconds / 1000;

    var message = timeIcon + typedTimeInSeconds + correctIcon + correctTyped + wrongIcon + wrongTyped;

    var element = document.getElementById("alterPlaceholder");
    element.innerHTML = message;

    //unhide
    $('#alterPlaceholder').fadeIn('fast');
    
    //hide
    var timeInMiliseconds = 3000;
    setTimeout(function () {
        $('#alterPlaceholder').fadeOut('slow');
    }, timeInMiliseconds);
}

function saveStatistics() {
    var correctTyped = parseInt($("#correctTyped").text(), 10);
    var wrongTyped = parseInt($("#wrongTyped").text(), 10);

    var time = $("#totalMilliseconds").text();

    $.ajax({
        url: '/Statistic/SaveData',
        data: {
            typedCorrect: correctTyped,
            typedWrong: wrongTyped,
            millisecondsOfTyping: time
        },
        type: 'POST',
        datatype: 'json',
        error: function () {
            console.log("Error while calling the /Statistics/SaveData from site.js, js function: saveStatistics()");
        }
    });
}

function isCapslock(e) {
    const IS_MAC = /Mac/.test(navigator.platform);

    const charCode = e.charCode;
    const shiftKey = e.shiftKey;

    if (charCode >= 97 && charCode <= 122) {
        capsLock = shiftKey;
    } else if (charCode >= 65 && charCode <= 90
        && !(shiftKey && IS_MAC)) {
        capsLock = !shiftKey;
    }

    return capsLock;
}
