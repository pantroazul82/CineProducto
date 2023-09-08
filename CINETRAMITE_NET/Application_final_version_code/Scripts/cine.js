//funcion que permite deshabilitar las cajas de texto de un formulario
function DisableEnableForm(accion, cambio) 
{
        if (accion == true) {
            $('.user-input').attr("disabled", "disabled");
            $('.user-input-cbl input:checkbox').attr("disabled", "disabled");
        }
        else 
        {
            $('.user-input').removeAttr("disabled");
            $('.user-input-cbl input:checkbox').removeAttr("disabled");
        }

        //cambio el link segun el proceso que debe realizar
        if (cambio == 'desactivar') {
            //quiere decir que debo poner el vinculo para que desactive las cajas
            if (document.getElementById('link') != null) {
                if (document.getElementById('link').innerHTML.indexOf("DisableEnableFormCustom") != -1) {
                    document.getElementById('link').innerHTML = "<div style='margin:0;text-align:left;text-decoration:underline;cursor:pointer;padding:0 0 20px 0;' onClick='DisableEnableFormCustom(false,\"activar\")'>Editar el formulario</div>";
                } else {
                    document.getElementById('link').innerHTML = "<div style='margin:0;text-align:left;text-decoration:underline;cursor:pointer;padding:0 0 20px 0;' onClick='DisableEnableForm(false,\"activar\")'>Editar el formulario</div>";
                }
            }
        }
        else {
            //quiere decir que pone el link para que habilite las cajas
            if (document.getElementById('link') != null) {
                if (document.getElementById('link').innerHTML.indexOf("DisableEnableFormCustom") != -1) {
                    document.getElementById('link').innerHTML = "<div style='margin:0;text-align:left;text-decoration:underline;cursor:pointer;padding:0 0 20px 0;' onClick='DisableEnableFormCustom(true,\"desactivar\")'>Desactivar el formulario</div>";
                } else {
                    document.getElementById('link').innerHTML = "<div style='margin:0;text-align:left;text-decoration:underline;cursor:pointer;padding:0 0 20px 0;' onClick='DisableEnableForm(true,\"desactivar\")'>Desactivar el formulario</div>";
                }
            
            }
        }
}
$(document).ready(function () {
 
    return;
    var tracker = new Object;
    var trackchange = false;
    $('input,select,textarea').each(function () {
        tracker[$(this).attr('id')] = false;
    })

  
    try{
        console.log(tracker);
        $('input,select,textarea').change(function () {
            tracker[$(this).attr('id')] = true;
        })
        $('a').click(function () {

            $('input,select,textarea').each(function () {
                if (tracker[$(this).attr('id')] == true) {
                    trackchange = true;}
            })
            console.log(trackchange);
            if (trackchange) {
                var acceptExit = confirm("Ha realizado cambios en el formulario. ¿Desea salir sin guardar?");
                if (!acceptExit) {
                    return false;}
            }
        })
    var countNotChecked = 0;
    $(".depending-box").prop('disabled', true);
    countFormChecked();
    function countFormChecked() {
        countNotChecked = 0
        $(".form-checkbox").each(function () {
            if ($(this).prop("checked") == false) {
                countNotChecked++;
            }
        })
        if (countNotChecked == 0) {
            $(".depending-box").prop('disabled', false);
        } else {
            $(".depending-box").prop('disabled', true);
        }
    }
    $(".form-checkbox").change(function () { countFormChecked() });


    var approvedAditional = true;
    var count = 0;
    $(".completeAttachment").each(function () {
        if ($(this).val() != 1) {
            approvedAditional = false;
        }
    })

    if (!approvedAditional) {
        if ($('.depending-box').is(':checked') || $('input[type=radio][value=none]').is(':checked')) {

            $('input[type=radio][value=solicitar-aclaraciones]').attr('checked', true)
            if (!$('.notRevision').is(':checked')) {
                $('input[type=submit]').click();
            }
        }
        $(".depending-box").prop('disabled', true);
    }
    }catch(exception){}

})
