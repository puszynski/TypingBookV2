// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ajaxMainMenu() {
    $("#Books_button").click(function () {
        $(".body-content").load(
            "/Book/Index"//, { model data goes here..}
        );
    });

    $("#Privacy_button").click(function () {
        $(".body-container").load(
            "/Home/Privacy"
        );
    });

    $("#TypingBook_button").click(function () {
        $(".body-container").load(
            "/Home/Index"
        );
    });
}

function ajaxLink() {
    $("#ajax_link").click(function (e) {

        e.preventDefault(); 
        
        var myLink = $(this);

        $.get(myLink.attr("hreaf"), function (res) {
            $('#' + myLink.data("target")).html(res);
        });
    });
}

// alternative of using Ajax links - data is get from anchor tag helpers https://stackoverflow.com/questions/39193604/ajax-actionlink-alternative-with-mvc-core
//$(function () {
//    $("#ajax-link").click(function (e) {

//        e.preventDefault();
//        var _this = $(this);
//        $.get(_this.attr("href"), function (res) {
//            $('#' + _this.data("target")).html(res);
//        });
//    });
//});


function typingBook(currentBookPage, bookPagesJson, isIntroduction) {
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

            document.body.style.backgroundColor = "dimgray";
            document.getElementById('typed_content').innerHTML += book_content.charAt(0);
            document.getElementById('book_content').innerHTML = book_content.substr(1);


            if (document.getElementById('book_content').innerHTML === '') {
                //when book pages end
                var bookPages = bookPagesJson;
                var nextPage = ++currentBookPage;

                saveBookPageProgress();
                saveStatisticsProgress();

                $('.progress-bar-correct').css({ 'width': '0%' });
                $('.progress-bar-wrong').css({ 'width': '0%' });
                $('.correctTyped').html('0');
                $('.wrongTyped').html('0');

                if (bookPages.length <= nextPage) {
                    if (isIntroduction === 1) {
                        window.location.href = '?bookID=2&bookPage=0';
                    }
                    else {
                        window.location.href = '@Url.Action("Index", "Book")';
                        //redirectToAction();
                    }
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

            document.body.style.backgroundColor = "white";
            updateBookPageStatusBar(pageLength);
        }
    };
}

function isSameChar(typedCharCode, charToType) {
    if (typedCharCode == charToType) { // TO NIE DZIAŁA :/ PRZEPUSZCZA WSZYTSKO!
        return true;
    }
    else if (charToType == 8217 || charToType == 8211 || charToType == 8220) {
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

function saveBookPageProgress() {
    var url = '/Statistics/SaveBookPageProgress';

    $.ajax({
        url: url,
        data: { // TODO
            input: "razdwatrzy"
        },
        type: 'GET',
        datatype: 'json'
    });
}


function saveStatisticsProgress() {
    var url = '/Statistics/SaveStatisticProgress';

    var correctTyped = parseInt($(".correctTyped").text(), 10);
    var wrongTyped = parseInt($(".wrongTyped").text(), 10);

    $.ajax({
        url: url,
        data: { // TODO
            correctTyped: correctTyped,
            wrongTyped: wrongTyped
        },
        type: 'GET',
        datatype: 'json'
        //success: function () {
        //    alert("Data has been added successfully.");  
        //    LoadData();
        //},
        //error: function () {
        //    alert("Error while inserting data");
        //}
    });
}
