<template>
  <n-modal
    v-model:show="showConfirmModal"
    preset="dialog"
    :title="confirmModalTitle"
    content="Are you sure?"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onPositiveClick"
    @negative-click="onNegativeClick"
  />
  <n-modal
    v-model:show="showRoomModal"
    class="w-100"
    preset="dialog"
    title="Edit room"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onSaveRoomModal"
    @negative-click="onCloseRoomModal"
  >
    <n-form ref="roomFormRef" inline :model="roomFormValue" class="row">
      <n-form-item label="Name">
        <n-input v-model:value="roomFormValue.name" placeholder="Input Name" />
      </n-form-item>
      <n-form-item>
        <n-switch
          v-model:value="roomFormValue.isPublic"
          :rail-style="railStyle"
        >
          <template #checked> Public </template>
          <template #unchecked> Private </template>
        </n-switch>
      </n-form-item>
      <div class="d-flex">
        <n-form-item label="Room users">
          <n-data-table
            :style="{
              height: '100%',
            }"
            :max-height="300"
            :bordered="true"
            :single-line="false"
            :columns="roomUsersColumns"
            :data="roomFormValue.users"
            :row-key="rowRoomUserKey"
            :pagination="pagination"
            :remote="true"
          />
        </n-form-item>
        <n-form-item label="All users">
          <n-data-table
            :style="{
              height: '100%',
            }"
            :max-height="300"
            :bordered="true"
            :single-line="false"
            :columns="allUsersColumns"
            :data="allUsersData"
            :row-key="rowKey"
            :pagination="pagination"
            :remote="true"
          />
        </n-form-item>
      </div>
    </n-form>
  </n-modal>

  <div class="d-flex" size="large">
    <div
      class="card-border mb-3 me-5 flex-card-content"
      style="width: inherit; height: max-content"
    >
      <n-list bordered clickable hoverable>
        <n-scrollbar style="max-height: 400px; min-width: 350px">
          <div v-for="(row, index) in roomsList" :key="index">
            <n-list-item @click="roomSelected(row)">
              <template #suffix>
                <n-space horizontal inline class="row-buttons-space">
                  <n-button
                    ghost
                    type="info"
                    strong
                    size="small"
                    @click="roomInfo(row.id)"
                    >Info</n-button
                  >
                  <n-button
                    ghost
                    type="error"
                    strong
                    size="small"
                    @click="deleteRoom(row.id)"
                    >Delete</n-button
                  >
                </n-space>
              </template>
              <n-thing :description="row.name"> </n-thing>
            </n-list-item>
          </div>
        </n-scrollbar>
      </n-list>
      <n-button type="success" class="mt-3" ghost @click="addRoom">
        <template #icon>
          <n-icon><plus-ico /></n-icon>
        </template>
      </n-button>
    </div>
    <div class="col me-4 flex-container">
      <div class="d-flex flex-column card-border mb-3 flex-stretch">
        <div class="text-center">
          <h5>
            <span class="fw-bold"> {{ currentRoom.name }}</span>
          </h5>
        </div>
        <div style="height: 70vh">
          <n-infinite-scroll :distance="10" @load="handleLoad">
            <div
              v-for="(item, index) in items"
              :key="item.key"
              class="message"
              :class="{ reverse: index % 5 === 0 }"
            >
              <img class="avatar" :src="item.avatar" alt="" />
              <span> {{ item.message }} {{ index % 5 === 0 ? "?" : "" }}</span>
            </div>
            <div v-if="loading" class="text">Loading...</div>
            <div v-if="noMore" class="text">No More 🤪</div>
          </n-infinite-scroll>
        </div>
      </div>
      <div class="mb-4" style="display: contents">
        <div class="card-border" style="width: inherit; height: max-content">
          <n-input-group>
            <n-input />
            <n-button type="primary" @click="sendMesage">
              <template #icon>
                <n-icon><send-ico /></n-icon>
              </template>
            </n-button>
          </n-input-group>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted, computed, type CSSProperties } from "vue";
import { RequestUtils } from "@/utils";
import {
  NButton,
  NInput,
  NInputGroup,
  NIcon,
  NInfiniteScroll,
  NModal,
  NSwitch,
  NForm,
  NFormItem,
  NDataTable,
  NSelect,
  NList,
  NScrollbar,
  NListItem,
  NSpace,
  NThing,
} from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";

import { Add as plusIco, ChatboxOutline as sendIco } from "@vicons/ionicons5";
import type { IBaseDto, INameDto, IRoomDto, IRoomUserDto } from "@/interfaces";

const allUsersColumns: TableColumn<INameDto>[] = [
  {
    title: "Name",
    key: "name",
  },
  {
    title: "",
    key: "success",
    width: 68,
    render(row) {
      return h(
        NButton,
        {
          ghost: true,
          type: "success",
          strong: true,
          size: "small",
          onClick: () => addUserToRoom(row),
        },
        { default: () => "Add" }
      );
    },
  },
];

const userRoomRoleOpetions = [
  {
    label: "User",
    value: "1",
  },
  {
    label: "Moder",
    value: "2",
  },
  {
    label: "Admin",
    value: "3",
  },
];

const roomUsersColumns: TableColumn<IRoomUserDto>[] = [
  {
    title: "Name",
    key: "user.name",
  },
  {
    title: "Role",
    key: "userRole",
    render(row, index) {
      return h(NSelect, {
        options: userRoomRoleOpetions,
        value: row.role,
        onUpdateValue(v) {
          roomFormValue.value.users[index].role = v;
        },
      });
    },
  },
  {
    width: 80,
    key: "delete",
    render(row) {
      return h(
        NButton,
        {
          ghost: true,
          type: "error",
          strong: true,
          size: "small",
          onClick: () => deleteUserFromRoom(row),
        },
        { default: () => "Delete" }
      );
    },
  },
];
const railStyle = ({
  focused,
  checked,
}: {
  focused: boolean;
  checked: boolean;
}) => {
  const style: CSSProperties = {};
  if (checked) {
    style.background = "#f5770a";
    if (focused) {
      style.boxShadow = "0 0 0 2px #f5770a";
    }
  } else {
    style.background = "#2080f0";
    if (focused) {
      style.boxShadow = "0 0 0 2px #2080f040";
    }
  }
  return style;
};

onMounted(async () => {
  await updateRoomsList();
});

const loading = ref(false);

const avatars = [
  "https://07akioni.oss-cn-beijing.aliyuncs.com/07akioni.jpeg",
  "https://avatars.githubusercontent.com/u/20943608?s=60&v=4",
  "https://avatars.githubusercontent.com/u/46394163?s=60&v=4",
  "https://avatars.githubusercontent.com/u/39197136?s=60&v=4",
  "https://avatars.githubusercontent.com/u/19239641?s=60&v=4",
];

const messages = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];

const mock = (i: number) => ({
  key: `${i}`,
  value: i,
  avatar: avatars[i % avatars.length],
  message: messages[Math.floor(Math.random() * messages.length)],
});

const items = ref(Array.from({ length: 10 }, (_, i) => mock(i)));
const noMore = computed(() => items.value.length > 16);

const handleLoad = async () => {
  if (loading.value || noMore.value) return;
  loading.value = true;
  await new Promise((resolve) => setTimeout(resolve, 1000));
  items.value.push(...[mock(items.value.length), mock(items.value.length + 1)]);
  loading.value = false;
};

const pagination = {
  pageSize: 10,
};

const rowKey = (row: IBaseDto) => row.id;
const rowRoomUserKey = (row: IRoomUserDto) => row.user.id;
const allUsersData = ref<INameDto[]>([]);
const showConfirmModal = ref(false);
const confirmModalTitle = ref<string>("");
let rowIdToDelete: string = "";
let isEditRoom = false;
const roomsList = ref<INameDto[]>([]);
const showRoomModal = ref(false);
const currentRoom = ref<INameDto>({
  id: "",
  name: "",
});

const roomFormValue = ref<IRoomDto>({
  id: "",
  name: "",
  chatType: "0",
  isPublic: true,
  boards: [],
  users: [],
});

async function addRoom() {
  roomFormValue.value = {
    id: "",
    name: "",
    chatType: "0",
    isPublic: true,
    boards: [],
    users: [],
  };
  showRoomModal.value = true;
  isEditRoom = false;
  await RequestUtils.default
    .sendRequest("users/get-key-name-list", "GET")
    .then(async (response: INameDto[]) => {
      if (response != null) {
        allUsersData.value = response;
      }
    })
    .finally(() => {
      console.log("get all user");
    });
}

function deleteUserFromRoom(row: IRoomUserDto) {
  roomFormValue.value.users = roomFormValue.value.users.filter((x) => {
    return x.user.id != row.user.id;
  });

  allUsersData.value.push(row.user);

  console.log("deleteUserFromRoom");
}
function addUserToRoom(row: INameDto) {
  allUsersData.value = allUsersData.value.filter((x) => {
    return x.id != row.id;
  });

  roomFormValue.value.users.push({ user: row, role: "1" });

  console.log("addUserToRoom");
}

function deleteRoom(id: string) {
  rowIdToDelete = id;
  confirmModalTitle.value = "Delete room";
  showConfirmModal.value = true;
}

async function roomSelected(row: INameDto) {
  currentRoom.value = row;
  await RequestUtils.default
    .sendRequest("rooms/chat", "GET", {
      boardId: currentRoom.value.id,
    })
    .then((res: any) => {
      console.log(res);
    })
    .finally(() => {
      console.log("get tickets list by room");
    });
}

async function roomInfo(id: string) {
  showRoomModal.value = true;
  isEditRoom = true;

  await RequestUtils.default
    .sendRequest("rooms", "GET", {
      id: id,
    })
    .then((res: IRoomDto) => {
      if (res != null) {
        roomFormValue.value = res;
      }
    })
    .finally(() => {
      console.log("get tickets list by room");
    });
  console.log("roomInfo");
}

function cleatChatWindow() {}
function sendMesage() {}

async function onPositiveClick() {
  if (confirmModalTitle.value == "Delete room") {
    await RequestUtils.default
      .sendRequest("rooms", "DELETE", rowIdToDelete)
      .then(async (res: number) => {
        if (res != 0) {
          showConfirmModal.value = false;
          if (currentRoom.value.id == rowIdToDelete) {
            currentRoom.value = { id: "", name: "" };
            cleatChatWindow();
          }
        }
      })
      .finally(() => {
        console.log("ticketInfo");
      });
  }
  console.log("onPositiveClick");
}

function onNegativeClick() {
  showConfirmModal.value = false;
  rowIdToDelete = "";
  console.log("onNegativeClick");
}

async function updateRoomsList() {
  roomsList.value = [
    { id: "1", name: "Chat 1" },
    { id: "2", name: "Chat 2" },
    { id: "3", name: "Chat 3" },
  ];
  await RequestUtils.default
    .sendRequest("rooms/by-user", "GET")
    .then((response: IRoomDto[]) => {
      if (response != null) {
        roomsList.value = response;
      }
    })
    .finally(() => {
      console.log("get rooms by user");
    });
}

async function onSaveRoomModal() {
  if (!isEditRoom) {
    await RequestUtils.default
      .sendRequest<IRoomDto>("rooms", "POST", roomFormValue.value)
      .then(async (response: string) => {
        if (response != null && response != "") {
          showRoomModal.value = false;
          await updateRoomsList();
        }
      })
      .finally(() => {
        console.log("save room");
      });
  } else {
    await RequestUtils.default
      .sendRequest<IRoomDto>("rooms", "PUT", roomFormValue.value)
      .then(async (response: number) => {
        if (response > 0) {
          showRoomModal.value = false;
          await updateRoomsList();
        }
      })
      .finally(() => {
        console.log("save room");
      });
  }
}
function onCloseRoomModal() {
  showRoomModal.value = false;
  console.log("addTicketsList");
}
</script>

<style scoped>
.message {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
  padding: 10px;
}

.message:last-child {
  margin-bottom: 0;
}

.reverse {
  flex-direction: row-reverse;
}

.text {
  text-align: center;
}

.reverse .avatar {
  margin-left: 10px;
}

.avatar {
  width: 28px;
  height: 28px;
  border-radius: 50%;
  margin-right: 10px;
}
</style>
