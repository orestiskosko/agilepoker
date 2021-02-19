import axios from "axios";

const baseUrl = process.env.SAPPER_APP_API_URL;
const usersRoute = `${baseUrl}/users`;
const roomsRoute = `${baseUrl}/rooms`;
const jiraRoute = `${baseUrl}/jira`;

export async function getUserById(id) {
    try {
        const response = await axios.get(`${usersRoute}/${id}`);
        return response.data;
    } catch (error) {
        console.error(error);
        return undefined;
    }
}

export async function createGuestUser(username) {
    try {
        const response = await axios.post(usersRoute, { username });
        return response.data;
    } catch (error) {
        console.error(error);
        return undefined;
    }
}

export async function getRoomById(roomId) {
    try {
        const response = await axios.get(`${roomsRoute}/${roomId}`);
        return response.data;
    } catch (error) {
        console.error(error);
        return undefined;
    }
}

export async function createRoom(ownerId, roomName) {
    try {
        const response = await axios.post(roomsRoute, { ownerId, name: roomName });
        return response.data;
    } catch (error) {
        console.error(error);
        return undefined;
    }
}

export async function setItems(items) {
    try {
        const response = await axios.post(`${roomsRoute}/items`, {
            data: items,
        });
        return response.data;
    } catch (error) {
        console.error(error);
        return undefined;
    }
}

export async function deleteRoom(roomId) {
    try {
        const response = await axios.delete(`${roomsRoute}/${roomId}`);
        return response.data;
    } catch (error) {
        console.error(error);
        return undefined;
    }
}

export async function searchJiraIssues(jql) {
    try {
        const response = await axios.get(jiraRoute, {
            params: {
                jql,
            },
        });
        console.log(response);
        return response.data;
    } catch (error) {
        console.error(error);
        return undefined;
    }
}
