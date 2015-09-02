$(function () {
    var Video = function () { };

    Video.prototype.Initialize = function () {
        this.setupFields();
        this.setupListeners();
        this.fields.video.removeAttr("controls");
        this.fields.video.removeAttr("poster");
        this.playVideo(0);
    }

    Video.prototype.setupFields = function() {
        this.fields = {
            video : $('video'),
            videoListContainer : document.getElementsByClassName("video-list")[0],
            linkArray : [],
            currentVideoIndex : 0
        }
        this.fields.links = this.fields.videoListContainer.children;
        this.fields.numberOfVideos = this.fields.links.length;
        this.fields.source = this.fields.video.children("source");
        this.setupLinkList();
    }

    Video.prototype.setupLinkList = function() {
        for (var i = 0; i < this.fields.numberOfVideos; i++) {
            this.fields.linkArray[i] = this.fields.links[i].href;
        }
    }

    Video.prototype.setupListeners = function() {
        this.fields.video.on('ended', this.playNextVideo.bind(this));

        this.fields.video.on('mouseenter', this.onVideoMouseOver.bind(this));

        this.fields.video.on('mouseleave', this.onVideoMouseOut.bind(this));

        $('body').keydown(this.onDocumentKeyPress.bind(this));

        $('.video-list').on('click', 'a', this.onThumbnailClick.bind(this));
    }

    Video.prototype.onDocumentKeyPress = function(e) {
        if (e.keyCode == 40 || e.keyCode == 39) { // down or right cursor
            this.playNextVideo();
        }
        if (e.keyCode == 38 || e.keyCode == 37) { // up or left cursor
            this.playPreviousVideo();
        }
    }

    Video.prototype.onThumbnailClick = function(e) {
        e.preventDefault();
        this.unhighlightVideos();
        this.playVideo($(e.target).parent().data('index'));
    }

    Video.prototype.onVideoMouseOver = function () {
        this.fields.video.attr("controls", "true");
    }

    Video.prototype.onVideoMouseOut = function () {
        this.fields.video.removeAttr("controls");
    }

    Video.prototype.playPreviousVideo = function() {
        this.unhighlightVideos();
        if (this.fields.currentVideoIndex == 0) {
            this.playVideo(this.fields.numberOfVideos - 1);
        } else {
            this.playVideo(this.fields.currentVideoIndex - 1);
        }
    }

    Video.prototype.playNextVideo = function () {
        this.unhighlightVideos();
        if ((this.fields.currentVideoIndex + 1) >= this.fields.numberOfVideos) {
            this.playVideo(0);
        } else {
            this.playVideo(this.fields.currentVideoIndex + 1);
        }
    }

    Video.prototype.unhighlightVideos = function() {
        for (var i = 0; i < this.fields.numberOfVideos; i++) {
            this.fields.links[i].classList.remove("currentvid");
        }
    }

    Video.prototype.playVideo = function(index) {
        this.fields.videoListContainer.children[index].classList.add("currentvid");
        if ($(this.fields.videoListContainer.children[index]).offset().top > 530) {
            this.fields.videoListContainer.children[index].scrollIntoView();
        }
        
        this.fields.source[0].src = this.fields.linkArray[index];
        this.fields.currentVideoIndex = index;
        this.fields.video[0].load();
        this.fields.video[0].play();
    }

    var video = new Video();
    video.Initialize();
});