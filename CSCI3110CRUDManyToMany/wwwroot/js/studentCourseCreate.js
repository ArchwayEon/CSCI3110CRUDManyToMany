"use strict";

import { FetchRepository } from "./FetchRepository.js";

(function _studentCourseCreate() {
    const formCreateStudentCourse =
        document.querySelector("#formCreateStudentCourse");
    formCreateStudentCourse.addEventListener('submit', async e => {
        e.preventDefault();
        const repo = new FetchRepository("/api/studentcourseapi");
        const formData = new FormData(formCreateStudentCourse);

        try {
            const result = await repo.create(formData);
            console.log('Success: the student course record was created');
            window.location.replace(`/student/details/${result.studentENumber}`); // redirect
        }
        catch (error) {
            console.error('Error:', error);
        }
    });
})();