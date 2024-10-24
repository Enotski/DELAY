<template>
  <n-modal
    v-model:show="showBoardModal"
    class="w-100"
    preset="dialog"
    title="Edit board"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onSaveBoardModal"
    @negative-click="onCloseBoardModal"
  >
    <n-form ref="boardFormRef" inline :model="boardFormValue" class="row">
      <n-form-item label="Name">
        <n-input v-model:value="boardFormValue.name" placeholder="Input Name" />
      </n-form-item>
      <n-form-item>
        <n-switch
          v-model:value="boardFormValue.isPublic"
          :rail-style="railStyle"
        >
          <template #checked> Public </template>
          <template #unchecked> Private </template>
        </n-switch>
      </n-form-item>
      <div>
        <n-form-item label="Description">
          <n-input
            class="w-100"
            v-model:value="boardFormValue.description"
            placeholder="Input description"
            type="textarea"
          />
        </n-form-item>
      </div>
      <div class="d-flex">
        <n-form-item label="Board users">
          <n-data-table
            :style="{
              height: '100%',
            }"
            :max-height="300"
            :bordered="true"
            :single-line="false"
            :columns="boardUsersColumns"
            :data="boardFormValue.users"
            :row-key="rowBoardUserKey"
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
    v-model:show="showTicketsListModal"
    class="w-100"
    preset="dialog"
    title="Edit tickets list"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onSaveTicketsListModal"
    @negative-click="onCloseTicketsListModal"
  >
    <n-form
      ref="ticketsListFormRef"
      inline
      :model="ticketsListFormValue"
      class="row"
    >
      <n-form-item label="Name">
        <n-input
          v-model:value="ticketsListFormValue.name"
          placeholder="Input Name"
        />
      </n-form-item>
    </n-form>
  </n-modal>
  <n-modal
    v-model:show="showTicketModal"
    class="w-100"
    preset="dialog"
    title="Edit ticket"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onSaveTicketModal"
    @negative-click="onCloseTicketModal"
  >
    <n-form
      ref="ticketsListFormRef"
      inline
      :model="ticketFormValue"
      class="row"
    >
      <n-form-item label="Name">
        <n-input
          v-model:value="ticketFormValue.name"
          placeholder="Input Name"
        />
      </n-form-item>
    </n-form>
  </n-modal>
  <div class="d-flex" size="large">
    <div
      class="card-border mb-3 me-5 flex-card-content"
      style="width: inherit; height: max-content"
    >
      <n-list bordered clickable hoverable>
        <template #header
          ><span class="fw-bold"> {{ currentBoard.name }}</span></template
        >
        <n-scrollbar style="max-height: 400px; min-width: 350px">
          <div v-for="(row, index) in boardsData" :key="index">
            <n-list-item @click="boardSelected(row)">
              <template #suffix>
                <n-space horizontal inline class="ticket-row-buttons-space">
                  <n-button
                    ghost
                    type="info"
                    strong
                    size="small"
                    @click="boardInfo(row.id)"
                    >Info</n-button
                  >
                  <n-button
                    ghost
                    type="error"
                    strong
                    size="small"
                    @click="deleteBoard(row.id)"
                    >Delete</n-button
                  >
                </n-space>
              </template>
              <n-thing :description="row.name"> </n-thing>
            </n-list-item>
          </div>
        </n-scrollbar>
      </n-list>
      <n-button type="success" class="mt-3" ghost @click="addBoard">
        <template #icon>
          <n-icon><plus-ico /></n-icon>
        </template>
      </n-button>
    </div>
    <div class="row flex-container" style="width: inherit; height: max-content">
      <div class="mb-4" style="display: contents">
        <div class="card-border mb-3" style="width: max-content">
          <n-button type="success" @click="addTicketsList">
            <template #icon>
              <n-icon><plus-ico /></n-icon> </template
          ></n-button>
        </div>
      </div>
      <div class="d-flex p-0 flex-stretch">
        <div v-for="(item, index) in ticketListsData" :key="index">
          <div
            class="card-border mb-3 me-4 flex-card-content"
            style="
              width: inherit;
              height: max-content;
              max-height: 550px;
              min-width: 400px;
            "
          >
            <n-list bordered clickable hoverable>
              <template #header
                ><div class="d-flex justify-content-between">
                  {{ item.name }}
                  <n-space horizontal inline class="ticket-row-buttons-space">
                    <n-button
                      ghost
                      type="info"
                      strong
                      size="small"
                      @click="ticketsListInfo(item.id)"
                      >Info</n-button
                    >
                    <n-button
                      ghost
                      type="error"
                      strong
                      size="small"
                      @click="deleteTicketsList(item.id)"
                      >Delete</n-button
                    >
                  </n-space>
                </div>
              </template>
              <n-scrollbar style="max-height: 400px">
                <div v-for="(ticket, index) in item.tickets" :key="index">
                  <n-list-item>
                    <template #suffix>
                      <n-space
                        horizontal
                        inline
                        class="ticket-row-buttons-space"
                      >
                        <n-button
                          ghost
                          type="info"
                          strong
                          size="small"
                          @click="ticketInfo(ticket.id)"
                          >Info</n-button
                        >
                        <n-button
                          ghost
                          type="error"
                          strong
                          size="small"
                          @click="deleteTicket(ticket.id)"
                          >Delete</n-button
                        >
                      </n-space>
                    </template>
                    <n-thing :description="ticket.name"> </n-thing>
                  </n-list-item>
                </div>
              </n-scrollbar>
            </n-list>
            <n-button type="success" class="mt-3" ghost @click="addTicket">
              <template #icon>
                <n-icon><plus-ico /></n-icon>
              </template>
            </n-button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted, type CSSProperties } from "vue";
import { RequestUtils } from "@/utils";
import {
  NSpace,
  NScrollbar,
  NThing,
  NList,
  NListItem,
  NDataTable,
  NButton,
  NIcon,
  NSwitch,
  NInput,
  NModal,
  NForm,
  NFormItem,
  NSelect,
} from "naive-ui";
import type { TableColumn } from "naive-ui/es/data-table/src/interface";
import { Add as plusIco } from "@vicons/ionicons5";
import {
  type ITicketDto,
  type IBoardDto,
  type IBaseDto,
  type IBoardUserDto,
  type INameDto,
} from "@/interfaces";
import type { ITicketsListDto } from "@/interfaces/api/contracts/board/tickets-list-dto";

const currentBoard = ref<INameDto>({
  id: "",
  name: "",
});

const confirmModalTitle = ref<string>("");
const boardsData = ref<INameDto[]>([]);
const ticketListsData = ref<ITicketsListDto[]>([]);
const boardFormValueDefault: IBoardDto = {
  id: "",
  name: "",
  description: "",
  isPublic: true,
  users: [],
};
const ticketsListFormValueDefault: ITicketsListDto = {
  id: "",
  name: "",
  boardId: "",
  tickets: [],
};
const ticketFormValueDefault: ITicketDto = {
  id: "",
  isDone: false,
  boardId: "",
  name: "",
  description: "",
  changedBy: "",
  createdBy: "",
  dateChange: "",
  createDate: "",
  deadLineDate: "",
  ticketListId: "",
  users: [],
};
const boardFormValue = ref<IBoardDto>(boardFormValueDefault);
const ticketsListFormValue = ref<ITicketsListDto>(ticketsListFormValueDefault);
const ticketFormValue = ref<ITicketDto>(ticketFormValueDefault);
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

const rowKey = (row: IBaseDto) => row.id;
const rowBoardUserKey = (row: IBoardUserDto) => row.user.id;

let rowIdToDelete: string = "";

const pagination = {
  pageSize: 10,
};

onMounted(async () => {
  await RequestUtils.default
    .sendRequest("boards/by-user", "GET")
    .then(async (response: IBoardDto[]) => {
      if (response != null) {
        boardsData.value = response;
      }
    })
    .finally(() => {
      console.log("get boards by user");
    });
});

const userBoardRoleOpetions = [
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

const boardUsersColumns: TableColumn<IBoardUserDto>[] = [
  {
    title: "Name",
    key: "user.name",
  },
  {
    title: "Role",
    key: "userRole",
    render(row, index) {
      return h(NSelect, {
        options: userBoardRoleOpetions,
        value: row.userRole,
        onUpdateValue(v) {
          boardFormValue.value.users[index].userRole = v;
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
          onClick: () => deleteUserFromBoard(row),
        },
        { default: () => "Delete" }
      );
    },
  },
];
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
          onClick: () => addUserToBoard(row),
        },
        { default: () => "Add" }
      );
    },
  },
];

let isEditBoard = false;
let isEditTicketList = false;
let isEditTicket = false;

const allUsersData = ref<INameDto[]>([]);
const showBoardModal = ref(false);
const showTicketsListModal = ref(false);
const showTicketModal = ref(false);
const showConfirmModal = ref(false);

async function addBoard() {
  boardFormValue.value = boardFormValueDefault;
  showBoardModal.value = true;
  isEditBoard = false;
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
function addUserToBoard(row: INameDto) {
  allUsersData.value = allUsersData.value.filter((x) => {
    return x.id != row.id;
  });

  boardFormValue.value.users.push({ user: row, userRole: "1" });

  console.log("addUserToBoard");
}
function deleteUserFromBoard(row: IBoardUserDto) {
  boardFormValue.value.users = boardFormValue.value.users.filter((x) => {
    return x.user.id != row.user.id;
  });

  allUsersData.value.push(row.user);

  console.log("deleteUserFromBoard");
}
async function onSaveBoardModal() {
  if (!isEditBoard) {
    await RequestUtils.default
      .sendRequest<IBoardDto>("boards", "POST", boardFormValue.value)
      .then(async (response: string) => {
        if (response != null && response != "") {
          showBoardModal.value = false;
        }
      })
      .finally(() => {
        console.log("save board");
      });
  } else {
    await RequestUtils.default
      .sendRequest<IBoardDto>("boards", "PUT", boardFormValue.value)
      .then(async (response: number) => {
        if (response > 0) {
          showBoardModal.value = false;
        }
      })
      .finally(() => {
        console.log("save board");
      });
  }
}
function onCloseBoardModal() {
  showBoardModal.value = false;
  console.log("addTicketsList");
}

async function addTicketsList() {
  showTicketsListModal.value = true;
  isEditTicketList = false;
  ticketsListFormValue.value = ticketsListFormValueDefault;
  ticketsListFormValue.value.boardId = currentBoard.value.id;
}
async function onSaveTicketsListModal() {
  if (!isEditTicketList) {
    await RequestUtils.default
      .sendRequest<ITicketsListDto>(
        "tickets-lists",
        "POST",
        ticketsListFormValue.value
      )
      .then(async (response: string) => {
        if (response != null && response != "") {
          showTicketsListModal.value = false;
        }
      })
      .finally(() => {
        console.log("edit TicketsList");
      });
  } else {
    await RequestUtils.default
      .sendRequest<ITicketsListDto>(
        "tickets-lists",
        "PUT",
        ticketsListFormValue.value
      )
      .then(async (response: number) => {
        if (response > 0) {
          showTicketsListModal.value = false;
        }
      })
      .finally(() => {
        console.log("add TicketsList");
      });
  }
}
function onCloseTicketsListModal() {
  showTicketsListModal.value = false;
  console.log("addTicketsList");
}

function addTicket() {
  showTicketModal.value = true;
  isEditTicket = false;
  ticketFormValue.value = ticketFormValueDefault;
  console.log("addTicket");
}
async function onSaveTicketModal() {
  if (!isEditTicketList) {
    await RequestUtils.default
      .sendRequest<ITicketDto>("tickets", "POST", ticketFormValue.value)
      .then(async (response: string) => {
        if (response != null && response != "") {
          showTicketModal.value = false;
        }
      })
      .finally(() => {
        console.log("edit Ticket");
      });
  } else {
    await RequestUtils.default
      .sendRequest<ITicketDto>("tickets", "PUT", ticketFormValue.value)
      .then(async (response: number) => {
        if (response > 0) {
          showTicketModal.value = false;
        }
      })
      .finally(() => {
        console.log("add Ticket");
      });
  }
}
function onCloseTicketModal() {
  showTicketModal.value = false;
  console.log("addTicketsList");
}

async function boardSelected(row: INameDto) {
  currentBoard.value = row;
  await RequestUtils.default
    .sendRequest("tickets-lists/by-board", "GET", {
      boardId: currentBoard.value.id,
    })
    .then((res: ITicketsListDto[]) => {
      if (res != null) {
        ticketListsData.value = res;
      }
    })
    .finally(() => {
      console.log("get tickets list by board");
    });
}

async function boardInfo(id: string) {
  showBoardModal.value = true;
  isEditBoard = true;

  await RequestUtils.default
    .sendRequest("boards", "GET", {
      id: id,
    })
    .then((res: IBoardDto) => {
      if (res != null) {
        boardFormValue.value = res;
      }
    })
    .finally(() => {
      console.log("get tickets list by board");
    });
  console.log("boardInfo");
}

async function ticketsListInfo(id: string) {
  showTicketsListModal.value = true;
  isEditTicketList = true;

  await RequestUtils.default
    .sendRequest("tickets-lists", "GET", {
      id: id,
      boardId: currentBoard.value.id,
    })
    .then((res: ITicketsListDto) => {
      if (res != null) {
        ticketsListFormValue.value = res;
      }
    })
    .finally(() => {
      console.log("ticketsListInfo");
    });
}

async function ticketInfo(id: string) {
  showTicketModal.value = true;
  isEditTicket = true;

  await RequestUtils.default
    .sendRequest("tickets", "GET", {
      id: id,
    })
    .then((res: ITicketDto) => {
      if (res != null) {
        ticketFormValue.value = res;
      }
    })
    .finally(() => {
      console.log("ticketInfo");
    });
}

async function onPositiveClick() {
  if (confirmModalTitle.value == "Delete board") {
    await RequestUtils.default
      .sendRequest("boards", "DELETE", rowIdToDelete)
      .then((res: number) => {
        if (res != 0) {
          showConfirmModal.value = false;
        }
      })
      .finally(() => {
        console.log("ticketInfo");
      });
  } else if (confirmModalTitle.value == "Delete tickets list") {
    await RequestUtils.default
      .sendRequest("tickets-lists", "DELETE", {
        id: rowIdToDelete,
        boardId: currentBoard.value.id,
      })
      .then((res: number) => {
        if (res != 0) {
          showConfirmModal.value = false;
        }
      })
      .finally(() => {
        console.log("ticketInfo");
      });
  } else {
    await RequestUtils.default
      .sendRequest("ticket", "DELETE", rowIdToDelete)
      .then((res: number) => {
        if (res != 0) {
          showConfirmModal.value = false;
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

function deleteBoard(id: string) {
  rowIdToDelete = id;
  confirmModalTitle.value = "Delete board";
  showConfirmModal.value = true;
}

function deleteTicketsList(id: string) {
  rowIdToDelete = id;
  confirmModalTitle.value = "Delete tickets list";
  showConfirmModal.value = true;
}

function deleteTicket(id: string) {
  rowIdToDelete = id;
  confirmModalTitle.value = "Delete ticket";
  showConfirmModal.value = true;
}
</script>

<style scoped>
.ticket-row-buttons-space {
  flex-flow: nowrap !important;
}
</style>
