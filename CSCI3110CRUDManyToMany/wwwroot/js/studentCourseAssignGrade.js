"use strict";

import { FetchRepository } from "./FetchRepository.js";

(function _studentCourseAssignGrade() {
    const formAssignGrade =
        document.querySelector("#formAssignGrade");
    formAssignGrade.addEventListener('submit', async e => {
        e.preventDefault();
        const repo = new FetchRepository("/api/studentcourseapi");
        repo.updateAPIName = "assigngrade";
        const formData = new FormData(formAssignGrade);
        const eNumber = formData.get("ENumber");

        try {
            await repo.update(formData);
            console.log("Success: the student's grade was changed");
            window.location.replace(`/student/details/${eNumber}`); // redirect
        }
        catch (error) {
            console.error('Error:', error);
        }
    });

})();