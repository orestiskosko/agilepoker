<script>
    export let message = "";
    export let show = false;
</script>

{#if show}
    <div
        class="flex items-center justify-center fixed w-full h-full top-0 left-0 z-10 bg-gray-800 bg-opacity-90"
    >
        <p class="text-white text-2xl uppercase">{message}</p>
    </div>
{/if}

<style>
</style>
