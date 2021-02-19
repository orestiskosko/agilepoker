<script>
    export let text = "";

    let initials = text.split(" ").map((p) => p.trim()[0].toUpperCase());
</script>

<div class="flex items-center justify-center h-12 w-12 bg-white rounded-full">
    <span class="text-3xl">{initials}</span>
</div>
