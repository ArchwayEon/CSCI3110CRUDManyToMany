"use strict";
(function _studentCourseRemove() {
    const formRemove =
        document.querySelector("#formRemove");
    formRemove.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/studentcourseapi/remove";
        const method = "delete";
        const formData = new FormData(formRemove);
        console.log(`${url} ${method}`);
        const eNumber = formData.get("ENumber");

        fetch(url, {
            method: method,
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('There was a network error!');
                }
                return response.status;
            })
            .then(result => {
                console.log(result);
                console.log("Success: the student grade record was removed");
                window.location.replace(`/student/details/${eNumber}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();