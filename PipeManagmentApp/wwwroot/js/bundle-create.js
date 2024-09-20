var removedPipes = [];
var addedPipes = [];

function removePipe(pipeId) {
    // Сохраняем ID удаляемой трубы
    removedPipes.push(pipeId);
    document.getElementById("removedPipes").value = removedPipes.join(",");

    // Находим трубу в списке
    const pipeElement = document.querySelector(`[onclick="removePipe(${pipeId})"]`).parentElement;

    // Визуально удаляем трубу из списка
    pipeElement.remove();

    // Добавляем трубу обратно в доступные трубы
    const pipeData = pipeElement.textContent.trim();
    const availablePipesContainer = document.getElementById("available-pipes");

    // Создаем новый элемент для доступной трубы
    const newPipeElement = document.createElement("li");
    newPipeElement.innerHTML = `
                <input type="checkbox" name="selectedPipes" value="${pipeId}" />
                ${pipeData}
            `;
    availablePipesContainer.appendChild(newPipeElement);
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

document.addEventListener("DOMContentLoaded", function () {
    // Получаем добавленные трубы из Model
    const addedPipes = @Html.Raw(Newtonsoft.Json.JsonConvert.SerializeObject(Model.AddedPipes));

    // Добавляем трубы в pipe-list
    addedPipes.forEach(pipeId => {
        const pipe = document.querySelector(`input[name="selectedPipes"][value="${pipeId}"]`);
        if (pipe) {
            // Визуально добавляем трубу в список пакета
            const pipeText = pipe.parentElement.textContent.trim();
            const newPipe = document.createElement("li");
            newPipe.innerHTML = pipeText + ` <button type="button" class="btn btn-danger btn-sm" onclick="removePipe(${pipe.value})">-</button>`;
            document.getElementById("pipe-list").appendChild(newPipe);

            // Убираем трубу из доступных для добавления
            pipe.parentElement.remove();
            addedPipes.push(pipe.value); // Добавляем в массив добавленных труб
        }
    });
});