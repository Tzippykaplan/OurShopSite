window.addEventListener("load", loadPage = () => {
    getCartItens();
    setQuantityAndTotalPrice()

});
let totalPrice = 0
let quantity = 0
setQuantityAndTotalPrice = () => {
    const cart = JSON.parse(sessionStorage.getItem("cart") || "[]")
    cart.forEach(item => {
        totalPrice += (item.product.price * item.amount)
        quantity += item.amount
    })
    setTextQuantityAndTotalPrice();
}
const setTextQuantityAndTotalPrice = () => {
    console.log(quantity)
    console.log(totalPrice)
    document.querySelector("#itemCount").textContent = quantity;
    document.querySelector(".totalAmount").textContent = totalPrice;
}

const getCartItens = () => {
    drawCartItens(JSON.parse(sessionStorage.getItem("cart")) || [])
}
const drawCartItens = (productList) => {
    const template = document.querySelector("#temp-row");
    const tableBody = document.querySelector("#items tbody")
    tableBody.innerHTML = ""
    console.log(productList)
    productList.forEach(item => {

        const cartItem = template.content.cloneNode(true);
        cartItem.querySelector(".imageColumn .image").style.backgroundImage = `url("../${encodeURI(item.product.imageUrl)}")`;
        cartItem.querySelector(".imageColumn a").href = item.product.imageUrl;
        cartItem.querySelector(".descriptionColumn .itemName").textContent = item.product.productName;
        cartItem.querySelector(".descriptionColumn .itemNumber").textContent = item.amount;
        cartItem.querySelector(".totalColumn.delete .expandoHeight .price").textContent = Math.round(item.product.price * item.amount);;
        cartItem.querySelector(".totalColumn .expandoHeight .DeleteButton").addEventListener("click", (e) => { deleteFromCart(item.product) })
        tableBody.appendChild(cartItem);
    })
}
const deleteFromCart = (product) => {
    cart = JSON.parse(sessionStorage.getItem("cart")) || []
    currentproduct = cart.find(item => item.product.productId == product.productId)
    if (currentproduct)
        currentproduct.amount = currentproduct.amount -= 1;
    if (currentproduct.amount <= 0)
        cart = cart.filter(filterItem => filterItem.product.productId != product.productId);
    sessionStorage.setItem("cart", JSON.stringify(cart));
    quantity--
    totalPrice -= product.price;
    setTextQuantityAndTotalPrice()
    getCartItens();
}
const getTodaysDate = () => {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`
}
const checkUserAuthentication = () => {
    const userId = sessionStorage.getItem("UserId");
    if (!userId) {
        alert("You must log in or sign up");
        window.location.href = "home.html";
        return false;
    }
    return userId;
};
const getOrderDetails = () => {
    if (!checkUserAuthentication()) return null;
    const products = []
    const cart = JSON.parse(sessionStorage.getItem("cart"));
    if (!cart || cart.length === 0) {
        alert("Your cart is empty");
        return null;
    }
    const date = getTodaysDate()
    cart.forEach(item => {
        products.push({
            "quantity": item.amount,
            "productId": item.product.productId
        })
    })

    order = {
        "orderDate": date,
        "userId": sessionStorage.getItem("UserId"),
        "orderSum": totalPrice,
        "orderItems": products
    }
    return order;

}
const placeOrder = async () => {
    const order = getOrderDetails();
    if (!order) return;
    try {
        const responsePost = await fetch(`/api/Ordrs`, {
            method: 'Post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(order)
        });
        if (responsePost.BadRequest) {
            console.log(responsePost)
            alert("Eror,please try again")
        }
        if (!responsePost.ok)
            alert("Eror,please try again")
        else {
            const orderData = await responsePost.json();
            alert(`Order ${orderData.orderId} placed successfully!`);
            sessionStorage.removeItem('cart');
            location.reload();
        }
    }
    catch (error) {
        console.log(error)
    }

}