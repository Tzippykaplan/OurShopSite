
const API_URL = '/api/Users';

const getDataFromDocument = (divId) => {
    const div = document.getElementById(divId);  
    const inputs = div.querySelectorAll('input'); 
    const formData = {};
    inputs.forEach(input => {
        formData[input.id] = input.value; 
    });

    return formData;
}
const validateEmail = (email) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}
const validateRequiredFields = (formData, requiredFields) => {
    for (const field of requiredFields) {
        if (!formData[field] || formData[field].trim() === '') {
            return false;
        }
    }
    return true;
}
const createUser = async () => {
    const user = getDataFromDocument('sign');
    const requiredFields = ['firstName', 'lastName', 'email', 'password'];

    if (!validateRequiredFields(user, requiredFields)) {
        alert("All fields are required");
        return;
    }

    if (!validateEmail(user.email)) {
        alert("Invalid email format");
        return;
    }
    const passWordStrength = await checkPasswordStrength(user.password)
    if (passWordStrength > 3) {
        try {
            const responsePost = await fetch(API_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });
            if (responsePost.status == 409) {

                alert("user name already exixst")
            }
            else if (!responsePost.ok) {

                alert("Error,plese try again")
            }
            else {
                const createdUser = await responsePost.json();
                alert(`User ${createdUser.firstName} created successfully!`);
            }
            checkPasswordStrength(user.password)

        }
        catch (error) {
            console.error('Error creating user:', error);
            alert("An error occurred while creating the user.");
        }
    }
    else {
        alert("password is weak");
    }
}

const showSignUp = () => {
    const signUpDiv = document.getElementById("sign");
    signUpDiv.className = "show";
}


const login = async () => {
    const data = getDataFromDocument('login');
    const requiredFields = ['emailLogin', 'passwordLogin'];

    if (!validateRequiredFields(data, requiredFields)) {
        alert("All fields are required");
        return;
    }

    if (!validateEmail(data.emailLogin)) {
        alert("Invalid email format");
        return;
    }
    try {
        const responsePost = await fetch(`${API_URL}/login/?email=${data.emailLogin}&password=${data.passwordLogin}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                email: data.email,
                password: data.password
            }
        });
        if (responsePost.status==204)
        alert("User not found")
        if (!responsePost.ok) 
        alert("Eror,please try again")
        else {
            const userData = await responsePost.json(); 
            sessionStorage.setItem("UserId", userData.userId)
            sessionStorage.setItem("userName", userData.firstName)
            alert(`${userData.firstName} login `)
            const cart = JSON.parse(sessionStorage.getItem("cart")||"[]")
            cart.length!=0 ? window.location.href = "Products.html?fromShoppingBag=1" : window.location.href = "Products.html"
        }
    }
    catch (error) {
        console.log(error)
    }
}


const  checkPasswordStrength = async ( password) => {
    try {
        const passwordStrength = await fetch(`${API_URL}/passwordStrength?password=${password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                password: password
            }
        });
        const p = await passwordStrength.json();
        return p;
 
       
    }
    catch (error) {
        console.log(error)
    }
}
const fillProgress = async ()=>{
    const progress = document.getElementById("progress")
    const password = document.getElementById("password").value
    if (!password) {
        progress.value = 0;
        return;
    }
    const passwordStrength = await checkPasswordStrength(password);
    progress.value = passwordStrength;
   
}