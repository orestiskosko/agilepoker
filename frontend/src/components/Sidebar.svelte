<script>
    import RoomParticipants from "./RoomParticipants.svelte";
    import Button from "./Button.svelte";
    import RoomLink from "./RoomLink.svelte";
    import * as roomStore from "../stores/roomStore";
    import * as userStore from "../stores/userStore";

    export let isLeader = false;
    export let onStopVoteClicked = () => {};
    export let onCloseRoom = () => {};
    export let onLeaveRoom = () => {};
</script>

<div class="min-h-screen w-64 fixed flex flex-col items-center py-6 px-3 glass">
    <div class="flex flex-col items-center justify-center">
        <div class="text-md">Welcome</div>
        <div class="text-2xl">{$userStore?.username ?? ""}</div>
    </div>
    <div class="text-2xl mt-12">{$roomStore?.name ?? ""}</div>
    <div class="flex-1 mt-3">
        <RoomParticipants />
    </div>
    <div class="flex flex-col space-y-2 w-full">
        {#if isLeader}
            {#if $roomStore.status === roomStore.RoomStatus.VOTING}
                <Button text="Stop Vote" on:click={onStopVoteClicked} />
            {/if}
            <RoomLink />
            <Button text="Close Room" color="red" on:click={onCloseRoom} />
        {:else}
            <Button text="Leave Room" color="red" on:click={onLeaveRoom} />
        {/if}
    </div>
</div>

<style>
    .glass {
        background: white;
        background: linear-gradient(to bottom, rgba(255, 255, 255, 0.6), rgba(255, 255, 255, 0.3));
    }
</style>
