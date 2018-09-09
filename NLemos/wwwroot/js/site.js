$(function () {
    expandImages();
    bindSearch();
    
    function expandImages() {
        $('.img-expansible').on('click', function () {
            $('.img-expansible-modal').html($(this).clone());
            $('#imgExpansibleModal').modal('show');
        });
    }

    function bindSearch() {
        $('#btnSearch').on('click', searchPosts);

        $("#txtSearch").on('keyup', function (e) {
            if (e.keyCode === 13) {
                searchPosts();
            }
        });
    }

    function searchPosts() {
        window.location = '/Search/' + $('#txtSearch').val();
    }
});