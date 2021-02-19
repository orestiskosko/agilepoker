import { writable } from "svelte/store";

const defaultUser = {
    id: "",
    username: "",
};

const { subscribe, set, update } = writable(defaultUser);

const reset = () => set(defaultUser);

export { subscribe, set, update, reset };
