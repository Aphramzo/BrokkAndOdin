//TW: you can tell this isn't my style - 'borrowed' most from https://codepen.io/dudleystorey/pen/vnedg
//TODO: refactor into a style more my liking

var video = document.getElementsByTagName("video")[0],
video_links = document.getElementsByClassName("video-list")[0],
source = video.getElementsByTagName("source"),
link_list = [],
currentVid = 0,
allLnks = video_links.children,
lnkNum = allLnks.length;
video.removeAttribute("controls");
video.removeAttribute("poster");

(function () {
    function playVid(index) {
        video_links.children[index].classList.add("currentvid");
        if ($(video_links.children[index]).offset().top > 530) {
            video_links.children[index].scrollIntoView();
        }
        
        source[0].src =  link_list[index];
        currentVid = index;
        video.load();
        video.play();
    }

    for (var i = 0; i < lnkNum; i++) {
        var filename = allLnks[i].href;
        link_list[i] = filename;
        (function (index) {
            allLnks[i].onclick = function (i) {
                i.preventDefault();
                for (var i = 0; i < lnkNum; i++) {
                    allLnks[i].classList.remove("currentvid");
                }
                playVid(index);
            }
        })(i);
    }
    video.addEventListener('ended', function() {
        allLnks[currentVid].classList.remove("currentvid");
        var nextVid;
        if ((currentVid + 1) >= lnkNum) {
            nextVid = 0;
        } else {
            nextVid = currentVid + 1;
        }
        playVid(nextVid);
    });

    video.addEventListener('mouseenter', function() {
        video.setAttribute("controls", "true");
    });

    video.addEventListener('mouseleave', function() {
        video.removeAttribute("controls");
    });

    var indexOf = function (needle) {
        if (typeof Array.prototype.indexOf === 'function') {
            indexOf = Array.prototype.indexOf;
        } else {
            indexOf = function (needle) {
                var i = -1, index = -1;
                for (i = 0; i < this.length; i++) {
                    if (this[i] === needle) {
                        index = i;
                        break;
                    }
                }
                return index;
            };
        }
        return indexOf.call(this, needle);
    };
    var focusedLink = document.activeElement;
    var index = indexOf.call(allLnks, focusedLink);

    document.addEventListener('keydown', function (e) {
        if (index) {
            var focusedElement = document.activeElement;
            if (e.keyCode == 40 || e.keyCode == 39) { // down or right cursor
                var nextNode = focusedElement.nextElementSibling;
                if (nextNode) { nextNode.focus(); } else { video_links.firstElementChild.focus(); }
            }
            if (e.keyCode == 38 || e.keyCode == 37) { // up or left cursor
                var previousNode = focusedElement.previousElementSibling;
                if (previousNode) { previousNode.focus(); } else { video_links.lastElementChild.focus(); }
            }
        }
    });

    playVid(0);

})();