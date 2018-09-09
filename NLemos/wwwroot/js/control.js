$(function () {
    const tagList = [];
    const tags = $('#tags');

    $(window).keydown(function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            return false;
        }
    });

    $('#btnAddTag').click(function (e) {
        const text = $('#tagName').val().toUpperCase();
        $('#tagName').val('');

        if (!text) {
            return;
        }

        if (tagList.find(t => t === text)) {
            return;
        }

        tagList.push(text);
        drawTags();
    });

    function drawTags() {
        tags.html('');

        let index = 0;
        for (let t of tagList) {
            tags.append(createTag(t));
            tags.append(createHiddenTag(index, t));
            index++;
        }
    }

    function createTag(text) {
        const btnTag = $('<button>', {
            type: 'button',
            class: 'btn btn-default btn-sm tag-btn'
        });

        btnTag.append(`<div>${text}</div>`);

        const btnRemove = $('<button>', {
            type: 'button',
            class: 'btn btn-default'
        });

        btnRemove.append('x');
        btnRemove.click(function () {
            removeTag(text);
        });

        btnTag.append(btnRemove);

        return btnTag;
    }

    function createHiddenTag(index, text) {
        const hidden = $('<input>', {
            type: 'hidden',
            name: `Tags[${index}].Name`,
            value: text
        });

        return hidden;
    }

    function removeTag(text) {
        tagList.splice(tagList.indexOf(text), 1);
        drawTags();
    }
});