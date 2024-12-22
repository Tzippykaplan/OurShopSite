
window.addEventListener("load", loadPage=()=>{
    getProducts();
    getCategories();
});

const getProducts=async()=> {
    try {
        const responseGet = await fetch(`/api/Products/`, {
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
        categoryItem.querySelector("label").querySelector(".OptionName").textContent = category.categoryName
        categoryContainer.appendChild(categoryItem)

    })
}