
window.addEventListener("load", loadPage=()=>{
    getProducts();
    getCategories();
});
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
let categoryIds = [1];

const addCategoryId = (categoryId) => {
    categoryIds.push(categoryId);
    console.log(categoryIds)
    getProducts(categoryIds);
}

const removeCategoryId = (categoryId) => {
    categoryIds = categoryIds.filter(category => category != categoryId)
    console.log(categoryIds)
    getProducts(categoryIds);

}
const findByDesc = document.querySelector("#nameSearch");
let desc = "";
const filterProducts = () => {
   desc= document.querySelector("#nameSearch").value
    console.log(desc)
    getProducts(categoryIds,desc);
}
