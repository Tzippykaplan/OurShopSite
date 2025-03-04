const API_URL = '/api/users';
const welcomeText = () => {
    const welcomeText = document.getElementById("welcome")
    welcomeText.textContent = `Hi ${sessionStorage.getItem("userName")},you'v connected successfuly! lets dive in...`
}
welcomeText();
const getDataFromDocument = () => {
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const email = document.querySelector("#email").value;
    const password = document.querySelector("#password").value;
    return { firstName, lastName, email, password }
}

const showUpdate = async() => {
    const updatepDiv = document.getElementById("update")
    updatepDiv.className = "show"
    try {
        const UserId = sessionStorage.getItem("UserId")
        const responseGet = await fetch(`${API_URL}/${UserId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (responseGet.ok) {
            const dataGet = await responseGet.json();
            document.querySelector("#firstName").value = dataGet.firstName;
            document.querySelector("#lastName").value = dataGet.lastName;
            document.querySelector("#email").value = dataGet.email;
        }
    }
    catch (error) {
        console.log(error)
    }
}

const updateUser = async () => {
    const user = getDataFromDocument();
    const passWordStrength = await checkPasswordStrength(user.password)
    if (passWordStrength > 3) {
        try {
            const UserId = sessionStorage.getItem("UserId")
            const responsePut = await fetch(`${API_URL}/${UserId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            });
            if (responsePut.status == 204)
                alert("User nor found")
            if (responsePut.ok) {
                sessionStorage.setItem("userName", user.firstName)
                welcomeText();
                alert(`User updated successfully`);

            }
        }
        catch (error) {
            console.log(error)
        }
    }
    else
    alert("password is weak");

}
const fillProgress = async () => {
    const progress = document.getElementById("progress")
    const password = document.getElementById("password").value
    if (!password) {
        progress.value = 0;
        return;
    }
    const passwordStrength = await checkPasswordStrength(password);
    progress.value = passwordStrength;

}
const checkPasswordStrength = async (password) => {
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
const goTOProducts=() => {
    window.location.href = "Products.html?fromShoppingBag=1"
}
 