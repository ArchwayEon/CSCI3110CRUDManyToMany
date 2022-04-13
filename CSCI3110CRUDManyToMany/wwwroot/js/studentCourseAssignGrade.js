"use strict";
(function _studentCourseAssignGrade() {
    const formAssignGrade =
        document.querySelector("#formAssignGrade");
    formAssignGrade.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/studentcourseapi/assigngrade";
        const method = "put";
        const formData = new FormData(formAssignGrade);
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
                console.log("Success: the student's grade was changed");
                window.location.replace(`/student/details/${eNumber}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();