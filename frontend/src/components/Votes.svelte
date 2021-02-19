<script>
    import * as roomStore from "../stores/roomStore";
    import * as userStore from "../stores/userStore";
    import * as wsconnection from "../services/wsconnection";

    let selectedVote = "";

    $: if ($roomStore.status === roomStore.RoomStatus.IDLE) {
        selectedVote = "";
    }

    let voteOptions = [
        { value: 0.5 },
        { value: 1 },
        { value: 2 },
        { value: 3 },
        { value: 5 },
        { value: 8 },
        { value: 13 },
    ];

    async function onVoteClick(e) {
        if ($roomStore.status === roomStore.RoomStatus.IDLE) {
            selectedVote = "";
            return;
        }
        selectedVote = +e.target.innerText;
        await wsconnection.invokeSendVote(
            $roomStore.id,
            $userStore.id,
            $roomStore.selectedItemId,
            e.target.innerText
        );
    }
</script>

<div class="grid lg:grid-cols-7 md:grid-cols-3 gap-4">
    {#each voteOptions as voteOption (voteOption.value)}
        <div
            class:bg-blue-400={selectedVote === voteOption.value}
            class="flex items-center justify-center text-4xl rounded bg-white bg-opacity-60 hover:bg-opacity-100 transition"
            on:click={onVoteClick}
        >
            <span class:animate-pulse={$roomStore.status === roomStore.RoomStatus.VOTING}
                >{voteOption.value}</span
            >
        </div>
    {/each}
</div>

<style>
    .grid > div::before {
        content: "";
        padding-top: 100%;
        display: block;
    }
</style>
