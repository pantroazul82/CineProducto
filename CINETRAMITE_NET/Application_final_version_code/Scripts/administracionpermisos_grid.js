jQuery(document).ready(function () {
    var lastselSubGrid;
    var lastselGrid;
    $("#grid_permission").jqGrid(
    {
        datatype: function () {
            $.ajax(
            {
                url: "Default.aspx/GetPermissionOptions", //PageMethod

                data:
                    "{'pPageSize':'" + $('#grid_permission').getGridParam("rowNum") +
                    "','pCurrentPage':'" + $('#grid_permission').getGridParam("page") +
                    "','pSortColumn':'" + $('#grid_permission').getGridParam("sortname") +
                    "','pSortOrder':'" + $('#grid_permission').getGridParam("sortorder")
                    + "'}", //Parametros de entrada del PageMethod

                dataType: "json",
                type: "post",
                contentType: "application/json; charset=utf-8",
                complete: function (jsondata, stat) {
                    if (stat == "success")
                        jQuery("#grid_permission")[0].addJSONData(JSON.parse
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
                { name: 'permission_name', index: 'permission_name', width: 150, align: 'Left', label: 'Permiso', editable: false },
                { name: 'permission_desc', index: 'permission_desc', width: 250, align: 'Left', label: 'Descripción', editable: false }
            ],
    ajaxRowOptions: { contentType: 'application/json; charset=utf-8' },
    serializeRowData: function (data) {
        return JSON.stringify(data);
    },
    pager: "#pager_permission", //Pager.
    loadtext: 'Cargando datos...',
    recordtext: "{0} - {1} de {2} elementos",
    emptyrecords: 'No hay resultados',
    pgtext: 'Pág: {0} de {1}', //Paging input control text format.
    rowNum: "10", // PageSize.
    rowList: [10, 20, 30], //Variable PageSize DropDownList.
    viewrecords: true, //Show the RecordCount in the pager.
    multiselect: false,
    sortname: "permission_name", //Default SortColumn
    sortorder: "desc", //Default SortOrder.
    width: "1000",
    height: "350",
    caption: "Asignación de permisos a los roles",
    subGrid: true,
    subGridRowExpanded: function (subgrid_id, row_id) {
        // we pass two parameters 
        // subgrid_id is a id of the div tag created whitin a table data 
        // the id of this elemenet is a combination of the "sg_" + id of the row 
        // the row_id is the id of the row 
        // If we want to pass additinal parameters to the url we can use 
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
                            url: "Default.aspx/getAssignedRoles", //PageMethod

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
            jsonReader: //Set the jsonReader to the JQJSonResponse squema to bind the data.
                    {
                    root: "Items",
                    page: "CurrentPage",
                    total: "PageCount",
                    records: "RecordCount",
                    repeatitems: true,
                    cell: "Row",
                    id: "ID" //index of the column with the PK in it    
                },
            colNames: ['Rol', ''],
            colModel:
                    [
                        { name: 'role_name', index: 'role_name', width: 100, align: 'center', edittype: 'select', editable: true, editoptions: { value: { 1: 'Solicitante', 2: 'Revisor', 3: 'Editor', 4: 'Director'}} },
                        { name: 'role_id', index: 'role_id', hidden: true, editable: false }
                    ],
            editurl: 'Default.aspx/EditAssignedRole',
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
            rowList: [10, 20, 30], //Variable PageSize DropDownList. 
            viewrecords: true, //Show the RecordCount in the pager.
            multiselect: false,
            sortname: "role_name", //Default SortColumn
            sortorder: "asc", //Default SortOrder.
            width: "600",
            height: "100%",
            caption: "Roles asignados"
        });
        jQuery("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: true, del: true },
                { serializeEditData: function (postdata) {
                    // your implementation of serializeEditData for edit
                }
                },
                {
                    editData: { permission_id: row_id }, serializeEditData: function (data) {
                        return JSON.stringify(data);
                    }
                },
                {
                    url: 'Default.aspx/DelAssignedRole',
                    delData: { permission_id: row_id },
                    serializeDelData: function (data) {
                        return JSON.stringify(data);
                    }
                }
                )
    },
    subGridRowColapsed: function (subgrid_id, row_id) {
    }
}).navGrid("#pager_permission", { edit: false, add: false, search: false, del: false },
                { serializeEditData: function (postdata) {
                    // your implementation of serializeEditData for edit
                }
                },
                { serializeEditData: function (data) {
                    return JSON.stringify(data);
                }
                },
                {
                    serializeDelData: function (data) {
                        return JSON.stringify(data);
                    }
                }
    );
$.extend($.jgrid.edit, {
    ajaxEditOptions: { contentType: "application/json" }
});
$.extend($.jgrid.del, {
    ajaxDelOptions: { contentType: "application/json" }
});
});