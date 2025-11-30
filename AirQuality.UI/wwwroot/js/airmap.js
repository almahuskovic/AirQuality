var map = L.map('map', {
    maxZoom: 18,
    minZoom: 2
}).setView([48.77, 9.18], 6);

//var markers = L.markerClusterGroup();
let markers = L.layerGroup().addTo(map);
map.addLayer(markers);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 18
}).addTo(map);

map.on('zoomend moveend', function () {
    loadCities(map.getBounds());
});

async function loadCities(bounds) {
    const url = `/AirQuality/GetAQIForVisibleCities?north=${bounds._northEast.lat}&south=${bounds._southWest.lat}&east=${bounds._northEast.lng}&west=${bounds._southWest.lng}`;

    const response = await fetch(url);
    if (response != null) {
        const cities = await response.json();
        console.log(cities);
        markers.clearLayers();

        cities.forEach(city => {
            let color = getAqiColor(city.current.us_Aqi);

            let marker = L.circleMarker([city.latitude, city.longitude], {
                radius: 12,
                weight: 2,
                color: "#fff",
                fillColor: color,
                fillOpacity: 0.9
            })
                .bindTooltip(`${city.name} - AQI: ${city.current.us_Aqi}`)
                .addTo(markers);
        });
    }
   

    //cities.forEach(c => {
    //    var color = getAqiColor(c.current.us_Aqi);

    //    var icon = L.divIcon({
    //        html: `<div class="aq-marker" style="background:${color}">${c.current.us_Aqi}</div>`,
    //        className: "marker-aqi",
    //        iconSize: [35, 35]
    //    });

    //    L.marker([c.latitude, c.longitude], { icon: icon })
    //        .bindPopup(`<b>${c.name}</b><br>AQI: ${c.current.us_Aqi}`)
    //        .addTo(markers);
    //});
   
}

function getAqiColor(aqi) {
    if (0 < aqi && aqi <= 50) return 'green';
    if (aqi >= 51 && aqi <= 100) return 'yellow';
    if (aqi >= 101 && aqi <= 150) return 'orange';
    if (aqi >= 151 && aqi <= 200) return 'red';
    if (aqi >= 201 && aqi <= 300) return 'purple';
    return 'maroon';
}

// ---- Legend ----
//var legend = document.getElementById("legend");
//legend.innerHTML = `
//    <h4>AQI Legend</h4>
//    <div><span style="background:#6BCB77"></span> Good (0-50)</div>
//    <div><span style="background:#FFD93D"></span> Moderate (51-100)</div>
//    <div><span style="background:#FF6B6B"></span> Unhealthy for Sensitive (101-150)</div>
//    <div><span style="background:#C44536"></span> Unhealthy (151-200)</div>
//    <div><span style="background:#8D5A97"></span> Very Unhealthy (201-300)</div>
//    <div><span style="background:#6A040F"></span> Hazardous (300+)</div>
//`;
