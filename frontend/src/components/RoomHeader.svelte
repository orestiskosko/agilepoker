<script>
    import { fade } from "svelte/transition";
    import * as roomStore from "../stores/roomStore";

    let selectedItemTitle = "";
    let now = new Date();
    let voteStartTime = now;
    let clockInterval = null;

    $: timeDiffMillis = new Date(now - voteStartTime);
    $: seconds = timeDiffMillis.getSeconds();
    $: minutes = timeDiffMillis.getMinutes();

    $: if ($roomStore.status === roomStore.RoomStatus.VOTING && !clockInterval) {
        now = voteStartTime = new Date();
        clockInterval = setInterval(() => (now = new Date()), 1000);
    } else if ($roomStore.status === roomStore.RoomStatus.IDLE) {
        clearInterval(clockInterval);
        clockInterval = null;
    }

    $: {
        const item = $roomStore.items.find((i) => i.id === $roomStore.selectedItemId);
        if (item) selectedItemTitle = `${item.data.key} - ${item.data.summary}`;
    }
</script>

<div class="text-center">
    <div
        transition:fade
        class="text-xl transition-colors font-bold"
        class:text-transparent={$roomStore.status !== roomStore.RoomStatus.VOTING}
    >
        {minutes < 10 ? "0" : ""}{minutes}:{seconds < 10 ? "0" : ""}{seconds}
    </div>
    {#if $roomStore.selectedItemId !== null}
        <h3
            class="text-3xl my-10"
            class:animate-pulse={$roomStore.status === roomStore.RoomStatus.VOTING}
        >
            {selectedItemTitle}
        </h3>
    {:else}
        <h3 class="text-3xl my-10">Select an item to start voting</h3>
    {/if}
</div>
