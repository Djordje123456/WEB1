$(function () {
    // Navigation
    loadNavigation();
    RedoLogin();

    // Tables
    getAllFitnessCenters();
    getOwnersFitnessCenters();
    getFitnessCenter();
    getGroupWorks();
    getGroupWorksByVisitor();
    getGroupWorksByTrainer();
    getPastGroupWorksByTrainer();
    getComments();
    getOwnerComments();
    loadCommentForm();
    getTrainersTable();
    getRegisteredVisitors();
    loadProfile();
    loadDataForModifyGroupWorks();
    loadDataForModifyFitnessCenter();

    // Forms
    awaitSearch();
    awaitSearchPastGroupWorks();
    awaitGroupWorksSearch();
    awaitLogin();
    awaitRegister();
    awaitRegisterTrainer();
    awaitRegisterFitnessCenter();
    awaitRegisterGroupWorks();
    awaitUpdateFitnessCenter();
    awaitUpdateGroupWorks();
    awaitUpdateProfile();
    awaitComment();

})





// Tables

const getAllFitnessCenters = () => {
    $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/FitnessCentersController/GetAll/`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {updateFitnessCentersTable(response)},
            error: (response) => {},
            complete: null
    })
}

const getOwnersFitnessCenters = () => {
    const queryString = new URLSearchParams();
    queryString.append('id', localStorage.getItem('userId'));

    $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/FitnessCentersController/GetByOwnerId/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {loadOwnersFitnessCentersTable(response)},
            error: (response) => {},
            complete: null
    })
}

const getFitnessCenter = () => {
    const params = new URLSearchParams(window.location.search);
    const fitnessId = params.get('fitnessId');

    const queryString = new URLSearchParams();
    queryString.append('id', fitnessId);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/FitnessCentersController/GetById/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {loadSingleFitnessCenter(response)},
        error: (response) => {},
        complete: null
    })
}

const getGroupWorks = () => {
    loadGroupWorksHeader()

    const params = new URLSearchParams(window.location.search);
    const fitnessId = params.get('fitnessId');

    const queryString = new URLSearchParams();
    queryString.append('id', fitnessId);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/GroupWorksController/GetAllNewByFitness/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {loadGroupWorksBody(response)},
        error: (response) => {},
        complete: null
    })
}

const getGroupWorksByVisitor = () => {
    const queryString = new URLSearchParams();
    queryString.append('userId', localStorage.getItem('userId'));

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/GroupWorksController/GetAllByVisitor/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {loadGroupWorksByVisitorBody(response)},
        error: (response) => {},
        complete: null
    })
}

const getGroupWorksByTrainer = () => {
    const queryString = new URLSearchParams();
    queryString.append('trainerId', localStorage.getItem('userId'));

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/GroupWorksController/GetAllByTrainer/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {loadGroupWorksByTrainerBody(response)},
        error: (response) => {},
        complete: null
    })
}

const getPastGroupWorksByTrainer = () => {
    const queryString = new URLSearchParams();
    queryString.append('trainerId', localStorage.getItem('userId'));

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/GroupWorksController/GetAllOldByTrainer/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {loadPastGroupWorksByTrainerBody(response)},
        error: (response) => {},
        complete: null
    })
}

const getComments = () => {
    const params = new URLSearchParams(window.location.search);
    const fitnessId = params.get('fitnessId');

    const queryString = new URLSearchParams();
    queryString.append('id', fitnessId);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/CommentsController/GetAllByFitness/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {loadCommentsForVisitor(response)},
        error: (response) => {},
        complete: null
    })
}

const getOwnerComments = () => {
    const queryString = new URLSearchParams();
    queryString.append('ownerId', localStorage.getItem('userId'));

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/CommentsController/GetAllByOwner/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {loadCommentsForOwner(response)},
        error: (response) => {},
        complete: null
    })
}

const getTrainersTable = () => {
    const queryString = new URLSearchParams();
    queryString.append('ownerId', localStorage.getItem('userId'));
    
    $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/UsersController/GetTrainersByOwner/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {loadTrainersTable(response)},
            error: (response) => {},
            complete: null
    })
}

const getRegisteredVisitors = () => {
    const params = new URLSearchParams(window.location.search);
    const groupWorksId = params.get('groupWorksId');

    const queryString = new URLSearchParams();
    queryString.append('id', groupWorksId);

    $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/UsersController/GetRegisteredVisitorsByGroupWork/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {loadRegisteredVisitorsTable(response)},
            error: (response) => {},
            complete: null
    })
}





// Buttons

const sortFitnessCenters = (type, order) => {
    const queryString = new URLSearchParams();
    queryString.append('direction', order);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/FitnessCentersController/SortBy${type}/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {updateFitnessCentersTable(response)},
        error: (response) => {},
        complete: null
    })
}

const sortGroupWorks = (type, order) => {
    const queryString = new URLSearchParams();
    queryString.append('userId', localStorage.getItem('userId'));
    queryString.append('direction', order);
    queryString.append('type', type);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/GroupWorksController/HistorySortBy/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {updateGroupWorksTable(response)},
        error: (response) => {},
        complete: null
    })
}

const sortPastGroupWorks = (type, order) => {
    const queryString = new URLSearchParams();
    queryString.append('trainerId', localStorage.getItem('userId'));
    queryString.append('direction', order);
    queryString.append('type', type);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/GroupWorksController/TrainerHistorySortBy/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {loadPastGroupWorksByTrainerBody(response)},
        error: (response) => {},
        complete: null
    })
}





// Forms

const awaitSearch = () => {
    const searchForm = $('#search-fitness-center');

    searchForm.on('submit', (e) => {
        e.preventDefault()
        const data = $(e.currentTarget).serializeArray();
        const queryString = new URLSearchParams();
        queryString.append('name', data[0].value);
        queryString.append('address', data[1].value);
        queryString.append('minYear', data[2].value);
        queryString.append('maxYear', data[3].value);

        $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/FitnessCentersController/SearchBy/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {updateFitnessCentersTable(response)},
            error: (response) => {},
            complete: null
        })
    })
}

const awaitSearchPastGroupWorks = () => {
    const searchForm = $('#search-past-group-works');

    searchForm.on('submit', (e) => {
        e.preventDefault()
        const data = $(e.currentTarget).serializeArray();
        const queryString = new URLSearchParams();
        queryString.append('trainerId', localStorage.getItem('userId'));
        queryString.append('name', data[0].value);
        queryString.append('type', data[1].value);
        queryString.append('minTime', data[2].value);
        queryString.append('maxTime', data[3].value);

        $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/GroupWorksController/TrainerHistorySearchBy?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {loadPastGroupWorksByTrainerBody(response)},
            error: (response) => {},
            complete: null
        })
    })
}

const awaitGroupWorksSearch = () => {
    const searchForm = $('#search-group-works');

    searchForm.on('submit', (e) => {
        e.preventDefault()
        const data = $(e.currentTarget).serializeArray();
        const queryString = new URLSearchParams();
        queryString.append('userId', localStorage.getItem('userId'));
        queryString.append('name', data[0].value);
        queryString.append('type', data[2].value);
        queryString.append('fitnessCenter', data[1].value);

        $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/GroupWorksController/HistorySearchBy/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {updateGroupWorksTable(response)},
            error: (response) => {},
            complete: null
        })
    })
}

const awaitLogin = () => {
    const loginForm = $('#login-user');

    loginForm.on('submit', (e) => {
        e.preventDefault()
        const data = $(e.currentTarget).serializeArray();
        const queryString = new URLSearchParams();
        queryString.append('username', data[0].value);
        queryString.append('password', data[1].value);

        $.ajax({
            type: 'PUT',
            url: `https://localhost:44351/api/UsersController/Login/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {Login(response)},
            error: (response) => {alert('Failed to login')},
            complete: null
        })
    })
}

const awaitRegister = () => {
    const registerForm = $('#register-user');

    registerForm.on('submit', (e) => {
        e.preventDefault()
        const formData = $(e.currentTarget).serializeArray();
        let registrationData = {}

        formData.forEach(field => {
            registrationData[field.name] = field.value
        })
        registrationData['Role'] = 'Visitor'

        $.ajax({
            type: 'POST',
            url: `https://localhost:44351/api/UsersController/RegisterUser`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(registrationData),
            dataType: 'text',
            crossDomain: true,
            success: () => {window.location.href = "../login.html"},
            error: (error) => {alert('Failed to register')},
            complete: null
        })
    })
}

const awaitRegisterTrainer = () => {
    const registerForm = $('#register-trainer');

    registerForm.on('submit', (e) => {
        e.preventDefault()
        const formData = $(e.currentTarget).serializeArray();

        const queryString = new URLSearchParams();
        queryString.append('name', formData[2].value);
        
        $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/FitnessCentersController/GetByName?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {
                if(response == 0) {
                    alert('Failed to register trainer. Fitness Center does not exist.')
                    return
                }

                let registrationData = {}

                formData.forEach(field => {
                    registrationData[field.name] = field.value
                })
                registrationData['Role'] = 'Trainer'
                registrationData['FitnessCenterId'] = response

                $.ajax({
                    type: 'POST',
                    url: `https://localhost:44351/api/UsersController/RegisterUser`,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(registrationData),
                    dataType: 'text',
                    crossDomain: true,
                    success: () => {window.location.href = "../index.html"},
                    error: (error) => {alert('Failed')},
                    complete: null
                })
            },
            error: (response) => {},
            complete: null
        })


    })
}

const awaitRegisterFitnessCenter = () => {
    const registerForm = $('#register-fitness-center');

    registerForm.on('submit', (e) => {
        e.preventDefault()
        const formData = $(e.currentTarget).serializeArray();
        let registrationData = {}

        formData.forEach(field => {
            registrationData[field.name] = field.value
        })

        const queryString = new URLSearchParams();
        queryString.append('ownerId', localStorage.getItem('userId'));

        $.ajax({
            type: 'POST',
            url: `https://localhost:44351/api/FitnessCentersController/Create/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(registrationData),
            dataType: 'text',
            crossDomain: true,
            success: () => {window.location.href = "../index.html"},
            error: (error) => {alert('Failed')},
            complete: null
        })
    })
}

const awaitRegisterGroupWorks = () => {
    const registerForm = $('#register-group-works');

    registerForm.on('submit', (e) => {
        e.preventDefault()
        const formData = $(e.currentTarget).serializeArray();
        let registrationData = {}

        formData.forEach(field => {
            registrationData[field.name] = field.value
        })

        const queryString = new URLSearchParams();
        queryString.append('trainerId', localStorage.getItem('userId'));

        $.ajax({
            type: 'POST',
            url: `https://localhost:44351/api/GroupWorksController/Create/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(registrationData),
            dataType: 'text',
            crossDomain: true,
            success: () => {window.location.href = "../index.html"},
            error: (error) => {alert('Failed to create group works')},
            complete: null
        })
    })
}

const awaitUpdateFitnessCenter = () => {
    const modifyForm = $('#modify-fitness-center');

    modifyForm.on('submit', (e) => {
        e.preventDefault()
        const formData = $(e.currentTarget).serializeArray();
        let updateData = {}

        const params = new URLSearchParams(window.location.search);
        const fitnessId = params.get('fitnessId');

        formData.forEach(field => {
            updateData[field.name] = field.value
        })
        updateData['Id'] = fitnessId

        const queryString = new URLSearchParams();
        queryString.append('ownerId', localStorage.getItem('userId'));

        $.ajax({
            type: 'PUT',
            url: `https://localhost:44351/api/FitnessCentersController/Update/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(updateData),
            dataType: 'text',
            crossDomain: true,
            success: () => {window.location.href = "../fitness-centers.html"},
            error: (error) => {alert('Failed to register fitness center')},
            complete: null
        })
    })
}

const awaitUpdateGroupWorks = () => {
    const modifyForm = $('#modify-group-works');

    modifyForm.on('submit', (e) => {
        e.preventDefault()
        const formData = $(e.currentTarget).serializeArray();
        let updateData = {}

        const params = new URLSearchParams(window.location.search);
        const groupWorksId = params.get('groupWorksId');

        formData.forEach(field => {
            updateData[field.name] = field.value
        })
        updateData['Id'] = groupWorksId

        const queryString = new URLSearchParams();
        queryString.append('trainerId', localStorage.getItem('userId'));

        $.ajax({
            type: 'PUT',
            url: `https://localhost:44351/api/GroupWorksController/Update/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(updateData),
            dataType: 'text',
            crossDomain: true,
            success: () => {window.location.href = "../trainer-group-works.html"},
            error: (error) => {},
            complete: null
        })
    })
}

const awaitUpdateProfile = () => {
    const registerForm = $('#update-user');

    registerForm.on('submit', (e) => {
        e.preventDefault()
        const formData = $(e.currentTarget).serializeArray();
        let registrationData = {}

        formData.forEach(field => {
            registrationData[field.name] = field.value
        })

        const queryString = new URLSearchParams();

        queryString.append('id', localStorage.getItem('userId'));

        $.ajax({
            type: 'PUT',
            url: `https://localhost:44351/api/UsersController/UpdateAsync/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(registrationData),
            dataType: 'text',
            crossDomain: true,
            success: () => {window.location.href = "../index.html"},
            error: (error) => {alert('Failed')},
            complete: null
        })
    })
}

const awaitComment = () => {
    const commentForm = $('#comment-form');

    commentForm.on('submit', (e) => {
        e.preventDefault()
        const formData = $(e.currentTarget).serializeArray();

        const params = new URLSearchParams(window.location.search);
        const fitnessId = params.get('fitnessId');

        let registrationData = {}
        formData.forEach(field => {
            registrationData[field.name] = field.value
        })
        registrationData['VisitorId'] = localStorage.getItem('userId')
        registrationData['FitnessCenterId'] = fitnessId

        $.ajax({
            type: 'POST',
            url: `https://localhost:44351/api/CommentsController/LeaveComment`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(registrationData),
            dataType: 'text',
            crossDomain: true,
            success: () => {},
            error: (error) => {alert('Failed to post comment')},
            complete: null
        })
    })
}





// Helpers

const updateFitnessCentersTable = (data) => {
    const fitnessCentersTableBody = $('.fitness-centers > tbody');
    let tableContent = ``

    data.forEach(data => {
        const dataTemplate = `<tr>
                                 <td>
                                     ${data.Name}
                                 </td>
                                 <td>
                                     ${data.Street} ${data.StreetNumber}, ${data.Town} ${data.ZipCode}
                                 </td>
                                 <td>
                                     ${data.CreatedAt}
                                 </td>
                                 <td>
                                     <a href="/fitness-center-details.html?fitnessId=${data.Id}">More</a>
                                 </td>
                             </tr>`
        tableContent = tableContent.concat(dataTemplate)
    })

    fitnessCentersTableBody.html(tableContent)
}

const loadOwnersFitnessCentersTable = (data) => {
    const fitnessCentersTableBody = $('.owners-fitness-centers > tbody');
    let tableContent = ``

    data.forEach(fitnessCenter => {
        const dataTemplate = `<tr>
                                 <td>
                                     ${fitnessCenter.Name}
                                 </td>
                                 <td>
                                     ${fitnessCenter.Street} ${fitnessCenter.StreetNumber}, ${fitnessCenter.Town} ${fitnessCenter.ZipCode}
                                 </td>
                                 <td>
                                     ${fitnessCenter.CreatedAt}
                                 </td>
                                 <td>
                                     <button onclick="location.href='/fitness-center-details.html?fitnessId=${fitnessCenter.Id}'">Details</button>
                                     <button onclick="location.href='/fitness-center-modify.html?fitnessId=${fitnessCenter.Id}'">Modify</button>
                                     <button onclick="deleteFitnessCenter(${fitnessCenter.Id})">Delete</button>
                                 </td>
                             </tr>`
        tableContent = tableContent.concat(dataTemplate)
    })

    fitnessCentersTableBody.html(tableContent)
}

const updateGroupWorksTable = (data) => {
    const groupWorksTableBody = $('.group-works-table > tbody');
    groupWorksTableBody.html(``)

    data.forEach(groupWork => {
        loadSingleGroupWork(groupWork)
    })
}

const loadSingleFitnessCenter = (data) => {
    const fitnessCenterTable = $('.fitness-center > tbody');
    fitnessCenterTable.html(
        `
        <tr>
            <td>
                ${data.Name}
            </td>
            <td>
                ${data.Street} ${data.StreetNumber}, ${data.Town} ${data.ZipCode}
            </td>
            <td>
                ${data.CreatedAt}
            </td>
            <td>
                ${data.MonthPrice}
            </td>
            <td>
                ${data.YearPrice}
            </td>
            <td>
                ${data.OneWorkPrice}
            </td>
            <td>
                ${data.OneGroupPrice}
            </td>
            <td>
                ${data.OnePersonalPrice}
            </td>
        </tr>
        `
    )
}

const loadGroupWorksHeader = () => {
    const tableHeader = $('.works-table > thead');

    let numberOfColumns = 6
    let lastColumn = ``

    if(CheckIfIsLoggenIn()) {
        numberOfColumns = 7
        lastColumn = `<th></th>`
    }

    tableHeader.html(
        `
        <tr>
            <th colspan="${numberOfColumns}">
                <h1>
                    Group Works
                </h1>
            </th>
        </tr>
        <tr>
            <th>Name</th>
            <th>Type</th>
            <th>Duration</th>
            <th>Date</th>
            <th>Maximum Visitors</th>
            <th>Registered Visitors</th>
            ${lastColumn}
        </tr>
        `
    )
}

const loadGroupWorksBody = (data) => {
    const groupWorksTable = $('.works-table > tbody');
    let loggedIn = CheckIfIsLoggenIn()
    let lastColumn = ``
    let allRows = ``
    let groupWorkType = `Yoga`

    data.forEach(groupWork => {
        if(loggedIn) {
            lastColumn =
                `
                <td>
                    <button onclick="signUpToGroupWork(${groupWork.Id})">Sign Up</button>
                </td>
                `
        }

        if(groupWork.WorkType === 2) {
            groupWorkType = `Legs`
        }
        else {
            groupWorkType = `Yoga`
        }

        const row =
            `
            <tr>
                <td>
                    ${groupWork.Name}
                </td>
                <td>
                    ${groupWorkType}
                </td>
                <td>
                    ${groupWork.Time}
                </td>
                <td>
                    ${new Date(Date.parse(groupWork.PlanTime)).toLocaleDateString()}
                </td>
                <td>
                    ${groupWork.MaxVisitors}
                </td>
                <td>
                    ${groupWork.CurrentNumberOfVisitors}
                </td>
                ${lastColumn}
            </tr>
            `
        allRows = allRows.concat(row)
    })

    groupWorksTable.html(allRows)
}

const loadGroupWorksByVisitorBody = (data) => {
    data.forEach(groupWork => {loadSingleGroupWork(groupWork)})
}

const loadGroupWorksByTrainerBody = (data) => {
    data.forEach(groupWork => {loadSingleTrainerGroupWork(groupWork)})
}

const loadPastGroupWorksByTrainerBody = (data) => {
    const groupWorksTable = $('.past-group-works-table > tbody');
    groupWorksTable.html(``)
    data.forEach(groupWork => {loadSingleTrainerPastGroupWork(groupWork)})
}

const loadSingleGroupWork = (groupWork) => {
    const queryString = new URLSearchParams();
    queryString.append('id', groupWork.FitnessCenterId);
    let row = ``
    let groupWorkType = ``
    let fitnessCenterName = ``
    const groupWorksTable = $('.group-works-table > tbody');

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/FitnessCentersController/GetById?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {
            fitnessCenterName = response.Name

            if(groupWork.WorkType === 2) {
                groupWorkType = `Legs`
            }
            else {
                groupWorkType = `Yoga`
            }

            row =
                `
                <tr>
                    <td>
                        ${groupWork.Name}
                    </td>
                    <td>
                        ${fitnessCenterName}
                    </td>
                    <td>
                        ${groupWorkType}
                    </td>
                    <td>
                        ${groupWork.Time}
                    </td>
                    <td>
                        ${new Date(Date.parse(groupWork.PlanTime)).toLocaleDateString()}
                    </td>
                    <td>
                        ${groupWork.MaxVisitors}
                    </td>
                    <td>
                        ${groupWork.CurrentNumberOfVisitors}
                    </td>
                </tr>
                `

            groupWorksTable.append(row)
        },
        error: (response) => {fitnessCenterName = 'Error'},
        complete: null
    })
}

const loadSingleTrainerGroupWork = (groupWork) => {
    const queryString = new URLSearchParams();
    queryString.append('id', groupWork.FitnessCenterId);
    let row = ``
    let groupWorkType = ``
    let fitnessCenterName = ``
    const groupWorksTable = $('.trainer-group-works-table > tbody');

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/FitnessCentersController/GetById?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {
            fitnessCenterName = response.Name

            if(groupWork.WorkType === 2) {
                groupWorkType = `Legs`
            }
            else {
                groupWorkType = `Yoga`
            }


            row =
                `
                <tr>
                    <td>
                        ${groupWork.Name}
                    </td>
                    <td>
                        ${fitnessCenterName}
                    </td>
                    <td>
                        ${groupWorkType}
                    </td>
                    <td>
                        ${groupWork.Time}
                    </td>
                    <td>
                        ${new Date(Date.parse(groupWork.PlanTime)).toLocaleDateString()}
                    </td>
                    <td>
                        ${groupWork.MaxVisitors}
                    </td>
                    <td>
                        ${groupWork.CurrentNumberOfVisitors}
                    </td>
                    <td>
                        <button onclick="location.href='/registered-visitors.html?groupWorksId=${groupWork.Id}'">Registered Visitors</button>
                        <button onclick="location.href='/group-works-modify.html?groupWorksId=${groupWork.Id}'">Modify</button>
                        <button onclick="deleteGroupWorks(${groupWork.Id})">Delete</button>
                    </td>
                </tr>
                `

            groupWorksTable.append(row)
        },
        error: (response) => {fitnessCenterName = 'Error'},
        complete: null
    })
}

const loadSingleTrainerPastGroupWork = (groupWork) => {
    const queryString = new URLSearchParams();
    queryString.append('id', groupWork.FitnessCenterId);
    let row = ``
    let groupWorkType = ``
    let fitnessCenterName = ``
    const groupWorksTable = $('.past-group-works-table > tbody');

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/FitnessCentersController/GetById?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {
            fitnessCenterName = response.Name

            if(groupWork.WorkType === 2) {
                groupWorkType = `Legs`
            }
            else {
                groupWorkType = `Yoga`
            }

            row =
                `
                <tr>
                    <td>
                        ${groupWork.Name}
                    </td>
                    <td>
                        ${fitnessCenterName}
                    </td>
                    <td>
                        ${groupWorkType}
                    </td>
                    <td>
                        ${groupWork.Time}
                    </td>
                    <td>
                        ${new Date(Date.parse(groupWork.PlanTime)).toLocaleDateString()}
                    </td>
                    <td>
                        ${groupWork.MaxVisitors}
                    </td>
                    <td>
                        ${groupWork.CurrentNumberOfVisitors}
                    </td>
                    <td>
                        <button onclick="location.href='/registered-visitors.html?groupWorksId=${groupWork.Id}'">Registered Visitors</button>
                    </td>
                </tr>
                `

            groupWorksTable.append(row)
        },
        error: (response) => {fitnessCenterName = 'Error'},
        complete: null
    })
}

const signUpToGroupWork = (groupWorkId) => {
    const queryString = new URLSearchParams();

    queryString.append('id', localStorage.getItem('userId'));
    queryString.append('groupWorkId', groupWorkId);

    $.ajax({
        type: 'PUT',
        url: `https://localhost:44351/api/UsersController/AssigneAsync/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (returnValue) => {},
        error: (error) => {},
        complete: null
    })
}

const loadCommentsForVisitor = (data) => {
    const commentsTable = $('.comments-table > tbody');
    commentsTable.html(``)

    data.forEach(comment => {
        const queryString = new URLSearchParams();
        queryString.append('id', comment.VisitorId);

        $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/UsersController/GetById/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {
                const commentsTable = $('.comments-table > tbody');
                const row =
                `
                <tr>
                    <td>
                        ${response.Username}
                    </td>
                    <td>
                        ${comment.Content}
                    </td>
                    <td>
                        ${comment.Grade}
                    </td>
                </tr>
                `

                commentsTable.append(row)
            },
            error: (response) => {},
            complete: null
        })
    })
}

const loadCommentsForOwner = (data) => {
    const commentsTable = $('.owner-comments-table > tbody');
    commentsTable.html(``)

    data.forEach(comment => {
        const queryString = new URLSearchParams();
        queryString.append('id', comment.VisitorId);

        $.ajax({
            type: 'GET',
            url: `https://localhost:44351/api/UsersController/GetById/?${queryString}`,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(null),
            dataType: 'json',
            crossDomain: true,
            success: (response) => {
                const commentsTable = $('.owner-comments-table > tbody');
                const row =
                `
                <tr>
                    <td>
                        ${response.Username}
                    </td>
                    <td>
                        ${comment.Content}
                    </td>
                    <td>
                        ${comment.Grade}
                    </td>
                    <td>
                        <button onclick="updateApprovalComment(${comment.Id}, true)">Approve</button>
                        <button onclick="updateApprovalComment(${comment.Id}, false)">Decline</button>
                    </td>
                </tr>
                `

                commentsTable.append(row)
            },
            error: (response) => {},
            complete: null
        })
    })
}

const updateApprovalComment = (commentId, commentStatus) => {
    const queryString = new URLSearchParams();
    
    queryString.append('userId', localStorage.getItem('userId'));
    queryString.append('commentId', commentId);
    queryString.append('isApproved', commentStatus);
    
    $.ajax({
        type: 'PUT',
        url: `https://localhost:44351/api/CommentsController/UpdateApproval/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {alert('Comment status updated')},
        error: (response) => {},
        complete: null
    })
}

const loadCommentForm = () => {
    const commentFormBody = $('.comment-form-body > tbody');
    
    if(!CheckIfIsLoggenIn()) {
        return;
    }


    const params = new URLSearchParams(window.location.search);
    const fitnessId = params.get('fitnessId');

    const queryString = new URLSearchParams();
    queryString.append('userId', localStorage.getItem('userId'));
    queryString.append('fitnessId', fitnessId);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/UsersController/GetUserWorkOutsByFitness/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {
            if(response.length == 0) {
                return;
            }

            commentFormBody.html(
                `
                <tr>
                    <td>
                        <label for="content">Content</label>
                    </td>
                    <td>
                        <input type="text" id="content" name="content" required>
                    </td>
                </tr>
        
                <tr>
                    <td>
                        <label for="grade">Grade</label>
                    </td>
                    <td>
                        <input type="text" id="grade" name="grade" required>
                    </td>
                </tr>
        
                <tr>
                    <th colspan="2">
                        <button type="submit">Comment</button>
                    </th>
                </tr>
                `
            )
        },
        error: (response) => {},
        complete: null
    })
}

const deleteFitnessCenter = (fitnessId) => {
    const queryString = new URLSearchParams();
    
    queryString.append('ownerId', localStorage.getItem('userId'));
    queryString.append('fitnessId', fitnessId);
    
    $.ajax({
        type: 'DELETE',
        url: `https://localhost:44351/api/FitnessCentersController/Delete/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {},
        error: (response) => {},
        complete: null
    })
}

const deleteGroupWorks = (groupWorksId) => {
    const queryString = new URLSearchParams();

    queryString.append('trainerId', localStorage.getItem('userId'));
    queryString.append('groupWorkId', groupWorksId);

    $.ajax({
        type: 'PUT',
        url: `https://localhost:44351/api/GroupWorksController/Delete/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {window.location.reload()},
        error: (response) => {},
        complete: null
    })
}

const loadTrainersTable = (data) => {
    const trainersTableBody = $('.trainers-table > tbody');
    let allRows = ``

    data.forEach(trainer => {
        const row = `
        <tr>
            <td>
                ${trainer.Name}
            </td>
            <td>
                ${trainer.LastName}
            </td>
            <td>
                <button onclick="blockTrainer(${trainer.Id})">Block</button>
            </td>
        </tr>
        `

        allRows = allRows.concat(row)
    })

    trainersTableBody.html(allRows)
}

const blockTrainer = (trainerId) => {
    const queryString = new URLSearchParams();
    
    queryString.append('ownerId', localStorage.getItem('userId'));
    queryString.append('userId', trainerId);
    
    $.ajax({
        type: 'PUT',
        url: `https://localhost:44351/api/UsersController/BlockuUser/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {window.location.href = "../trainers.html"},
        error: (response) => {},
        complete: null
    })
}

const loadRegisteredVisitorsTable = (users) => {
    const trainersTableBody = $('.registered-visitors-table > tbody');
    let allRows = ``

    users.forEach(user => {
        const row = `
        <tr>
            <td>
                ${user.Name}
            </td>
            <td>
                ${user.LastName}
            </td>
        </tr>
        `

        allRows = allRows.concat(row)
    })

    trainersTableBody.html(allRows)
}

const loadProfile = () => {
    let page = window.location.href
    if(!page.endsWith(`profile.html`)) {
        return;
    }

    const queryString = new URLSearchParams();
    queryString.append('id', localStorage.getItem('userId'));

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/UsersController/GetById/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {
            document.getElementById('name').value = response.Name
            document.getElementById('lastName').value = response.LastName
            if(response.Gender == 1) {
                document.getElementById(`male`).checked = true;
            } else {
                document.getElementById(`female`).checked = true;
            }
            document.getElementById('email').value = response.Email
            document.getElementById('bornAt').value = response.BornAt.split('T')[0]
        },
        error: (response) => {},
        complete: null
    })
}

const loadDataForModifyGroupWorks = () => {
    let page = window.location.href
    if(!page.includes(`group-works-modify.html`)) {
        return;
    }

    const params = new URLSearchParams(window.location.search);
    const groupWorksId = params.get('groupWorksId');

    const queryString = new URLSearchParams();
    queryString.append('id', groupWorksId);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/GroupWorksController/GetById/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {
            document.getElementById('name').value = response.Name
            document.getElementById('maxVisitors').value = response.MaxVisitors
            document.getElementById('time').value = response.Time
            if(response.WorkType === 2) {
                document.getElementById('legs').checked = true
            }
            else {
                document.getElementById('yoga').checked = true
            }
            document.getElementById('planTime').value = response.PlanTime.split('T')[0]
        },
        error: (response) => {},
        complete: null
    })
}

const loadDataForModifyFitnessCenter = () => {
    let page = window.location.href
    if(!page.includes(`fitness-center-modify.html`)) {
        return;
    }

    const params = new URLSearchParams(window.location.search);
    const fitnessId = params.get('fitnessId');

    const queryString = new URLSearchParams();
    queryString.append('id', fitnessId);

    $.ajax({
        type: 'GET',
        url: `https://localhost:44351/api/FitnessCentersController/GetById/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {
            document.getElementById('name').value = response.Name
            document.getElementById('street').value = response.Street
            document.getElementById('streetNumber').value = response.StreetNumber
            document.getElementById('town').value = response.Town
            document.getElementById('zipCode').value = response.ZipCode
            document.getElementById('createdAt').value = response.CreatedAt
            document.getElementById('monthPrice').value = response.MonthPrice
            document.getElementById('yearPrice').value = response.YearPrice
            document.getElementById('oneWorkPrice').value = response.OneWorkPrice
            document.getElementById('oneGroupPrice').value = response.OneGroupPrice
            document.getElementById('onePersonalPrice').value = response.OnePersonalPrice
        },
        error: (response) => {},
        complete: null
    })
}




// Navigation

const loadNavigation = () => {
    const navBar = $('#navbar');
    let navBarContent = ``

    if(!CheckIfIsLoggenIn()) {
        navBarContent = `<button onclick="window.location.href='../index.html'">Home</button>
                         <button onclick="window.location.href='../login.html'">Login</button>
                         <button onclick="window.location.href='../register.html'">Register</button>`
    }
    else if(localStorage.getItem('userRole') === `1`) {
        navBarContent = `<button onclick="window.location.href='../index.html'">Home</button>
                         <button onclick="window.location.href='../profile.html'">Profile</button>
                         <button onclick="window.location.href='../group-works.html'">Group Works</button>
                         <button onclick="Logout()">Logout</button>`
    }
    else if(localStorage.getItem('userRole') === `2`) {
        navBarContent = `<button onclick="window.location.href='../index.html'">Home</button>
                         <button onclick="window.location.href='../profile.html'">Profile</button>
                         <button onclick="window.location.href='../register-group-works.html'">Create Group Works</button>
                         <button onclick="window.location.href='../trainer-group-works.html'">Upcoming Group Works</button>
                         <button onclick="window.location.href='../past-group-works.html'">Past Group Works</button>
                         <button onclick="Logout()">Logout</button>`
    }
    else if(localStorage.getItem('userRole') === `3`) {
        navBarContent = `<button onclick="window.location.href='../index.html'">Home</button>
                         <button onclick="window.location.href='../profile.html'">Profile</button>
                         <button onclick="window.location.href='../register-trainer.html'">Register Trainer</button>
                         <button onclick="window.location.href='../trainers.html'">Trainers</button>
                         <button onclick="window.location.href='../comments.html'">Comments</button>
                         <button onclick="window.location.href='../fitness-centers.html'">Fitness Centers</button>
                         <button onclick="window.location.href='../register-fitness-center.html'">Register Fitness Center</button>
                         <button onclick="Logout()">Logout</button>`
    }

    navBar.html(navBarContent)
}





// Session

const CheckIfIsLoggenIn = () => {
    const userId = localStorage.getItem('userId')
    const loginTime = localStorage.getItem('loginTime')

    if(userId == null || loginTime == null || userId == "null" || loginTime == "null") {
        return false
    }
    else if (Date.now() - loginTime >= 10800000) {
        localStorage.setItem('userId', null);
        localStorage.setItem('loginTime', null);
        localStorage.setItem('userRole', null);
        return false
    }

    return true
}

const Login = (user) => {
    localStorage.setItem('userId', user.Id)
    localStorage.setItem('userRole', user.Role)
    localStorage.setItem('loginTime', Date.now())
    window.location.href = "../index.html"
}

const Logout = () => {
    localStorage.setItem('userId', null)
    localStorage.setItem('userRole', null)
    localStorage.setItem('loginTime', null)
    window.location.href = "../index.html"
}

const RedoLogin = () => {
    userId = localStorage.getItem('userId')
    if(userId == `null`) {
        return;
    }

    const queryString = new URLSearchParams();
    queryString.append('id', userId);
    
    $.ajax({
        type: 'PUT',
        url: `https://localhost:44351/api/UsersController/RedoLogin/?${queryString}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(null),
        dataType: 'json',
        crossDomain: true,
        success: (response) => {},
        error: (response) => {Logout()},
        complete: null
    })
}