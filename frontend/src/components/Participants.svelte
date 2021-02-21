<script>
    import * as roomStore from "../stores/roomStore";

    function userHasVoted(participantId) {
        return !!$roomStore.votes.find(
            (v) =>
                v.roomId === $roomStore.id &&
                v.userId === participantId &&
                v.itemId === $roomStore.selectedItemId
        )?.vote;
    }
</script>

<div class="flex flex-col space-y-3">
    {#each $roomStore.participants as participant (participant.id)}
        <div
            class="flex py-3 px-5 border rounded space-x-5 divide-x bg-white bg-opacity-50"
            class:bg-opacity-100={userHasVoted(participant.id)}
        >
            <div class="flex-1">{participant.username}</div>
            <div class="flex-grow-0 pl-5">
                {#if $roomStore.status === roomStore.RoomStatus.IDLE}
                    {$roomStore.votes.find(
                        (v) =>
                            v.roomId === $roomStore.id &&
                            v.userId === participant.id &&
                            v.itemId === $roomStore.selectedItemId
                    )?.vote ?? "?"}
                {:else}
                    <div>-</div>
                {/if}
            </div>
        </div>
    {:else}
        <div>No participants. That's weird.</div>
    {/each}
</div>
