var removedPipes = [];
var addedPipes = [];

function removePipe(pipeId) {
    removedPipes.push(pipeId);
    document.getElementById("removedPipes").value = removedPipes.join(",");

    // Удаляем визуально трубу из списка
    document.querySelector(`[onclick="removePipe(${pipeId})"]`).parentElement.remove();
}

function addPipes() {
    const selectedPipes = document.querySelectorAll('input[name="selectedPipes"]:checked');
    selectedPipes.forEach(pipe => {
        addedPipes.push(pipe.value);
        document.getElementById("addedPipes").value = addedPipes.join(",");

        // Визуально добавляем трубу в список пакета
        const pipeText = pipe.parentElement.textContent.trim();
        const newPipe = document.createElement("li");
        newPipe.innerHTML = pipeText + ` <button type="button" class="btn btn-danger btn-sm" onclick="removePipe(${pipe.value})">-</button>`;
        document.getElementById("pipe-list").appendChild(newPipe);

        // Убираем трубу из доступных для добавления
        pipe.parentElement.remove();
    });
}