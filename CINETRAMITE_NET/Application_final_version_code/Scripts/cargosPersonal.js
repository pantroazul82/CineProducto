jQuery(document).ready(function () {
    var lastselSubGrid;
    var lastselGrid;
    $("#grid_cargospersonal").jqGrid(
    {
        datatype: function () {
            $.ajax(
            {
                url: "Default.aspx/getPersonalPosition", //PageMethod

                data:
                    "{'pPageSize':'" + $('#grid_cargospersonal').getGridParam("rowNum") +
                    "','pCurrentPage':'" + $('#grid_cargospersonal').getGridParam("page") +
                    "','pSortColumn':'" + $('#grid_cargospersonal').getGridParam("sortname") +
                    "','pSortOrder':'" + $('#grid_cargospersonal').getGridParam("sortorder")
                    + "'}", //Parametros de entrada del PageMethod

                dataType: "json",
                type: "post",
                contentType: "application/json; charset=utf-8",
                complete: function (jsondata, stat) {
                    if (stat == "success")
                        jQuery("#grid_cargospersonal")[0].addJSONData(JSON.parse
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
            { name: 'position_name', index: 'position_name', width: 250, align: 'center', label: 'Nombre', editable: true, edittype: 'text', editoptions: { size: 5, maxlength: 100} },
            { name: 'position_description', index: 'position_description', width: 250, align: 'center', label: 'Descripción', editable: true, edittype: 'text', editoptions: { size: 5, maxlength: 100} }
            
        ],
    onSelectRow: function (id) {
            if (id && id !== lastselGrid) {
                jQuery("#grid_cargospersonal").restoreRow(lastselGrid);
                jQuery("#grid_cargospersonal").editRow(id, true);
                lastselGrid = id;
                console.log(id);
            }
        },
    editurl: 'Default.aspx/EditPositionOption',
    ajaxRowOptions: { contentType: 'application/json; charset=utf-8' },
    serializeRowData: function (data) {
    console.log(data);
        return JSON.stringify(data);
    },
    pager: "#pager_cargospersonal", //Pager.
    loadtext: 'Cargando datos...',
    recordtext: "{0} - {1} de {2} elementos",
    emptyrecords: 'No hay resultados',
    pgtext: 'Pág: {0} de {1}', //Paging input control text format.
    rowNum: "10", // PageSize.
    rowList: [5, 10, 20, 30, 40, 50], //Variable PageSize DropDownList.
    viewrecords: true, //Show the RecordCount in the pager.
    multiselect: false,
    sortname: "position_id", //Default SortColumn
    sortorder: "desc", //Default SortOrder.
    width: "1000",
    height: "350",
    caption: "Opciones de cargos de Personal",
    subGrid: true,
    subGridRowExpanded: function (subgrid_id, row_id) {
        // we pass two parameters 
        // subgrid_id is a id of the div tag created whitin a table data 
        // the id of this elemenet is a combination of the "sg_" + id of the row 
        // the row_id is the id of the row 
        // If we wan to pass additinal parameters to the url we can use 
        // a method getRowData(row_id) - which returns associative array in type name-value 
        // here we can easy construct the flowing 
        var subgrid_table_id, pager_id;
        subgrid_table_id = subgrid_id + "_t";
        pager_id = "p_" + subgrid_table_id;
        $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");

        jQuery("#" + subgrid_table_id).jqGrid({
            //este es mi subgrid
            datatype: function () {
                $.ajax(
                    {
                        url: "Default.aspx/getPositionDetail", //PageMethod

                        data: "{'pPageSize':'" + $("#" + subgrid_table_id).getGridParam("rowNum") +
                        "','pCurrentPage':'" + $("#" + subgrid_table_id).getGridParam("page") +
                        "','pSortColumn':'" + $("#" + subgrid_table_id).getGridParam("sortname") +
                        "','pSortOrder':'" + $("#" + subgrid_table_id).getGridParam("sortorder") +
                        "','id':'" + $("#" + subgrid_table_id).getGridParam("ajaxGridOptions") + "'}", //PageMethod Parametros de entrada

                        dataType: "json",
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        complete: function (jsondata, stat) {
                            if (stat == "success")
                                jQuery("#" + subgrid_table_id)[0].addJSONData(JSON.parse(jsondata.responseText).d);
                            else
                                alert(JSON.parse(jsondata.responseText).Message);
                        }
                    });
            },
            jsonReader: //Set the jsonReader to the JQStaffOptionDetailJSonResponse squema to bind the data.
                {
                root: "Items",
                page: "CurrentPage",
                total: "PageCount",
                records: "RecordCount",
                repeatitems: true,
                cell: "Row",
                id: "ID" //index of the column with the PK in it    
            },
            
            colModel: //Columns
            [
                { name: 'position_name', index: 'position_name', width: 250, align: 'center', label: 'Nombre', editable: true, edittype: 'text', editoptions: { size: 5, maxlength: 100} },
                { name: 'position_description', index: 'position_description', width: 250, align: 'center', label: 'Descripción', editable: true, edittype: 'text', editoptions: { size: 5, maxlength: 100} },
                { name: 'position_father_id', index: 'position_father_id', hidden: true, editable: true }
            ],  
            onSelectRow: function (id) {
                if (id && id !== lastselSubGrid) {
                    jQuery("#" + subgrid_table_id).restoreRow(lastselSubGrid);
                    jQuery("#" + subgrid_table_id).editRow(id, true);
                    lastselSubGrid = id;
                }
            },
            editurl: 'Default.aspx/EditPositionOptionDetail',
            ajaxRowOptions: { contentType: 'application/json; charset=utf-8' },
            serializeRowData: function (data) {
                return JSON.stringify(data);
            },
            pager: pager_id, //Pager.
            loadtext: 'Cargando datos...',
            recordtext: "{0} - {1} de {2} elementos",
            emptyrecords: 'No hay resultados',
            pgtext: 'Pág: {0} de {1}', //Paging input control text format.
            rowNum: "10", // PageSize.
            ajaxGridOptions: row_id,
            rowList: [5, 10, 20, 30, 40, 50], //Variable PageSize DropDownList. 
            viewrecords: true, //Show the RecordCount in the pager.
            multiselect: false,
            sortname: "position_id", //Default SortColumn
            sortorder: "desc", //Default SortOrder.
            width: "600",
            height: "100%",
            caption: "Detalle de cargos"
        });
        jQuery("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: true, del: true },
            { serializeEditData: function (postdata) {
                // your implementation of serializeEditData for edit
                }
            },
            {
                editData: { position_father_id: row_id }, serializeEditData: function (data) {
                    return JSON.stringify(data);
                }
            },
            {
                url: 'Default.aspx/DelPositionDetail', 
                serializeDelData: function (data) {
                    return JSON.stringify(data);
                }
            }
            )
    },
    subGridRowColapsed: function (subgrid_id, row_id) {
        // this function is called before removing the data 
        //var subgrid_table_id; 
        //subgrid_table_id = subgrid_id+"_t"; 
        //jQuery("#"+subgrid_table_id).remove(); 
    }
}).navGrid("#pager_cargospersonal", { edit: false, add: true, search: false, del: true },
            { serializeEditData: function (postdata) {
                // your implementation of serializeEditData for edit
                }
            },
            { serializeEditData: function (data) {
                    return JSON.stringify(data);
                }
            },
            {
                url: 'Default.aspx/DelPositionDetail', 
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