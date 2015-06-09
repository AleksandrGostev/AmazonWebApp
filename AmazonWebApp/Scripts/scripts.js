var preloadedContent = '';
$(document).ready(function () {
    loadCurrencies();

    $('#search-form').submit(function () {
        loadItems(function (html) {
            $('.items').append(html);
            $('.load-more').show();
            preloadNextItems();
        });
        return false;
    });

    $('body').on('click', '.load-more', function () {
        $('.items').append(preloadedContent);
        $('#currencies').trigger('change');
        preloadNextItems();
    });
});

function loadItems(callback) {
    var url = $('#search-form').attr('action');
    var q = $('#q').val();
    var lastIndex = +$('.item:last-child').attr('data-index-on-page') + 1;
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            q: q,
            page: $('.item:last-child').attr('data-page'),
            lastIndexOnPage: lastIndex
        },
        success: function (data) {
            callback(data);
            $('#currencies').trigger('change');
        }
    });
}

function preloadNextItems() {
    loadItems(function (html) {
        preloadedContent = html;
    });
}

function loadCurrencies() {
    $.ajax({
        url: "https://openexchangerates.org/api/latest.json?app_id=a8959d4dd24846a7a7d88405fdb3e630",
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            var baseCurrency = { "key": data.base, "value": data.rates[data.base] };
            $('#currencies').append('<option value=' + baseCurrency.value + '>' + baseCurrency.key + '</option>');
            $.each(data.rates, function (key, value) {
                if (baseCurrency.key == key) return;
                $('#currencies').append('<option value=' + value + '>' + key + '</option>');
            });
            bindCurrencyActions();
        }
    });
}

function bindCurrencyActions() {
    $("#currencies").change(function () {
        var rate = $(this).val();
        var currency = $(this).find('option:selected').text();
        $('.item').find('.item-price').each(function (i, e) {
            var curPrice = +$(e).attr('data-usd-price');
            var newPrice = (curPrice * rate).toFixed(2) + " " + currency;
            $(e).text(newPrice);
        });
    });
}