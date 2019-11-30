var TempProduct = [];

function EditOrders(id) {

    var orderId = document.getElementById("orderid "+id).innerHTML;
    var productId = document.getElementById("productid " + id).innerHTML;
    var productName = document.getElementById("productName " + id).innerHTML;
    var description = document.getElementById("description " + id).innerHTML;
    var unitPrice = document.getElementById("unitprice " + id).innerHTML;
    var quantity = document.getElementById("quantity " + id).innerHTML;
    var editBtn = document.getElementById("EditBtn " + id);


    if (editBtn.value === "Edit") {
        document.getElementById("productid").value = productId;
        document.getElementById("id").value = id;
        document.getElementById("orderId").value = orderId;
        document.getElementById("Name").value = productName;
        document.getElementById("description").value = description;
        document.getElementById("UnitPrice").value = unitPrice;
        document.getElementById("Quantity").value = quantity;
        editBtn.value = "Undo Edit";
    } else {
        editBtn.value = "Edit";
    }
}

function DeleteOrderItem(id) {
    var orderId = document.getElementById("orderid " + id).innerHTML;
    var productId = document.getElementById("productid " + id).innerHTML;
    var unitPrice = document.getElementById("unitprice " + id).innerHTML;
    var deleteBtn = document.getElementById("DeleteBtn " + id);

    var orderItems = {
        Id: 0,
        ProductId: 0,
        UnitPrice: 0,
        Quantity: 0,
        OrderId: 0
    };

    if (deleteBtn.value === "Delete") {
        orderItems.Id = id;
        orderItems.ProductId = parseInt(productId);
        orderItems.OrderId = parseInt(orderId);
        orderItems.Quantity = 0;
        orderItems.UnitPrice = parseInt(unitPrice);
        TempProduct.push(orderItems);
        deleteBtn.value = "Undo Delete";
        document.getElementById("SaveChanges").hidden = false;
        document.getElementById("EditBtn " + id).hidden = true;
    } else {
        deleteBtn.value = "Delete";
    }
}

function ConfirmOrderChanges() {

    var orderItems = {
        Id: 0,
        ProductId: 0,
        UnitPrice: 0,
        Quantity: 0,
        OrderId: 0
    };

    orderItems.Id = parseInt(document.getElementById("id").value);
    orderItems.ProductId = parseInt(document.getElementById("productid").value);
    orderItems.UnitPrice = parseInt(document.getElementById("UnitPrice").value);
    orderItems.Quantity = parseInt(document.getElementById("Quantity").value);
    orderItems.OrderId = parseInt(document.getElementById("orderId").value);
    document.getElementById("updatedQuantity " + orderItems.Id).textContent = "Quantity: " + orderItems.Quantity;
    document.getElementById("updateTotalAmount " + orderItems.Id)
        .textContent = "Total Amount: Rs: " + (parseInt(document.getElementById("Quantity").value) *
            parseInt(document.getElementById("UnitPrice").value));
    document.getElementById("EditBtn " + orderItems.Id).value = "Undo Edit";

    TempProduct.push(orderItems);

    document.getElementById("SaveChanges").hidden = false;
}

function Confirm() {

    $.ajax({
        url: '../OrderEdit',
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(TempProduct),
        processData: false,
        success: function (data, textStatus, jQxhr) {
            console.log(data, textStatus, jQxhr);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(jqXhr, textStatus, errorThrown);
        }
    });
}