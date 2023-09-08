jQuery(document).ready(function () {
    var lastselSubGrid;
    var lastselGrid;
    $("#grid_role").jqGrid(
    {
        datatype: function () {
            $.ajax(
            {
                url: "Default.aspx/GetRoleOptions", //PageMethod

                data:
                    "{'pPageSize':'" + $('#grid_role').getGridParam("rowNum") +
                    "','pCurrentPage':'" + $('#grid_role').getGridParam("page") +
                    "','pSortColumn':'" + $('#grid_role').getGridParam("sortname") +
                    "','pSortOrder':'" + $('#grid_role').getGridParam("sortorder")
                    + "'}", //Parametros de entrada del PageMethod

                dataType: "json",
                type: "post",
                contentType: "application/json; charset=utf-8",
                complete: function (jsondata, stat) {
                    if (stat == "success")
                        jQuery("#grid_role")[0].addJSONData(JSON.parse
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
                { name: 'role_name', index: 'role_name', width: 150, align: 'Left', label: 'Rol', editable: false }
            ],
        ajaxRowOptions: { contentType: 'application/json; charset=utf-8' },
        serializeRowData: function (data) {
            return JSON.stringify(data);
        },
        pager: "#pager_role", //Pager.
        loadtext: 'Cargando datos...',
        recordtext: "{0} - {1} de {2} elementos",
        emptyrecords: 'No hay resultados',
        pgtext: 'Pág: {0} de {1}', //Paging input control text format.
        rowNum: "10", // PageSize.
        rowList: [10, 20, 30], //Variable PageSize DropDownList.
        viewrecords: true, //Show the RecordCount in the pager.
        multiselect: false,
        sortname: "role_name", //Default SortColumn
        sortorder: "desc", //Default SortOrder.
        width: "1000",
        height: "350",
        caption: "Asignación de roles a los usuarios",
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
                            url: "Default.aspx/GetAssignedUsers", //PageMethod

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
                colNames: ['Id', 'Nombre de usuario',''],
                colModel:
                    [
                        { name: 'idusuario', index: 'idusuario', width: 100, align: 'center', editable: true, edittype: 'text', editoptions: { size: 5, maxlength: 10}},
                        { name: 'username', index: 'username', width: 200, align: 'left', editable: false },
                        { name: 'role_id', index: 'role_id', hidden: true, editable: true }
                    ],
                editurl: 'Default.aspx/EditAssignedUser',
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
                sortname: "idusuario", //Default SortColumn
                sortorder: "asc", //Default SortOrder.
                width: "600",
                height: "100%",
                caption: "Usuarios asignados"
            });
            jQuery("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: true, del: true },
                { serializeEditData: function (postdata) {
                    // your implementation of serializeEditData for edit
                    }
                },
                {
                    editData: { role_id: row_id }, serializeEditData: function (data) {
                        return JSON.stringify(data);
                    }
                },
                {
                    url: 'Default.aspx/DelAssignedUser',
                    delData: { role_id: row_id },
                    serializeDelData: function (data) {
                        return JSON.stringify(data);
                    }
                }
                )
        },
        subGridRowColapsed: function (subgrid_id, row_id) {
        }
    }).navGrid("#pager_role", { edit: false, add: false, search: false, del: false },
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