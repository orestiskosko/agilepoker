<script>
    import * as snackbarStore from "../stores/snackbarStore";
    import { fade } from "svelte/transition";
</script>

<div class="fixed top-3 right-3 space-y-1 flex flex-col items-end">
    {#each $snackbarStore.entries as entry (entry.id)}
        <div
            class:bg-green-500={entry.level === snackbarStore.SnackbarLevel.INFO}
            class:bg-yellow-500={entry.level === snackbarStore.SnackbarLevel.WARN}
            class:bg-red-500={entry.level === snackbarStore.SnackbarLevel.ERR}
            class="w-48 text-center px-4 py-3 rounded border text-white"
        >
            {entry.message}
        </div>
    {/each}
</div>
