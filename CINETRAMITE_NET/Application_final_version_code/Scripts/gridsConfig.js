
$(function () {

    $("#jqGridTable").jqGrid({
        url: $("#urlbase").val() + "Callbacks/List.aspx",
        datatype: 'xml',
        mtype: 'POST',
        colNames: ['Id', 'Título obra', 'Fecha solicitud', 'Fecha solicitud aclaraciones', 'Fecha envío aclaraciones', 'Fecha Resolución', 'Estado', 'Resolución'],
        colModel: [
      { name: 'project_id', index: 'project_id', width: 55 },
      { name: 'project_name', index: 'project_name', width: 90, formatter: 'linkToProjectFormatter' },
      { name: 'project_request_date', index: 'project_request_date', width: 80, align: 'right', formatter: 'dateFormatter', searchoptions: { dataInit: function (el) { $(el).datepicker({ dateFormat: 'dd/mm/yy', onSelect: function (dateText, inst) { $("#jqGridTable")[0].triggerToolbar(); } }); } } },
      { name: 'project_clarification_request_date', index: 'project_clarification_request_date', formatter: 'dateFormatter', width: 80, align: 'right', searchoptions: { dataInit: function (el) { $(el).datepicker({ dateFormat: 'dd/mm/yy', onSelect: function (dateText, inst) { $("#jqGridTable")[0].triggerToolbar(); } }); } } },
      { name: 'project_clarification_response_date', index: 'project_clarification_response_date', formatter: 'dateFormatter', width: 80, align: 'right', searchoptions: { dataInit: function (el) { $(el).datepicker({ dateFormat: 'dd/mm/yy', onSelect: function (dateText, inst) { $("#jqGridTable")[0].triggerToolbar(); } }); } } },
      { name: 'project_resolution_date', index: 'project_resolution_date', width: 80, formatter: 'dateFormatter', align: 'right', searchoptions: { dataInit: function (el) { $(el).datepicker({ dateFormat: 'dd/mm/yy', onSelect: function (dateText, inst) { $("#jqGridTable")[0].triggerToolbar(); } }); } } },
      { name: 'state_name', index: 'state_name', width: 150, stype: 'select', editoptions: { value: ":Todas;Creada:Creada;Enviada:Enviada;Aprobada:Aprobada;Aclaraciones solicitadas:Aclaraciones solicitadas"} },
      { name: 'resolution_path', index: 'resolution_path', width: 90, formatter: 'linkToPojectResolution' }
    ],
        pager: '#jqGridPager',
        rowNum: 10,
        autowidth: true,
        height: "auto",
        rowList: [10, 20, 30],
        sortname: 'project_id',
        sortorder: 'asc',
        viewrecords: true,
        hidegrid: false,
        gridview: true,
        caption: 'Listado de Obras'
    });
    jQuery("#jqGridTable").jqGrid('navButtonAdd', "#jqGridPager", {
        caption: "Toggle",
        title: "Toggle Search Toolbar",
        buttonicon: 'ui-icon-pin-s',
        onClickButton: function () {
            mainGrid[0].toggleToolbar()
        }
    });
    jQuery("#jqGridTable").jqGrid('filterToolbar', {
        searchOnEnter: false,
        defaultSearch: 'cn'
    });
    jQuery.extend($.fn.fmatter, {
        linkToProjectFormatter: function (cellvalue, options, rowdata) {
            //console.log(options)
            return "<a href='" + $("#urlbase").val() + "DatosProyecto.aspx?project_id=" + options['rowId'] + "'>" + cellvalue + "</a>";
        }
    });
    jQuery.extend($.fn.fmatter, {
        linkToPojectResolution: function (cellvalue, options, rowdata) {
            //console.log(options)
            if (cellvalue != "" && cellvalue != null) {
                var notification = false;
                $.ajax({
                    url: "Default.aspx/getDateNotification",
                    type: "post",
                    data : "{project_id:"+options['rowId']+"}",
                    datatype: "json",
                    contentType: "application/json; charset=utf-8",
                    async :false,
                    success: function (data) {
                        console.log(data.d)
                        notification = data.d;
                    }
                })
                if (notification == true) {
                    return "<a href='" + $("#urlbase").val() + "uploads/resolutions/" + options['rowId'] + "/" + cellvalue + "' target='_blank'>Resolución</a>";
                } else {
                    return "";
                }
            }
            else {
                return "";
            }

        }
    });
    jQuery.extend($.fn.fmatter, {
        dateFormatter: function (cellvalue, options, rowdata) {
            if (cellvalue != undefined) {
                var dateArr = cellvalue.split(':');
                return dateArr[0] + ':' + dateArr[1] + dateArr[2].substr(2);
            }
            else return '';
        }
    });
}); 