<script>
    import * as roomStore from "../stores/roomStore";

    export let item;
    export let isLeader;
    export let onClick;
</script>

<div
    class="flex flex-col justify-evenly py-3 border rounded space-y-3 divide-y bg-white bg-opacity-60 hover:bg-opacity-100"
    class:bg-blue-400={item.status === roomStore.ItemStatus.VOTING &&
        $roomStore.selectedItemId === item.id}
    class:bg-green-400={item.status === roomStore.ItemStatus.VOTED}
>
    <div class="flex items-center justify-center px-3">
        <span
            class="rounded-full h-2 w-2 bg-gray-400"
            class:bg-green-600={item.data.priority === "Medium"}
            class:bg-red-600={item.data.priority === "High"}
        />
        <span class="ml-3">{item.data.key}</span>
    </div>
    <div class="flex-1 px-3 pt-3">
        <span>{item.data.summary}</span>
    </div>
    <div class="flex items-center  px-3 pt-3">
        {#if isLeader}
            <button class="w-full px-3 rounded focus:outline-none" on:click={() => onClick(item.id)}
                >Vote</button
            >
        {/if}
        <button
            class="w-full px-3 rounded focus:outline-none"
            on:click={() => window.open(item.data.link)}>Open</button
        >
    </div>
</div>
