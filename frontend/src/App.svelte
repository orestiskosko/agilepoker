<script>
    import Router from "svelte-spa-router";
    import Login from "./components/Login.svelte";
    import NotFound from "./components/NotFound.svelte";
    import Room from "./components/Room.svelte";
    import * as wsconnection from "./services/wsconnection";
    import { onMount } from "svelte";
    import Snackbar from "./components/Snackbar.svelte";
    import * as roomStore from "./stores/roomStore";
    import Home from "./components/Home.svelte";

    onMount(async () => await wsconnection.startSignalRConnection());
</script>

<Snackbar />
<main
    class="min-h-screen bg-gradient-to-tr from-green-400 to-blue-500 text-gray-700"
    class:bg-light-blue-50={$roomStore.status === roomStore.RoomStatus.VOTING}
>
    <Router
        routes={{
            "/": Home,
            "/login": Login,
            "/room/:id": Room,
            "*": NotFound,
        }}
    />
</main>
