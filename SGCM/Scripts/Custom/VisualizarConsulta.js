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

        editable: false,
        events:
        {
            url: '/SGCM/Consulta/BuscarItensVisualizarConsultas',
            error: function () { alert("ERROR WHILE FETCHING EVENTS") },
            data: {
                medicoId: $('#medicoId').val(),
            }
        }
    });
});