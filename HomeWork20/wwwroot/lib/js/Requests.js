function customRequest(type, id, isEmptyField, url) {
    let bool = isEmptyField;
    let index = id;
    var t = type;
    var data;
    if (type === "DELETE") {
        t = 'DELETE';
        data = { id: index };
    }
    
    if (type === "PUT") {
        t = 'PUT';
        data = {
            id: id,
            Surname: document.getElementById("updSurname_" + index).value,
            FirstName: document.getElementById("updFirstName_" + index).value,
            Patronymic: document.getElementById("updPatronymic_" + index).value,
            PhoneNumber: document.getElementById("updPhoneNumber_" + index).value,
            Adress: document.getElementById("updAdress_" + index).value
        };

        if (bool != true) {
            data.Descriptions = document.getElementById("updDescriptions_" + index).value;
        }
    }

    fetch(url, {
        method: t,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(res => {
            if (res.ok) { console.log(t + " request successful") }
            else { console.log(t + " request unsuccessful") }
            return res
        })
        .then(res => res.json())
        .then(data => data)
        .catch(error => error)
};