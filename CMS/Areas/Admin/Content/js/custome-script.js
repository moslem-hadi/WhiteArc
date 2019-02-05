
$(document).ready(function () {

    hideloading();

});

function hideloading() {
    setTimeout(
  function () {
      $(".pageloading").fadeOut("fast");
  }, 1000);
}
//******************!اسکریپت های دست ساز**********************
function deletePageContentRecord(recID) {

    swal({
        title: "از حذف رکورد اطمینان دارید؟",
        text: "تمام اطلاعات مرتبط حذف خواهند شد!",
        type: "error",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonClass: 'btn btn-success',
        confirmButtonClass: 'btn btn-danger',
        confirmButtonText: 'بله حذف شود!',
        cancelButtonText: "انصراف",
        closeOnConfirm: false
    },
    function () {

        $.ajax(
          {
              url: 'page/DeleteConfirmed',
              method: 'POST',
              data: 'id=' + recID,
              dataType: 'JSON',
              success: function (result) {
                  if (result == "success") {
                      swal({
                          title: 'حذف شد!',
                          text: 'رکورد مورد نظر با موفقیت حذف شد',
                          type: 'success',
                          confirmButtonText: 'اوکی'
                      });
                      var grid = $("#grid").data("kendoGrid");
                      grid.dataSource.read();
                  }
                  else
                      swal({
                          title: 'حذف نشد!',
                          text: 'عملیات موفقیت آمیز نبود',
                          type: 'warning',
                          confirmButtonText: 'اوکی'
                      });

              }
          }
          );
    });
};


function deleteDownloadRecord(recID) {

    swal({
        title: "از حذف رکورد اطمینان دارید؟",
        text: "تمام اطلاعات مرتبط حذف خواهند شد!",
        type: "error",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonClass: 'btn btn-success',
        confirmButtonClass: 'btn btn-danger',
        confirmButtonText: 'بله حذف شود!',
        cancelButtonText: "انصراف",
        closeOnConfirm: false
    },
    function () {

        $.ajax(
          {
              url: 'download/DeleteConfirmed',
              method: 'POST',
              data: 'id=' + recID,
              dataType: 'JSON',
              success: function (result) {
                  if (result == "success") {
                      swal({
                          title: 'حذف شد!',
                          text: 'رکورد مورد نظر با موفقیت حذف شد',
                          type: 'success',
                          confirmButtonText: 'اوکی'
                      });
                      var grid = $("#grid").data("kendoGrid");
                      grid.dataSource.read();
                  }
                  else
                      swal({
                          title: 'حذف نشد!',
                          text: 'عملیات موفقیت آمیز نبود',
                          type: 'warning',
                          confirmButtonText: 'اوکی'
                      });

              }
          }
          );
    });
};
$(document).ready(function () {
     
    $('.pageDeleteConfirmation').on('click', function (e, data) {
        if (!data) {
            e.preventDefault(); 
            swal({
                title: "از حذف رکورد اطمینان دارید؟",
                text: "تمام اطلاعات مرتبط حذف خواهند شد!",
                type: "error",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonClass: 'btn btn-success',
                confirmButtonClass: 'btn btn-danger',
                confirmButtonText: 'بله حذف شود!',
                cancelButtonText: "انصراف",
                closeOnConfirm: false
            },
            function (isConfirm) {
                if (isConfirm) {
                    $('.pageDeleteConfirmation').trigger('click', {});
                }
            });
        } else {
            $('form').submit();
        }
    });


}); 



function deletePostRecord(recID) {

    swal({
        title: "از حذف رکورد اطمینان دارید؟",
        text: "تمام اطلاعات مرتبط حذف خواهند شد!",
        type: "error",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonClass: 'btn btn-success',
        confirmButtonClass: 'btn btn-danger',
        confirmButtonText: 'بله حذف شود!',
        cancelButtonText: "انصراف",
        closeOnConfirm: false
    },
    function () {

        $.ajax(
          {
              url: 'post/DeleteConfirmed',
              method: 'POST',
              data: 'id=' + recID,
              dataType: 'JSON',
              success: function (result) {
                  if (result == "success") {
                      swal({
                          title: 'حذف شد!',
                          text: 'رکورد مورد نظر با موفقیت حذف شد',
                          type: 'success',
                          confirmButtonText: 'اوکی'
                      });
                      var grid = $("#grid").data("kendoGrid");
                      grid.dataSource.read();
                  }
                  else
                      swal({
                          title: 'حذف نشد!',
                          text: 'عملیات موفقیت آمیز نبود',
                          type: 'warning',
                          confirmButtonText: 'اوکی'
                      });

              }
          }
          );
    });
};



/*************PostGroup****************************/


function deletePostGroupRecord(recID) {

    swal({
        title: "از حذف رکورد اطمینان دارید؟",
        text: "تمام اطلاعات مرتبط حذف خواهند شد!",
        type: "error",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonClass: 'btn btn-success',
        confirmButtonClass: 'btn btn-danger',
        confirmButtonText: 'بله حذف شود!',
        cancelButtonText: "انصراف",
        closeOnConfirm: false
    },
    function () {
        $.ajax(
          {
              url: '/admin/postgroup/DeleteConfirmed',
              method: 'POST',
              data: 'id=' + recID,
              dataType: 'JSON',
              success: function (result) {
                  if (result == "success") {
                      swal({
                          title: 'حذف شد!',
                          text: 'رکورد مورد نظر با موفقیت حذف شد',
                          type: 'success',
                          confirmButtonText: 'اوکی'
                      });
                     location.reload();
                  }
                  else
                      swal({
                          title: 'حذف نشد!',
                          text: 'عملیات موفقیت آمیز نبود',
                          type: 'warning',
                          confirmButtonText: 'اوکی'
                      });

              }
          }
          );
    });
};






function deleteUserRecord(recID) {
    
    swal({
        title: "از حذف رکورد اطمینان دارید؟",
        text: "تمام اطلاعات مرتبط حذف خواهند شد!",
        type: "error",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonClass: 'btn btn-success',
        confirmButtonClass: 'btn btn-danger',
        confirmButtonText: 'بله حذف شود!',
        cancelButtonText: "انصراف",
        closeOnConfirm: false
    },
    function () {
        $.ajax(
          {
              url: '/admin/user/DeleteConfirmed',
              method: 'POST',
              data: 'id=' + recID,
              dataType: 'JSON',
              success: function (result) {
                  if (result == "success") {
                      swal({
                          title: 'حذف شد!',
                          text: 'رکورد مورد نظر با موفقیت حذف شد',
                          type: 'success',
                          confirmButtonText: 'اوکی'
                      });
                      window.location='/admin/user';
                  }
                  else
                      swal({
                          title: 'حذف نشد!',
                          text: 'عملیات موفقیت آمیز نبود',
                          type: 'warning',
                          confirmButtonText: 'اوکی'
                      });

              }
          }
          );
    });
};





/*************TICKET****************************/


function deleteTicketRecord(recID) {

    swal({
        title: "از حذف رکورد اطمینان دارید؟",
        text: "تمام اطلاعات مرتبط حذف خواهند شد!",
        type: "error",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonClass: 'btn btn-success',
        confirmButtonClass: 'btn btn-danger',
        confirmButtonText: 'بله حذف شود!',
        cancelButtonText: "انصراف",
        closeOnConfirm: false
    },
    function () {

        $.ajax(
          {
              url: 'support/DeleteConfirmed',
              method: 'POST',
              data: 'id=' + recID,
              dataType: 'JSON',
              success: function (result) {
                  if (result == "success") {
                      swal({
                          title: 'حذف شد!',
                          text: 'رکورد مورد نظر با موفقیت حذف شد',
                          type: 'success',
                          confirmButtonText: 'اوکی'
                      });
                      var grid = $("#grid").data("kendoGrid");
                      grid.dataSource.read();
                  }
                  else
                      swal({
                          title: 'حذف نشد!',
                          text: 'عملیات موفقیت آمیز نبود',
                          type: 'warning',
                          confirmButtonText: 'اوکی'
                      });

              }
          }
          );
    });
};






/**********************Product***********************/

        function deleteProductRecord(recID) {

            swal({
                title: "از حذف رکورد اطمینان دارید؟",
                text: "تمام اطلاعات مرتبط حذف خواهند شد!",
                type: "error",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonClass: 'btn btn-success',
                confirmButtonClass: 'btn btn-danger',
                confirmButtonText: 'بله حذف شود!',
                cancelButtonText: "انصراف",
                closeOnConfirm: false
            },
            function () {

                $.ajax(
                  {
                      url: 'product/DeleteConfirmed',
                      method: 'POST',
                      data: 'id=' + recID,
                      dataType: 'JSON',
                      success: function (result) {
                          if (result == "success") {
                              swal({
                                  title: 'حذف شد!',
                                  text: 'رکورد مورد نظر با موفقیت حذف شد',
                                  type: 'success',
                                  confirmButtonText: 'اوکی'
                              });
                              var grid = $("#grid").data("kendoGrid");
                              grid.dataSource.read();
                          }
                          else
                              swal({
                                  title: 'حذف نشد!',
                                  text: 'عملیات موفقیت آمیز نبود',
                                  type: 'warning',
                                  confirmButtonText: 'اوکی'
                              });

                      }
                  }
                  );
            });
        };





        function deleteCategoryRecord(recID) {

            swal({
                title: "از حذف رکورد اطمینان دارید؟",
                text: "تمام اطلاعات مرتبط حذف خواهند شد!",
                type: "error",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonClass: 'btn btn-success',
                confirmButtonClass: 'btn btn-danger',
                confirmButtonText: 'بله حذف شود!',
                cancelButtonText: "انصراف",
                closeOnConfirm: false
            },
            function () {

                $.ajax(
                  {
                      url: 'category/DeleteConfirmed',
                      method: 'POST',
                      data: 'id=' + recID,
                      dataType: 'JSON',
                      success: function (result) {
                          if (result == "success") {
                              swal({
                                  title: 'حذف شد!',
                                  text: 'رکورد مورد نظر با موفقیت حذف شد',
                                  type: 'success',
                                  confirmButtonText: 'اوکی'
                              });

                              window.location = "/admin/category?langID=" + $("#LanguageList").value;
                          }
                          else
                              swal({
                                  title: 'حذف نشد!',
                                  text: 'عملیات موفقیت آمیز نبود',
                                  type: 'warning',
                                  confirmButtonText: 'اوکی'
                              });

                      }
                  }
                  );
            });
        };




//*****************Sticky*********************
        function sticky_relocate() {
            var window_top = $(window).scrollTop();
            if ($("#sticky-anchor").length) {
            var div_top = $('#sticky-anchor').offset().top - 60;

            var contentpageMarginRight = parseInt($(".content-page").css("marginRight"));
            if (window_top > div_top) {
                $('#sticky-anchor').height(  $('#sticky').outerHeight());
                $('#sticky').addClass('stick');
                $("#sticky").css({ 'right': contentpageMarginRight  });
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

$(".OpenPopUp").on("click", function (event) {
    event.preventDefault();
    var link = '/admin/filemanager?chooser=1';
    var folder = $(this).data('folder');
    var element = $(this).data('element');
    link = link + '&folder=' + folder + '&element=' + element;

    PopupCenter(link, "انتخاب کنید", 800, 500)
});



});//ready

function PopupCenter(url, title, w, h) {
    // Fixes dual-screen position                         Most browsers      Firefox
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (w / 2)) + dualScreenLeft;
    var top = ((height / 2) - (h / 2)) + dualScreenTop;
    var newWindow = window.open(url, title, 'toolbar=no,scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

    // Puts focus on the newWindow
    if (window.focus) {
        newWindow.focus();
    }
}
function setUrlToElement(el, txt) {
    $("#" + el).val(txt);
}







function setUrl(t, elementID) {
    var urlInput = document.getElementById(elementID);
    if (urlInput.value == '')
        urlInput.value = t.value.replace(/\s/g, '-');
}


function nospaces(t) {
    if (t.value.match(/\s/g)) {
        t.value = t.value.replace(/\s/g, '-');
    }
}





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


function copyTextToClipboard(pretext, txtbx) {
    var textArea = document.createElement("textarea");
    textArea.style.position = 'fixed';
    textArea.style.top = 0;
    textArea.style.left = 0;
    textArea.style.width = '2em';
    textArea.style.height = '2em';
    textArea.style.padding = 0;
    textArea.style.border = 'none';
    textArea.style.outline = 'none';
    textArea.style.boxShadow = 'none';
    textArea.style.background = 'transparent';
    textArea.value = pretext + document.getElementById(txtbx).value;

    document.body.appendChild(textArea);

    textArea.select();

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        console.log('Copying text command was ' + msg);
    } catch (err) {
        console.log('Oops, unable to copy');
    }

    document.body.removeChild(textArea);
}



