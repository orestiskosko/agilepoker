import { writable } from "svelte/store";

const SnackbarLevel = {
    INFO: "info",
    WARN: "warn",
    ERR: "error",
};
Object.freeze(SnackbarLevel);

const defaultSnackbarState = {
    entries: [],
};

const { subscribe, set, update } = writable(defaultSnackbarState);

const show = (message, level = SnackbarLevel.INFO, timeout = 3000) =>
    update((s) => {
        const id = `${message}_${Date.now().valueOf()}`;
        s.entries.push({ id, message, level, handled: false });

        setTimeout(() => {
            removeMessage(id);
        }, timeout);

        return s;
    });

const removeMessage = (id) =>
    update((s) => {
        const messageIndex = s.entries.findIndex((m) => m.id === id);
        s.entries.splice(messageIndex, 1);
        return s;
    });

export { subscribe, set, update, show, SnackbarLevel };
