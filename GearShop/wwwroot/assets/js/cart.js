
$(document).ready(function () {
    $(document).on('click', '.addToCart', function () {
        var productId = $(this).data("id");
        $.ajax({
            url: "/Customer/Cart/AddItem",
            data: {
                productID: productId
            },
            success: function (data) {
                console.log(data);
                Swal.fire({
                    title: "Add To Cart Successfully",
                    width: 600,
                    padding: "3em",
                    iconColor: "#716add",
                    background: "#fff url(https://images6.alphacoders.com/130/1301340.jpg)",
                    backdrop: `rgba(0,0,123,0.4)
                                       url("/images/nyncat.gif")
                                       left top
                                       no-repeat`,
                    showConfirmButton: false,
                    timer: 1200
                });
                $("#count-cart").html(data.quantity); // Assuming there is an element with ID 'count-cart' to display the quantity
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                    footer: '<a href="#">Why do I have this issue?</a>'
                });
            }
        });
    });
});

