<script>
    function onClickCopy(e) {
        navigator.permissions.query({ name: "clipboard-write" }).then((result) => {
            if (result.state == "granted" || result.state == "prompt") {
                navigator.clipboard.writeText(location.href);
                e.target.innerText = "Copied!";
                setTimeout(() => (e.target.innerText = "Copy Room Url"), 5000);
            }
        });
    }
</script>

<button class="btn-gray" on:click={onClickCopy}>Copy Room Url</button>
