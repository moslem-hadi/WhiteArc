
$(document).ready(function () {

    hideloading();

});

function hideloading() {
    setTimeout(
  function () {
      $(".pageloading").fadeOut("fast");
  }, 1000);
}



//*****************Sticky*********************
function sticky_relocate() {
    var window_top = $(window).scrollTop();
    if ($("#sticky-anchor").length) {
        var div_top = $('#sticky-anchor').offset().top - 60;

        var contentpageMarginRight = parseInt($(".content-page").css("marginRight"));
        if (window_top > div_top) {
            $('#sticky-anchor').height($('#sticky').outerHeight());
            $('#sticky').addClass('stick');
            $("#sticky").css({ 'right': contentpageMarginRight });
            $("#sticky").css({ 'padding-right': 30 });

        } else {
            $('#sticky').removeClass('stick');
            $('#sticky-anchor').height(0);
            $("#sticky").css({ 'padding-right': 11 });
        }
    }
}

$(function () {
    $(window).scroll(sticky_relocate);
    sticky_relocate();
     


    $('.counter').maxlength({
        alwaysShow: true,
        allowOverMax: false,
        threshold: 0,
        warningClass: "label label-info",
        limitReachedClass: "label label-warning",
        placement: 'bottom',
        preText: '',
        separator: ' / ',
        postText: ''
    });

});

//**************************************






//****************clock******************
function clock() {
    var today = new Date();
    var hours = today.getHours();
    var minutes = today.getMinutes();
    var seconds = today.getSeconds();
    var time_holder; // holds the time

    //if the first radio button is checked display 12-hours format time

    // add a leading zero if less than 10
    hours = ((hours < 10) ? "0" + hours : hours);
    minutes = ((minutes < 10) ? "0" + minutes : minutes);
    seconds = ((seconds < 10) ? "0" + seconds : seconds);

    time_holder = hours + ":" + minutes + ":" + seconds;

    document.getElementById('jsClock').innerHTML = time_holder;
     

    // keep the clock ticking
    setTimeout("clock()", 1000);
}





function readTicket(id) {
    $.ajax({
        url: '/support/MarkAsRead/' + id,
        dataType: "Post",
        contentType: "application/json; charset=utf-8"
    });
}




function readTicketCount(id) {
    $.ajax({
        url: '/support/GetUnreadTickets',
        dataType: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Success) { // this sets the value from the response
                if (result.Result != 0)
                    $('#spnticketcount').html(result.Result);
            } else {
                $('#spnticketcount').html("");
            }
        }
    });
}






(function ($) {

    'use strict';

    function initNavbar() {

        $('.navbar-toggle').on('click', function (event) {
            $(this).toggleClass('open');
            $('#navigation').slideToggle(400);
        });

        $('.navigation-menu>li').slice(-1).addClass('last-elements');

        $('.navigation-menu li.has-submenu a[href="#"]').on('click', function (e) {
            if ($(window).width() < 992) {
                e.preventDefault();
                $(this).parent('li').toggleClass('open').find('.submenu:first').toggleClass('open');
            }
        });
    }

    function init() {
        initNavbar();
    }

    init();

})(jQuery);

$('.slimscroll-noti').slimScroll({
    height: '230px',
    position: 'right',
    size: "5px",
    color: '#98a6ad',
    wheelStep: 10
});
 




function addCommas(nStr) {
    x = ('' + nStr).replace(/,/g, '');
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x)) {
        x = x.replace(rgx, '$1' + ',' + '$2');
    }
    return x;
}

function formatNumber() {
    val = $(this).val();
    $(this).val(addCommas(val));
}

function convertNumberE2P(StrWithNumber) {
    StrWithNumber = String(StrWithNumber);
    var fn = ["۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹"];
    var en = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
    var StrWithNumber2;

    do {
        StrWithNumber2 = StrWithNumber;
        for (var i = 0; i < 10; i++) {
            StrWithNumber = StrWithNumber.replace(en[i], fn[i]);
        }

        if (StrWithNumber2 === StrWithNumber) {
            break;
        }
    } while (true);
    return StrWithNumber;
}


function ConvertNumberP2E(StrWithNumber) {
    var fn = ["۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹"];
    var fn2 = ["٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩"];
    var en = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];

    do {
        StrWithNumber2 = StrWithNumber;
        for (var i = 0; i < 10; i++) {
            StrWithNumber = StrWithNumber.replace(fn[i], en[i]);
            StrWithNumber = StrWithNumber.replace(fn2[i], en[i]);
        }

        if (StrWithNumber2 === StrWithNumber) {
            break;
        }
    } while (true);
    return StrWithNumber;
}


function UnmaskAmount(Amount) {
    a = '';
    while (true) {
        a = Amount.replace(',', '');
        if (a == Amount) {
            break;
        }
        Amount = a;

    }
    return a;
}
