import * as api from "./api.js";
import Session from "../src/session.js";
import * as userService from "./userService.js";
import * as roomService from "./roomService.js";
import * as state from "./state.js";

function onReceiveVote(vote) {
    document.getElementById("vote").innerText = vote;
}

async function onUserJoinedRoom(username) {
    console.log(`User ${username} has joined the room!`);

    const existingRoom = state.getRoom();
    const updatedRoom = await api.getRoomById(existingRoom.id);
    state.setRoom(updatedRoom);

    const participantList = document.getElementById("participant-list");
    participantList.innerText = "";

    state.getRoom().participants.forEach((participant) => {
        console.log(participant);
        const pNode = document.createElement("p");
        pNode.classList.add("participant");
        pNode.innerText = participant.username;
        participantList.appendChild(pNode);
    });
}

async function onUserLeftRoom(username) {
    console.log(`User ${username} has left the room!`);

    const room = await api.getRoomById(state.getRoom().id);
    state.setRoom(room);

    const participantList = document.getElementById("participant-list");
    participantList.innerText = "";

    state.getRoom().participants.forEach((participant) => {
        console.log(participant);
        const pNode = document.createElement("p");
        pNode.classList.add("participant");
        pNode.innerText = participant.username;
        participantList.appendChild(pNode);
    });
}

async function onVoteClick(event) {
    let vote = event.target.innerText;

    document.querySelectorAll(".vote").forEach((el) => {
        el.classList.remove("selected");
    });

    event.target.classList.toggle("selected");

    try {
        await connection.invoke("SendVote", state.getRoom().id, vote);
    } catch (err) {
        console.error(err);
    }
}

document.querySelectorAll(".vote").forEach((el) => {
    el.addEventListener("click", onVoteClick);
});

async function initializeRoom(userId, roomId, username) {
    var userInfo;
    if (userId) {
        userInfo = userService.getUserInfo();
    } else {
        const userDto = await api.createGuestUser(username);
        userInfo = new Session(userDto.id, userDto.username);
        userService.setUserInfo(userInfo);
    }
    state.setUserInfo(userInfo);

    var roomDto;
    if (roomId) {
        roomDto = await api.getRoomById(roomId);
    } else {
        roomDto = await api.createRoom(userInfo.id);
    }

    roomService.setRoomId(roomDto.id);
    state.setRoom(roomDto);
    state.setIsLeader(userInfo.id === roomDto.ownerId);

    await connection.invoke("JoinRoom", roomDto.id, userInfo.id);

    console.log(state.getRoom());

    document.getElementById("welcome").innerText = `Welcome ${userInfo.username}`;
    document.getElementById("room-link-input").value = `${location.host}?roomId=${roomDto.id}`;
    document.getElementById("room-link-button").addEventListener("click", (event) => {
        navigator.permissions.query({ name: "clipboard-write" }).then((result) => {
            if (result.state == "granted" || result.state == "prompt") {
                navigator.clipboard.writeText(document.getElementById("room-link-input").value);
                event.target.innerText = "COPIED!";
                setTimeout(() => (event.target.innerText = "COPY"), 5000);
            }
        });
    });
    if (!state.getIsLeader()) {
        document.getElementById("room-link-container").classList.add("hidden");
        document.getElementById("close-room-button").classList.add("hidden");
    }
}

function showLoginPage() {
    document.getElementById("login-section").classList.remove("hidden");
    document.getElementById("room-section").classList.add("hidden");
}

function showRoomPage() {
    document.getElementById("login-section").classList.add("hidden");
    document.getElementById("room-section").classList.remove("hidden");
}

function showCreateRoomButton() {
    document.getElementById("create-room-button").classList.remove("hidden");
    document.getElementById("join-room-button").classList.add("hidden");
}

function showJoinRoomButton() {
    document.getElementById("create-room-button").classList.add("hidden");
    document.getElementById("join-room-button").classList.remove("hidden");
}

// On Create Room
document.getElementById("create-room-button").addEventListener("click", async (event) => {
    const username = document.getElementById("username-input").value;

    if (!username) {
        return;
    }

    await initializeRoom(undefined, undefined, username);
    showRoomPage();
});

// On Join Room
document.getElementById("join-room-button").addEventListener("click", async (event) => {
    const username = document.getElementById("username-input").value;

    if (!username) {
        return;
    }

    await initializeRoom(undefined, state.getRoom().id, username);
    showRoomPage();
});

// On Close Room
document.getElementById("close-room-button").addEventListener("click", async (event) => {
    showLoginPage();
    document.getElementById("welcome").innerText = "";
    userService.removeUserInfo();
    roomService.removeRoomId();
    await connection.invoke("LeaveRoom", state.getRoom().id, state.getUserInfo().id);
    state.reset();
});

window.onbeforeunload = function () {
    connection.invoke("LeaveRoom", state.getRoom().id, state.getUserInfo().id);
};

async function initialize() {
    // Start the SignalR connection.
    await startSignalRConnection();

    var urlParams = new URLSearchParams(location.search);
    if (urlParams.has("roomId")) {
        const roomId = urlParams.get("roomId");
        const roomDto = await api.getRoomById(roomId);

        if (roomDto) {
            state.setRoom(roomDto);

            const userInfo = userService.getUserInfo();
            if (userInfo) {
                await initializeRoom(userInfo.id, state.getRoom().id, userInfo.username);
                showRoomPage();
                return;
            }

            showLoginPage();
            showJoinRoomButton();
            return;
        }
    }

    if (userService.getUserInfo() && roomService.getRoomId()) {
        await initializeRoom(userService.getUserInfo().id, roomService.getRoomId());
        showRoomPage();
        return;
    }

    showLoginPage();
    showCreateRoomButton();
}

initialize();
