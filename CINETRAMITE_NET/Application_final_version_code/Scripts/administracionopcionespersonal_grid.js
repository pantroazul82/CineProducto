$(document).ready(function () {
    REinit();
}

);

function REinit() {
    var lastselSubGrid;
    var lastselGrid;
    //  alert("Entro");
    $("#grid").jqGrid(
    {
        datatype: function () {
            $.ajax(
            {
                url: "Default.aspx/GetStaffOptions", //PageMethod

                data:
                    "{'pPageSize':'" + $('#grid').getGridParam("rowNum") +
                    "','pCurrentPage':'" + $('#grid').getGridParam("page") +
                    "','pSortColumn':'" + $('#grid').getGridParam("sortname") +
                    "','pSortOrder':'" + $('#grid').getGridParam("sortorder")
                    + "'}", //Parametros de entrada del PageMethod

                dataType: "json",
                type: "post",
                contentType: "application/json; charset=utf-8",
                complete: function (jsondata, stat) {
                    if (stat == "success")
                        jQuery("#grid")[0].addJSONData(JSON.parse
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
            { name: 'production_type_name', index: 'production_type_name', width: 150, align: 'Center', label: 'Tipo de producción', editable: true, edittype: 'select', editoptions: {value:{1:'Producción',2:'Coproducción'}} },
            { name: 'project_type_name', index: 'project_type_name', width: 250, align: 'Left', label: 'Tipo de proyecto', editable: true, edittype: 'select', editoptions: { dataUrl: 'Web.aspx/?method=GetProjectTypesSelect'} },
            { name: 'project_genre_name', index: 'project_genre_name', width: 100, align: 'Left', label: 'Clasificación', editable: true, edittype: 'select', editoptions: { dataUrl: 'Web.aspx/?method=GetGenresSelect'} },
            { name: 'has_domestic_director_description', index: 'has_domestic_director_description', width: 130, align: 'Center', label: 'Director Colombiano', editable: true, edittype: 'select', editoptions: {value:{0:'No',1:'Si'}} },
            { name: 'description', index: 'description', width: 370, align: 'center', label: 'Descripción', editable: true, edittype: 'text', editoptions: { size: 50, maxlength: 100} },
            { name: 'staff_option_personal_option', index: 'staff_option_personal_option', width: 150, align: 'center', label: 'Asignado a', editable: true, edittype: 'select', editoptions: {value:{1:'Acuerdo Iberoamericano',2:'Decreto 255'}} },
            { name: 'percentage_init', index: 'percentage_init', width: 250, align: 'center', label: 'Porcentaje desde', editable: true, edittype: 'text', editoptions: { size: 5, maxlength: 100} },
            { name: 'percentage_end', index: 'percentage_end', width: 250, align: 'center', label: 'Porcentaje hasta', editable: true, edittype: 'text', editoptions: { size: 5, maxlength: 100} }
            
        ],
    onSelectRow: function (id) {
            if (id && id !== lastselGrid) {
                jQuery("#grid").restoreRow(lastselGrid);
                jQuery("#grid").editRow(id, true);
                lastselGrid = id;
            }
        },
    editurl: 'Default.aspx/EditStaffOption',
    ajaxRowOptions: { contentType: 'application/json; charset=utf-8' },
    serializeRowData: function (data) {
        return JSON.stringify(data);
    },
    pager: "#pager", //Pager.
    loadtext: 'Cargando datos...',
    recordtext: "{0} - {1} de {2} elementos",
    emptyrecords: 'No hay resultados',
    pgtext: 'Pág: {0} de {1}', //Paging input control text format.
    rowNum: "10", // PageSize.
    rowList: [5, 10, 20, 30, 40, 50], //Variable PageSize DropDownList.
    viewrecords: true, //Show the RecordCount in the pager.
    multiselect: false,
    sortname: "production_type_name", //Default SortColumn
    sortorder: "desc", //Default SortOrder.
    width: "1000",
    height: "350",
    caption: "Opciones de Personal",
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
                        url: "Default.aspx/GetStaffOptionDetail", //PageMethod

                        data: "{'pPageSize':'" + $("#" + subgrid_table_id).getGridParam("rowNum") +
                        "','pCurrentPage':'" + $("#" + subgrid_table_id).getGridParam("page") +
                        "','pSortColumn':'" + $("#" + subgrid_table_id).getGridParam("sortname") +
                        "','pSortOrder':'" + $("#" + subgrid_table_id).getGridParam("sortorder") +
                        "','id':'" + $("#" + subgrid_table_id).getGridParam("ajaxGridOptions") +
                            "','version':'" + document.getElementById("ctl00_MainContent_cmbVersion2").value +
                        "'}", //PageMethod Parametros de entrada

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
            colNames: ['Cargo', 'Cantidad', ''],
            colModel:
                [
                    { name: 'position_name', index: 'position_name', width: 200, align: 'left', editable: true, edittype: 'select', editoptions: { dataUrl: 'Web.aspx/?method=GetPositionsSelect'} },
                    { name: 'position_qty', index: 'position_qty', width: 100, align: 'center', editable: true, edittype: 'text', editoptions: { size: 1, maxlength: 2} },
                    { name: 'staff_option_id', index: 'staff_option_id', hidden: true, editable: true }
                ],
            onSelectRow: function (id) {
                if (id && id !== lastselSubGrid) {
                    jQuery("#" + subgrid_table_id).restoreRow(lastselSubGrid);
                    jQuery("#" + subgrid_table_id).editRow(id, true);
                    lastselSubGrid = id;
                }
            },
            editurl: 'Default.aspx/EditStaffOptionDetail',
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
            sortname: "position_name", //Default SortColumn
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
                editData: { staff_option_id: row_id }, serializeEditData: function (data) {
                    return JSON.stringify(data);
                }
            },
            {
                url: 'Default.aspx/DelStaffOptionDetail', 
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
}).navGrid("#pager", { edit: false, add: true, search: false, del: true },
            { serializeEditData: function (postdata) {
                // your implementation of serializeEditData for edit
                }
            },
            { serializeEditData: function (data) {
                    return JSON.stringify(data);
                }
            },
            {
                url: 'Default.aspx/DelStaffOption', 
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
}