<script>
    import { onMount } from "svelte";
    import { fade } from "svelte/transition";
    import Spinner from "./Spinner.svelte";

    import { push, querystring } from "svelte-spa-router";
    import * as api from "../api/api";
    import * as roomStore from "../stores/roomStore";
    import * as userStore from "../stores/userStore";
    import * as sessionService from "../services/sessionService";
    import * as snackbarStore from "../stores/snackbarStore";
    import qs from "qs";
    import Button from "./Button.svelte";

    let username = "";
    let roomName = "";
    let isInvited = false;
    let joinRoomId = "";
    let isLoading = true;

    onMount(() => {
        const session = sessionService.getSession();
        if (session) {
            push(`/room/${session.roomId}`);
            return;
        }
        userStore.reset();
        roomStore.reset();
        sessionService.removeSession();
        const queryParams = qs.parse($querystring);
        isInvited = queryParams.roomId ? true : false;
        joinRoomId = queryParams.roomId ?? "";
        isLoading = false;
    });

    async function onCreateRoom(e) {
        isLoading = true;
        e.preventDefault();

        if (!username) {
            isLoading = false;
            snackbarStore.show("Username cannot be empty.", "error");
            return;
        }

        if (!roomName) {
            isLoading = false;
            snackbarStore.show("Room name cannot be empty.", "error");
            return;
        }

        const userDto = await api.createGuestUser(username);
        const roomDto = await api.createRoom(userDto.id, roomName);
        if (!userDto || !roomDto) {
            isLoading = false;
            alert("Oops. Something went wrong. Please try again.");
            return;
        }

        userStore.set(userDto);
        roomStore.set(roomDto);

        sessionService.setSession(userDto.id, roomDto.id);

        push(`/room/${roomDto.id}`);
    }

    async function onJoinRoom(e) {
        isLoading = true;
        e.preventDefault();

        if (!username) {
            isLoading = false;
            alert("Please enter a username.");
            return;
        }

        const userDto = await api.createGuestUser(username);
        if (!userDto) {
            isLoading = false;
            alert("Oops. Something went wrong. Please try again.");
            return;
        }
        userStore.update((u) => userDto);

        const roomDto = await api.getRoomById(joinRoomId);
        roomStore.update((r) => roomDto);

        sessionService.setSession(userDto.id, roomDto.id);

        push(`/room/${roomDto.id}`);
    }
</script>

{#if isLoading}
    <Spinner />
{:else}
    <div transition:fade class="min-h-screen flex items-center justify-center">
        <div class="flex flex-col items-center m-auto p-8 space-y-4 rounded-2xl glass">
            <h1 class="text-center text-4xl">Welcome to SAPP</h1>
            <h3 class="text-center text-md">Simplest Agile Poker Planner</h3>

            <form class="flex flex-col items-stretch space-y-3 w-full">
                <input
                    id="username-input"
                    bind:value={username}
                    type="username"
                    placeholder="Username"
                    class="p-3 border rounded bg-gray-200 focus:outline-none focus:bg-white"
                />
                {#if !isInvited}
                    <input
                        bind:value={roomName}
                        type="text"
                        placeholder="Room Name"
                        class="p-3 border rounded bg-gray-200 focus:outline-none focus:bg-white"
                    />
                    <Button text="Create Room" on:click={onCreateRoom} />
                {:else}
                    <Button text="Join Room" on:click={onJoinRoom} />
                {/if}
            </form>
        </div>
    </div>
{/if}

<style>
    .glass {
        background: white;
        background: linear-gradient(
            to right bottom,
            rgba(255, 255, 255, 0.7),
            rgba(255, 255, 255, 0.2)
        );
    }
</style>
