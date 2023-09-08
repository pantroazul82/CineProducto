$(document).ready(function () {
    /**
    * Handles the event of external links getting clicked in the document.
    *
    * @example onExternalLinkClicked("http://www.google.com")
    *
    * @param String link
    */
    jQuery('#documentViewer').bind('onExternalLinkClicked', function (e, link) {

        //window.location.href = link; //uncomment to let viewer navigate
    });

    /**
    * Recieves progress information about the document being loaded
    *
    * @example onProgress( 100,10000 );
    *
    * @param int loaded
    * @param int total
    */
    jQuery('#documentViewer').bind('onProgress', function (e, loadedBytes, totalBytes) {
    });

    /**
    * Handles the event of a document is in progress of loading
    *
    */
    jQuery('#documentViewer').bind('onDocumentLoading', function (e) {
    });

    /**
    * Handles the event of a document is in progress of loading
    *
    */
    jQuery('#documentViewer').bind('onPageLoading', function (e, pageNumber) {
    });

    /**
    * Receives messages about the current page being changed
    *
    * @example onCurrentPageChanged( 10 );
    *
    * @param int pagenum
    */
    jQuery('#documentViewer').bind('onCurrentPageChanged', function (e, pagenum) {
    });

    /**
    * Receives messages about the document being loaded
    *
    * @example onDocumentLoaded( 20 );
    *
    * @param int totalPages
    */
    jQuery('#documentViewer').bind('onDocumentLoaded', function (e, totalPages) {
        //loadAnnotations();
        console.log(222)
    });

    /**
    * Receives messages about the page loaded
    *
    * @example onPageLoaded( 1 );
    *
    * @param int pageNumber
    */
    jQuery('#documentViewer').bind('onPageLoaded', function (e, pageNumber) {
    });

    /**
    * Receives messages about the page loaded
    *
    * @example onErrorLoadingPage( 1 );
    *
    * @param int pageNumber
    */
    jQuery('#documentViewer').bind('onErrorLoadingPage', function (e, pageNumber) {
    });

    /**
    * Receives error messages when a document is not loading properly
    *
    * @example onDocumentLoadedError( "Network error" );
    *
    * @param String errorMessage
    */
    jQuery('#documentViewer').bind('onDocumentLoadedError', function (e, errMessage) {
        alert(errMessage);

    });

    /**
    * Receives error messages when a document has finished printed
    *
    * @example onDocumentPrinted();
    *
    */
    jQuery('#documentViewer').bind('onDocumentPrinted', function (e) {
    });

    /**
    * Handles the event of annotations getting clicked.
    *
    * @example onMarkClicked(object)
    *
    * @param Object mark that was clicked
    */
    jQuery('#documentViewer').bind('onMarkClicked', function (e, mark) {
        console.log(mark)
        if (mark['type'] === 'note') {
            project_nid = $('#project_nid').attr('value');
            file_name = $('#file_name').attr('value');
            $.post("/cinematografia/document/annotation/update", {
                mark: mark,
                project_nid: project_nid,
                file_name: file_name
            });
        }
    });

    /**
    * Handles the event of annotations getting clicked.
    *
    * @example onMarkCreated(object)
    *
    * @param Object mark that was created
    */
    jQuery('#documentViewer').bind('onMarkCreated', function (e, mark) {
        console.log(345)
        if (mark['type'] === 'note') {
            project_nid = $('#project_nid').attr('value');
            file_name = $('#file_name').attr('value');
            $.post("/cinematografia/document/annotation/save", {
                mark: mark,
                project_nid: project_nid,
                file_name: file_name
            });
        }
    });

    /**
    * Handles the event of annotations getting clicked.
    *
    * @example onMarkDeleted(object)
    *
    * @param Object mark that was deleted
    */
    jQuery('#documentViewer').bind('onMarkDeleted', function (e, mark) {
        if (mark['type'] === 'note') {
            project_nid = $('#project_nid').attr('value');
            file_name = $('#file_name').attr('value');
            $.post("/cinematografia/document/annotation/delete", {
                mark: mark,
                project_nid: project_nid,
                file_name: file_name
            });
        }
    });

    /**
    * Handles the event of annotations getting clicked.
    *
    * @example onMarkChanged(object)
    *
    * @param Object mark that was changed
    */
    jQuery('#documentViewer').bind('onMarkChanged', function (e, mark) {
        if (mark['type'] === 'note') {
            project_nid = $('#project_nid').attr('value');
            file_name = $('#file_name').attr('value');
            $.post("/cinematografia/document/annotation/update", {
                mark: mark,
                project_nid: project_nid,
                file_name: file_name
            });
        }
    });
});





function loadAnnotations(documentString) {
    var viewer = $FlexPaper('documentViewer');
    $.get("Callbacks/annot.svc/load", {
        "documentString": documentString
    },
            function (data) {
                var annotations = data['d'];

                $.each(annotations, function (key, value) {
                    var id = value['annotation_id'];
                    var type = value['annotation_type'];
                    var note = value['annotation_text'];
                    var positionX = parseInt(value['annotation_x']);
                    var positionY = parseInt(value['annotation_y']);
                    var width = parseInt(value['annotation_width']);
                    var height = parseInt(value['annotation_height']);
                    var collapsed = value['annotation_folded'];
                    var readonly = value['annotation_readonly'];
                    var pageIndex = value['annotation_displayformat'];
                    var displayFormat = 'html';

                    var annotBlockText = note;
                    if (annotBlockText.length > 50) {
                        annotBlockText = annotBlockText.substring(0, 47) + "..."
                    }
                    $("#annotationsBlock").append("<a name='" + pageIndex + "' class='annotationList' href='#currentDocumentTitle'>" + annotBlockText + "</a>")
                    $(".annotationList").click(function () {
                        $FlexPaper('documentViewer').gotoPage($(this).attr('name'))
                    })
                    // Se muestra la anotacion
                    viewer.addMark({
                        id: id,
                        type: type,
                        note: note,
                        positionX: positionX,
                        positionY: positionY,
                        width: width,
                        pageIndex: pageIndex,
                        height: height,
                        collapsed: collapsed,
                        readonly: readonly,
                        displayFormat: 'html'
                    });
                });

            }
        );
}