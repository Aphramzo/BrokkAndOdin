$(function () {
    var Site = function () { };

    Site.prototype.Initialize = function () {
        this.CheckForMemories();
    }

    Site.prototype.CheckForMemories = function() {
        $.ajax('/AnyMemories', {
            dataType: 'json',
            method: 'post',
            success: function(response) {
                if (response != null) {
                    this.MemoriesFound();
                }
            }.bind(this)
        });
    }

    Site.prototype.MemoriesFound = function() {
        $('.menu button, .menu #memoriesLink a').prepend('<i class="fa fa-exclamation-circle"></i>');
    }

    var site = new Site();
    site.Initialize();
});