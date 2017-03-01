var configFontAwesome = {
    custom: {
        families: ['fontawesome'],
        urls: ['https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css']
    },
    fontactive: function () {
        $('.rateit-fa').rateit();
    }
};
WebFont.load(configFontAwesome);
