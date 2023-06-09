export const domManager = {
    addChild: (parentIdentifier, childContent) => {
        const parent = document.querySelector(parentIdentifier);
        if (parent) {
            parent.insertAdjacentHTML("beforeend", childContent);
        } else {
            console.error("Could not find HTML element:", parentIdentifier);
        }
    },

    addEventListener: (parentObject, eventType, eventHandler) => {
        if (parentObject instanceof HTMLElement) {
            parentObject.addEventListener(eventType, eventHandler);
        } else {
            console.error("Invalid parent object provided:", parentObject);
        }
    },

    removeChildren: (parentIdentifier) => {
        const parent = document.querySelector(parentIdentifier);
        if (parent) {
            parent.innerHTML = "";
        } else {
            console.error("Could not find HTML element:", parentIdentifier);
        }
    },
};
