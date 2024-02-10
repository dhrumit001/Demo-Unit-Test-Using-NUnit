$(document).ready(function () { 
    $("#fileProgress").hide();

    $('#categorySelectBox').selectpicker();

    $('#courseDesc').summernote({
        placeholder: 'Please enter a description',
        height: 200,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']]
        ],
        callbacks: {
            onChange: function (contents, $editable) {
                var code = $('#courseDesc').summernote('code');
                $('#Description').val(code);
            }
        }
    });

    //$('#courseInstruction').summernote({
    //    placeholder: 'Please enter a instruction',
    //    height: 200,
    //    toolbar: [
    //        ['style', ['bold', 'italic', 'underline', 'clear']],
    //        ['font', ['strikethrough', 'superscript', 'subscript']],
    //        ['fontsize', ['fontsize']],
    //        ['color', ['color']],
    //        ['para', ['ul', 'ol', 'paragraph']],
    //        ['height', ['height']]
    //    ],
    //    callbacks: {
    //        onChange: function (contents, $editable) {
    //            var code = $('#courseInstruction').summernote('code');
    //            $('#Instruction').val(code);
    //        }
    //    }
    //});

    $('#categorySelectBox').change(function () {
        var selectedItem = $('#categorySelectBox').val();
        $('#Category').val(selectedItem);
    });

});

function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;

    if (files[0]) {
        if (files[0].type.includes('image')) {
            $('#MediaType').val('image');
        } else if (files[0].type.includes('video')) {
            $('#MediaType').val('video');
        } else {
            showToastError('Only image/video file accepted');
        }
    }

    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("file", files[i]);
    }

    startUpdatingProgressIndicator();
    $.ajax(
        {
            url: "/FileUpload",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                stopUpdatingProgressIndicator();
                if (data === 'Error') {
                    showToastError();
                } else {
                    showToastSuccess('File uploaded successfully');
                    $('#MediaUrl').val(data);

                    if ($('#MediaType').val() == 'image') {
                        $("#imgPreview").show();
                        $("#videoPreview").hide();
                        $("#imgPreview").attr("src", "/uploads/" + data);
                    }
                    if ($('#MediaType').val() == 'video') {
                        $("#videoPreview").show();
                        $("#imgPreview").hide();
                        $("#videoPreview").html('<source src="/uploads/' + data + '" type="video/mp4"></source>');
                    }
                }
            }
        }
    );
}

var intervalId;

function startUpdatingProgressIndicator() {
    $("#fileProgress").show();

    intervalId = setInterval(
        function () {
            // We use the POST requests here to avoid caching problems (we could use the GET requests and disable the cache instead)
            $.post(
                "/FileUpload/progress",
                function (progress) {
                    $("#fileUploadProgress")
                        .css("width", progress + "%")
                        .attr("aria-valuenow", progress)
                        .text(progress + "% Complete");
                }
            );
        },
        10
    );
}

function stopUpdatingProgressIndicator() {
    clearInterval(intervalId);
}