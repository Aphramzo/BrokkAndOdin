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
            currentVideoIndex: 0,
            reverseButton: $('.btn-reverse'),
            shuffleButton: $('.btn-shuffle'),
            isReverse: false
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

        this.fields.reverseButton.on('click', this.reversePlayOrder.bind(this));

        this.fields.shuffleButton.on('click', this.shuffleVideos.bind(this));

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

    Video.prototype.playPreviousVideo = function (e, skipReverseCheck) {
        if (this.fields.isReverse && !skipReverseCheck) {
            this.playNextVideo(null, true);
            return;
        }
        this.unhighlightVideos();
        if (this.fields.currentVideoIndex == 0) {
            this.playVideo(this.fields.numberOfVideos - 1);
        } else {
            this.playVideo(this.fields.currentVideoIndex - 1);
        }
    }

    Video.prototype.playNextVideo = function (e, skipReverseCheck) {
        if (this.fields.isReverse && !skipReverseCheck) {
            this.playPreviousVideo(null, true);
            return;
        }
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

    Video.prototype.reversePlayOrder = function() {
        this.fields.isReverse = !this.fields.isReverse;
        if (this.fields.isReverse) {
            this.fields.reverseButton.addClass('btn-primary').removeClass('btn-default');
        } else {
            this.fields.reverseButton.addClass('btn-default').removeClass('btn-primary');
        }
    };

    Video.prototype.shuffleVideos = function() {
        this.shuffleLinks();
        var links = this.fields.links;
        $.each(links, function(i, link) {
            var $link = $(link);
            $link.insertAfter($link.siblings(':eq(' + i + ')'));
            $link.data('index', i);
        });
        this.setupLinkList();
        this.fields.videoListContainer.children[this.fields.currentVideoIndex].scrollIntoView();
    }

    Video.prototype.shuffleLinks = function () {
        //TW: Copied shuffle algorithm from here: http://stackoverflow.com/questions/2450954/how-to-randomize-shuffle-a-javascript-array
        //get non dom dependant array
        var array = $(this.fields.videoListContainer).children('a').map(function () {
            return this;
        });

        var currentIndex = array.length, temporaryValue, randomIndex;

        // While there remain elements to shuffle...
        while (0 !== currentIndex) {

            // Pick a remaining element...
            randomIndex = Math.floor(Math.random() * currentIndex);
            currentIndex -= 1;

            //TW: TODO: I don't like having this here, but I cant think of another
            //way to track the index of the currently playing video
            if (randomIndex == this.fields.currentVideoIndex) {
                this.fields.currentVideoIndex = currentIndex;
            }
            else if (currentIndex == this.fields.currentVideoIndex) {
                this.fields.currentVideoIndex = randomIndex;
            }
            // And swap it with the current element.
            temporaryValue = array[currentIndex];
            array[currentIndex] = array[randomIndex];
            array[randomIndex] = temporaryValue;
        }

        this.fields.links = array;
    }

    Video.prototype.playVideo = function(index) {
        this.fields.videoListContainer.children[index].classList.add("currentvid");
        var offsetTop = $(this.fields.videoListContainer.children[index]).offset().top;
        if (offsetTop > 530 || offsetTop < 0) {
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