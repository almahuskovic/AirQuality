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
    if (response.ok) {
        const cities = await response.json();
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
}

function getAqiColor(aqi) {
    if (0 < aqi && aqi <= 50) return 'green';
    if (aqi >= 51 && aqi <= 100) return 'yellow';
    if (aqi >= 101 && aqi <= 150) return 'orange';
    if (aqi >= 151 && aqi <= 200) return 'red';
    if (aqi >= 201 && aqi <= 300) return 'purple';
    return 'maroon';
}

const searchInput = document.getElementById("city-search");
const resultsList = document.getElementById("searchResults");
const inputGroup = document.getElementById("input-group");
const searchForm = searchInput.closest('form');
let searchTimeout = null;

searchInput.addEventListener("input", function () {
    const query = this.value.trim();
    clearTimeout(searchTimeout);

    if (query.length < 2) {
        resultsList.innerHTML = "";
        return;
    }

    searchTimeout = setTimeout(async () => {
        const response = await fetch(`/City?name=${query}`);
        const data = await response.json();
        resultsList.innerHTML = "";

        data.forEach(city => {
            const item = document.createElement("button");
            item.className = "list-group-item list-group-item-action";
            item.innerText = city.name + ', ' + city.country;

            item.addEventListener("click", (e) => {
                e.preventDefault();
                e.stopPropagation();
                map.flyTo([city.latitude, city.longitude], 9, { animate: true, duration: 1.2 });
                resultsList.innerHTML = "";
                searchInput.value = city.name;
            });

            resultsList.appendChild(item);
        });
    }, 300);
});

document.addEventListener('click', (e) => {
    if (!searchForm.contains(e.target)) {
        resultsList.style.display = 'none';
    }
});