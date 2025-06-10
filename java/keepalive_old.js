var kaHttpRequest = false;
var kaOldSessionId = '';
if (typeof kaDebug == 'undefined') {
    var kaDebug = false;
}
if (typeof kaServerPage == 'undefined') {
    var kaServerPage = '../java/getsession.aspx';
}
if (typeof kaInterval == 'undefined') {
    var kaInterval = 10;
}
if (typeof kaOkMessage == 'undefined') {
    var kaOkMessage = '<span style="color: #41930a;">Session alive</span>';
}
if (typeof kaExpiredMessage == 'undefined') {
    var kaExpiredMessage = '<span style="color: #b82c06;">Session expired</span>';
}
if (typeof kaErrorMessage == 'undefined') {
    var kaErrorMessage = '<span style="color: #b82c06;">Session check error</span>';
}
if (typeof kaStatusElementID == 'undefined') {
    var kaStatusElementID = 'sessionstatus';
}

kaAjax('POST', kaServerPage, '', kaStatusElementID);
setInterval("kaAjax('POST', kaServerPage, '', kaStatusElementID)", kaInterval * 5000);

function kaAjax(httpRequestMethod, url, parameters, target) {
    kaHttpRequest = false;
    document.getElementById(target).innerHTML = 'Wait...'
    if (window.XMLHttpRequest) { // For Mozilla, Safari, Opera, IE7+
        kaHttpRequest = new XMLHttpRequest();
        if (kaHttpRequest.overrideMimeType) {
            kaHttpRequest.overrideMimeType('text/html');
            //Change MimeType to match the data type of the server response.
            //Examples: "text/xml", "text/html", "text/plain"
        }
    }
    else if (window.ActiveXObject) { // For IE6
        try {
            kaHttpRequest = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                kaHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e)
      { }
        }
    }
    if (!kaHttpRequest) {
        alert('Giving up :( Cannot create an XMLHTTP instance');
        return false;
    }
    kaHttpRequest.onreadystatechange = function () { updateElement(target); };
    if (httpRequestMethod == 'GET') {
        var ser = Math.round(Math.random() * 1000000); // Anti-caching random number
        kaHttpRequest.open('GET', url + '?' + parameters + '&random=' + ser, true);
        kaHttpRequest.send(null);
    }
    else if (httpRequestMethod == 'POST') {
        kaHttpRequest.open('POST', url, true);
        kaHttpRequest.setRequestHeader('Content-Type',
      'application/x-www-form-urlencoded');
        kaHttpRequest.send(parameters);
    }
    else {
        alert('Sorry, unsupported HTTP method');
    }
}

function updateElement(target) {
    if (kaHttpRequest.readyState == 4) {
        if (kaDebug == true) {
            alert(kaHttpRequest.responseText);
        }
        if (kaHttpRequest.status == 200) {
            if (kaOldSessionId == '') {
                kaOldSessionId = kaHttpRequest.responseText;
            }
            if (kaHttpRequest.responseText == kaOldSessionId) {
                document.getElementById(target).innerHTML = kaOkMessage;
            }
            else {
                document.getElementById(target).innerHTML = kaExpiredMessage;
            }
        }
        else {
            document.getElementById(target).innerHTML = kaErrorMessage;
        }
    }
}
