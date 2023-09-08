jQuery(document).ready(function () {
    var lastselGrid;
    $("#grid_attachment").jqGrid(
        {
            datatype: function () {
                $.ajax(
                {
                    url: "Default.aspx/GetAttachmentOptions", //PageMethod

                    data:
                        "{'pPageSize':'" + $('#grid_attachment').getGridParam("rowNum") +
                        "','pCurrentPage':'" + $('#grid_attachment').getGridParam("page") +
                        "','pSortColumn':'" + $('#grid_attachment').getGridParam("sortname") +
                        "','pSortOrder':'" + $('#grid_attachment').getGridParam("sortorder")
                        + "'}", //Parametros de entrada del PageMethod

                    dataType: "json",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    complete: function (jsondata, stat) {
                        if (stat == "success")
                            jQuery("#grid_attachment")[0].addJSONData(JSON.parse
                                (jsondata.responseText).d);
                        else
                            alert(JSON.parse(jsondata.responseText).Message);
                    }
                });
            },
            jsonReader: //jsonReader &#8211;> JQGridJSonResponse data.
            {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: true,
            cell: "Row",
            id: "ID"
        },
        colModel: //Columns
            [
                { name: 'attachment_name', index: 'attachment_name', width: 150, align: 'Left', label: 'Nombre', editable: true, edittype: 'text' },
                { name: 'attachment_description', index: 'attachment_description', width: 230, align: 'Left', label: 'Descripcion', editable: true, edittype: 'text'},
                { name: 'attachment_section', index: 'attachment_section', width: 50, align: 'Center', label: 'Sección', editable: true, edittype: 'select', editoptions: { value: { 0: 'Seleccione', 1: 'Datos del Productor', 41: 'Datos Personal', 3: 'Coproductores', 7: 'Datos de la obra', 16: 'Datos de Financiación', 33: 'Resolución de aprobación o rechazo', 34: 'Modificaciones a la resolución' } } },
                { name: 'attachment_format', index: 'attachment_format', width: 50, align: 'Center', label: 'Formato', editable: true, edittype: 'select', editoptions: {value:{'pdf':'PDF','xls':'Excel'}} },
                { name: 'attachment_order', index: 'attachment_order', width: 50, align: 'Center', label: 'Orden', editable: true, edittype: 'text'},
                { name: 'attachment_foreing_producer', index: 'attachment_foreing_producer', width: 60, align: 'Center', label: 'A.Extranjeros', editable: true, edittype: 'select', editoptions: {value:{1:'Si',2:'No'}} }
            ],
        onSelectRow: function (id) {
                if (id && id !== lastselGrid) {
                    jQuery("#grid_attachment").restoreRow(lastselGrid);
                    jQuery("#grid_attachment").editRow(id, true);
                    lastselGrid = id;
                }
            },
        editurl: 'Default.aspx/EditAttachmentOption',
        ajaxRowOptions: { contentType: 'application/json; charset=utf-8' },
        serializeRowData: function (data) {
            return JSON.stringify(data);
        },
        pager: "#pager_attachment", //Pager.
        loadtext: 'Cargando datos...',
        recordtext: "{0} - {1} de {2} elementos",
        emptyrecords: 'No hay resultados',
        pgtext: 'Pág: {0} de {1}', //Paging input control text format.
        rowNum: "10", // PageSize.
        rowList: [10, 20, 30], //Variable PageSize DropDownList.
        viewrecords: true, //Show the RecordCount in the pager.
        multiselect: false,
        sortname: "attachment_section", //Default SortColumn
        sortorder: "desc", //Default SortOrder.
        width: "1000",
        height: "350",
        caption: "Adjuntos"
    }).navGrid("#pager_attachment", { edit: false, add: true, search: false, del: true },
                { serializeEditData: function (postdata) {
                    // your implementation of serializeEditData for edit
                    }
                },
                { serializeEditData: function (data) {
                        return JSON.stringify(data);
                    }
                },
                {
                    url: 'Default.aspx/DelAttachmentOption', 
                    serializeDelData: function (data) {
                        return JSON.stringify(data);
                    }
                }
    );
    $.extend($.jgrid.edit, {
        ajaxEditOptions: { contentType: "application/json" },
    });
    $.extend($.jgrid.del, {
        ajaxDelOptions: { contentType: "application/json" },
    });
}); 