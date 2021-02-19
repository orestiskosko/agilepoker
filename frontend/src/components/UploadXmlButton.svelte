<script>
    import * as roomStore from "../stores/roomStore";
    import * as wsconnection from "../services/wsconnection";
    import parser from "fast-xml-parser";

    async function onChange() {
        const reader = new FileReader();
        reader.addEventListener("load", async function (data) {
            const xmlData = parser.parse(reader.result);
            const items = xmlData.rss.channel.item;
            const mappedItems = items.map((i) => ({
                key: i.key,
                summary: i.summary,
                priority: i.priority,
                link: i.link,
            }));
            await wsconnection.invokeSetItems($roomStore.id, { issues: mappedItems });
        });
        reader.readAsText(this.files[0]);
    }
</script>

<div class="flex flex-col md:flex-row sm:space-x-0 sm:space-y-3 md:space-x-3 md:space-y-0">
    <label
        class="w-full flex flex-col items-center px-4 py-4 bg-white bg-opacity-60 hover:bg-opacity-100 rounded border cursor-pointer"
    >
        <svg
            class="w-8 h-8"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 20 20"
        >
            <path
                d="M16.88 9.1A4 4 0 0 1 16 17H5a5 5 0 0 1-1-9.9V7a3 3 0 0 1 4.52-2.59A4.98 4.98 0 0 1 17 8c0 .38-.04.74-.12 1.1zM11 11h3l-4-4-4 4h3v3h2v-3z"
            />
        </svg>
        <span class="mt-2">Import from JIRA XML</span>
        <input class="hidden" type="file" accept="text/xml" on:change={onChange} />
    </label>
</div>
