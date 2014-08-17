$(document).ready(function () {
    reloadReCaptcha();
    reloadReCaptcha2();

    $('#reloadCaptcha').click(function () {
        reloadReCaptcha();
    });
    $('#reloadCaptcha2').click(function () {
        reloadReCaptcha2();
    });
});
function reloadReCaptcha() {
    var newUrl = AjaxCall("../Home/GetRecaptchaImage");
    $('#reCaptchaImage').attr("src", newUrl);
}
function reloadReCaptcha2() {
    var newUrl = AjaxCall("../Home/GetRecaptchaImage2");
    $('#reCaptchaImage2').attr("src", newUrl);
}
function AjaxCall(ajaxUrl) {

    var returnValue = "";
    $.ajax({
        type: "POST", 		//GET or POST or PUT or DELETE verb
        url: ajaxUrl, 		// Location of the service
        data: "", 		//Data sent to server
        contentType: "application/json; charset=utf-8",		// content type sent to server
        dataType: "json", 	//Expected data format from server
        async: false,
        success: function (data) { returnValue = data; },
        error: function (data) { alert('Fail' + data); }	// When Service call fails
    });
    return returnValue;
}