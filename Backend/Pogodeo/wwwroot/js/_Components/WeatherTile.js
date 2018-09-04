(function () {

    Vue.component('weather-tile', {
        template: '#weatherTileTemplate',
        props: ['weather'],
        data: function () {
            return {
                weatherData: {
                    Name: "",
                    Value: ""
                }
            }
        },

        computed: {
            logoPath: function () {
                return "/images/Logos/" + this.weatherData.Name + ".png";
            }
        },

        mounted: function () {
            this.weatherData.Name = this.$props.weather.Key;
            this.weatherData.Value = this.$props.weather.Value.ValueTemperature;
        },

        methods: {
        }
    })

})();