function LoadProducts(id) {
    var http = new XMLHttpRequest();

    http.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var obj = JSON.parse(this.responseText);

            var resultObject = obj.result;

            if (resultObject != undefined || resultObject != null) {

                document.getElementById("hiddenQuantity").hidden = false;
                document.getElementById("description").innerHTML = resultObject.description;
                document.getElementById("unitprice").innerHTML = resultObject.unitPrice;
                document.getElementById("quantity").disabled = false;
                document.getElementById("quantityInStock").innerHTML = resultObject.quantity;

            } else {

                document.getElementById("description").innerHTML = "Product not found!";
                document.getElementById("unitprice").innerHTML = "Product not found!";
                document.getElementById("hiddenQuantity").hidden = true;
                console.log("No product found!");

            }
        }
    };

    http.open("GET", "../product/GetProductById/" + parseInt(id), true);
    http.send();

}