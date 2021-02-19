import Session from "../models/session";

const sessionKey = "user-info";

export function getSession() {
    const sessionString = sessionStorage.getItem(sessionKey);

    if (!sessionString) {
        return undefined;
    }

    const rawSession = JSON.parse(sessionString);
    return new Session(rawSession.userId, rawSession.roomId);
}

export function setSession(userId, roomId) {
    const session = new Session(userId, roomId);
    sessionStorage.setItem(sessionKey, JSON.stringify(session));
}

export function removeSession() {
    sessionStorage.removeItem(sessionKey);
}
