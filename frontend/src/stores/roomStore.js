import { writable } from "svelte/store";

const RoomStatus = {
    IDLE: 0,
    VOTING: 1,
};
Object.freeze(RoomStatus);

const ItemStatus = {
    IDLE: 0,
    VOTING: 1,
    VOTED: 2,
};
Object.freeze(ItemStatus);

const defaultRoom = {
    id: null,
    name: null,
    leaderId: null,
    status: RoomStatus.IDLE,
    participants: [],
    items: [],
    votes: [],
    selectedItemId: null,
};

const { subscribe, set, update } = writable(defaultRoom);

const reset = () => set(defaultRoom);

const setStatus = (status) =>
    update((r) => {
        r.status = status;
        return r;
    });

const setItems = (items) => {
    update((r) => {
        r.items = items;
        return r;
    });
};

const setSelectedItem = (itemId) => {
    update((r) => {
        r.selectedItemId = itemId;
        return r;
    });
};

const addParticipant = (participant) => {
    update((r) => {
        if (r.participants.map((p) => p.id).includes(participant.id)) {
            return r;
        }

        r.participants = [...r.participants, participant];
        return r;
    });
};

const removeParticipant = (participantId) => {
    update((r) => {
        const participantIndex = r.participants.findIndex((p) => p.id === participantId);

        if (participantIndex !== -1) {
            r.participants.splice(participantIndex, 1);
        }

        return r;
    });
};

const setParticipantVote = (voteDto) => {
    update((r) => {
        const voteExistsIndex = r.votes.findIndex(
            (v) =>
                v.roomId === voteDto.roomId &&
                v.userId === voteDto.userId &&
                v.itemId === voteDto.itemId
        );

        if (voteExistsIndex !== -1) {
            r.votes.splice(voteExistsIndex, 1);
        }

        r.votes.push(voteDto);
        return r;
    });
};

const setSelectedItemStatus = (status) => {
    update((r) => {
        const itemIndex = r.items.findIndex((i) => i.id === r.selectedItemId);
        r.items[itemIndex] = { ...r.items[itemIndex], status };
        return r;
    });
};

const resetUserItemVotes = () => {
    update((r) => {
        r.votes = r.votes.filter((v) => v.itemId !== r.selectedItemId);
        return r;
    });
};

export {
    RoomStatus,
    ItemStatus,
    subscribe,
    set,
    update,
    reset,
    setStatus,
    setItems,
    setSelectedItem,
    addParticipant,
    removeParticipant,
    setParticipantVote,
    setSelectedItemStatus,
    resetUserItemVotes,
};
