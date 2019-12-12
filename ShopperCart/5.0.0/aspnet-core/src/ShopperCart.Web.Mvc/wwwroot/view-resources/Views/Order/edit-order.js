var TempProduct = [];
var breakCount = 0;
var rowCount = 1;
const spinner = document.getElementById("spinner");

function showSpinner() {
    spinner.style.visibility = 'visible';
    spinner.style.display = 'inline';
    spinner.style.opacity = '0.5';
}

function closeSpinner() {
    spinner.style.visibility = 'hidden';
    spinner.style.display = 'none';
}

function EditOrders(id) {

    var orderId = document.getElementById("orderid " + id).innerHTML;
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
    var quantity = document.getElementById("quantity " + id).innerHTML;
    var deleteBtn = document.getElementById("DeleteBtn " + id);

    var orderItems = {
        Id: 0,
        ProductId: 0,
        UnitPrice: 0,
        Quantity: 0,
        OrderId: 0,
        IsDeleted:true
    };

    if (deleteBtn.value === "Delete") {
        orderItems.Id = id;
        orderItems.ProductId = parseInt(productId);
        orderItems.OrderId = parseInt(orderId);
        orderItems.Quantity = parseInt(quantity);
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
        OrderId: 0,
        IsDeleted:false
    };

    orderItems.Id = parseInt(document.getElementById("id").value);
    orderItems.ProductId = parseInt(document.getElementById("productid").value);
    orderItems.UnitPrice = parseInt(document.getElementById("UnitPrice").value);
    orderItems.Quantity = parseInt(document.getElementById("Quantity").value);
    orderItems.OrderId = parseInt(document.getElementById("orderId").value);
    document.getElementById("updatedQuantity " + orderItems.Id).textContent = "Quantity: " + orderItems.Quantity;
    document.getElementById("updateTotalAmount " + orderItems.Id).textContent = "Total Amount: Rs: " +
        (parseInt(document.getElementById("Quantity").value) *
            parseInt(document.getElementById("UnitPrice").value));
    document.getElementById("EditBtn " + orderItems.Id).value = "Undo Edit";

    var exist = false;

    for (var i = 0; i < TempProduct.length; i++) {

        var existing = TempProduct[i].ProductId;

        if (parseInt(existing) === parseInt(orderItems.ProductId)) {

            var tempQuantity = parseInt(TempProduct[i].Quantity);
            var existingQuantity = parseInt(orderItems.Quantity);
            var tempTotal = tempQuantity + existingQuantity;
            TempProduct[i].Quantity = parseInt(tempTotal);
            exist = true;
            break;
        }

    }

    if (TempProduct.length == 0) {
        TempProduct.push(orderItems);
    } else if (exist == false) {
        TempProduct.push(orderItems);
    }

    document.getElementById("SaveChanges").hidden = false;
}

function Confirm() {

    var date = new Date();
    var currentDate = JSON.stringify(date);
    var customerId = document.getElementById("customerId").innerHTML;
    var orderIdTemp = document.getElementById("orderIdTemp").value;

    var order = {
        Id: 0,
        CustomerId: 0,
        Date: null,
        OrderItems: TempProduct
    }

    order.Id = parseInt(orderIdTemp);
    order.CustomerId = parseInt(customerId);
    order.Date = currentDate.replace(/^"(.*)"$/, '$1');

    showSpinner();

    $.ajax({
        url: 'http://localhost:21021/api/v1/Order',
        dataType: 'json',
        type: 'put',
        contentType: 'application/json',
        data: JSON.stringify(order),
        processData: false,
        success: function (data, textStatus, jQxhr) {
            console.log(data, textStatus, jQxhr);
            closeSpinner();
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(jqXhr, textStatus, errorThrown);
            closeSpinner();
        }
    });
}

function EraseChanges() {
    TempProduct = [];
    document.getElementById("SaveChanges").hidden = true;
    alert("Save changes removed successfully");
}

function PlaceNewOrder(id) {

    var temp = document.getElementById("productId");
    var productId = temp.options[temp.selectedIndex].value;
    var productName = temp.options[temp.selectedIndex].textContent;
    var description = document.getElementById("descriptionText").value;
    var unitprice = document.getElementById("UnitPriceText").value;
    var quantity = document.getElementById("QuantityText").value;
    var ItemCount = document.getElementById("orderItemCount").innerHTML;

    var orderItems = {
        Id: 0,
        ProductId: 0,
        UnitPrice: 0,
        Quantity: 0,
        OrderId: 0,
        IsDeleted: false
    };

    orderItems.ProductId = parseInt(productId);
    orderItems.UnitPrice = parseInt(unitprice);
    orderItems.Quantity = parseInt(quantity);
    orderItems.OrderId = parseInt(id);

    var exist = false;
    var count = 0;
    var update = false;

    for (i = 0; i < parseInt(ItemCount); i++) {


        var divCollection = document.getElementById("panel1");

        var divProductId = divCollection.children[count].children[0].children[0].children["productid"].innerHTML;


        if (divProductId === productId) {
            var divId = divCollection.children[count].children[0].children[0].children["tempid"].innerHTML;
            var tempQuantity = document.getElementById("quantity " + divId).innerHTML;
            orderItems.Quantity = orderItems.Quantity + parseInt(tempQuantity);
            orderItems.ProductId = parseInt(divProductId);
            orderItems.Id = parseInt(divId);
            document.getElementById("quantity " + divId).innerHTML = orderItems.Quantity;
            document.getElementById("updatedQuantity " + divId).innerHTML = "Quantity: " + orderItems.Quantity;
            update = true;
            break;
        }

        count += 2;
    }

    for (var i = 0; i < TempProduct.length; i++) {

        var existing = TempProduct[i].ProductId;

        if (parseInt(existing) === parseInt(productId)) {

            var unitPriceValue = TempProduct[i].UnitPrice;
            var tempQuantity = parseInt(TempProduct[i].Quantity);
            var existingQuantity = parseInt(quantity);
            var tempTotal = tempQuantity + existingQuantity;

            if (update == false) {
                document.getElementById("qty" + (i + 1)).value = tempTotal;
                document.getElementById("tableForm").rows[i + 1].cells[4].innerHTML = "Rs: " + (parseInt(tempTotal) * parseInt(unitPriceValue));
            }
            TempProduct[i].Quantity = parseInt(tempTotal);
            exist = true;
            break;
        }
    }

    if (update == true && exist == false) {
        TempProduct.push(orderItems);
    }
    else if (TempProduct.length == 0) {
        TempProduct.push(orderItems);
        CreateTableRow(productName, description, unitprice, quantity);
        rowCount++;
    } else if (exist == false) {
        TempProduct.push(orderItems);
        CreateTableRow(productName, description, unitprice, quantity);
        rowCount++;
    }
}

function CreateTableRow(productName, description, unitPrice, quantity) {

    var table = document.getElementById("tableForm");
    var deleteBtn = document.createElement("input");
    var editBtn = document.createElement("input");
    var quantityText = document.createElement("input");

    deleteBtn.setAttribute('type', 'button');
    deleteBtn.setAttribute('value', 'Delete');
    deleteBtn.classList.add("btn");
    deleteBtn.classList.add("btn-sm");
    deleteBtn.classList.add("btn-danger");

    editBtn.setAttribute('type', 'button');
    editBtn.setAttribute('value', 'Edit');
    editBtn.classList.add("btn");
    editBtn.classList.add("btn-sm");
    editBtn.classList.add("btn-warning");
    deleteBtn.classList.add("margin-left");

    quantityText.setAttribute('type', 'number');
    quantityText.setAttribute('id', 'qty' + rowCount);
    quantityText.classList.add('form-control');
    quantityText.classList.add('col-md-6');
    quantityText.disabled = true;

    if (breakCount == 0) {
        var row = table.insertRow(0);
        var lineBreak = document.createElement("span");
        lineBreak.classList.add("badge");
        lineBreak.classList.add("label-primary");
        var breakerCell = row.insertCell(0);
        breakerCell.appendChild(lineBreak);
        breakerCell.innerHTML = "Newly Added Item(s)";
    }
    breakCount++;

    var row = table.insertRow(rowCount);
    var productNameCell = row.insertCell(0);
    var descriptionCell = row.insertCell(1);
    descriptionCell.colSpan = 2;
    var unitpriceCell = row.insertCell(2);
    var quantityCell = row.insertCell(3);
    var totalCell = row.insertCell(4);
    var editBtnCell = row.insertCell(5);
    var deleteBtnCell = row.insertCell(6);

    deleteBtnCell.appendChild(deleteBtn);
    editBtnCell.appendChild(editBtn);
    quantityCell.appendChild(quantityText);

    productNameCell.innerHTML = productName;
    descriptionCell.innerHTML = description;
    unitpriceCell.innerHTML = "Rs: " + unitPrice;
    quantityText.value = quantity;
    totalCell.innerHTML = "Rs: " + parseInt(quantity) * parseInt(unitPrice);
    editBtnCell.appendChild(editBtn);
}   