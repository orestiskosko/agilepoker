<script>
    import RoomHeader from "./RoomHeader.svelte";
    import * as api from "../api/api";
    import { onMount } from "svelte";
    import * as roomStore from "../stores/roomStore";
    import * as userStore from "../stores/userStore";
    import * as sessionService from "../services/sessionService";
    import { push } from "svelte-spa-router";
    import Votes from "./Votes.svelte";
    import RoomItems from "./RoomItems.svelte";
    import * as wsconnection from "../services/wsconnection";
    import Button from "./Button.svelte";
    import MainScreen from "./MainScreen.svelte";
    import Sidebar from "./Sidebar.svelte";

    export let params = {};
    let isLoading = true;
    let isLeader;

    onMount(async () => {
        let roomDto = $roomStore;
        if (!roomDto.id) roomDto = await api.getRoomById(params.id);

        if (!roomDto) {
            roomStore.reset();
            userStore.reset();
            sessionService.removeSession();
            isLoading = false;
            return;
        }
        roomStore.set(roomDto);

        const session = sessionService.getSession();

        if (!session) {
            push(`/login?roomId=${roomDto.id}`);
            return;
        }

        const userDto = await api.getUserById(session.userId);
        userStore.set(userDto);
        roomStore.addParticipant(userDto);

        await wsconnection.invokeJoinRoom(userDto.id, roomDto.id);

        isLeader = $roomStore.leaderId === $userStore.id;
        isLoading = false;
    });

    async function onCloseRoom() {
        await wsconnection.invokeCloseRoom($roomStore.id);
        sessionService.removeSession();
        push("/login");
    }

    async function onLeaveRoom() {
        await wsconnection.invokeLeaveRoom($userStore.id, $roomStore.id);
        userStore.reset();
        roomStore.reset();
        sessionService.removeSession();
        push("/login");
    }

    async function onStopVoteClicked() {
        await wsconnection.invokeStopVoting($roomStore.id);
    }

    $: if ($roomStore.status === roomStore.RoomStatus.VOTING) {
        window.scrollTo({ top: 0, behavior: "smooth" });
    }
</script>

<MainScreen {isLoading}>
    <div slot="sidebar">
        <Sidebar {isLeader} {onStopVoteClicked} {onCloseRoom} {onLeaveRoom} />
    </div>
    <div slot="content" class="h-full p-8 glass rounded-xl">
        {#if $roomStore?.id}
            <div class="flex flex-col items-stretch space-y-12">
                <RoomHeader />
                <Votes />
                <RoomItems />
            </div>
        {:else}
            <div class="flex flex-col items-center justify-center space-y-6 h-full">
                <h1 class="text-2xl">Room is no longer available.</h1>
                <Button
                    text="Return to login page"
                    on:click={() => {
                        sessionService.removeSession();
                        push("/login");
                    }}
                />
            </div>
        {/if}
    </div>
</MainScreen>

<style>
    .glass {
        background: white;
        background: linear-gradient(to bottom, rgba(255, 255, 255, 0.6), rgba(255, 255, 255, 0.2));
    }
</style>
