﻿$(document).ready(function () {
    $('.btn-daterange').daterangepicker(
        {
            ranges: {
                'Last 7 Days': [moment().subtract('days', 6), moment()],
                'Last 30 Days': [moment().subtract('days', 29), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')],
                'This Year': [moment().startOf('year'), moment().endOf('year')],
                'Last Year': [moment().subtract('year', 1).startOf('year'), moment().subtract('year', 1).endOf('year')]
            },
            startDate: $('#StartDate').val(),
            endDate: $('#EndDate').val(),
            opens: 'left'
        },
        function (start, end) {
            $('#StartDate').val(start.format('MM/DD/YYYY'));
            $('#EndDate').val(end.format('MM/DD/YYYY'));
        }
    );
});