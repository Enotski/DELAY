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
        <div style="height: 60vh">
          <n-list bordered>
            <n-scrollbar style="max-height: 400px; min-width: 350px">
              <div v-for="(item, index) in messagesList" :key="index">
                <n-list-item>
                  <div>
                    <div
                      v-if="item.author != userStore.user.name"
                      :title="item.time"
                    >
                      <span>{{ item.author }}</span
                      >:&nbsp;<span style="font-weight: bold">{{
                        item.text
                      }}</span>
                    </div>
                    <div v-else :title="item.time">
                      <span style="font-weight: bold">{{ item.text }}</span>
                    </div>
                  </div>
                </n-list-item>
              </div>
            </n-scrollbar>
          </n-list>
        </div>
      </div>
      <div class="mb-4" style="display: contents">
        <div class="card-border" style="width: inherit; height: max-content">
          <n-input-group>
            <n-input v-model:value="message" />
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
import { useUserStore } from "@/stores/user-store";
import { ref, h, onMounted, computed, type CSSProperties } from "vue";
import { RequestUtils } from "@/utils";
import {
  NButton,
  NInput,
  NInputGroup,
  NIcon,
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
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalR";
import { Add as plusIco, ChatboxOutline as sendIco } from "@vicons/ionicons5";
import type {
  IBaseDto,
  IMessageDto,
  INameDto,
  IRoomDto,
  IRoomUserDto,
} from "@/interfaces";

const userStore = useUserStore();
const hubConnection = new HubConnectionBuilder()
  .withAutomaticReconnect()
  .configureLogging(LogLevel.Debug)
  .withUrl("https://localhost:7259/chat", {
    accessTokenFactory: () => RequestUtils.getAccessToken(),
  })
  .build();

hubConnection.on("NewMessage", (message: IMessageDto) => {
  messagesList.value.push(message);
  console.log(message);
});

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

onMounted(async () => {
  hubConnection
    .start()
    .then(function () {
      console.log("Connected!");
    })
    .catch(function (err) {
      return console.error(err.toString());
    });

  await updateRoomsList();
});

const message = ref<string>("");

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
const messagesList = ref<IMessageDto[]>([]);
const showRoomModal = ref(false);
const currentRoom = ref<INameDto>({
  id: "",
  name: "",
});

const roomFormValue = ref<IRoomDto>({
  id: "",
  name: "",
  chatType: "0",
  boards: [],
  users: [],
});

async function addRoom() {
  roomFormValue.value = {
    id: "",
    name: "",
    chatType: "0",
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
    .sendRequest("chats/messages", "GET", {
      chatId: currentRoom.value.id,
    })
    .then((res: IMessageDto[]) => {
      messagesList.value = res;
    })
    .finally(() => {
      console.log("get messages list by room");
    });
}

async function roomInfo(id: string) {
  showRoomModal.value = true;
  isEditRoom = true;

  await RequestUtils.default
    .sendRequest("chats", "GET", {
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

function clearChatWindow() {
  messagesList.value = [];
}
async function sendMesage() {
  let model: IMessageDto = {
    isCurrentUserMessage: true,
    text: message.value,
    author: "You",
    time: new Date().toLocaleString("ru-Ru").replace(",", ""),
    chatId: currentRoom.value.id,
  };
  message.value = "";

  hubConnection.invoke<IMessageDto>("PostMessage", model).catch(function (err) {
    return console.error(err.toString());
  });
  // await RequestUtils.default
  //   .sendRequest("chats/messages", "POST", messObj)
  //   .then((res: number) => {})
  //   .finally(() => {
  //     console.log("get tickets list by room");
  //   });
}

async function onPositiveClick() {
  if (confirmModalTitle.value == "Delete room") {
    await RequestUtils.default
      .sendRequest("chats", "DELETE", rowIdToDelete)
      .then(async (res: number) => {
        if (res != 0) {
          showConfirmModal.value = false;
          if (currentRoom.value.id == rowIdToDelete) {
            currentRoom.value = { id: "", name: "" };
            clearChatWindow();
            await updateRoomsList();
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
  await RequestUtils.default
    .sendRequest("chats/by-user", "GET")
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
      .sendRequest<IRoomDto>("chats", "POST", roomFormValue.value)
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
      .sendRequest<IRoomDto>("chats", "PUT", roomFormValue.value)
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
