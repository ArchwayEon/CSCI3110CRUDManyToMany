"use strict";

import { FetchRepository } from "./FetchRepository.js";

(function _studentCourseRemove() {
    const formRemove =
        document.querySelector("#formRemove");
    formRemove.addEventListener('submit', async e => {
        e.preventDefault();
        const repo = new FetchRepository("/api/studentcourseapi");
        repo.deleteAPIName = "remove";
        const formData = new FormData(formRemove);
        const eNumber = formData.get("ENumber");

        try {
            await repo.delete(formData);
            console.log("Success: the student grade record was removed");
            window.location.replace(`/student/details/${eNumber}`); // redirect
        }
        catch (error) {
            console.error('Error:', error);
        }
    });

})();