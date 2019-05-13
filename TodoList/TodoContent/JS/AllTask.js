$(document).ready(function () {
    $(function () {
        taskStatuses = JSON.parse(taskStatuses);
        taskPriorities = JSON.parse(taskPriorities);

        $("#SaveTaskPopup").dialog({
            height: 300,
            width: 450,
            modal: true,
            resizable: true,
            dialogClass: 'no-close success-dialog',
            autoOpen: false,
        });

        $(document).on("click", "table tr td #lnkEditTask", function () {
            var taskId = $(this).attr('data-taskid');
            $.ajax({
                url: 'PopulateTask',
                data: {
                    projectType: projectType,
                    prjectId: prjectId,
                    taskId: taskId,
                },
                cache: false,
                success: function (responce) {
                    var tblHtml = TaskTemplate(responce, taskId);

                    tblHtml += '<hr />';

                    tblHtml += '<div style="margin-left: 144px;">';
                    tblHtml += '<input id="btnUpdate" type="submit" value="Update" />';
                    tblHtml += '<input id="btnCancel" type="submit" value="Cancel" />';
                    tblHtml += '</div>';

                    $("#SaveTaskPopup").html(tblHtml);
                    $('#txtEstimatedCompletionDate').datepicker();
                    $("#SaveTaskPopup").dialog('open');

                    $('#successMessage').css('display', 'block');
                    $('#successMessage').html('Task saved successfully.');
                },
                error: function () {
                    $('#errorMessage').css('display', 'block');
                    $('#errorMessage').html('Error while processing data.');
                }
            });
        });

        function TaskTemplate(responce, taskId) {
            var tblHtml = '<table>';

            tblHtml += '<tr><td>';
            tblHtml += '<label id="lblName">Name</label>';
            tblHtml += '</td>';

            tblHtml += '<td>';
            //tblHtml += '<input id="txtName" type="text" value="' + responce != null ? responce.Name : null + '" data-TaskId="' + taskId + '" >';
            if (responce != null)
                tblHtml += '<input id="txtName" type="text" value="' + responce.Name + '" data-TaskId="' + taskId + '" />';
            else
                tblHtml += '<input id="txtName" type="text" value="" />';

            tblHtml += '</td></tr>';

            tblHtml += '<tr><td>';
            tblHtml += '<label id="lblDescription">Description</label>';
            tblHtml += '</td>';

            tblHtml += '<td>';
            if (responce != null)
                tblHtml += '<input id="txtDescription" type="text" value="' + responce.Description + '" >';
            else
                tblHtml += '<input id="txtDescription" type="text" value="" >';
            tblHtml += '</td></tr>';

            tblHtml += '<tr><td>';
            tblHtml += '<label id="lblStatus">Status</label>';
            tblHtml += '</td>';

            tblHtml += '<td>';
            tblHtml += '<select id="ddlStatuses">';

            tblHtml += '<option value="0">-- Select --</option>';

            for (var i = 0; i < taskStatuses.length; i++) {
                if (responce != null && responce.Status == taskStatuses[i].Value) {
                    tblHtml += '<option selected value="' + taskStatuses[i].Value + '">' +
                        taskStatuses[i].Text + '</option>';
                }
                else {
                    tblHtml += '<option value="' + taskStatuses[i].Value + '">' +
                        taskStatuses[i].Text + '</option>';
                }
            }

            tblHtml += '</select>';
            tblHtml += '</td></tr>';

            tblHtml += '<tr><td>';
            tblHtml += '<label id="lblProjectName">Project name</label>';
            tblHtml += '</td>';

            tblHtml += '<td>';
            tblHtml += '<input id="txtProjectName" type="text" value="' + projectName +
                '" data-ProjectId="' + prjectId + '" data-ProjectType="' + projectType + '" disabled>';
            tblHtml += '</td></tr>';

            if (projectType == 1) {//Agile
                tblHtml += '<tr><td>';
                tblHtml += '<label id="lblEfforts">Efforts</label>';
                tblHtml += '</td>';

                tblHtml += '<td>';
                if (responce != null)
                    tblHtml += '<input id="txtEfforts" type="text" value="' + responce.Efforts + '" >';
                else
                    tblHtml += '<input id="txtEfforts" type="text" value="" >';
                tblHtml += '</td></tr>';

                tblHtml += '<tr><td>';
                tblHtml += '<label id="lblStoryPoints">Story points</label>';
                tblHtml += '</td>';

                tblHtml += '<td>';
                if (responce != null)
                    tblHtml += '<input id="txtStoryPoints" type="text" value="' + responce.StoryPoints + '" >';
                else
                    tblHtml += '<input id="txtStoryPoints" type="text" value="" >';
                tblHtml += '</td></tr>';

                tblHtml += '<tr><td>';
                tblHtml += '<label id="lblBurnedHours">Burned hours</label>';
                tblHtml += '</td>';

                tblHtml += '<td>';
                if (responce != null)
                    tblHtml += '<input id="txtBurnedHours" type="text" value="' + responce.BurnedHours + '" >';
                else
                    tblHtml += '<input id="txtBurnedHours" type="text" value="" >';
                tblHtml += '</td></tr>';
            }
            else if (projectType == 2) {//Normal
                tblHtml += '<tr><td>';
                tblHtml += '<label id="lblPriority">Priority</label>';
                tblHtml += '</td>';

                tblHtml += '<td>';

                tblHtml += '<select id="ddlTaskPriorities">';

                tblHtml += '<option value="0">-- Select --</option>';

                for (var i = 0; i < taskPriorities.length; i++) {
                    if (responce != null && responce.Priority == taskPriorities[i].Value) {
                        tblHtml += '<option selected value="' + taskPriorities[i].Value + '">' +
                            taskPriorities[i].Text + '</option>';
                    }
                    else {
                        tblHtml += '<option value="' + taskPriorities[i].Value + '">' +
                            taskPriorities[i].Text + '</option>';
                    }
                }

                tblHtml += '</select>';

                tblHtml += '</td></tr>';

                tblHtml += '<tr><td>';
                tblHtml += '<label id="lblEstimatedCompletionDate">Completion date</label>';
                tblHtml += '</td>';

                var estimatedDate = responce != null ? new Date(parseInt(responce.EstimatedCompletionDate.substr(6, 13))) : new Date();
                var formattedDate = (estimatedDate.getMonth() + 1) + "/" + estimatedDate.getDate() + "/" +
                    estimatedDate.getFullYear();
                tblHtml += '<td>';
                tblHtml += '<input id="txtEstimatedCompletionDate" type="text" value="' + formattedDate + '" >';
                tblHtml += '</td></tr>';
            }
            tblHtml += '</tr></table>';

            return tblHtml;
        }

        //dynamic control
        $(document).on("click", "#btnUpdate", function () {

            var taskId = $('#txtName').attr('data-TaskId');

            if ($('#txtProjectName').attr('data-ProjectType') == '1' && ValidateAgileTasks() == true) {

            }

            if ($('#txtProjectName').attr('data-ProjectType') == '2' && ValidateNormalTasks() == true) {

                var todoTask = {
                    todoTaskModel:
                    {
                        Id: taskId,
                        ProjectId: $('#txtProjectName').attr('data-ProjectId'),
                        Name: $('#txtName').val(),
                        Description: $('#txtDescription').val(),
                        Status: $('#ddlStatuses').val(),
                        Priority: $('#ddlTaskPriorities').val(),
                        EstimatedCompletionDate: $('#txtEstimatedCompletionDate').val()
                    }
                };

                $.ajax({
                    url: 'UpdateNormalTask',
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(todoTask),
                    cache: false,
                    success: function (responce) {
                        debugger;
                        var newRow = PopulateTaskListRow(responce, false);

                        $('table tr[data-TaskId="' + taskId + '"]').html(newRow);

                        $("#SaveTaskPopup").dialog('close');

                        $('#successMessage').css('display', 'block');
                        $('#successMessage').html('Task updated successfully.');
                    },
                    error: function () {
                        $('#errorMessage').css('display', 'block');
                        $('#errorMessage').html('Error while processing data.');
                    }
                });
            }
        });

        function PopulateTaskListRow(responce, isInsert) {
            var newRow = '<td>';
            newRow += '<input type="checkbox" data-TaskId="' + responce.Id + '" />';
            newRow += '</td>';

            newRow += '<td>';
            newRow += '<label>' + responce.Name + '</label>';
            newRow += '</td>';

            newRow += '<td>';
            newRow += '<label>' + responce.Description + '</label>';
            newRow += '</td>';

            newRow += '<td>';
            newRow += '<label>' + projectName + '</label>';
            newRow += '</td>';

            newRow += '<td>';
            //newRow += '<label>' + responce.Status == 1 ? "ToDo" : responce.Status == 2 ? "InProgress" : "Completed" + '</label>';
            if (responce.Status == 1)
                newRow += '<label>ToDo</label>';
            else if (responce.Status == 2)
                newRow += '<label>In-Progress</label>';
            else if (responce.Status == 3)
                newRow += '<label>Completed</label>';
            newRow += '</td>';

            var modifiededDate;

            if (isInsert == true)
                modifiededDate = responce != null ? new Date(parseInt(responce.CreatedDate.substr(6, 13))) : new Date();
            else
                modifiededDate = responce != null ? new Date(parseInt(responce.UpdatedDate.substr(6, 13))) : new Date();

            var formattedDate = (modifiededDate.getMonth() + 1) + "/" + modifiededDate.getDate() + "/" +
                modifiededDate.getFullYear();

            newRow += '<td>';
            newRow += '<label>' + formattedDate + '</label>';
            newRow += '</td>';

            newRow += '<td>';
            newRow += '<a id="lnkEditTask" class="glyphicon glyphicon-pencil" href="#" data-ProjectId="' + prjectId + '"' +
                'data-ProjectName="' + projectName + '" data-ProjectType="' + projectType + '" data-TaskId="' + responce.Id + '"></a>';
            newRow += '<a id="lnkRemoveTask" class="glyphicon glyphicon-trash" href="#" data-ProjectType="' + projectType + '"' +
                'data-TaskId="' + responce.Id + '"></a>';
            newRow += '</td>';

            return newRow;
        }

        //dynamic control
        $(document).on("click", "#btnSubmit", function () {
            if ($('#txtProjectName').attr('data-ProjectType') == '1' && ValidateAgileTasks() == true) {
                alert('clicked!!!!!!');
            }

            if ($('#txtProjectName').attr('data-ProjectType') == '2' && ValidateNormalTasks() == true) {

                var todoTask = {
                    todoTaskModel:
                    {
                        ProjectId: $('#txtProjectName').attr('data-ProjectId'),
                        Name: $('#txtName').val(),
                        Description: $('#txtDescription').val(),
                        Status: $('#ddlStatuses').val(),
                        Priority: $('#ddlTaskPriorities').val(),
                        EstimatedCompletionDate: $('#txtEstimatedCompletionDate').val()
                    }
                };

                $.ajax({
                    url: 'InsertNormalTask',
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(todoTask),
                    cache: false,
                    success: function (responce) {
                        debugger;
                        var newRow = '<tr data-TaskId="' + responce.Id + '">';
                        newRow += PopulateTaskListRow(responce, true);
                        newRow += '</tr>';

                        $('table').append(newRow);

                        $("#SaveTaskPopup").dialog('close');

                        $('#successMessage').css('display', 'block');
                        $('#successMessage').html('Task added successfully.');
                    },
                    error: function () {
                        $('#errorMessage').css('display', 'block');
                        $('#errorMessage').html('Error while processing data.');
                    }
                });
            }
        });

        function ValidateAgileTasks() {
            var flag = true;
            if ($('#txtName').val() == '') {
                $('#txtName').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtName').css("border-color", "initial");
            }

            if ($('#txtDescription').val() == '') {
                $('#txtDescription').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtDescription').css("border-color", "initial");
            }

            if ($('#ddlStatuses').val() == '0') {
                $('#ddlStatuses').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#ddlStatuses').css("border-color", "initial");
            }

            if ($('#txtProjectName').val() == '') {
                $('#txtProjectName').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtProjectName').css("border-color", "initial");
            }

            if ($('#txtEfforts').val() == '') {
                $('#txtEfforts').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtEfforts').css("border-color", "initial");
            }

            if ($('#txtStoryPoints').val() == '') {
                $('#txtStoryPoints').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtStoryPoints').css("border-color", "initial");
            }

            if ($('#txtBurnedHours').val() == '') {
                $('#txtBurnedHours').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtBurnedHours').css("border-color", "initial");
            }

            return flag;
        }

        function ValidateNormalTasks() {
            var flag = true;
            if ($('#txtName').val() == '') {
                $('#txtName').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtName').css("border-color", "initial");
            }

            if ($('#txtDescription').val() == '') {
                $('#txtDescription').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtDescription').css("border-color", "initial");
            }

            if ($('#ddlStatuses').val() == '0') {
                $('#ddlStatuses').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#ddlStatuses').css("border-color", "initial");
            }

            if ($('#txtProjectName').val() == '') {
                $('#txtProjectName').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtProjectName').css("border-color", "initial");
            }

            if ($('#ddlTaskPriorities').val() == '0') {
                $('#ddlTaskPriorities').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#ddlTaskPriorities').css("border-color", "initial");
            }

            if ($('#txtEstimatedCompletionDate').val() == '') {
                $('#txtEstimatedCompletionDate').css("border-color", "Red");
                flag = false;
            }
            else {
                $('#txtEstimatedCompletionDate').css("border-color", "initial");
            }

            return flag;
        }

        $('#lnkCreateNew').click(function () {
            var tblHtml = TaskTemplate(null, null);

            tblHtml += '<hr />';

            tblHtml += '<div style="margin-left: 144px;">';
            tblHtml += '<input id="btnSubmit" type="submit" value="Submit" />';
            tblHtml += '<input id="btnCancel" type="submit" value="Cancel" />';
            tblHtml += '</div>';

            $("#SaveTaskPopup").html(tblHtml);
            $('#txtEstimatedCompletionDate').datepicker();
            $("#SaveTaskPopup").dialog('open');
        });

        $(document).on("click", "table tr td #lnkRemoveTask", function () {
            var taskId = $(this).attr('data-taskid');
            var del = confirm("Do you want to delete this record?");
            if (del == true) {
                $.ajax({
                    url: 'DeleteNormalTask',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({
                        taskId: taskId
                    }),
                    success: function () {
                        $('table tr[data-TaskId="' + taskId + '"]').remove();

                        $('#successMessage').css('display', 'block');
                        $('#successMessage').html('Task deleted successfully.');
                    },
                    error: function (ex) {
                        $('#errorMessage').css('display', 'block');
                        $('#errorMessage').html('Error while processing data.');
                    }
                });
            }

            return del;
        });

        $(document).on("click", "#btnCancel", function () {
            $("#SaveTaskPopup").dialog('close');
        });

        $('#btnDeleteTasks').click(function () {
            var taskIds = new Array();
            $('table tr td input[type="checkbox"]:checked').each(function () {
                taskIds.push($(this).attr('data-taskid'))
            });
            $.ajax({
                url: 'DeleteTasks',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    taskIds: taskIds
                }),
                success: function () {
                    $('table tr td input[type="checkbox"]:checked').each(function () {                    
                        $('table tr[data-TaskId="' + $(this).attr('data-taskid') + '"]').remove();
                    });

                    $('#successMessage').css('display', 'block');
                    $('#successMessage').html('Task(s) deleted successfully.');
                },
                error: function (ex) {
                    $('#errorMessage').css('display', 'block');
                    $('#errorMessage').html('Error while processing data.');
                }
            });
        });
    });
});

