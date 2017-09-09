$(document).ready(function ()
{
    $('#calendar').fullCalendar({
        header: {
            left: '',
            center: 'prev,today,next',
            right: ''
        },

        buttonText: {
            today: 'Hoje'
        },

        columnFormat: 'ddd DD/MM',

        defaultView: 'agendaWeek',

        allDaySlot: false,
        locale: 'pt-BR',
        weekends: false,

        slotDuration: '00:15:00',
        slotLabelFormat: 'H:mm',
        slotLabelInterval: 15,

        minTime: '06:00:00',
        maxTime: '18:00:00',

        eventBackgroundColor: 'red',

        unselectAuto: false,
        selectable: true,
        selectHelper: true,
        selectAllow: function (selectInfo) {
            var startMoment = moment(selectInfo.start);
            var endMoment = moment(selectInfo.end);

            if (startMoment.day() != endMoment.day())
                return false;

            return (1 == 1);
        },
        select: function (start, end) {

            var startMoment = moment(start);
            var endMoment = moment(end);

            $('#DataInicioEventoAgenda').val(startMoment.format('DD/MM/YYYY HH:mm:ss'));
            $('#DataFimEventoAgenda').val(endMoment.format('DD/MM/YYYY HH:mm:ss'));

            $('#DataEvento').val(startMoment.format('DD/MM/YYYY'));
            $('#HorarioInicioEvento').val(startMoment.format('HH:mm'));
            $('#HorarioFimEvento').val(endMoment.format('HH:mm'));

            $('#PacienteIdEventoAgenda').val('');
            $('#Paciente').val('');

            $("#Paciente").prop('readonly', false);

            $('#botaoExcluir').hide();
            $('#botaoPaciente').hide();
            $('#botaoSalvar').show();

            $("#modalEventoAgenda").modal({ backdrop: "static" });            
        },
        selectOverlap: function (event) {
            return event.rendering === 'background';
        },
        selectConstraint: function (event) {
            return true;
        },

        eventOverlap: false,

        eventClick: function (calEvent, jsEvent, view) {

            var startMoment = moment(calEvent.start);
            var endMoment = moment(calEvent.end);

            $('#IdEventoAgenda').val(calEvent.id);

            $('#DataInicioEventoAgenda').val(startMoment.format('DD/MM/YYYY hh:mm:ss'));
            $('#DataFimEventoAgenda').val(endMoment.format('DD/MM/YYYY hh:mm:ss'));

            $('#DataEvento').val(startMoment.format('DD/MM/YYYY'));
            $('#HorarioInicioEvento').val(startMoment.format('hh:mm'));
            $('#HorarioFimEvento').val(endMoment.format('hh:mm'));

            $('#PacienteIdEventoAgenda').val(calEvent.pacienteId);

            $('#Paciente').val(calEvent.title);
            $("#Paciente").prop('readonly', true);

            $("#botaoPaciente").prop('href', '/SGCM/Paciente/Visualizar?id=' + calEvent.pacienteId);

            $('#botaoExcluir').show();
            $('#botaoPaciente').show();
            $('#botaoSalvar').hide();

            $('#PacienteIdEventoAgenda-error').css('display', 'none');

            $("#modalEventoAgenda").modal({ backdrop: "static" });
        },

        editable: false,
        events:
        {
            url: '/SGCM/Consulta/BuscarItensAgenda',
            error: function () { alert("ERROR WHILE FETCHING EVENTS") },
            data: {
                medicoId: $('#medicoId').val(),
            }
        }
    });

    $("#formEventoAgenda").submit(function (e) {

        $("#formEventoAgenda").data("validator").settings.ignore = "";

        if ($("#formEventoAgenda").valid()) {
            $.ajax({
                type: 'POST',
                url: '/SGCM/Consulta/SalvarEvento',
                data: $('#formEventoAgenda').serialize(),
                success: function (data) {
                    $('#calendar').fullCalendar('refetchEvents');

                    clearSelections();

                    $('#modalEventoAgenda').modal('hide');
                }
            });
        }

        e.preventDefault();
    });

    $("#botaoExcluir").click(function (e) {
        $.ajax({
            type: 'POST',
            url: '/SGCM/Consulta/ExcluirItemAgenda',
            data: { id: $('#IdEventoAgenda').val() },
            success: function (data) {

                $('#calendar').fullCalendar('refetchEvents');

                clearSelections();

                $('#modalEventoAgenda').modal('hide');
            }
        });
    });

    //Autocomplete
    $("#Paciente").autocomplete({
        source: "/SGCM/Paciente/BuscarPacienteAutocomplete",
        minLength: 1,
        select: function (event, ui) {
            $('#PacienteIdEventoAgenda').val(ui.item.id);
        },
        appendTo: "#modalEventoAgenda",
        change: function (event, ui) {
            if(!ui.item){
                $(event.target).val("");
                $('#PacienteIdEventoAgenda').val("");
            }
        }, 
        focus: function (event, ui) {
            return false;
        }
    });

    $('[data-toggle="tooltip"]').tooltip();
});

function clearSelections() {
    $('#calendar').fullCalendar('unselect');
    $('#PacienteIdEventoAgenda').val("");
    $('#Paciente').val("");
}