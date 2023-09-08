jQuery(document).ready(function () {
    var lastselGrid;
    $("#grid_formatos").jqGrid(
        {
            datatype: function () {
                $.ajax(
                {
                    url: "Default.aspx/GetFormats", //PageMethod

                    data:
                        "{'pPageSize':'" + $('#grid_formatos').getGridParam("rowNum") +
                        "','pCurrentPage':'" + $('#grid_formatos').getGridParam("page") +
                        "','pSortColumn':'" + $('#grid_formatos').getGridParam("sortname") +
                        "','pSortOrder':'" + $('#grid_formatos').getGridParam("sortorder")
                        + "'}", //Parametros de entrada del PageMethod

                    dataType: "json",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    complete: function (jsondata, stat) {
                        if (stat == "success")
                            jQuery("#grid_formatos")[0].addJSONData(JSON.parse
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
                { name: 'format_type_id', index: 'format_type_id', width: 50, align: 'Center', label: 'Tipo de Formato', editable: true, edittype: 'select', editoptions: {value:{0:'Seleccione',1:'Formatos de rodaje',2:'Formatos de Exhibición'}} },
                { name: 'format_name', index: 'format_name', width: 250, align: 'Left', label: 'Nombre del formato', editable: true, edittype: 'text'}
            ],
        onSelectRow: function (id) {
                if (id && id !== lastselGrid) {
                    jQuery("#grid_formatos").restoreRow(lastselGrid);
                    jQuery("#grid_formatos").editRow(id, true);
                    lastselGrid = id;
                }
            },
        editurl: 'Default.aspx/EditFormatOption',
        ajaxRowOptions: { contentType: 'application/json; charset=utf-8' },
        serializeRowData: function (data) {
            console.log(data);
            return JSON.stringify(data);
        },
        pager: "#pager_formatos", //Pager.
        loadtext: 'Cargando datos...',
        recordtext: "{0} - {1} de {2} elementos",
        emptyrecords: 'No hay resultados',
        pgtext: 'Pág: {0} de {1}', //Paging input control text format.
        rowNum: "10", // PageSize.
        rowList: [10, 20, 30], //Variable PageSize DropDownList.
        viewrecords: true, //Show the RecordCount in the pager.
        multiselect: false,
        sortname: "format_id", //Default SortColumn
        sortorder: "desc", //Default SortOrder.
        width: "1000",
        height: "350",
        caption: "Formatos"
    }).navGrid("#pager_formatos", { edit: false, add: true, search: false, del: true },
                { serializeEditData: function (postdata) {
                    // your implementation of serializeEditData for edit
                    }
                },
                { serializeEditData: function (data) {
                        return JSON.stringify(data);
                    }
                },
                {
                    url: 'Default.aspx/DelFormatOption', 
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