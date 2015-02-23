var ImageOptions = function () { };

ImageOptions.prototype.initialize = function (imgSelector) {
    this.fields = {};
    this.fields.imageSelector = imgSelector;
    this.fields.overlay = $('<div id=\'image-overlay\' />');
    this.fields.overlay.html('<div class="col-md-2 col-md-offset-9"><button id="share-button" class="btn btn-info"><i class="fa fa-share"></i> Share</button></div>');
    this.setupListeners();
};

ImageOptions.prototype.setupListeners = function () {
    $('.rg-gallery').on('mouseenter', this.fields.imageSelector, this.showOptions.bind(this));

    $('.rg-gallery').on('mouseleave', ".rg-image", this.hideOptions.bind(this));

    $('.rg-gallery').on('click', '#share-button', this.sharePhoto.bind(this));
};

ImageOptions.prototype.showOptions = function () {
    $(this.fields.imageSelector).parent().append(this.fields.overlay);
    this.fields.overlay.slideDown(250);
};

ImageOptions.prototype.hideOptions = function (event) {
    this.fields.overlay.slideUp(500, this.fields.overlay.remove);
};

ImageOptions.prototype.sharePhoto = function () {
    var picId = $('div.es-carousel').find('li.selected').find('img').data('photo-id');
    var share = window.open('/?photo=' + picId, '_blank');
};