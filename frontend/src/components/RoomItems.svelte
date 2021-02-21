<script>
    import * as roomStore from "../stores/roomStore";
    import * as userStore from "../stores/userStore";
    import * as snackbarStore from "../stores/snackbarStore";
    import * as wsconnection from "../services/wsconnection";
    import UploadXmlButton from "./UploadXmlButton.svelte";
    import Item from "./Item.svelte";

    let isLeader = $roomStore.leaderId === $userStore.id;
    let searchText = "";
    let items = [];

    $: if (searchText) {
        items = $roomStore.items.filter((i) => {
            console.log(i.data.key);
            return i.data.summary.includes(searchText) || i.data.key.includes(searchText);
        });
    } else {
        items = $roomStore.items;
    }

    async function onItemClick(itemId) {
        if (!isLeader) return;
        roomStore.setSelectedItem(itemId);
        snackbarStore.show(`Item ready to vote.`);
        await wsconnection.invokeSetSelectedItem($roomStore.id, itemId);
        await wsconnection.invokeStartVoting($roomStore.id);
    }
</script>

<div class="space-y-3 text-center">
    <h4 class="text-2xl">Vote Items</h4>

    {#if isLeader}
        <UploadXmlButton />
        <input
            class="w-full p-3 border rounded focus:outline-none"
            type="search"
            placeholder="Search item..."
            bind:value={searchText}
        />
    {/if}

    {#if $roomStore.items.length}
        <div class="grid sm:grid-cols-1 md:grid-cols-3 lg:grid-cols-4 gap-4">
            {#each items as item (item.id)}
                {#key item.id}
                    <Item {item} {isLeader} onClick={onItemClick} />
                {/key}
            {/each}
        </div>
    {:else}
        <div class="text-center">No items to show.</div>
    {/if}
</div>
