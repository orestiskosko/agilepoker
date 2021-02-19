<script>
    import { fade } from "svelte/transition";
    import Spinner from "./Spinner.svelte";

    export let isLoading = false;
</script>

<div class="min-h-screen flex">
    {#if isLoading}
        <Spinner />
    {:else}
        <div transition:fade class="flex-shrink-0 w-64">
            <slot name="sidebar" />
        </div>
        <div transition:fade class="flex-1 p-6">
            <slot name="content" />
        </div>
    {/if}
</div>
