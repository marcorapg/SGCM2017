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

        //defaultDate: '@string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)',

        defaultDate: '2017-07-22',

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

            $('#botaoExcluir').hide();
            $('#botaoSalvar').show();

            $("#modalEventoAgenda").modal({ backdrop: "static" });            
        },
        selectOverlap: function (event) {
            return event.rendering === 'background';
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

            $('#botaoExcluir').show();
            $('#botaoSalvar').hide();

            $("#modalEventoAgenda").modal({ backdrop: "static" });
        },

        editable: false,
        events:
        {
            url: '/SGCM/Agenda/BuscarItensAgenda',
            error: function () { $('#script-warning').show(); },
            data: {
                medicoId: $('#medicoId').val(),
            }
        }
    });

    $("#formEventoAgenda").submit(function (e) {
        $.ajax({
            type: 'POST',
            url: '/SGCM/Agenda/SalvarEvento',
            data: $('#formEventoAgenda').serialize(),
            success: function (data) {
                var eventData = {
                    start: new moment(data.dataInicio),
                    end: new moment(data.dataFim)
                };

                $('#calendar').fullCalendar('refetchEvents');

                clearSelections();

                $('#modalEventoAgenda').modal('hide');
            }
        });

        e.preventDefault();
    });

    $("#botaoExcluir").click(function (e) {
        $.ajax({
            type: 'POST',
            url: '/SGCM/Agenda/ExcluirItemAgenda',
            data: { id: $('#IdEventoAgenda').val() },
            success: function (data) {

                $('#calendar').fullCalendar('refetchEvents');

                clearSelections();

                $('#modalEventoAgenda').modal('hide');
            }
        });
    });
});

function clearSelections() {
    $('#calendar').fullCalendar('unselect');
}