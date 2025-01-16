let categoryIds = [];
let quantity = 0;
window.addEventListener("load", () => {
    initializePage();
});
const initializePage = () => {
    getProducts();
    getCategories();
    handleShoppingBag();
}
const handleShoppingBag = () => {
    const UrlParams = new URLSearchParams(window.location.search);
    const fromShoppingBag = UrlParams.get("fromShoppingBag")
    if (fromShoppingBag === "1") {
        setBagSum();
    }
    else {
        setTextBagSum()
    }
}

const getProducts = async (categoryIds = [], desc = "", minPrice = 0, maxPrice = 0) => {

    const params = buildSearchParams(categoryIds, desc, minPrice, maxPrice);
    try {
        const responseGet = await fetch(`/api/Products?${params}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!responseGet.ok)
            alert("Eror,please try again")
        else {
            drawProducts(await responseGet.json());
        }
    }
    catch (error) {
        console.log(error)
    }
}
const buildSearchParams = (categoryIds, desc, minPrice, maxPrice) => {
    const params = new URLSearchParams();
    if (desc) params.append("desc", desc);
    if (minPrice) params.append("minPrice", minPrice);
    if (maxPrice) params.append("maxPrice", maxPrice);
    categoryIds.forEach(id => params.append("categoryIds", id));
    return params;
}
const getCategories = async () => {
    try {
        const responseGet = await fetch(`/api/Categories/`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!responseGet.ok)
            alert("Eror,please try again")
        else {
            drawCategories(await responseGet.json());
        }
    }
    catch (error) {
        console.log(error)
    }

}

const drawProducts = (productList) => {
    const tempCard = document.querySelector("template#temp-card")
    const productContainer = document.querySelector("#ProductList")
    productContainer.innerHTML = ""
    productList.forEach(product => {
        const clone = tempCard.content.cloneNode(true);
        clone.querySelector(".img-w").querySelector("img").src = `../${product.imageUrl}`
        clone.querySelector("h1").textContent = product.productName
        clone.querySelector(".price").textContent = product.price
        clone.querySelector("p > button").addEventListener("click", () => addToCart(product))
        productContainer.appendChild(clone)

    })
}
const drawCategories = (categoryList) => {
    const tempCategory = document.querySelector("template#temp-category")
    const categoryContainer = document.querySelector("#categoryList")
    categoryList.forEach(category => {
        const categoryItem = tempCategory.content.cloneNode(true);
        categoryItem.querySelector(".opt").addEventListener("change", (e) => {
            e.target.checked ? addCategoryId(category.categoryId) : removeCategoryId(category.categoryId)
        })
        categoryItem.querySelector("label").querySelector(".OptionName").textContent = category.categoryName
        categoryContainer.appendChild(categoryItem)

    })
}

const addCategoryId = (categoryId) => {
    categoryIds.push(categoryId);
    filterProducts();
}

const removeCategoryId = (categoryId) => {
    categoryIds = categoryIds.filter(category => category != categoryId)
    filterProducts();

}

const filterProducts = () => {
    const minPrice = document.querySelector("#minPrice").value
    const maxPrice = document.querySelector("#maxPrice").value
    const desc = document.querySelector("#nameSearch").value
    getProducts(categoryIds, desc, minPrice, maxPrice);
}

const addToCart = (product) => {
    cart = JSON.parse(sessionStorage.getItem("cart")) || []
    currentproduct = cart.find(item => item.product.productId == product.productId)
    currentproduct ? currentproduct.amount = currentproduct.amount += 1 :
        cart.push({ product: product, amount: 1 })
    sessionStorage.setItem("cart", JSON.stringify(cart));
    quantity += 1
    setTextBagSum()
}
const setBagSum = () => {
    const cart = JSON.parse(sessionStorage.getItem("cart") || "[]")
    quantity = cart.reduce((sum, item) => sum + item.amount, 0);
    setTextBagSum()
}
const setTextBagSum = () => {
    document.querySelector("#ItemsCountText").textContent = quantity
}
