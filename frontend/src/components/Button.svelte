<script>
    export let id = "";
    export let text = "";
    export let color = "blue";
    export let disabled = false;
</script>

<button
    {id}
    on:click
    class:btn-main={color === "blue"}
    class:btn-red={color === "red"}
    class:btn-gray={color === "gray"}
    {disabled}
>
    {text}
</button>
