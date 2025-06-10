var keepAlive = {
    refresh: function () {
        $.get('/keep-alive.ashx');
        console.log("---------------keep-----------------");
        setTimeout(keepAlive.refresh, 30000);       
    }
}; $(document).ready(keepAlive.refresh());