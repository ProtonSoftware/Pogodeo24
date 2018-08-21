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
            this.weatherData.Name = this.$props.weather.WeatherProviderAPIName;
            this.weatherData.Value = this.$props.weather.Celsius;
        },

        methods: {
        }
    })

})();