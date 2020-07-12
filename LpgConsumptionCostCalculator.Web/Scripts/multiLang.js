var MultiLanguageDemo = MultiLanguageDemo || {};

MultiLanguageDemo.createNameSpace = function (namespace) {

    //get the namespace string and split it
    var namespaceArr = namespace.split('.');
    var parent = MultiLanguageDemo;

    // excluding the parent
    if (namespaceArr[0] === 'MultiLanguageDemo') {
        namespaceArr = namespaceArr.slice(1);
    }

    // loop through the array and create a nested namespace 
    for (var i = 0; i < namespaceArr.length; i++) {

        var partname = namespaceArr[i];

        // check if the current parent already has the namespace declared
        // if it isn't, then create it
        if (typeof parent[partname] === 'undefined') {
            parent[partname] = {};
        }

        // get a reference to the deepest element in the hierarchy so far
        parent = parent[partname];
    }
    //  empty namespaces created and can be used.
    return parent;
};

MultiLanguageDemo.createNameSpace("MultiLanguageDemo.Cookies");

MultiLanguageDemo.Cookies.getCookie = function (ck_name) {
    var ck_value = document.cookie;
    var ck_start = ck_value.indexOf(" " + ck_name + "=");
    if (ck_start == -1) {
        ck_start = ck_value.indexOf(ck_name + "=");
    }
    if (ck_start == -1) {
        ck_value = null;
    } else {
        ck_start = ck_value.indexOf("=", ck_start) + 1;
        var ck_end = ck_value.indexOf(";", ck_start);
        if (ck_end == -1) {
            ck_end = ck_value.length;
        }
        ck_value = unescape(ck_value.substring(ck_start, ck_end));
    }
    return ck_value;
};

MultiLanguageDemo.Cookies.setCookie = function (ck_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var ck_value = escape(value) + ((exdays == null) ? "" : "; path=/;expires=" + exdate.toUTCString());
    document.cookie = ck_name + "=" + ck_value;
};