
var map;

function getInfoWindows(objet) {
    var color = objet.isInti ? 'w3-green' : 'w3-red';
    return '<div class="w3-card-4" style="width:100%; height:100%;">'
        + '<header class="w3-container ' + color + ' ">'
        + '<h6>' + objet.name + '</h6>'
        + '</header>'
        + '<div class="w3-container">'
        + ' <p>Contacto: ' + objet.contacto + '</p>'
        + ' <p>Telefono:' + objet.tel + '</p>'
        + ' <p>mail: ' + objet.email + '</p>' +
        ' </div>'
        + ' <footer class="w3-container ' + color + '">'
        + ' <p>direccion: ' + objet.direc + ' </p> </footer>'
        + '</div>'
}


//set the center to a position
function setCenter(lat, lon) {
    map.setZoom(13);
    map.setCenter(new google.maps.LatLng(lat, lon));
}

//reset the inti users
function setIntiUsersToMap(locations) {
    var mapDiv = document.getElementById('map');
    map = new google.maps.Map(mapDiv, { zoom: 13 });
    var infowindow = new google.maps.InfoWindow();
    var bounds = new google.maps.LatLngBounds();
    var markers = [];
    //get all the circles
    for (i = 0; i < locations.length; i++) {
        //creates circles
        var iconPath = "";

        if (locations[i].isInti) {
            iconPath = 'http://orig09.deviantart.net/4a3c/f/2011/338/e/f/the_green_circle___emblem___logo_by_exxp0-d4i51r3.png';
        }
        else {
            iconPath = 'http://tecfa.unige.ch/guides/svg/ex/html5/svg-import/huge-red-circle.svg';
        }
        var circleIcon = new google.maps.MarkerImage(
        iconPath,
        null,
        new google.maps.Point(0, 0),
        new google.maps.Point(13, 15),
        new google.maps.Size(25, 25));

        //put the circle icon
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(locations[i].lat, locations[i].lng),
            map: map,
            icon: circleIcon
        });

        //create the circle icon
        var pinIcon = new google.maps.MarkerImage(
            locations[i].icon,
            null,
            null,
            null,
            new google.maps.Size(45, 45)
        );

        //create the marker for the inti user
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(locations[i].lat, locations[i].lng),
            map: map,
            icon: pinIcon, animation: google.maps.Animation.DROP
        });

        //the bounds make that visible all the icons
        bounds.extend(new google.maps.LatLng(locations[i].lat, locations[i].lng));

        //markers is a list of inti users, this will be used for MarkerCluster
        markers.push(marker);

        //add the listener for the markers of inti users
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                infowindow.setContent(getInfoWindows(locations[i]));
                infowindow.open(map, marker);
            }
        })(marker, i));
    }
    //cluster styles
    var clusterStyles = [
        {
            textColor: 'white',
            url: 'https://upload.wikimedia.org/wikipedia/commons/e/e6/Lol_circle.png',
            height: 50,
            width: 50
        },
        {
            textColor: 'white',
            url: 'http://www.clipartx.com/uploads/pink/pink-circle-155099',
            height: 50,
            width: 50
        },
        {
            textColor: 'white',
            url: 'http://icons.iconarchive.com/icons/danieledesantis/playstation-flat/512/playstation-circle-dark-icon.png',
            height: 50,
            width: 50
        }
    ];

    //creates the marker cluster Options
    var markerClusterOptions = {
        gridSize: 50,
        styles: clusterStyles,
        maxZoom: 15
    };

    var markerCluster = new MarkerClusterer(map, markers, markerClusterOptions);
    map.fitBounds(bounds);
}