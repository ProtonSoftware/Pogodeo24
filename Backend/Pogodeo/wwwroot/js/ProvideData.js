(function () {

    var ProvideData = new Vue({
        el: '#provideData',
        template: '#provideDataTemplate',

        methods: {
            SubmitForm: function () {
                this.$refs.InputForm.submit();
            }
        }
    })

})();