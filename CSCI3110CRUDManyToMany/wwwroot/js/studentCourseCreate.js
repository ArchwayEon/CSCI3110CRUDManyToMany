"use strict";
(function _studentCourseCreate() {
    const formCreateStudentCourse =
        document.querySelector("#formCreateStudentCourse");
    formCreateStudentCourse.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/studentcourseapi/create";
        const method = "post";
        const formData = new FormData(formCreateStudentCourse);
        console.log(`${url} ${method}`);

        fetch(url, {
            method: method,
            body: formData
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('There was a network error!');
            }
            return response.json();
        })
        .then(result => {
            console.log('Success: the student course record was created');
            window.location.replace(`/student/details/${result.studentENumber}`); // redirect
        })
        .catch(error => {
            console.error('Error:', error);
        });
    });

})();