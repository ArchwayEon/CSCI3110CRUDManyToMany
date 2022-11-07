"use strict";

import { FetchRepository } from "./FetchRepository.js";

(async function _studentCourseIndex() {
    const repo = new FetchRepository("/api/studentcourseapi");
    repo.readAllAPIName = "studentgradesreport";

    try {
        const result = await repo.readAll();
        populateTable(result);
    }
    catch(error){
        console.error('Error:', error);
    }
})();

function populateTable(result) {
    const tableBody = document.getElementById("tableBody");
    result.forEach((item) => {
        const tr = document.createElement("tr");
        for (let key in item) {
            const td = document.createElement("td");
            let text = item[key];
            if(text === '' && key === 'letterGrade') {
               text = "No grade";
            }
            let textNode = document.createTextNode(text);
            td.appendChild(textNode);
            tr.appendChild(td);
        }
        tableBody.appendChild(tr);
    });
}