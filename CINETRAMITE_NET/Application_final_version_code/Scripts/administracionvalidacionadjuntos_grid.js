jQuery(document).ready(function () {
    var lastselGrid;
    $("#grid_validationattachment").jqGrid(
        {
            datatype: function () {
                $.ajax(
                {
                    url: "Default.aspx/GetValidationAttachmentOptions", //PageMethod

                    data:
                        "{'pPageSize':'" + $('#grid_validationattachment').getGridParam("rowNum") +
                        "','pCurrentPage':'" + $('#grid_validationattachment').getGridParam("page") +
                        "','pSortColumn':'" + $('#grid_validationattachment').getGridParam("sortname") +
                        "','pSortOrder':'" + $('#grid_validationattachment').getGridParam("sortorder")
                        + "'}", //Parametros de entrada del PageMethod

                    dataType: "json",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    complete: function (jsondata, stat) {
                        if (stat == "success")
                            jQuery("#grid_validationattachment")[0].addJSONData(JSON.parse
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
                { name: 'attachment_name', index: 'attachment_name', width: 70, align: 'Left', label: 'Adjunto', editable: true, edittype: 'select', editoptions: { dataUrl: 'Web.aspx/?method=GetAttachmentsSelect'}},
                {
                    name: 'variable', index: 'variable', width: 100, align: 'Left', label: 'Variable', editable: true, edittype: 'select', editoptions: {
                        value: {
                            'type_company': 'Tipo de Empresa', 'producer_type': 'Tipo de productor', 'total_cost': 'Costo total', 'project_genre': 'Tipo de proyecto',
                            'domestic_producers_qty': 'Cantidad de productores nacionales', 'production_type': 'Tipo de Producción', 'project_type': 'Duración'
                        }
                    }
                },
                { name: 'validation_type', index: 'validation_type', width: 50, align: 'Left', label: 'Tipo de validación', editable: true, edittype: 'select', editoptions: {value:{'optional':'Opcional','required':'Requerido','excluded':'Excluido'}} },
                { name: 'value', index: 'value', width: 100, align: 'left', label: 'Valor', editable: true, edittype: 'text'},
                { name: 'validation_operator', index: 'validation_operator', width: 20, align: 'Left', label: 'Operador', editable: true, edittype: 'select', editoptions: {value:{'=':'=','>':'>','<':'<'}} },
                { name: 'active', index: 'active', width: 30, align: 'Center', label: 'Activa', editable: true, edittype: 'select', editoptions: {value:{'1':'Si','0':'No',}} }
            ],
        onSelectRow: function (id) {
                if (id && id !== lastselGrid) {
                    jQuery("#grid_validationattachment").restoreRow(lastselGrid);
                    jQuery("#grid_validationattachment").editRow(id, true);
                    lastselGrid = id;
                }
            },
        editurl: 'Default.aspx/EditValidationAttachmentOption',
        ajaxRowOptions: { contentType: 'application/json; charset=utf-8' },
        serializeRowData: function (data) {
            return JSON.stringify(data);
        },
        pager: "#pager_validationattachment", //Pager.
        loadtext: 'Cargando datos...',
        recordtext: "{0} - {1} de {2} elementos",
        emptyrecords: 'No hay resultados',
        pgtext: 'Pág: {0} de {1}', //Paging input control text format.
        rowNum: "10", // PageSize.
        rowList: [10, 20, 30], //Variable PageSize DropDownList.
        viewrecords: true, //Show the RecordCount in the pager.
        multiselect: false,
        sortname: "attachment_name", //Default SortColumn
        sortorder: "desc", //Default SortOrder.
        width: "1000",
        height: "350",
        caption: "Reglas de validación de adjuntos"
    }).navGrid("#pager_validationattachment", { edit: true, add: true, search: false, del: true },
                { serializeEditData: function (data) {
                    // your implementation of serializeEditData for edit
                    return JSON.stringify(data);
                    }
                },
                { serializeEditData: function (data) {
                        return JSON.stringify(data);
                    }
                },
                {
                    url: 'Default.aspx/DelValidationAttachmentOption', 
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