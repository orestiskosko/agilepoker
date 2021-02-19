import * as signalR from "@microsoft/signalr";
import * as roomStore from "../stores/roomStore";
import * as snackbarStore from "../stores/snackbarStore";
import * as sessionService from "./sessionService";
import { push } from "svelte-spa-router";
import { get } from "svelte/store";

export const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${process.env.SAPPER_APP_API_URL}/votinghub`)
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("errorOccured", (err) => console.error(err));
connection.on("userJoinedRoom", onUserJoinedRoom);
connection.on("userLeftRoom", onUserLeftRoom);
connection.on("roomClosed", onRoomClosed);
connection.on("itemsSet", onItemsSet);
connection.on("selectedItemSet", onSelectedItemSet);
connection.on("userVoted", onUserVoted);
connection.on("votingStarted", onVotingStarted);
connection.on("votingCompleted", onVotingCompleted);
connection.on("votingStopped", onVotingStopped);

export async function invokeJoinRoom(userId, roomId) {
    try {
        await connection.invoke("JoinRoom", roomId, userId);
    } catch (err) {
        console.log(err);
        setTimeout(() => invokeJoinRoom(userId, roomId), 500);
    }
}
export async function invokeLeaveRoom(userId, roomId) {
    try {
        await connection.invoke("LeaveRoom", roomId, userId);
    } catch (err) {
        snackbarStore.show(err, snackbarStore.SnackbarLevel.ERR);
        console.log(err);
    }
}
export async function invokeCloseRoom(roomId) {
    try {
        await connection.invoke("CloseRoom", roomId);
    } catch (err) {
        snackbarStore.show(err, snackbarStore.SnackbarLevel.ERR);
        console.log(err);
    }
}
export async function invokeSetItems(roomId, items) {
    try {
        await connection.invoke("SetItems", roomId, items);
    } catch (err) {
        snackbarStore.show(err, snackbarStore.SnackbarLevel.ERR);
        console.log(err);
    }
}
export async function invokeSetSelectedItem(roomId, itemId) {
    try {
        await connection.invoke("SetSelectedItem", roomId, itemId);
    } catch (err) {
        snackbarStore.show(err, snackbarStore.SnackbarLevel.ERR);
        console.log(err);
    }
}
export async function invokeStartVoting(roomId) {
    try {
        await connection.invoke("StartVoting", roomId);
    } catch (err) {
        snackbarStore.show(err, snackbarStore.SnackbarLevel.ERR);
        console.log(err);
    }
}
export async function invokeStopVoting(roomId) {
    try {
        await connection.invoke("StopVoting", roomId);
    } catch (err) {
        snackbarStore.show(err, snackbarStore.SnackbarLevel.ERR);
        console.log(err);
    }
}
export async function invokeCompleteVoting(roomId) {
    try {
        await connection.invoke("CompleteVoting", roomId);
    } catch (err) {
        snackbarStore.show(err, snackbarStore.SnackbarLevel.ERR);
        console.log(err);
    }
}
export async function invokeSendVote(roomId, userId, itemId, voteData) {
    try {
        await connection.invoke("SendVote", roomId, userId, itemId, voteData);
    } catch (err) {
        snackbarStore.show(err, snackbarStore.SnackbarLevel.ERR);
        console.log(err);
    }
}

connection.onclose(async (err) => {
    snackbarStore.show(`Connection dropped.`, snackbarStore.SnackbarLevel.WARN);
    await startSignalRConnection();
});

export async function startSignalRConnection() {
    try {
        snackbarStore.show("Connecting...");
        await connection.start();
        snackbarStore.show("Connected.");
        console.log("SignalR Connected.");
    } catch (err) {
        snackbarStore.show("Connection failed.", snackbarStore.SnackbarLevel.ERR);
        console.log(err);
        setTimeout(startSignalRConnection, 5000);
    }
}

function onUserJoinedRoom(user) {
    roomStore.addParticipant(user);
    snackbarStore.show(`${user.username} has joined.`);
}

function onUserLeftRoom(user) {
    roomStore.removeParticipant(user.id);
    snackbarStore.show(`${user.username} has left.`);
}

function onRoomClosed(roomId) {
    snackbarStore.show(`Room has closed.`);
    sessionService.removeSession();
    push("/login");
}

function onItemsSet(items) {
    console.log("Items Set", items);
    roomStore.setItems(items);
}

function onSelectedItemSet(item) {
    roomStore.setSelectedItem(item.id);
}

function onUserVoted(voteDto) {
    roomStore.setParticipantVote(voteDto);
    const username = get(roomStore).participants.find((p) => p.id === voteDto.userId).username;
    snackbarStore.show(`${username} voted.`);
}

function onVotingStarted() {
    roomStore.setStatus(roomStore.RoomStatus.VOTING);
    roomStore.resetUserItemVotes();
    roomStore.setSelectedItemStatus(roomStore.ItemStatus.VOTING);
    snackbarStore.show(`Voting started.`);
}

function onVotingCompleted() {
    roomStore.setStatus(roomStore.RoomStatus.IDLE);
    roomStore.setSelectedItemStatus(roomStore.ItemStatus.VOTED);
    snackbarStore.show(`Voting completed.`);
}

function onVotingStopped() {
    roomStore.setStatus(roomStore.RoomStatus.IDLE);
    roomStore.setSelectedItemStatus(roomStore.ItemStatus.IDLE);
    snackbarStore.show(`Voting stopped.`);
}
