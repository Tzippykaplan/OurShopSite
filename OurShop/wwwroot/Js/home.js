
const API_URL = '/api/users';

const getDataFromDocument = (divId) => {
    const div = document.getElementById(divId);  
    const inputs = div.querySelectorAll('input'); 
    const formData = {};
    inputs.forEach(input => {
        formData[input.id] = input.value; 
    });

    return formData;
}
const createUser = async () => {
    const user = getDataFromDocument('sign');
    try {
        const responsePost = await fetch( API_URL , {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        if (responsePost.status==409) {

            alert("user name already exixst")
        }
        else if (!responsePost.ok) { 
           
            alert("Error,plese try again")}
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

const showSignUp = () => {
    const signUpDiv = document.getElementById("sign");
    signUpDiv.className = "show";
}


const login = async () => {
    const data = getDataFromDocument('login');
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
         window.location.href = "Products.html"
        }
    }
    catch (error) {
        console.log(error)
    }
}

const updateUser = async () => {
    const user = getDataFromDocument();
    try {
        const UserId = sessionStorage.getItem("UserId")
        const responsePut = await fetch(`${API_URL }/${UserId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });

        if (!responsePut.ok)
            alert("Eror,please try again")
        else {
            const dataPut = await responsePut.json();
            alert(`${dataPut.firstName} updated `)
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