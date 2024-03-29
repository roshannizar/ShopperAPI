﻿var products = [];
var total = 0;
var grandTotal = 0;
var rowCount = 2;
var d = new Date();
var temp = JSON.stringify(d);
var currentDate = temp.replace(/^"(.*)"$/, '$1');
const spinner = document.getElementById("spinner");

function CalculateTotal(quantity) {
    var quantity = document.getElementById("quantity").value;
    var unitprice = document.getElementById("unitprice").innerHTML;
    var quantityInStock = document.getElementById("quantityInStock").innerHTML;

    var total = parseFloat(unitprice) * parseInt(quantity);

    if (parseInt(quantity) > parseInt(quantityInStock)) {
        document.getElementById("quantity").value = quantityInStock;
        alert("You are extending the limit of the stock available!");
    } else {
        document.getElementById("totalamount").innerHTML = total;
    }
}

function CreateOrderLine() {
    var temp = document.getElementById("productId");
    var productId = temp.options[temp.selectedIndex].value;
    var productName = temp.options[temp.selectedIndex].textContent;
    var description = document.getElementById("description").innerHTML;
    var unitPrice = document.getElementById("unitprice").innerHTML;
    var quantity = document.getElementById("quantity").value;

    var orderLine = {
        productId: 0,
        productName: "",
        unitPrice: 0,
        quantity: 0,
        orderId: 0
    }

    if (products) {
        orderLine.productId = parseInt(productId);
        orderLine.unitPrice = parseInt(unitPrice);
        orderLine.quantity = parseInt(quantity);
        orderLine.productName = productName;
        orderLine.orderId = 0;
        var exist = false;

        for (var i = 0; i < products.length; i++) {

            var existing = products[i].productId;

            if (parseInt(existing) === parseInt(productId)) {

                var tempQuantity = parseInt(products[i].quantity);
                var existingQuantity = parseInt(quantity);
                var tempTotal = tempQuantity + existingQuantity;
                var unitPrice = products[i].unitPrice;
                document.getElementById("qty" + (i + 2)).value = tempTotal;
                document.getElementById("tableForm").rows[i + 2].cells[4].innerHTML = (parseInt(tempTotal) * parseInt(unitPrice));
                products[i].quantity = parseInt(tempTotal);
                exist = true;
                break;
            }

        }

        if (products.length == 0) {
            products.push(orderLine);
            CreateTableRow(productName, description, unitPrice, quantity);
            rowCount++;
        } else if (exist == false) {
            products.push(orderLine);
            CreateTableRow(productName, description, unitPrice, quantity);
            rowCount++;
        }

        total = total + (parseInt(quantity) * parseInt(unitPrice));

        document.getElementById("grandtotal").innerHTML = "Rs: " + total;
        document.getElementById("productId").value = 0;
        document.getElementById("description").innerHTML = "Description";
        document.getElementById("unitprice").innerHTML = "Rs: 50";
        document.getElementById("quantity").value = "";
        document.getElementById("hiddenQuantity").hidden = true;
    } else {
        console.log("Product is empty!");
    }
}

function showSpinner() {
    spinner.style.visibility = 'visible';
    spinner.style.display = 'inline';
    spinner.style.opacity = '0.5';
}

function closeSpinner() {
    spinner.style.visibility = 'hidden';
    spinner.style.display = 'none';
}

function ConfirmOrder() {
    showSpinner();
    var customerId = document.getElementById("customerId").value;
    var date = new Date();
    var currentDate = JSON.stringify(date);
    var order = {
        CustomerId: customerId,
        Date: currentDate,
        OrderItems: products
    }

    if (customerId != 0 && products.length != 0) {

        order.CustomerId = parseInt(customerId);
        order.Date = currentDate.replace(/^"(.*)"$/, '$1');

        $.ajax({
            url: 'http://localhost:21021/api/v1/Order',
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(order),
            processData: false,
            success: function (data, textStatus, jQxhr) {
                console.log(data, textStatus, jQxhr);
                closeSpinner();
                location.replace('https://localhost:62114/Order/Get');
            },
            error: function (jqXhr, textStatus, errorThrown) {
                console.log(jqXhr, textStatus, errorThrown);
                closeSpinner();
            }
        });

    } else {
        alert("Choose a customer");
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
    deleteBtn.classList.add("margin-left");

    editBtn.setAttribute('type', 'button');
    editBtn.setAttribute('value', 'Edit');
    editBtn.classList.add("btn");
    editBtn.classList.add("btn-sm");
    editBtn.classList.add("btn-warning");

    quantityText.setAttribute('type', 'number');
    quantityText.setAttribute('id', 'qty' + rowCount);
    quantityText.classList.add('form-control');
    quantityText.classList.add('col-md-6');
    quantityText.disabled = true;

    var row = table.insertRow(rowCount);
    var productNameCell = row.insertCell(0);
    var descriptionCell = row.insertCell(1);
    descriptionCell.colSpan = 2;
    var unitpriceCell = row.insertCell(2);
    var quantityCell = row.insertCell(3);
    var totalCell = row.insertCell(4);
    var editBtnCell = row.insertCell(5);

    editBtnCell.appendChild(deleteBtn);
    editBtnCell.appendChild(editBtn);
    quantityCell.appendChild(quantityText);

    productNameCell.innerHTML = productName;
    descriptionCell.innerHTML = description;
    unitpriceCell.innerHTML = "Rs: " + unitPrice;
    quantityText.value = quantity;
    totalCell.innerHTML = "Rs: " + parseInt(quantity) * parseInt(unitPrice);
    editBtnCell.appendChild(editBtn);
} 