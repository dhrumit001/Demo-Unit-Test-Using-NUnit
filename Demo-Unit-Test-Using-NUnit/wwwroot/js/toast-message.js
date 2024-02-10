function showToastSuccess(message = "") {
    $.toast({
        heading: "Success",
        text: message,
        icon: "success",
        loader: true,
        loaderBg: "#fff",
        showHideTransition: "fade",
        hideAfter: 3000,
        allowToastClose: false,
        position: {
            right: 10,
            top: 10
        }
    });
}

function showToastError(message = "") {
    $.toast({
        heading: "Error",
        text: message,
        icon: "error",
        loader: true,
        loaderBg: "#fff",
        showHideTransition: "fade",
        hideAfter: 3000,
        allowToastClose: false,
        position: {
            right: 10,
            top: 10
        }
    });
}

function showToastWarning(message = "") {
    $.toast({
        heading: "Warning",
        text: message,
        icon: "warning",
        loader: true,
        loaderBg: "#fff",
        showHideTransition: "fade",
        hideAfter: 3000,
        allowToastClose: false,
        position: {
            right: 10,
            top: 10
        }
    });
}

function showToastInfo(message = "") {
    $.toast({
        heading: "Info",
        text: message,
        icon: "info",
        loader: true,
        loaderBg: "#fff",
        showHideTransition: "fade",
        hideAfter: 3000,
        allowToastClose: false,
        position: {
            right: 10,
            top: 10
        }
    });
}