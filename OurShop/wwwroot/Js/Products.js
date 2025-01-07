
window.addEventListener("load", loadPage=()=>{
    getProducts();
    getCategories();
    const UrlParams = new URLSearchParams(window.location.search);
    const fromShoppingBag = UrlParams.get("fromShoppingBag")
    if (fromShoppingBag === "1") {
        setBagSum();
    }
    else {
         setTextBagSum()
    }
   
}
);
const getProducts = async (categoryIds = null, desc = null, minPrice = null, maxPrice = null) => {

    const params = new URLSearchParams();
    if (desc) params.append("desc", desc);
    if (minPrice) params.append("minPrice", minPrice);
    if (maxPrice) params.append("maxPrice", maxPrice);
    if (categoryIds && categoryIds.length > 0) {
        categoryIds.forEach(id => params.append("categoryIds", id));
    }
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
    productContainer.innerHTML=""
    console.log(productList)
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
    console.log(categoryList)
    categoryList.forEach(category => {
        const categoryItem = tempCategory.content.cloneNode(true);
        categoryItem.querySelector(".opt").addEventListener("change", (e) => {
            e.target.checked ? addCategoryId(category.categoryId) : removeCategoryId(category.categoryId)
        })
        categoryItem.querySelector("label").querySelector(".OptionName").textContent = category.categoryName
        categoryContainer.appendChild(categoryItem)

    })
}

let categoryIds = [];
let desc = "";
let minPrice = 0;
let maxPrice = 0
const addCategoryId = (categoryId) => {
    categoryIds.push(categoryId);
    getProducts(categoryIds, desc, minPrice, maxPrice);
}

const removeCategoryId = (categoryId) => {
    categoryIds = categoryIds.filter(category => category != categoryId)
    getProducts(categoryIds, desc, minPrice, maxPrice);

}

const filterProducts = () => {
    minPrice = document.querySelector("#minPrice").value
    maxPrice = document.querySelector("#maxPrice").value
    desc= document.querySelector("#nameSearch").value
    console.log(desc)
    getProducts(categoryIds,desc,minPrice,maxPrice);
}
let quantity = 0
const addToCart = (product) => {
    cart = JSON.parse(sessionStorage.getItem("cart"))||[]
    currentproduct = cart.find(item => item.product.productId == product.productId)
    currentproduct ? currentproduct.amount = currentproduct.amount+=1 :
         cart.push({ product: product,amount:1 })
    sessionStorage.setItem("cart", JSON.stringify(cart));
    quantity += 1
    setTextBagSum()
}
const setBagSum = () => {
    const cart = JSON.parse(sessionStorage.getItem("cart") || "[]")
    cart.forEach(item => {
        quantity += item.amount
    })
    setTextBagSum()
}
    const setTextBagSum = () => {
        document.querySelector("#ItemsCountText").textContent = quantity  
    }
